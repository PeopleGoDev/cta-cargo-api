using CtaCargo.CctImportacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Waybill = CtaCargo.CctImportacao.Domain.Model.Iata.WaybillManifest;
using Flight = CtaCargo.CctImportacao.Domain.Model.Iata.FlightManifest;
using HouseWaybill = CtaCargo.CctImportacao.Domain.Model.Iata.HouseManifest;
using HouseManifest = CtaCargo.CctImportacao.Domain.Model.Iata.HouseMasterManifest;
using System.Linq;
using System.Xml;
using CtaCargo.CctImportacao.Application.Support.Contracts;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Domain.Model.Iata.HouseManifest;

namespace CtaCargo.CctImportacao.Application.Support;

public enum IataXmlPurposeCode
{
    Creation,
    Update,
    Deletion
}
public class MotorIata : IMotorIata
{
    public List<ArquivoXML> VoosXML { get; set; }
    public List<ArquivoXML> MastersXML { get; set; }
    public List<ArquivoXML> HousesXML { get; set; }
    public List<ArquivoXML> MasterHouseXML { get; set; }

    public string GenFullManifest(Voo vooCompleto)
    {
        return "";
    }

    public string GenFlightManifest(Voo voo)
    {
        try
        {
            Flight.FlightManifestType manvoo = new Flight.FlightManifestType();

            // MessageHeaderDocument
            Flight.MessageHeaderDocumentType headerdoc = new Flight.MessageHeaderDocumentType
            {
                ID = new Flight.IDType() { Value = $"{voo.Numero}_{((DateTime)voo.DataHoraSaidaEstimada).ToString("ddMMMyyy_ddMMyyyyhhmmss")}" },
                Name = new Flight.TextType() { Value = "Transport Loading Report" },
                TypeCode = new Flight.DocumentCodeType() { Value = Flight.DocumentNameCodeContentType.Item122 },
                IssueDateTime = voo.DataEmissaoXML.Value.AddHours(-3),
                PurposeCode = new Flight.CodeType() { Value = "Creation" },
                VersionID = new Flight.IDType() { Value = "2.00" },
                SenderParty = new Flight.SenderPartyType[2],
                RecipientParty = new Flight.RecipientPartyType[1]
            };
            headerdoc.SenderParty[0] = new Flight.SenderPartyType
            {
                PrimaryID = new Flight.IDType { Value = "HDQTTKE", schemeID = "C" }
            };
            headerdoc.SenderParty[1] = new Flight.SenderPartyType
            {
                PrimaryID = new Flight.IDType { Value = "HDQTTKE", schemeID = "P" }
            };

            headerdoc.RecipientParty[0] = new Flight.RecipientPartyType
            {
                PrimaryID = new Flight.IDType { Value = "BRCUSTOMS", schemeID = "C" }
            };
        
            manvoo.MessageHeaderDocument = headerdoc;

            // BusinessHeaderDocument
            Flight.BusinessHeaderDocumentType busdoc = new Flight.BusinessHeaderDocumentType();
            busdoc.ID = new Flight.IDType { Value = $"{voo.Numero}{ ((DateTime)voo.DataHoraSaidaEstimada).ToString("yyyyMMdd") }{voo.PortoIataOrigemInfo.Codigo}" };
            busdoc.IncludedHeaderNote = new Flight.HeaderNoteType[1];
            busdoc.IncludedHeaderNote[0] = new Flight.HeaderNoteType();
            busdoc.IncludedHeaderNote[0].Content = new Flight.TextType { Value = "FlightManifest" };
            manvoo.BusinessHeaderDocument = busdoc;

            // LogisticsTransportMovement
            Flight.LogisticsTransportMovementType logmov = new Flight.LogisticsTransportMovementType();
            logmov.StageCode = new Flight.CodeType { Value = "Main-Carriage" };
            logmov.ModeCode = new Flight.TransportModeCodeType { Value = Flight.TransportModeCodeContentType.Item4 };
            logmov.Mode = new Flight.TextType { Value = "AIR TRANSPORT" };
            logmov.ID = new Flight.IDType { Value = voo.Numero };
            logmov.SequenceNumeric = 1;

            double totalWeightGross = voo.Masters.Sum(x => x.PesoTotalBruto);
            int totalPecas = voo.Masters.Sum(x => x.TotalPecas);
            int totalPacotes = voo.Masters.Count();
            logmov.TotalGrossWeightMeasure = new Flight.MeasureType
            {
                unitCode = Flight.MeasurementUnitCommonCodeContentType.KGM,
                unitCodeSpecified = true,
                Value = Convert.ToDecimal(totalWeightGross)
            };
            logmov.TotalPackageQuantity = new Flight.QuantityType { Value = totalPacotes };
            logmov.TotalPieceQuantity = new Flight.QuantityType { Value = totalPecas };
            logmov.DepartureEvent = new Flight.DepartureEventType();
            logmov.DepartureEvent.DepartureOccurrenceDateTime = (DateTime)voo.DataHoraSaidaReal;
            logmov.DepartureEvent.DepartureDateTimeTypeCode = new Flight.CodeType { Value = "A" };
            logmov.DepartureEvent.OccurrenceDepartureLocation = new Flight.DepartureLocationType();
            logmov.DepartureEvent.OccurrenceDepartureLocation.ID = new Flight.IDType { Value = voo.PortoIataOrigemInfo.Codigo };
            logmov.DepartureEvent.OccurrenceDepartureLocation.Name = new Flight.TextType { Value = voo.PortoIataOrigemInfo.Nome };
            logmov.DepartureEvent.OccurrenceDepartureLocation.TypeCode = new Flight.CodeType { Value = "Airport" };
            manvoo.LogisticsTransportMovement = logmov;

            var arrivalEvents = new List<Flight.ArrivalEventType>();

            foreach (VooTrecho trecho in voo.Trechos)
            {
                // ArrivalEvent
                Flight.ArrivalEventType arrevent = new Flight.ArrivalEventType();
                if (trecho.DataHoraChegadaEstimada != null)
                {
                    arrevent.ArrivalOccurrenceDateTime = trecho.DataHoraChegadaEstimada.Value;
                    arrevent.ArrivalDateTimeTypeCode = new Flight.CodeType { Value = "S" };
                    arrevent.ArrivalOccurrenceDateTimeSpecified = true;
                };
                if (trecho.DataHoraSaidaEstimada != null)
                {
                    arrevent.DepartureOccurrenceDateTime = trecho.DataHoraSaidaEstimada;
                    arrevent.DepartureDateTimeTypeCode = new Flight.CodeType { Value = "S" };
                    arrevent.DepartureOccurrenceDateTimeSpecified = true;
                };
                arrevent.OccurrenceArrivalLocation = new Flight.ArrivalLocationType();
                arrevent.OccurrenceArrivalLocation.ID = new Flight.IDType { Value = trecho.PortoIataDestinoInfo.Codigo };
                arrevent.OccurrenceArrivalLocation.Name = new Flight.TextType { Value = trecho.PortoIataDestinoInfo.Nome };
                arrevent.OccurrenceArrivalLocation.TypeCode = new Flight.CodeType { Value = "Airport" };

                List<Flight.TransportCargoType> transportes = new List<Flight.TransportCargoType>();
                // adicionar masters
                IEnumerable<UldMaster> uldsMaster = trecho.ULDs.Where(x => x.DataExclusao == null && x.ULDCaracteristicaCodigo != "BLK");
                IEnumerable<UldMaster> blk = trecho.ULDs.Where(x => x.DataExclusao == null && x.ULDCaracteristicaCodigo == "BLK");

                if (uldsMaster.Count() > 0)
                {

                    var grupos = uldsMaster.GroupBy(r => (r.ULDCaracteristicaCodigo, r.ULDId, r.ULDIdPrimario))
                        .Select(g => (g.Key.ULDCaracteristicaCodigo, g.Key.ULDId, g.Key.ULDIdPrimario, Pecas: g.Sum(x => x.QuantidadePecas), Peso: g.Sum(x => x.Peso)));

                    foreach (var item in grupos)
                    {

                        var ulds = uldsMaster.Where(x => x.ULDCaracteristicaCodigo == item.ULDCaracteristicaCodigo &&
                                            x.ULDId == item.ULDId &&
                                            x.ULDIdPrimario == item.ULDIdPrimario).ToList();

                        Flight.TransportCargoType transpuld = new Flight.TransportCargoType();

                        transpuld.TypeCode = new Flight.CodeType { Value = "ULD" };

                        Enum.TryParse("KGM", out Flight.MeasurementUnitCommonCodeContentType pesoUN);

                        Flight.UnitLoadTransportEquipmentType unitequip = new Flight.UnitLoadTransportEquipmentType
                        {
                            ID = new Flight.IDType { Value = item.ULDId },
                            CharacteristicCode = new Flight.CodeType { Value = item.ULDCaracteristicaCodigo },
                            OperatingParty = new Flight.OperatingPartyType
                            {
                                PrimaryID = new Flight.IDType { Value = item.ULDIdPrimario }
                            },
                            PieceQuantity = new Flight.QuantityType { Value = Convert.ToDecimal(item.Pecas) },
                            GrossWeightMeasure = new Flight.MeasureType
                            {
                                Value = Convert.ToDecimal(item.Peso),
                                unitCodeSpecified = true,
                                unitCode = pesoUN
                            }
                        };

                        transpuld.UtilizedUnitLoadTransportEquipment = unitequip;

                        // Monta a lista de Master
                        List<Flight.MasterConsignmentType> masterConsigments = new List<Flight.MasterConsignmentType>();

                        foreach (UldMaster uldMaster in ulds)
                        {

                            Flight.MasterConsignmentType masterC = new Flight.MasterConsignmentType();

                            Enum.TryParse(uldMaster.MasterInfo.PesoTotalBrutoUN, out Flight.MeasurementUnitCommonCodeContentType pesoTotalUN);

                            masterC.GrossWeightMeasure = new Flight.MeasureType
                            {
                                Value = Convert.ToDecimal(uldMaster.Peso),
                                unitCodeSpecified = true,
                                unitCode = pesoTotalUN
                            };

                            masterC.PackageQuantity = new Flight.QuantityType { Value = 1 };
                            masterC.TotalPieceQuantity = new Flight.QuantityType { Value = Convert.ToDecimal(uldMaster.QuantidadePecas) };
                            masterC.SummaryDescription = new Flight.TextType { Value = uldMaster.MasterInfo.DescricaoMercadoria };
                            masterC.TransportSplitDescription = new Flight.TextType { Value = uldMaster.TotalParcial };
                            masterC.TransportContractDocument = new Flight.TransportContractDocumentType();

                            masterC.TransportContractDocument.ID = new Flight.IDType
                            {
                                Value = uldMaster.MasterInfo.IndicadorAwbNaoIata ? uldMaster.MasterInfo.Numero : uldMaster.MasterInfo.Numero.Insert(3, "-")
                            };

                            masterC.OriginLocation = new Flight.OriginLocationType
                            {
                                ID = new Flight.IDType { Value = uldMaster.MasterInfo.AeroportoOrigemInfo.Codigo },
                                Name = new Flight.TextType { Value = uldMaster.MasterInfo.AeroportoOrigemInfo.Nome }
                            };

                            masterC.FinalDestinationLocation = new Flight.FinalDestinationLocationType
                            {
                                ID = new Flight.IDType { Value = uldMaster.MasterInfo.AeroportoDestinoInfo.Codigo },
                                Name = new Flight.TextType { Value = uldMaster.MasterInfo.AeroportoDestinoInfo.Nome }
                            };

                            if (uldMaster.MasterInfo.IndicadorAwbNaoIata)
                            {
                                masterC.IncludedCustomsNote = new Flight.CustomsNoteType[1];
                                masterC.IncludedCustomsNote[0] = new Flight.CustomsNoteType
                                {
                                    Content = new Flight.TextType { Value = "NON-IATA" },
                                    ContentCode = new Flight.CodeType { Value = "DI" },
                                    SubjectCode = new Flight.CodeType { Value = "WBI" },
                                    CountryID = new Flight.CountryIDType { Value = Flight.ISOTwoletterCountryCodeIdentifierContentType.BR }
                                };
                            };

                            masterConsigments.Add(masterC);
                        }

                        transpuld.IncludedMasterConsignment = masterConsigments.ToArray();

                        transportes.Add(transpuld);
                    }
                }

                if (blk.Count() > 0)
                {

                    Flight.TransportCargoType transpuld = new Flight.TransportCargoType();

                    transpuld.TypeCode = new Flight.CodeType { Value = "BLK" };

                    List<Flight.MasterConsignmentType> masterConsigments = new List<Flight.MasterConsignmentType>();

                    foreach (UldMaster uldMaster in blk)
                    {
                        Flight.MasterConsignmentType masterC = new Flight.MasterConsignmentType();

                        Enum.TryParse(uldMaster.MasterInfo.PesoTotalBrutoUN, out Flight.MeasurementUnitCommonCodeContentType pesoTotalUN);

                        masterC.GrossWeightMeasure = new Flight.MeasureType
                        {
                            Value = Convert.ToDecimal(uldMaster.Peso),
                            unitCodeSpecified = true,
                            unitCode = pesoTotalUN
                        };

                        masterC.PackageQuantity = new Flight.QuantityType { Value = 1 };
                        masterC.TotalPieceQuantity = new Flight.QuantityType { Value = Convert.ToDecimal(uldMaster.QuantidadePecas) };
                        masterC.SummaryDescription = new Flight.TextType { Value = uldMaster.MasterInfo.DescricaoMercadoria };
                        masterC.TransportSplitDescription = new Flight.TextType { Value = "T" };
                        masterC.TransportContractDocument = new Flight.TransportContractDocumentType
                        {
                            ID = new Flight.IDType { Value = uldMaster.MasterInfo.IndicadorAwbNaoIata ? uldMaster.MasterInfo.Numero : uldMaster.MasterInfo.Numero.Insert(3, "-") }
                        };

                        masterC.OriginLocation = new Flight.OriginLocationType
                        {
                            ID = new Flight.IDType { Value = uldMaster.MasterInfo.AeroportoOrigemInfo.Codigo },
                            Name = new Flight.TextType { Value = uldMaster.MasterInfo.AeroportoOrigemInfo.Nome }
                        };

                        masterC.FinalDestinationLocation = new Flight.FinalDestinationLocationType
                        {
                            ID = new Flight.IDType { Value = uldMaster.MasterInfo.AeroportoDestinoInfo.Codigo },
                            Name = new Flight.TextType { Value = uldMaster.MasterInfo.AeroportoDestinoInfo.Nome }
                        };

                        if (uldMaster.MasterInfo.IndicadorAwbNaoIata)
                        {
                            masterC.IncludedCustomsNote = new Flight.CustomsNoteType[1];
                            masterC.IncludedCustomsNote[0] = new Flight.CustomsNoteType
                            {
                                Content = new Flight.TextType { Value = "NON-IATA" },
                                ContentCode = new Flight.CodeType { Value = "DI" },
                                SubjectCode = new Flight.CodeType { Value = "WBI" },
                                CountryID = new Flight.CountryIDType { Value = Flight.ISOTwoletterCountryCodeIdentifierContentType.BR }
                            };
                        };

                        masterConsigments.Add(masterC);
                    }

                    transpuld.IncludedMasterConsignment = masterConsigments.ToArray();
                    transportes.Add(transpuld);
                }

                if(blk.Count() == 0 && uldsMaster.Count() == 0)
                {
                    Flight.TransportCargoType transpuld = new Flight.TransportCargoType();

                    transpuld.TypeCode = new Flight.CodeType { Value = "NIL" };

                    transportes.Add(transpuld);
                }
                
                arrevent.AssociatedTransportCargo = transportes.ToArray();

                arrivalEvents.Add(arrevent);
            }
            
            manvoo.ArrivalEvent = arrivalEvents.ToArray();

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "iata:datamodel:3");
            ns.Add("ns2", "iata:flightmanifest:1");

            return SerializeFromStream<Flight.FlightManifestType>(manvoo, ns);
        }
        catch (Exception ex)
        {
            throw new Exception($"Não foi possivel gerar o arquivo XML do voo ({ex.Message}).");
        }
    }

    /// <summary>
    /// Gera o XML do manifesto
    /// </summary>
    /// <param name="master"></param>
    /// <returns></returns>
    public string GenMasterManifest(Master master, IataXmlPurposeCode purposeCode)
    {
        Waybill.WaybillType manmaster = new Waybill.WaybillType();

        #region MessageHeaderDocument
        if (master.XmlIssueDate == null)
            master.XmlIssueDate = DateTime.UtcNow;

        Waybill.MessageHeaderDocumentType headerDocument = new Waybill.MessageHeaderDocumentType();
        headerDocument.ID = new Waybill.IDType() { Value = $"{master.Numero}_{master.DataEmissaoXML.Value.ToString("ddMMyyyyhhmmss")}" };
        headerDocument.Name = new Waybill.TextType() { Value = "MASTER AIR WAYBILL" };

        if (master.CodigoConteudo != null && master.CodigoConteudo == "D")
            headerDocument.TypeCode = new Waybill.DocumentCodeType() { Value = Waybill.DocumentNameCodeContentType.Item740 }; // Direct
        else
            headerDocument.TypeCode = new Waybill.DocumentCodeType() { Value = Waybill.DocumentNameCodeContentType.Item741 }; // Consolidated

        headerDocument.IssueDateTime = master.XmlIssueDate.Value.AddHours(-3);
        headerDocument.PurposeCode = new Waybill.CodeType() { Value = purposeCode.ToString() };

        headerDocument.VersionID = new Waybill.IDType() { Value = "3.00" };
        headerDocument.SenderParty = new Waybill.SenderPartyType[2];
        headerDocument.SenderParty[0] = new Waybill.SenderPartyType();
        headerDocument.SenderParty[0].PrimaryID = new Waybill.IDType { Value = "HDQTTKE", schemeID = "C" };
        headerDocument.SenderParty[1] = new Waybill.SenderPartyType();
        headerDocument.SenderParty[1].PrimaryID = new Waybill.IDType { Value = "HDQTTKE", schemeID = "P" };
        headerDocument.RecipientParty = new Waybill.RecipientPartyType[1];
        headerDocument.RecipientParty[0] = new Waybill.RecipientPartyType();
        headerDocument.RecipientParty[0].PrimaryID = new Waybill.IDType { Value = "BRCUSTOMS", schemeID = "C" };
        manmaster.MessageHeaderDocument = headerDocument;

        #endregion

        #region BusinessHeaderDocument

        Waybill.BusinessHeaderDocumentType businessDocument = new Waybill.BusinessHeaderDocumentType();
        if (master.IndicadorAwbNaoIata)
            businessDocument.ID = new Waybill.IDType() { Value = $"{master.Numero}" };
        else
            businessDocument.ID = new Waybill.IDType() { Value = $"{master.Numero.Insert(3, "-")}" };

        Waybill.HeaderNoteType headerNoteType;

        if (master.CodigoConteudo != null && master.CodigoConteudo == "D")  // Direct
        {
            headerNoteType = new Waybill.HeaderNoteType()
            {
                ContentCode = new Waybill.CodeType() { Value = "D" },
                Content = new Waybill.TextType() { Value = "Direct" }
            };
        }
        else // Consolidated
        {
            headerNoteType = new Waybill.HeaderNoteType()
            {
                ContentCode = new Waybill.CodeType() { Value = "C" },
                Content = new Waybill.TextType() { Value = "Consolidation" }
            };
        }

        businessDocument.SignatoryCarrierAuthentication = new Waybill.CarrierAuthenticationType
        {
            ActualDateTime = master.DataEmissaoXML.Value,
            IssueAuthenticationLocation = new Waybill.AuthenticationLocationType
            {
                Name = new Waybill.TextType { Value = master.AutenticacaoSignatariaLocal }
            },
            Signatory = new Waybill.TextType
            {
                Value = master.AutenticacaoSignatariaNome
            }
        };

        Waybill.HeaderNoteType[] headersNoteType = new Waybill.HeaderNoteType[1];
        headersNoteType[0] = headerNoteType;
        businessDocument.IncludedHeaderNote = headersNoteType;
        manmaster.BusinessHeaderDocument = businessDocument;

        #endregion

        #region MasterConsignment

        Enum.TryParse(master.PesoTotalBrutoUN, out Waybill.MeasurementUnitCommonCodeContentType pesoTotalUN);
        Waybill.MasterConsignmentType masterConsigment = new Waybill.MasterConsignmentType();
        masterConsigment.TotalPieceQuantity = new Waybill.QuantityType() { Value = master.TotalPecas };
        masterConsigment.IncludedTareGrossWeightMeasure = new Waybill.MeasureType()
        {
            Value = Convert.ToDecimal(master.PesoTotalBruto),
            unitCodeSpecified = master.PesoTotalBrutoUN == null ? false : true,
            unitCode = pesoTotalUN
        };
        masterConsigment.ProductID = new Waybill.IDType { Value = "GEN" };
        if(master.ValorFretePP > 0)
            masterConsigment.TotalChargePrepaidIndicator = true;

        #region MasterConsigment > ConsignorParty
        Enum.TryParse(master.ExpedidorPaisCodigo, out Waybill.ISOTwoletterCountryCodeIdentifierContentType codigoPaisExpedidor);
        masterConsigment.ConsignorParty = new Waybill.ConsignorPartyType
        {
            Name = new Waybill.TextType { Value = master.ExpedidorNome },
            PostalStructuredAddress = new Waybill.StructuredAddressType
            {
                PostcodeCode = new Waybill.CodeType { Value = master.ExpedidorPostal },
                StreetName = new Waybill.TextType { Value = master.ExpedidorEndereco },
                CityName = new Waybill.TextType { Value = master.ExpedidorCidade },
                CountryID = new Waybill.CountryIDType { Value = codigoPaisExpedidor }
            }
        };
        #endregion

        #region MasterConsigment > ConsigneeParty
        Enum.TryParse(master.ConsignatarioPaisCodigo, out Waybill.ISOTwoletterCountryCodeIdentifierContentType codigoPaisConsignatario);
        masterConsigment.ConsigneeParty = new Waybill.ConsigneePartyType
        {
            Name = new Waybill.TextType { Value = master.ConsignatarioNome },
            PostalStructuredAddress = new Waybill.StructuredAddressType
            {
                PostcodeCode = new Waybill.CodeType { Value = master.ConsignatarioPostal },
                StreetName = new Waybill.TextType { Value = master.ConsignatarioEndereco },
                CityName = new Waybill.TextType { Value = master.ConsignatarioCidade },
                CountryID = new Waybill.CountryIDType { Value = codigoPaisConsignatario }
            }
        };
        #endregion

        #region MasterConsigment > FreightForwarderParty
        Enum.TryParse(master.EmissorPaisCodigo, out Waybill.ISOTwoletterCountryCodeIdentifierContentType codigoPaisAgenteCarga);
        //masterConsigment.FreightForwarderParty = new Waybill.FreightForwarderPartyType
        //{
        //    Name = new Waybill.TextType { Value = master.EmissorNome },
        //    CargoAgentID = new Waybill.IDType { Value = master.EmissorCargoAgenteLocalizacao },
        //    FreightForwarderAddress = new Waybill.FreightForwarderAddressType
        //    {
        //        StreetName = new Waybill.TextType { Value = master.EmissorEndereco },
        //        CityName = new Waybill.TextType { Value = master.EmissorCidade },
        //        CountryID = new Waybill.CountryIDType { Value = codigoPaisAgenteCarga }
        //    }
        //};
        #endregion

        #region MasterConsigment > OriginLocation
        masterConsigment.OriginLocation = new Waybill.OriginLocationType
        {
            ID = new Waybill.IDType { Value = master.AeroportoOrigemInfo.Codigo },
            Name = new Waybill.TextType { Value = master.AeroportoOrigemInfo.Nome }
        };
        #endregion

        #region MasterConsigment > FinalDestinationLocation
        masterConsigment.FinalDestinationLocation = new Waybill.FinalDestinationLocationType
        {
            ID = new Waybill.IDType { Value = master.AeroportoDestinoInfo.Codigo },
            Name = new Waybill.TextType { Value = master.AeroportoDestinoInfo.Nome }
        };
        #endregion

        #region MasterConsigment > SpecifiedLogisticsTransportMovement
        masterConsigment.SpecifiedLogisticsTransportMovement = new Waybill.LogisticsTransportMovementType[1];
        masterConsigment.SpecifiedLogisticsTransportMovement[0] = new Waybill.LogisticsTransportMovementType();
        masterConsigment.SpecifiedLogisticsTransportMovement[0].StageCode = new Waybill.CodeType { Value = "Main-Carriage" };
        masterConsigment.SpecifiedLogisticsTransportMovement[0].ModeCode = new Waybill.TransportModeCodeType { Value = Waybill.TransportModeCodeContentType.Item4 };
        masterConsigment.SpecifiedLogisticsTransportMovement[0].Mode = new Waybill.TextType { Value = "AIR TRANSPORT" };
        masterConsigment.SpecifiedLogisticsTransportMovement[0].ID = new Waybill.IDType { Value = master.VooInfo.Numero };
        masterConsigment.SpecifiedLogisticsTransportMovement[0].SequenceNumeric = 1;
        masterConsigment.SpecifiedLogisticsTransportMovement[0].UsedLogisticsTransportMeans = new Waybill.LogisticsTransportMeansType
        {
            Name = new Waybill.TextType
            {
                Value = master.VooInfo.Numero.Substring(0, 2)
            }
        };

        if (master.VooInfo.Trechos?.Count() > 0)
        {
            var firstLag = (master.VooInfo.Trechos.FirstOrDefault(x => x.DataExclusao == null));

            if (firstLag != null)
            {
                masterConsigment.SpecifiedLogisticsTransportMovement[0].ArrivalEvent = new Waybill.ArrivalEventType
                {
                    ScheduledOccurrenceDateTimeSpecified = true,
                    ScheduledOccurrenceDateTime = firstLag.DataHoraChegadaEstimada.Value,
                    OccurrenceArrivalLocation = new Waybill.ArrivalLocationType
                    {
                        ID = new Waybill.IDType { Value = firstLag.PortoIataDestinoInfo.Codigo },
                        Name = new Waybill.TextType { Value = firstLag.PortoIataDestinoInfo.Nome },
                        TypeCode = new Waybill.CodeType { Value = "Airport" }
                    }
                };
            }
        }
        masterConsigment.SpecifiedLogisticsTransportMovement[0].DepartureEvent = new Waybill.DepartureEventType
        {
            ScheduledOccurrenceDateTimeSpecified = true,
            ScheduledOccurrenceDateTime = (DateTime)master.VooInfo.DataHoraSaidaEstimada,
            OccurrenceDepartureLocation = new Waybill.DepartureLocationType
            {
                ID = new Waybill.IDType { Value = master.VooInfo.PortoIataOrigemInfo.Codigo },
                Name = new Waybill.TextType { Value = master.VooInfo.PortoIataOrigemInfo.Nome },
                TypeCode = new Waybill.CodeType { Value = "Airport" }
            }
        };
        #endregion

        #region MasterConsigment - IncludedCustomsNote
        string tipoDoc = "";

        var customsNote = new List<Waybill.CustomsNoteType>();

        if (master.ConsignatarioCNPJ != null && master.ConsignatarioCNPJ.Length > 0)
        {
            if (master.ConsignatarioCNPJ.StartsWith("PP"))
                tipoDoc = $"PASSPORT{master.ConsignatarioCNPJ.Substring(2)}";
            else if (master.ConsignatarioCNPJ.Length == 11)
                tipoDoc = $"CPF{master.ConsignatarioCNPJ}";
            else
                tipoDoc = $"CNPJ{master.ConsignatarioCNPJ}";

            if (tipoDoc.Length > 0)
            {
                customsNote.Add(new Waybill.CustomsNoteType
                {
                    CountryID = new Waybill.CountryIDType() { Value = Waybill.ISOTwoletterCountryCodeIdentifierContentType.BR },
                    SubjectCode = new Waybill.CodeType() { Value = "CNE" },
                    ContentCode = new Waybill.CodeType() { Value = "T" },
                    Content = new Waybill.TextType() { Value = tipoDoc }
                });
            };
        };


        if (master.CodigoRecintoAduaneiro != null && master.CodigoRecintoAduaneiro.Length == 7)
        {
            customsNote.Add(new Waybill.CustomsNoteType
            {
                CountryID = new Waybill.CountryIDType() { Value = Waybill.ISOTwoletterCountryCodeIdentifierContentType.BR },
                SubjectCode = new Waybill.CodeType() { Value = "CCL" },
                ContentCode = new Waybill.CodeType() { Value = "M" },
                Content = new Waybill.TextType() { Value = $"CUSTOMSWAREHOUSE{master.CodigoRecintoAduaneiro.ToString()}" }
            });
        }

        if (master.IndicadorAwbNaoIata)
        {
            customsNote.Add(new Waybill.CustomsNoteType
            {
                ContentCode = new Waybill.CodeType() { Value = "DI" },
                Content = new Waybill.TextType() { Value = "NON-IATA" },
                SubjectCode = new Waybill.CodeType() { Value = "WBI" },
                CountryID = new Waybill.CountryIDType() { Value = Waybill.ISOTwoletterCountryCodeIdentifierContentType.BR }
            });
        }

        masterConsigment.IncludedCustomsNote = customsNote.ToArray();
        #endregion

        Enum.TryParse(master.ValorFretePPUN, out Waybill.ISO3AlphaCurrencyCodeContentType valorPPUN);
        Enum.TryParse(master.ValorFreteFCUN, out Waybill.ISO3AlphaCurrencyCodeContentType valorFCUN);

        #region MasterConsigment - ApplicableOriginCurrencyExchange

        masterConsigment.ApplicableOriginCurrencyExchange = new Waybill.OriginCurrencyExchangeType
        {
            SourceCurrencyCode = new Waybill.CurrencyCodeType { Value = valorPPUN }
        };
        #endregion

        #region MasterConsigment > ApplicableRating

        List<Waybill.RatingType> ratingList = new List<Waybill.RatingType>();
        int sequencyRating = 0;

        // Prepraid
        sequencyRating++;
        Waybill.RatingType ratingType = new Waybill.RatingType();
        ratingType.TypeCode = new Waybill.CodeType { Value = "F" };

        ratingType.TotalChargeAmount = new Waybill.AmountType
        {
            Value = master.ValorFretePP + master.ValorFreteFC,
            currencyIDSpecified = true,
            currencyID = valorPPUN
        };

        ratingType.ConsignmentItemQuantity = new Waybill.QuantityType { Value = 1 };
        Waybill.MasterConsignmentItemType masterConsignmentItem = new Waybill.MasterConsignmentItemType();
        masterConsignmentItem.SequenceNumeric = sequencyRating;
        masterConsignmentItem.GrossWeightMeasure = new Waybill.MeasureType
        {
            unitCodeSpecified = true,
            unitCode = pesoTotalUN,
            Value = Convert.ToDecimal(master.PesoTotalBruto)
        };
        if (master?.Volume > 0)
        {
            masterConsignmentItem.GrossVolumeMeasure = new Waybill.MeasureType
            {
                unitCodeSpecified = true,
                unitCode = Waybill.MeasurementUnitCommonCodeContentType.MTQ,
                Value = Convert.ToDecimal(master.Volume)
            };
        } else
        {
            masterConsignmentItem.TransportLogisticsPackage = new Waybill.LogisticsPackageType[1];

            masterConsignmentItem.TransportLogisticsPackage[0] = new Waybill.LogisticsPackageType
            {
                ItemQuantity = new Waybill.QuantityType { Value = Convert.ToDecimal(master.TotalPecas) },
                GrossWeightMeasure = new Waybill.MeasureType
                {
                    unitCodeSpecified = true,
                    unitCode = pesoTotalUN,
                    Value = Convert.ToDecimal(master.PesoTotalBruto)
                },
                LinearSpatialDimension = new Waybill.SpatialDimensionType
                {
                    HeightMeasure = new Waybill.MeasureType
                    {
                        Value = 10,
                        unitCode = Waybill.MeasurementUnitCommonCodeContentType.CMT,
                        unitCodeSpecified = true
                    },
                    LengthMeasure = new Waybill.MeasureType
                    {
                        Value = 10,
                        unitCode = Waybill.MeasurementUnitCommonCodeContentType.CMT,
                        unitCodeSpecified = true
                    },
                    WidthMeasure = new Waybill.MeasureType
                    {
                        Value = 10,
                        unitCode = Waybill.MeasurementUnitCommonCodeContentType.CMT,
                        unitCodeSpecified = true
                    }
                }
            };
        };
        masterConsignmentItem.PieceQuantity = new Waybill.QuantityType { Value = master.TotalPecas };
        masterConsignmentItem.NatureIdentificationTransportCargo = new Waybill.TransportCargoType();
        masterConsignmentItem.NatureIdentificationTransportCargo.Identification = new Waybill.TextType()
        {
            Value = master.DescricaoMercadoria
        };

        Waybill.MasterConsignmentItemType[] masterConsignmentItems = new Waybill.MasterConsignmentItemType[1];
        masterConsignmentItems[0] = masterConsignmentItem;

        ratingType.IncludedMasterConsignmentItem = masterConsignmentItems;
        ratingList.Add(ratingType);

        masterConsigment.ApplicableRating = ratingList.ToArray();

        #endregion

        #region MasterConsigment > ApplicableTotalRating

        var applicableTotalRatingList = new List<Waybill.TotalRatingType>();

        var applicableTotalRating = new Waybill.TotalRatingType();
        applicableTotalRating.TypeCode = new Waybill.CodeType() { Value = "F" };
        
        var sumArray = new List<Waybill.PrepaidCollectMonetarySummationType>();

        if(master.ValorFretePP > 0)
        {
            sumArray.Add( new Waybill.PrepaidCollectMonetarySummationType
            {
                WeightChargeTotalAmount = new Waybill.AmountType
                {
                    currencyIDSpecified = true,
                    currencyID = valorPPUN,
                    Value = master.ValorFretePP
                },
                GrandTotalAmount = new Waybill.AmountType
                {
                    Value = master.ValorFretePP,
                    currencyIDSpecified = true,
                    currencyID = valorPPUN
                },
                PrepaidIndicator = true
            });
        }

        if(master.ValorFreteFC > 0)
        {
            sumArray.Add(new Waybill.PrepaidCollectMonetarySummationType
            {
                WeightChargeTotalAmount = new Waybill.AmountType
                {
                    currencyIDSpecified = true,
                    currencyID = valorFCUN,
                    Value = master.ValorFreteFC
                },
                GrandTotalAmount = new Waybill.AmountType
                {
                    Value = master.ValorFreteFC,
                    currencyIDSpecified = true,
                    currencyID = valorFCUN
                },
                PrepaidIndicator = true
            });
        }

        applicableTotalRating.ApplicablePrepaidCollectMonetarySummation = sumArray.ToArray();

        applicableTotalRatingList.Add(applicableTotalRating);

        masterConsigment.ApplicableTotalRating = applicableTotalRatingList.ToArray();
        #endregion

        manmaster.MasterConsignment = masterConsigment;

        #endregion

        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        ns.Add("", "iata:datamodel:3");
        ns.Add("ns2", "iata:waybill:1");

        return SerializeFromStream<Waybill.WaybillType>(manmaster, ns);

    }

    public string GenHouseManifest(House house, IataXmlPurposeCode purposeCode)
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
            ID = new IDType { Value = $"{ house.Numero }_{ house.DataEmissaoXML.Value.ToString("ddMMyyyhhmmss") }" },
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
                    Name = new TextType { Value = house.AeroportoOrigemInfo.Codigo }
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
            OriginLocation = new OriginLocationType {
                ID = new IDType { Value = house.AeroportoOrigemInfo.Codigo },
                Name = new TextType { Value = house.AeroportoOrigemInfo.Nome }
            },
            FinalDestinationLocation = new FinalDestinationLocationType
            {
                ID = new IDType { Value = house.AeroportoDestinoInfo.Codigo  },
                Name = new TextType {  Value = house.AeroportoDestinoInfo.Nome }
            }
        };

        #region MasterConsigment -> IncludedHouseConsigmnet
        manhouse.MasterConsignment.IncludedHouseConsignment = new HouseConsignmentType
        {
            //InsuranceValueAmount = new AmountType { currencyID = valorPPUN, currencyIDSpecified = true, Value = 0 },
            TotalChargePrepaidIndicatorFlag = house.ValorFreteFC == 0 ? true : false,
            ValuationTotalChargeAmount = new AmountType { currencyID = valorPPUN, currencyIDSpecified = true, Value = 0 },
            TaxTotalChargeAmount = new AmountType { currencyID = valorPPUN, currencyIDSpecified = true, Value = 0 },
            WeightTotalChargeAmount = new AmountType { currencyID = valorPPUN, currencyIDSpecified = true, Value = 0 },
            TotalDisbursementPrepaidIndicator = false,
            AgentTotalDisbursementAmount = new AmountType { currencyID = valorPPUN, currencyIDSpecified = true, Value = 0},
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
            }: null,
            ConsignorParty = new ConsignorPartyType
            {
                Name = new TextType { Value = house.ExpedidorNome },
                PostalStructuredAddress = new StructuredAddressType
                {
                    PostcodeCode = new CodeType { Value = house.ExpedidorPostal },
                    StreetName = new TextType {  Value = house.ExpedidorEndereco },
                    CityName = new TextType { Value = house.ExpedidorCidade },
                    CountryID = new CountryIDType {  
                        Value = (ISOTwoletterCountryCodeIdentifierContentType) 
                        Enum.Parse(typeof(ISOTwoletterCountryCodeIdentifierContentType), house.ExpedidorPaisCodigo) }
                }
            },
            ConsigneeParty = new ConsigneePartyType
            {
                Name = new TextType { Value = house.ConsignatarioNome },
                PostalStructuredAddress = new StructuredAddressType
                {
                    PostcodeCode = new CodeType { Value = house.ConsignatarioPostal },
                    StreetName = new TextType { Value = house.ConsignatarioEndereco },
                    CityName = new TextType {  Value = house.ConsignatarioCidade },
                    CountryID = new CountryIDType
                    {
                        Value = (ISOTwoletterCountryCodeIdentifierContentType)
                        Enum.Parse(typeof(ISOTwoletterCountryCodeIdentifierContentType), house.ConsignatarioPaisCodigo)
                    }
                }
            },
            OriginLocation = new OriginLocationType
            {
                ID = new IDType { Value = house.AeroportoOrigemInfo.Codigo },
                Name = new TextType {  Value = house.AeroportoOrigemInfo.Nome }
            },
            FinalDestinationLocation = new FinalDestinationLocationType
            {
                ID = new IDType { Value = house.AeroportoDestinoInfo.Codigo },
                Name = new TextType { Value = house.AeroportoDestinoInfo.Nome }
            },
            HandlingOSIInstructions =  new OSIInstructionsType[1],
            IncludedHouseConsignmentItem = new HouseConsignmentItemType[1],
            TotalDisbursementPrepaidIndicatorSpecified = true
            //SpecifiedLogisticsTransportMovement = new HouseManifest.LogisticsTransportMovementType[1]
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
        if(house.NCMLista != null)
        {
            var ncmArray = house.NCMLista.Split(",");
            var ncmArrayObject = from c in ncmArray
                                 select new CodeType
                                 {
                                     Value = c.Replace(".","")
                                 };

            manhouse.MasterConsignment.IncludedHouseConsignment.IncludedHouseConsignmentItem[0].TypeCode =
                ncmArrayObject.ToArray();
        }
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
                AppliedAmount = new AmountType { currencyID = valorPPUN , Value = (house.ValorFretePP + house.ValorFreteFC) }
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

        if(customsNoteType.Count > 0)
            manhouse.MasterConsignment.IncludedHouseConsignment.IncludedCustomsNote = customsNoteType.ToArray();

        //manhouse.MasterConsignment.IncludedHouseConsignment.SpecifiedLogisticsTransportMovement[0] = new HouseManifest.LogisticsTransportMovementType
        //{
        //    StageCode = new HouseManifest.CodeType { Value = "Main-Carriage" },
        //    ArrivalEvent = new HouseManifest.ArrivalEventType
        //    {
        //        ScheduledOccurrenceDateTime = DateTime.Now.AddDays(1),
        //        OccurrenceArrivalLocation = new HouseManifest.ArrivalLocationType
        //        {
        //            ID = new HouseManifest.IDType { Value = house.AeroportoDestinoCodigo.ToString() },
        //            Name = new HouseManifest.TextType { Value = house.AeroportoDestinoInfo.Nome }
        //        }
        //    }
        //};

        #endregion

        #endregion

        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        ns.Add("", "iata:datamodel:3");
        ns.Add("ns2", "iata:waybill:1");
        ns.Add("q1", "iata:housewaybill:1");
        return SerializeFromStream<HouseWaybillType>(manhouse, ns);
    }

    public string GenMasterHouseManifest(SubmeterRFBMasterHouseItemRequest masterInfo, List<House> houses, IataXmlPurposeCode purposeCode, DateTime issueDate)
    {
        var portOrigin = new HouseManifest.OriginLocationType { ID = new HouseManifest.IDType { Value = masterInfo.OriginLocation } };
        var portDestiny = new HouseManifest.FinalDestinationLocationType { ID = new HouseManifest.IDType { Value = masterInfo.DestinationLocation } };
        Enum.TryParse(masterInfo.TotalWeightUnit, out HouseManifest.MeasurementUnitCommonCodeContentType totalWeightUN);
        HouseManifest.HouseManifestType manhouse = new HouseManifest.HouseManifestType();

        #region MessageHeaderDocument
        manhouse.MessageHeaderDocument = new HouseManifest.MessageHeaderDocumentType();
        manhouse.MessageHeaderDocument.ID = new HouseManifest.IDType { Value = $"{ masterInfo.MasterNumber }" };
        manhouse.MessageHeaderDocument.Name = new HouseManifest.TextType { Value = "Cargo Manifest" };
        manhouse.MessageHeaderDocument.IssueDateTime = issueDate;
        manhouse.MessageHeaderDocument.TypeCode = new HouseManifest.DocumentCodeType { Value = HouseManifest.DocumentNameCodeContentType.Item785 };
        manhouse.MessageHeaderDocument.PurposeCode = new HouseManifest.CodeType { Value = purposeCode.ToString() };
        manhouse.MessageHeaderDocument.VersionID = new HouseManifest.IDType { Value = "2.00" };
        manhouse.MessageHeaderDocument.SenderParty = new HouseManifest.SenderPartyType[2];
        manhouse.MessageHeaderDocument.RecipientParty = new HouseManifest.RecipientPartyType[1];
        manhouse.MessageHeaderDocument.SenderParty[0] = new HouseManifest.SenderPartyType
        {
            PrimaryID = new HouseManifest.IDType { schemeID = "C", Value = "HDQTTKE" }
        };
        manhouse.MessageHeaderDocument.SenderParty[1] = new HouseManifest.SenderPartyType
        {
            PrimaryID = new HouseManifest.IDType { schemeID = "P", Value = "HDQTTKE" }
        };
        manhouse.MessageHeaderDocument.RecipientParty[0] = new HouseManifest.RecipientPartyType
        {
            PrimaryID = new HouseManifest.IDType { schemeID = "C", Value = "BRCUSTOMS" }
        };
        #endregion

        #region BusinessHeaderDocument
        manhouse.BusinessHeaderDocument = new HouseManifest.BusinessHeaderDocumentType { ID = new HouseManifest.IDType { Value = masterInfo.MasterNumber } };
        #endregion

        #region MasterHeaderDocument
        manhouse.MasterConsignment = new HouseManifest.MasterConsignmentType
        {
            IncludedTareGrossWeightMeasure = new HouseManifest.MeasureType
            {
                unitCode = totalWeightUN,
                Value = Convert.ToDecimal(masterInfo.TotalWeight)
            },
            //ConsignmentItemQuantity = new HouseManifest.QuantityType { Value = masterInfo.TotalPiece },
            //PackageQuantity = new HouseManifest.QuantityType { Value = masterInfo.TotalPiece },
            TotalPieceQuantity = new HouseManifest.QuantityType { Value = masterInfo.TotalPiece },
            TransportContractDocument = new HouseManifest.TransportContractDocumentType { ID = new HouseManifest.IDType { Value = masterInfo.MasterNumber.Insert(3, "-") } },
            OriginLocation = portOrigin,
            FinalDestinationLocation = portDestiny,
        };

        List<HouseManifest.HouseConsignmentType> includedHouseCOnsigmentType = new List<HouseManifest.HouseConsignmentType>();

        houses.ForEach(house =>
        {
            var portOrigin = new HouseManifest.OriginLocationType { ID = new HouseManifest.IDType { Value = house.AeroportoOrigemCodigo } };
            var portDestiny = new HouseManifest.FinalDestinationLocationType { ID = new HouseManifest.IDType { Value = house.AeroportoDestinoCodigo } };
            Enum.TryParse(house.PesoTotalBrutoUN, out HouseManifest.MeasurementUnitCommonCodeContentType pesoTotalUN);
            includedHouseCOnsigmentType.Add(new HouseManifest.HouseConsignmentType
            {
                SequenceNumeric = 1,
                GrossWeightMeasure = new HouseManifest.MeasureType
                {
                    unitCode = pesoTotalUN,
                    Value = Convert.ToDecimal(house.PesoTotalBruto)
                },
                PackageQuantity = new HouseManifest.QuantityType { Value = house.TotalVolumes },
                TotalPieceQuantity = new HouseManifest.QuantityType { Value = house.TotalVolumes },
                SummaryDescription = new HouseManifest.TextType { Value = house.DescricaoMercadoria },
                TransportContractDocument = new HouseManifest.TransportContractDocumentType { ID = new HouseManifest.IDType { Value = house.Numero } },
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
        return SerializeFromStream<HouseManifest.HouseManifestType>(manhouse, ns);
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
