using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Support.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Model.Iata.HouseManifest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using HouseMasterManifest = CtaCargo.CctImportacao.Domain.Model.Iata.HouseMasterManifest;

namespace CtaCargo.CctImportacao.Application.Support;

public class MotorIataHouse : IMotorIataHouse
{
    public string GenHouseManifest(House house, List<NaturezaCarga> naturezaCargaList, IataXmlPurposeCode purposeCode)
    {
        HouseWaybillType manhouse = new HouseWaybillType();

        Enum.TryParse(house.PesoTotalBrutoUN, out MeasurementUnitCommonCodeContentType pesoTotalUN);
        Enum.TryParse(house.ValorFretePPUN, out ISO3AlphaCurrencyCodeContentType valorPPUN);
        Enum.TryParse(house.ValorFreteFCUN, out ISO3AlphaCurrencyCodeContentType valorFCUN);
        Enum.TryParse(house.VolumeUN, out MeasurementUnitCommonCodeContentType volumeUN);

        #region MessageHeaderDocument
        if (house.XmlIssueDate == null)
            house.XmlIssueDate = DateTime.UtcNow;

        manhouse.MessageHeaderDocument = new MessageHeaderDocumentType
        {
            ID = new IDType { Value = $"{house.Numero}_{house.DataEmissaoXML.Value:ddMMyyyhhmmss}" },
            Name = new TextType { Value = "House Waybill" },
            TypeCode = new DocumentCodeType { Value = DocumentNameCodeContentType.Item703 },
            IssueDateTime = house.XmlIssueDate.Value.AddHours(-3),
            PurposeCode = new CodeType { Value = purposeCode.ToString() },
            VersionID = new IDType { Value = "3.00" },
            SenderParty = new SenderPartyType[2],
            RecipientParty = new RecipientPartyType[1],
        };
        manhouse.MessageHeaderDocument.SenderParty[0] = new SenderPartyType
        {
            PrimaryID = new IDType { schemeID = "C", Value = "HDQTTKE" }
        };
        manhouse.MessageHeaderDocument.SenderParty[1] = new SenderPartyType
        {
            PrimaryID = new IDType { schemeID = "P", Value = "HDQTTKE" }
        };
        manhouse.MessageHeaderDocument.RecipientParty[0] = new RecipientPartyType
        {
            PrimaryID = new IDType { schemeID = "C", Value = "BRCUSTOMS" }
        };
        #endregion

        #region BusinessHeaderDocument
        manhouse.BusinessHeaderDocument = new BusinessHeaderDocumentType
        {
            ID = new IDType { Value = house.Numero },
            IncludedHeaderNote = new HeaderNoteType[1],
            SignatoryConsignorAuthentication = new ConsignorAuthenticationType
            {
                Signatory = new TextType { Value = house.SignatarioNome }
            },
            SignatoryCarrierAuthentication = new CarrierAuthenticationType
            {
                ActualDateTime = house.DataEmissaoXML.Value,
                Signatory = new TextType { Value = "180020" },
                IssueAuthenticationLocation = new AuthenticationLocationType
                {
                    Name = new TextType { Value = house.AeroportoOrigemCodigo }
                }
            }
        };
        manhouse.BusinessHeaderDocument.IncludedHeaderNote[0] = new HeaderNoteType
        {
            ContentCode = new CodeType { Value = "A" },
            Content = new TextType { Value = "As Agreed" }
        };
        manhouse.BusinessHeaderDocument.SignatoryConsignorAuthentication = new ConsignorAuthenticationType
        {
            Signatory = new TextType { Value = "SIGNATURE" }
        };
        #endregion

        #region MasterConsigment
        manhouse.MasterConsignment = new MasterConsignmentType
        {
            TotalPieceQuantity = new QuantityType { Value = house.TotalVolumes },
            TransportContractDocument = new TransportContractDocumentType
            {
                ID = new IDType { Value = house.MasterNumeroXML }
            },
            OriginLocation = new OriginLocationType
            {
                ID = new IDType { Value = house.AeroportoOrigemCodigo },
                Name = new TextType { Value = house.AeroportoOrigemInfo?.Nome }
            },
            FinalDestinationLocation = new FinalDestinationLocationType
            {
                ID = new IDType { Value = house.AeroportoDestinoCodigo },
                Name = new TextType { Value = house.AeroportoDestinoInfo?.Nome }
            }
        };

        #region MasterConsigment -> IncludedHouseConsigmnet
        manhouse.MasterConsignment.IncludedHouseConsignment = new HouseConsignmentType
        {
            TotalChargePrepaidIndicatorFlag = (house.ValorFreteFC == 0),
            ValuationTotalChargeAmount = new AmountType { currencyID = valorPPUN, currencyIDSpecified = true, Value = 0 },
            TaxTotalChargeAmount = new AmountType { currencyID = valorPPUN, currencyIDSpecified = true, Value = 0 },
            WeightTotalChargeAmount = new AmountType { currencyID = valorPPUN, currencyIDSpecified = true, Value = 0 },
            TotalDisbursementPrepaidIndicator = false,
            AgentTotalDisbursementAmount = new AmountType { currencyID = valorPPUN, currencyIDSpecified = true, Value = 0 },
            CarrierTotalDisbursementAmount = new AmountType { currencyID = valorPPUN, currencyIDSpecified = true, Value = 0 },
            TotalPrepaidChargeAmount = new AmountType { currencyID = valorPPUN, currencyIDSpecified = true, Value = house.ValorFretePP },
            TotalCollectChargeAmount = new AmountType { currencyID = valorFCUN, currencyIDSpecified = true, Value = house.ValorFreteFC },
            IncludedTareGrossWeightMeasure = new MeasureType { unitCode = pesoTotalUN, unitCodeSpecified = true, Value = Convert.ToDecimal(house.PesoTotalBruto) },
            NilCarriageValueIndicator = false,
            NilCustomsValueIndicator = false,
            NilInsuranceValueIndicator = false,
            NilCarriageValueIndicatorSpecified = true,
            NilCustomsValueIndicatorSpecified = true,
            NilInsuranceValueIndicatorSpecified = true,
            ConsignmentItemQuantity = new QuantityType { Value = house.TotalVolumes },
            PackageQuantity = new QuantityType { Value = house.TotalVolumes },
            TotalPieceQuantity = new QuantityType { Value = house.TotalVolumes },
            SummaryDescription = new TextType { Value = house.DescricaoMercadoria },
            FreightRateTypeCode = new CodeType { Value = "RATE" },
            FreightForwarderParty = house.AgenteDeCargaNome != null ? new FreightForwarderPartyType
            {
                Name = new TextType { Value = house.AgenteDeCargaInfo.Nome },
                PostalStructuredAddress = new StructuredAddressType
                {
                    PostcodeCode = new CodeType { Value = house.AgenteDeCargaInfo.Complemento },
                    StreetName = new TextType { Value = house.AgenteDeCargaInfo.Endereco },
                    CityName = new TextType { Value = house.AgenteDeCargaInfo.Cidade },
                    CountryID = new CountryIDType
                    {
                        Value = (ISOTwoletterCountryCodeIdentifierContentType)
                        Enum.Parse(typeof(ISOTwoletterCountryCodeIdentifierContentType), house.AgenteDeCargaInfo.Pais)
                    }
                }
            } : null,
            ConsignorParty = new ConsignorPartyType
            {
                Name = new TextType { Value = house.ExpedidorNome },
                PostalStructuredAddress = new StructuredAddressType
                {
                    PostcodeCode = new CodeType { Value = house.ExpedidorPostal },
                    StreetName = new TextType { Value = house.ExpedidorEndereco },
                    CityName = new TextType { Value = house.ExpedidorCidade },
                    PostOfficeBox = new TextType { Value = house.ExpedidorPostal },
                    CountryID = new CountryIDType
                    {
                        Value = (ISOTwoletterCountryCodeIdentifierContentType)
                        Enum.Parse(typeof(ISOTwoletterCountryCodeIdentifierContentType), house.ExpedidorPaisCodigo)
                    }
                }
            },
            ConsigneeParty = new ConsigneePartyType
            {
                Name = new TextType { Value = house.ConsignatarioNome },
                PostalStructuredAddress = new StructuredAddressType
                {
                    PostcodeCode = new CodeType { Value = house.ConsignatarioPostal },
                    StreetName = new TextType { Value = house.ConsignatarioEndereco },
                    CityName = new TextType { Value = house.ConsignatarioCidade },
                    PostOfficeBox = new TextType { Value = house.ConsignatarioPostal },
                    CountryID = new CountryIDType
                    {
                        Value = (ISOTwoletterCountryCodeIdentifierContentType)
                        Enum.Parse(typeof(ISOTwoletterCountryCodeIdentifierContentType), house.ConsignatarioPaisCodigo)
                    }
                }
            },
            OriginLocation = new OriginLocationType
            {
                ID = new IDType { Value = house.AeroportoOrigemCodigo },
                Name = new TextType { Value = house.AeroportoOrigemInfo?.Nome }
            },
            FinalDestinationLocation = new FinalDestinationLocationType
            {
                ID = new IDType { Value = house.AeroportoDestinoCodigo },
                Name = new TextType { Value = house.AeroportoDestinoInfo?.Nome }
            },
            HandlingOSIInstructions = new OSIInstructionsType[1],
            IncludedHouseConsignmentItem = new HouseConsignmentItemType[1],
            TotalDisbursementPrepaidIndicatorSpecified = true
        };

        if (house.Volume != null)
            manhouse.MasterConsignment.IncludedHouseConsignment.GrossVolumeMeasure = new MeasureType
            {
                unitCode = volumeUN,
                unitCodeSpecified = house.VolumeUN != null,
                Value = Convert.ToDecimal(house.Volume)
            };

        manhouse.MasterConsignment.IncludedHouseConsignment.HandlingOSIInstructions[0] = new OSIInstructionsType
        {
            Description = new TextType { Value = house.DescricaoMercadoria }
        };
        manhouse.MasterConsignment.IncludedHouseConsignment.IncludedHouseConsignmentItem[0] = new HouseConsignmentItemType
        {
            SequenceNumeric = 1,
            GrossWeightMeasure = new MeasureType
            {
                Value = Convert.ToDecimal(house.PesoTotalBruto),
                unitCode = (MeasurementUnitCommonCodeContentType)
                Enum.Parse(typeof(MeasurementUnitCommonCodeContentType), house.PesoTotalBrutoUN),
                unitCodeSpecified = true
            },
            TotalChargeAmount = new AmountType
            {
                Value = house.ValorFretePP + house.ValorFreteFC,
                currencyIDSpecified = true,
                currencyID = (ISO3AlphaCurrencyCodeContentType)
                    Enum.Parse(typeof(ISO3AlphaCurrencyCodeContentType), house.ValorFretePPUN)
            },
            PieceQuantity = new QuantityType { Value = house.TotalVolumes },
            Information = new TextType { Value = "NDA" },
            NatureIdentificationTransportCargo = new TransportCargoType
            {
                Identification = new TextType { Value = house.DescricaoMercadoria }
            },
            ApplicableFreightRateServiceCharge = new FreightRateServiceChargeType[1]
        };
        if (house.NCMLista != null)
        {
            var ncmArray = house.NCMLista.Split(",");
            var ncmArrayObject = from c in ncmArray
                                 select new CodeType
                                 {
                                     Value = c.Replace(".", "")
                                 };

            manhouse.MasterConsignment.IncludedHouseConsignment.IncludedHouseConsignmentItem[0].TypeCode =
                ncmArrayObject.ToArray();
        }

        #region MasterConsigment > HandlingSPHInstructions
        if (!string.IsNullOrEmpty(house.NaturezaCargaLista))
        {
            var naturezaCargas = house.NaturezaCargaLista.Split(",");
            List<SPHInstructionsType> instructions = new List<SPHInstructionsType>();

            foreach (var nat in naturezaCargas)
            {
                string SPHIDescription = naturezaCargaList.FirstOrDefault(x => x.Codigo == nat)?.Descricao;

                instructions.Add(new SPHInstructionsType
                {
                    DescriptionCode = new CodeType { Value = nat },
                    Description = new TextType { Value = SPHIDescription }
                });
            }
            if (instructions.Count > 0)
                manhouse.MasterConsignment.IncludedHouseConsignment.HandlingSPHInstructions = instructions.ToArray();
        }
        #endregion

        manhouse.MasterConsignment.IncludedHouseConsignment.IncludedHouseConsignmentItem[0].ApplicableFreightRateServiceCharge[0]
            = new FreightRateServiceChargeType
            {
                ChargeableWeightMeasure = new MeasureType
                {
                    Value = Convert.ToDecimal(house.PesoTotalBruto),
                    unitCode = (MeasurementUnitCommonCodeContentType)
                        Enum.Parse(typeof(MeasurementUnitCommonCodeContentType), house.PesoTotalBrutoUN),
                    unitCodeSpecified = true
                },
                AppliedRate = (house.ValorFretePP + house.ValorFreteFC),
                AppliedAmount = new AmountType { currencyID = valorPPUN, Value = (house.ValorFretePP + house.ValorFreteFC) }
            };

        var customsNoteType = new List<CustomsNoteType>();

        if (house.ConsignatarioCNPJ != null && house.ConsignatarioCNPJ.Length > 0)
        {
            string tipoDoc = "";

            if (house.ConsignatarioCNPJ.StartsWith("PP"))
                tipoDoc = $"PASSPORT{house.ConsignatarioCNPJ.Substring(2)}";
            else if (house.ConsignatarioCNPJ.Length == 11)
                tipoDoc = $"CPF{house.ConsignatarioCNPJ}";
            else
                tipoDoc = $"CNPJ{house.ConsignatarioCNPJ}";

            if (tipoDoc.Length > 0)
            {
                customsNoteType.Add(new CustomsNoteType
                {
                    CountryID = new CountryIDType() { Value = ISOTwoletterCountryCodeIdentifierContentType.BR },
                    SubjectCode = new CodeType() { Value = "CNE" },
                    ContentCode = new CodeType() { Value = "T" },
                    Content = new TextType() { Value = tipoDoc }
                });
            };
        };

        if (house.CodigoRecintoAduaneiro != null && house.CodigoRecintoAduaneiro.Length == 7)
        {
            customsNoteType.Add(new CustomsNoteType
            {
                CountryID = new CountryIDType { Value = ISOTwoletterCountryCodeIdentifierContentType.BR },
                SubjectCode = new CodeType { Value = "CCL" },
                ContentCode = new CodeType { Value = "M" },
                Content = new TextType { Value = $"CUSTOMSWAREHOUSE{house.CodigoRecintoAduaneiro.ToString()}" }
            });
        };

        if (customsNoteType.Count > 0)
            manhouse.MasterConsignment.IncludedHouseConsignment.IncludedCustomsNote = customsNoteType.ToArray();
        #endregion

        #endregion

        XmlSerializerNamespaces ns = new();
        ns.Add("", "iata:datamodel:3");
        ns.Add("ns2", "iata:waybill:1");
        ns.Add("q1", "iata:housewaybill:1");
        return SerializeFromStream(manhouse, ns);
    }

    public string GenMasterHouseManifest(SubmeterRFBMasterHouseItemRequest masterInfo, List<House> houses, IataXmlPurposeCode purposeCode, DateTime issueDate)
    {
        var portOrigin = new HouseMasterManifest.OriginLocationType { ID = new HouseMasterManifest.IDType { Value = masterInfo.OriginLocation } };
        var portDestiny = new HouseMasterManifest.FinalDestinationLocationType { ID = new HouseMasterManifest.IDType { Value = masterInfo.DestinationLocation } };
        Enum.TryParse(masterInfo.TotalWeightUnit, out HouseMasterManifest.MeasurementUnitCommonCodeContentType totalWeightUN);
        HouseMasterManifest.HouseManifestType manhouse = new HouseMasterManifest.HouseManifestType();

        #region MessageHeaderDocument
        manhouse.MessageHeaderDocument = new HouseMasterManifest.MessageHeaderDocumentType();
        manhouse.MessageHeaderDocument.ID = new HouseMasterManifest.IDType { Value = $"{masterInfo.MasterNumber}" };
        manhouse.MessageHeaderDocument.Name = new HouseMasterManifest.TextType { Value = "Cargo Manifest" };
        manhouse.MessageHeaderDocument.IssueDateTime = issueDate;
        manhouse.MessageHeaderDocument.TypeCode = new HouseMasterManifest.DocumentCodeType { Value = HouseMasterManifest.DocumentNameCodeContentType.Item785 };
        manhouse.MessageHeaderDocument.PurposeCode = new HouseMasterManifest.CodeType { Value = purposeCode.ToString() };
        manhouse.MessageHeaderDocument.VersionID = new HouseMasterManifest.IDType { Value = "2.00" };
        manhouse.MessageHeaderDocument.SenderParty = new HouseMasterManifest.SenderPartyType[2];
        manhouse.MessageHeaderDocument.RecipientParty = new HouseMasterManifest.RecipientPartyType[1];
        manhouse.MessageHeaderDocument.SenderParty[0] = new HouseMasterManifest.SenderPartyType
        {
            PrimaryID = new HouseMasterManifest.IDType { schemeID = "C", Value = "HDQTTKE" }
        };
        manhouse.MessageHeaderDocument.SenderParty[1] = new HouseMasterManifest.SenderPartyType
        {
            PrimaryID = new HouseMasterManifest.IDType { schemeID = "P", Value = "HDQTTKE" }
        };
        manhouse.MessageHeaderDocument.RecipientParty[0] = new HouseMasterManifest.RecipientPartyType
        {
            PrimaryID = new HouseMasterManifest.IDType { schemeID = "C", Value = "BRCUSTOMS" }
        };
        #endregion

        #region BusinessHeaderDocument
        manhouse.BusinessHeaderDocument = new HouseMasterManifest.BusinessHeaderDocumentType { ID = new HouseMasterManifest.IDType { Value = masterInfo.MasterNumber } };
        #endregion

        #region MasterHeaderDocument
        manhouse.MasterConsignment = new HouseMasterManifest.MasterConsignmentType
        {
            IncludedTareGrossWeightMeasure = new HouseMasterManifest.MeasureType
            {
                unitCode = totalWeightUN,
                Value = Convert.ToDecimal(masterInfo.TotalWeight)
            },
            //ConsignmentItemQuantity = new HouseMasterManifest.QuantityType { Value = masterInfo.TotalPiece },
            //PackageQuantity = new HouseMasterManifest.QuantityType { Value = masterInfo.TotalPiece },
            TotalPieceQuantity = new HouseMasterManifest.QuantityType { Value = masterInfo.TotalPiece },
            TransportContractDocument = new HouseMasterManifest.TransportContractDocumentType { ID = new HouseMasterManifest.IDType { Value = masterInfo.MasterNumber.Insert(3, "-") } },
            OriginLocation = portOrigin,
            FinalDestinationLocation = portDestiny,
        };

        List<HouseMasterManifest.HouseConsignmentType> includedHouseCOnsigmentType = new List<HouseMasterManifest.HouseConsignmentType>();

        houses.ForEach(house =>
        {
            var portOrigin = new HouseMasterManifest.OriginLocationType { ID = new HouseMasterManifest.IDType { Value = house.AeroportoOrigemCodigo } };
            var portDestiny = new HouseMasterManifest.FinalDestinationLocationType { ID = new HouseMasterManifest.IDType { Value = house.AeroportoDestinoCodigo } };
            Enum.TryParse(house.PesoTotalBrutoUN, out HouseMasterManifest.MeasurementUnitCommonCodeContentType pesoTotalUN);
            includedHouseCOnsigmentType.Add(new HouseMasterManifest.HouseConsignmentType
            {
                SequenceNumeric = 1,
                GrossWeightMeasure = new HouseMasterManifest.MeasureType
                {
                    unitCode = pesoTotalUN,
                    Value = Convert.ToDecimal(house.PesoTotalBruto)
                },
                PackageQuantity = new HouseMasterManifest.QuantityType { Value = house.TotalVolumes },
                TotalPieceQuantity = new HouseMasterManifest.QuantityType { Value = house.TotalVolumes },
                SummaryDescription = new HouseMasterManifest.TextType { Value = house.DescricaoMercadoria },
                TransportContractDocument = new HouseMasterManifest.TransportContractDocumentType { ID = new HouseMasterManifest.IDType { Value = house.Numero } },
                OriginLocation = portOrigin,
                FinalDestinationLocation = portDestiny
            });
        });

        manhouse.MasterConsignment.IncludedHouseConsignment = includedHouseCOnsigmentType.ToArray();
        #endregion


        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        ns.Add("", "iata:datamodel:3");
        ns.Add("ns2", "iata:waybill:1");
        ns.Add("q1", "iata:housewaybill:1");
        return SerializeFromStream<HouseMasterManifest.HouseManifestType>(manhouse, ns);
    }

    private string SerializeFromStream<T>(T tr, XmlSerializerNamespaces ns)
    {
        XmlWriterSettings ws = new XmlWriterSettings();
        ws.Indent = false;
        ws.NewLineHandling = NewLineHandling.None;
        StringWriter w = new Utf8StringWriter();

        XmlSerializer ser = new XmlSerializer(typeof(T));
        using (XmlWriter wr = XmlWriter.Create(w, ws))
        {
            ser.Serialize(wr, tr, ns);
            return w.GetStringBuilder().ToString();
        }
    }
}