using CtaCargo.CctImportacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Waybill = CCTImportacao.IataXML.Importa.Iata.WaybillManifest;
using Flight = CCTImportacao.IataXML.Importa.Iata.FlightManifest;
using HouseWaybill = CCTImportacao.IataXML.Importa.Iata.HouseManifest;
using HouseManifest = CCTImportacao.IataXML.Importa.Iata.HouseMasterManifest;
using System.Linq;
using System.Xml;
using CtaCargo.CctImportacao.Application.Support.Contracts;
using CtaCargo.CctImportacao.Application.Dtos.Request;

namespace CtaCargo.CctImportacao.Application.Support
{
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
                Flight.MessageHeaderDocumentType headerdoc = new Flight.MessageHeaderDocumentType();
                headerdoc.ID = new Flight.IDType() { Value = $"{ voo.Numero }_{ ((DateTime)voo.DataHoraSaidaEstimada).ToString("ddMMMyyy_ddMMyyyyhhmmss") }" };
                headerdoc.Name = new Flight.TextType() { Value = "Transport Loading Report" };
                headerdoc.TypeCode = new Flight.DocumentCodeType() { Value = Flight.DocumentNameCodeContentType.Item122 };
                headerdoc.IssueDateTime = voo.DataEmissaoXML.Value.AddHours(-3);
                headerdoc.PurposeCode = new Flight.CodeType() { Value = "Creation" };
                headerdoc.VersionID = new Flight.IDType() { Value = "2.00" };
                headerdoc.SenderParty = new Flight.SenderPartyType[2];
                headerdoc.SenderParty[0] = new Flight.SenderPartyType();
                headerdoc.SenderParty[0].PrimaryID = new Flight.IDType { Value = "HDQTTKE", schemeID = "C" };
                headerdoc.SenderParty[1] = new Flight.SenderPartyType();
                headerdoc.SenderParty[1].PrimaryID = new Flight.IDType { Value = "HDQTTKE", schemeID = "P" };
                headerdoc.RecipientParty = new Flight.RecipientPartyType[1];
                headerdoc.RecipientParty[0] = new Flight.RecipientPartyType();
                headerdoc.RecipientParty[0].PrimaryID = new Flight.IDType { Value = "BRCUSTOMS", schemeID = "C" };
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

                // ArrivalEvent
                Flight.ArrivalEventType arrevent = new Flight.ArrivalEventType();
                if (voo.DataHoraChegadaEstimada != null)
                {
                    arrevent.ArrivalOccurrenceDateTime = (DateTime)voo.DataHoraChegadaEstimada;
                    arrevent.ArrivalDateTimeTypeCode = new Flight.CodeType { Value = "S" };
                    arrevent.ArrivalOccurrenceDateTimeSpecified = true;
                }
                arrevent.DepartureOccurrenceDateTime = (DateTime)voo.DataHoraSaidaEstimada;
                arrevent.DepartureDateTimeTypeCode = new Flight.CodeType { Value = "S" };
                arrevent.DepartureOccurrenceDateTimeSpecified = true;
                arrevent.OccurrenceArrivalLocation = new Flight.ArrivalLocationType();
                arrevent.OccurrenceArrivalLocation.ID = new Flight.IDType { Value = voo.PortoIataDestinoInfo.Codigo };
                arrevent.OccurrenceArrivalLocation.Name = new Flight.TextType { Value = voo.PortoIataDestinoInfo.Nome };
                arrevent.OccurrenceArrivalLocation.TypeCode = new Flight.CodeType { Value = "Airport" };

                List<Flight.TransportCargoType> transportes = new List<Flight.TransportCargoType>();
                // adicionar masters
                IEnumerable<UldMaster> uldsMaster = voo.ULDs.Where(x => x.DataExclusao == null && x.ULDCaracteristicaCodigo != "BLK");
                IEnumerable<UldMaster> blk = voo.ULDs.Where(x => x.DataExclusao == null && x.ULDCaracteristicaCodigo == "BLK");

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
                            masterC.TransportContractDocument.ID = new Flight.IDType { Value = uldMaster.MasterInfo.Numero.Insert(3, "-") };

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

                            // NCM do Master
                            //string[] NCMs = uldMaster.MasterInfo.NCMLista.Split(",");
                            
                            //masterC.IncludedMasterConsignmentItem = new Flight.IncludedMasterConsignmentItem();
                            
                            //masterC.IncludedMasterConsignmentItem.TypeCode = new Flight.CodeType[NCMs.Length];
                            
                            //for (int i = 0; i < NCMs.Length; i++)
                            //    masterC.IncludedMasterConsignmentItem.TypeCode[i] = new Flight.CodeType { Value = NCMs[i] };

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
                            ID = new Flight.IDType { Value = uldMaster.MasterInfo.Numero.Insert(3, "-") }
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

                        // NCM do Master
                        //string[] NCMs = uldMaster.MasterInfo.NCMLista.Split(",");

                        //masterC.IncludedMasterConsignmentItem = new Flight.IncludedMasterConsignmentItem();

                        //masterC.IncludedMasterConsignmentItem.TypeCode = new Flight.CodeType[NCMs.Length];

                        //for (int i = 0; i < NCMs.Length; i++)
                        //    masterC.IncludedMasterConsignmentItem.TypeCode[i] = new Flight.CodeType { Value = NCMs[i] };

                        masterConsigments.Add(masterC);
                    }
                    transpuld.IncludedMasterConsignment = masterConsigments.ToArray();
                    transportes.Add(transpuld);
                }

                arrevent.AssociatedTransportCargo = transportes.ToArray();
                manvoo.ArrivalEvent = new Flight.ArrivalEventType[1];
                manvoo.ArrivalEvent[0] = arrevent;

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

            Waybill.MessageHeaderDocumentType headerDocument = new Waybill.MessageHeaderDocumentType();
            headerDocument.ID = new Waybill.IDType() { Value = $"{ master.Numero }_{ DateTime.Now.ToString("ddMMyyyhhmmss") }" };
            headerDocument.Name = new Waybill.TextType() { Value = "MASTER AIR WAYBILL" };

            if(master.CodigoConteudo != null && master.CodigoConteudo == "D")
                headerDocument.TypeCode = new Waybill.DocumentCodeType() { Value = Waybill.DocumentNameCodeContentType.Item740 }; // Direct
            else
                headerDocument.TypeCode = new Waybill.DocumentCodeType() { Value = Waybill.DocumentNameCodeContentType.Item741 }; // Consolidated

            headerDocument.IssueDateTime = master.DataEmissaoXML.Value;
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
            businessDocument.ID = new Waybill.IDType() { Value = $"{ master.Numero.Insert(3, "-") }" };

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
                ActualDateTime = master.AutenticacaoSignatarioData.Value,
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
            masterConsigment.SpecifiedLogisticsTransportMovement[0].ArrivalEvent = new Waybill.ArrivalEventType
            {
                ScheduledOccurrenceDateTimeSpecified = true,
                ScheduledOccurrenceDateTime = (DateTime)master.VooInfo.DataHoraChegadaEstimada,
                OccurrenceArrivalLocation = new Waybill.ArrivalLocationType
                {
                    ID = new Waybill.IDType { Value = master.VooInfo.PortoIataDestinoInfo.Codigo },
                    Name = new Waybill.TextType { Value = master.VooInfo.PortoIataDestinoInfo.Nome },
                    TypeCode = new Waybill.CodeType { Value = "Airport" }
                }
            };
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
            if (master.ConsignatarioCNPJ.StartsWith("PP"))
                tipoDoc = $"PASSPORT{ master.ConsignatarioCNPJ.Substring(2) }";
            else if (master.ConsignatarioCNPJ.Length == 11)
                tipoDoc = $"CPF{ master.ConsignatarioCNPJ }";
            else
                tipoDoc = $"CNPJ{ master.ConsignatarioCNPJ }";

            var customsNote = new List<Waybill.CustomsNoteType>();

            if (master.CodigoRecintoAduaneiro != null)
            {
                customsNote.Add(new Waybill.CustomsNoteType
                {
                    CountryID = new Waybill.CountryIDType() { Value = Waybill.ISOTwoletterCountryCodeIdentifierContentType.BR },
                    SubjectCode = new Waybill.CodeType() { Value = "CCL" },
                    ContentCode = new Waybill.CodeType() { Value = "M" },
                    Content = new Waybill.TextType() { Value = $"CUSTOMSWAREHOUSE{master.CodigoRecintoAduaneiro.ToString()}" }
                });
            }

            if(master.IndicadorAwbNaoIata)
            {
                customsNote.Add(new Waybill.CustomsNoteType
                {
                    ContentCode = new Waybill.CodeType() { Value = "DI" },
                    Content = new Waybill.TextType() { Value = "NON-IATA" },
                    SubjectCode = new Waybill.CodeType() { Value = "WBI" },
                    CountryID = new Waybill.CountryIDType() { Value = Waybill.ISOTwoletterCountryCodeIdentifierContentType.BR }
                });
            }

            customsNote.Add(new Waybill.CustomsNoteType
            {
                CountryID = new Waybill.CountryIDType() { Value = Waybill.ISOTwoletterCountryCodeIdentifierContentType.BR },
                SubjectCode = new Waybill.CodeType() { Value = "CNE" },
                ContentCode = new Waybill.CodeType() { Value = "T" },
                Content = new Waybill.TextType() { Value = tipoDoc }
            });

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
            if (master.ValorFretePP > 0)
            {
                sequencyRating++;
                Waybill.RatingType ratingType = new Waybill.RatingType();
                ratingType.TypeCode = new Waybill.CodeType { Value = "F" };

                ratingType.TotalChargeAmount = new Waybill.AmountType
                {
                    Value = master.ValorFretePP,
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
                masterConsignmentItem.GrossVolumeMeasure = new Waybill.MeasureType
                {
                    unitCodeSpecified = true,
                    unitCode = Waybill.MeasurementUnitCommonCodeContentType.MTQ,
                    Value = 6
                };
                masterConsignmentItem.PieceQuantity = new Waybill.QuantityType { Value = master.TotalPecas };
                masterConsignmentItem.NatureIdentificationTransportCargo = new Waybill.TransportCargoType();
                masterConsignmentItem.NatureIdentificationTransportCargo.Identification = new Waybill.TextType()
                {
                    Value = master.DescricaoMercadoria
                };

                Waybill.MasterConsignmentItemType[] masterConsignmentItems = new Waybill.MasterConsignmentItemType[1];
                masterConsignmentItems[0] = masterConsignmentItem;
                masterConsignmentItems[0].ApplicableFreightRateServiceCharge = new Waybill.FreightRateServiceChargeType
                {
                    CategoryCode = new Waybill.CodeType { Value = "Q" },
                    ChargeableWeightMeasure = new Waybill.MeasureType
                    {
                        unitCodeSpecified = true,
                        Value = Convert.ToDecimal(master.PesoTotalBruto),
                        unitCode = pesoTotalUN
                    },
                    AppliedRate = master.ValorFretePP / Convert.ToDecimal(master.PesoTotalBruto),
                    AppliedAmount = new Waybill.AmountType
                    {
                        currencyIDSpecified = true,
                        currencyID = valorPPUN,
                        Value = master.ValorFretePP
                    }
                };
                ratingType.IncludedMasterConsignmentItem = masterConsignmentItems;
                ratingList.Add(ratingType);
            }

            // Freight Collect
            if (master.ValorFreteFC > 0)
            {
                sequencyRating++;
                Waybill.RatingType ratingType = new Waybill.RatingType();
                ratingType.TypeCode = new Waybill.CodeType { Value = "C" };

                ratingType.TotalChargeAmount = new Waybill.AmountType
                {
                    Value = master.ValorFreteFC,
                    currencyIDSpecified = true,
                    currencyID = valorFCUN
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
                masterConsignmentItem.GrossVolumeMeasure = new Waybill.MeasureType
                {
                    unitCodeSpecified = true,
                    unitCode = Waybill.MeasurementUnitCommonCodeContentType.MTQ,
                    Value = 6
                };
                masterConsignmentItem.PieceQuantity = new Waybill.QuantityType { Value = master.TotalPecas };
                masterConsignmentItem.NatureIdentificationTransportCargo = new Waybill.TransportCargoType();
                masterConsignmentItem.NatureIdentificationTransportCargo.Identification = new Waybill.TextType()
                {
                    Value = master.DescricaoMercadoria
                };

                Waybill.MasterConsignmentItemType[] masterConsignmentItems = new Waybill.MasterConsignmentItemType[1];
                masterConsignmentItems[0] = masterConsignmentItem;
                masterConsignmentItems[0].ApplicableFreightRateServiceCharge = new Waybill.FreightRateServiceChargeType
                {
                    CategoryCode = new Waybill.CodeType { Value = "Q" },
                    ChargeableWeightMeasure = new Waybill.MeasureType
                    {
                        unitCodeSpecified = true,
                        Value = Convert.ToDecimal(master.PesoTotalBruto),
                        unitCode = pesoTotalUN
                    },
                    AppliedRate = master.ValorFretePP / Convert.ToDecimal(master.PesoTotalBruto),
                    AppliedAmount = new Waybill.AmountType
                    {
                        currencyIDSpecified = true,
                        currencyID = valorFCUN,
                        Value = master.ValorFreteFC
                    }
                };
                ratingType.IncludedMasterConsignmentItem = masterConsignmentItems;
                ratingList.Add(ratingType);
            }

            masterConsigment.ApplicableRating = ratingList.ToArray();

            #endregion

            #region MasterConsigment > ApplicableTotalRating

            masterConsigment.ApplicableTotalRating = new Waybill.TotalRatingType[1];

            masterConsigment.ApplicableTotalRating[0] = new Waybill.TotalRatingType();
            masterConsigment.ApplicableTotalRating[0].TypeCode = new Waybill.CodeType() { Value = "F" };
            List<Waybill.PrepaidCollectMonetarySummationType> listSumType = new List<Waybill.PrepaidCollectMonetarySummationType>();

            if (master.ValorFretePP > 0)
            {
                Waybill.PrepaidCollectMonetarySummationType sumPP = new Waybill.PrepaidCollectMonetarySummationType();
                sumPP.WeightChargeTotalAmount = new Waybill.AmountType
                {
                    currencyIDSpecified = true,
                    currencyID = valorPPUN,
                    Value = master.ValorFretePP
                };
                sumPP.GrandTotalAmount = new Waybill.AmountType()
                {
                    Value = master.ValorFretePP,
                    currencyIDSpecified = true,
                    currencyID = valorPPUN
                };
                sumPP.PrepaidIndicator = true;
                listSumType.Add(sumPP);
            };

            if (master.ValorFreteFC > 0)
            {
                Waybill.PrepaidCollectMonetarySummationType sumFC = new Waybill.PrepaidCollectMonetarySummationType();
                sumFC.WeightChargeTotalAmount = new Waybill.AmountType
                {
                    currencyIDSpecified = true,
                    currencyID = valorFCUN,
                    Value = master.ValorFreteFC
                };

                sumFC.GrandTotalAmount = new Waybill.AmountType()
                {
                    Value = master.ValorFreteFC,
                    currencyIDSpecified = true,
                    currencyID = valorFCUN
                };
                sumFC.PrepaidIndicator = false;
                listSumType.Add(sumFC);
            };
            masterConsigment.ApplicableTotalRating[0].ApplicablePrepaidCollectMonetarySummation = new Waybill.PrepaidCollectMonetarySummationType[listSumType.Count];
            masterConsigment.ApplicableTotalRating[0].ApplicablePrepaidCollectMonetarySummation = listSumType.ToArray();
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
            HouseWaybill.HouseWaybillType manhouse = new HouseWaybill.HouseWaybillType();

            Enum.TryParse(house.PesoTotalBrutoUN, out HouseWaybill.MeasurementUnitCommonCodeContentType pesoTotalUN);
            Enum.TryParse(house.ValorFretePPUN, out HouseWaybill.ISO3AlphaCurrencyCodeContentType valorPPUN);
            Enum.TryParse(house.ValorFreteFCUN, out HouseWaybill.ISO3AlphaCurrencyCodeContentType valorFCUN);
            Enum.TryParse(house.VolumeUN, out HouseWaybill.MeasurementUnitCommonCodeContentType volumeUN);

            #region MessageHeaderDocument
            manhouse.MessageHeaderDocument = new HouseWaybill.MessageHeaderDocumentType
            {
                ID = new HouseWaybill.IDType { Value = $"{ house.Numero }_{ DateTime.Now.ToString("ddMMyyyhhmmss") }" },
                Name = new HouseWaybill.TextType { Value = "House Waybill" },
                TypeCode = new HouseWaybill.DocumentCodeType { Value = HouseWaybill.DocumentNameCodeContentType.Item703 },
                IssueDateTime = house.DataEmissaoXML.Value,
                PurposeCode = new HouseWaybill.CodeType { Value = purposeCode.ToString() },
                VersionID = new HouseWaybill.IDType { Value = "3.00" },
                SenderParty = new HouseWaybill.SenderPartyType[2],
                RecipientParty = new HouseWaybill.RecipientPartyType[1],
            };
            manhouse.MessageHeaderDocument.SenderParty[0] = new HouseWaybill.SenderPartyType
            {
                PrimaryID = new HouseWaybill.IDType { schemeID = "C", Value = "HDQTTKE" }
            };
            manhouse.MessageHeaderDocument.SenderParty[1] = new HouseWaybill.SenderPartyType
            {
                PrimaryID = new HouseWaybill.IDType { schemeID = "P", Value = "HDQTTKE" }
            };
            manhouse.MessageHeaderDocument.RecipientParty[0] = new HouseWaybill.RecipientPartyType
            {
                PrimaryID = new HouseWaybill.IDType { schemeID = "C", Value = "BRCUSTOMS" }
            };
            #endregion

            #region BusinessHeaderDocument
            manhouse.BusinessHeaderDocument = new HouseWaybill.BusinessHeaderDocumentType
            {
                ID = new HouseWaybill.IDType { Value = house.Numero },
                IncludedHeaderNote = new HouseWaybill.HeaderNoteType[1],
                SignatoryConsignorAuthentication = new HouseWaybill.ConsignorAuthenticationType
                {
                    Signatory = new HouseWaybill.TextType { Value = house.SignatarioNome }
                },
                SignatoryCarrierAuthentication = new HouseWaybill.CarrierAuthenticationType
                {
                    ActualDateTime = DateTime.Now,
                    Signatory = new HouseWaybill.TextType { Value = "180020" },
                    IssueAuthenticationLocation = new HouseWaybill.AuthenticationLocationType
                    {
                        Name = new HouseWaybill.TextType { Value = house.AeroportoOrigemInfo.Codigo }
                    }
                }
            };
            manhouse.BusinessHeaderDocument.IncludedHeaderNote[0] = new HouseWaybill.HeaderNoteType
            {
                ContentCode = new HouseWaybill.CodeType { Value = "A" },
                Content = new HouseWaybill.TextType { Value = "As Agreed" }
            };
            manhouse.BusinessHeaderDocument.SignatoryConsignorAuthentication = new HouseWaybill.ConsignorAuthenticationType
            {
                Signatory = new HouseWaybill.TextType { Value = "SIGNATURE" }
            };
            #endregion

            #region MasterConsigment
            manhouse.MasterConsignment = new HouseWaybill.MasterConsignmentType
            {
                TotalPieceQuantity = new HouseWaybill.QuantityType { Value = house.TotalVolumes },
                TransportContractDocument = new HouseWaybill.TransportContractDocumentType
                {
                    ID = new HouseWaybill.IDType { Value = house.MasterNumeroXML }
                },
                OriginLocation = new HouseWaybill.OriginLocationType {
                    ID = new HouseWaybill.IDType { Value = house.AeroportoOrigemInfo.Codigo },
                    Name = new HouseWaybill.TextType { Value = house.AeroportoOrigemInfo.Nome }
                },
                FinalDestinationLocation = new HouseWaybill.FinalDestinationLocationType
                {
                    ID = new HouseWaybill.IDType { Value = house.AeroportoDestinoInfo.Codigo  },
                    Name = new HouseWaybill.TextType {  Value = house.AeroportoDestinoInfo.Nome }
                }
            };

            #region MasterConsigment -> IncludedHouseConsigmnet
            manhouse.MasterConsignment.IncludedHouseConsignment = new HouseWaybill.HouseConsignmentType
            {
                //InsuranceValueAmount = new HouseManifest.AmountType { currencyID = HouseManifest.ISO3AlphaCurrencyCodeContentType.USD, currencyIDSpecified = true, Value = 0 },
                TotalChargePrepaidIndicatorFlag = house.ValorFreteFC == 0 ? true : false,
                ValuationTotalChargeAmount = new HouseWaybill.AmountType { currencyID = HouseWaybill.ISO3AlphaCurrencyCodeContentType.USD, currencyIDSpecified = true, Value = 0 },
                TaxTotalChargeAmount = new HouseWaybill.AmountType { currencyID = HouseWaybill.ISO3AlphaCurrencyCodeContentType.USD, currencyIDSpecified = true, Value = 0 },
                WeightTotalChargeAmount = new HouseWaybill.AmountType { currencyID = HouseWaybill.ISO3AlphaCurrencyCodeContentType.USD, currencyIDSpecified = true, Value = 0 },
                //TotalDisbursementPrepaidIndicator = false,
                //AgentTotalDisbursementAmount = new HouseManifest.AmountType { currencyID = HouseManifest.ISO3AlphaCurrencyCodeContentType.USD, currencyIDSpecified = true, Value = 0},
                //CarrierTotalDisbursementAmount = new HouseManifest.AmountType { currencyID = HouseManifest.ISO3AlphaCurrencyCodeContentType.USD, currencyIDSpecified = true, Value = 0 },
                TotalPrepaidChargeAmount = new HouseWaybill.AmountType { currencyID = valorPPUN, currencyIDSpecified = true, Value = house.ValorFretePP },
                TotalCollectChargeAmount = new HouseWaybill.AmountType { currencyID = valorFCUN, currencyIDSpecified = true, Value = house.ValorFreteFC },
                IncludedTareGrossWeightMeasure = new HouseWaybill.MeasureType { unitCode = pesoTotalUN, unitCodeSpecified = true, Value = Convert.ToDecimal(house.PesoTotalBruto) },
                NilCarriageValueIndicator = false,
                NilCustomsValueIndicator = false,
                NilInsuranceValueIndicator = false,
                NilCarriageValueIndicatorSpecified = true,
                NilCustomsValueIndicatorSpecified = true,
                NilInsuranceValueIndicatorSpecified = true,
                ConsignmentItemQuantity = new HouseWaybill.QuantityType { Value = house.TotalVolumes },
                PackageQuantity = new HouseWaybill.QuantityType { Value = house.TotalVolumes },
                TotalPieceQuantity = new HouseWaybill.QuantityType { Value = house.TotalVolumes },
                SummaryDescription = new HouseWaybill.TextType { Value = house.DescricaoMercadoria },
                FreightRateTypeCode = new HouseWaybill.CodeType { Value = "RATE" },
                FreightForwarderParty = house.AgenteDeCargaNome != null ? new HouseWaybill.FreightForwarderPartyType
                {
                    Name = new HouseWaybill.TextType { Value = house.AgenteDeCargaInfo.Nome },
                    PostalStructuredAddress = new HouseWaybill.StructuredAddressType
                    {
                        PostcodeCode = new HouseWaybill.CodeType { Value = house.AgenteDeCargaInfo.Complemento },
                        StreetName = new HouseWaybill.TextType { Value = house.AgenteDeCargaInfo.Endereco },
                        CityName = new HouseWaybill.TextType { Value = house.AgenteDeCargaInfo.Cidade },
                        CountryID = new HouseWaybill.CountryIDType
                        {
                            Value = (HouseWaybill.ISOTwoletterCountryCodeIdentifierContentType)
                            Enum.Parse(typeof(HouseWaybill.ISOTwoletterCountryCodeIdentifierContentType), house.AgenteDeCargaInfo.Pais)
                        }
                    }
                }: null,
                ConsignorParty = new HouseWaybill.ConsignorPartyType
                {
                    Name = new HouseWaybill.TextType { Value = house.ExpedidorNome },
                    PostalStructuredAddress = new HouseWaybill.StructuredAddressType
                    {
                        PostcodeCode = new HouseWaybill.CodeType { Value = house.ExpedidorPostal },
                        StreetName = new HouseWaybill.TextType {  Value = house.ExpedidorEndereco },
                        CityName = new HouseWaybill.TextType { Value = house.ExpedidorCidade },
                        CountryID = new HouseWaybill.CountryIDType {  
                            Value = (HouseWaybill.ISOTwoletterCountryCodeIdentifierContentType) 
                            Enum.Parse(typeof(HouseWaybill.ISOTwoletterCountryCodeIdentifierContentType), house.ExpedidorPaisCodigo) }
                    }
                },
                ConsigneeParty = new HouseWaybill.ConsigneePartyType
                {
                    Name = new HouseWaybill.TextType { Value = house.ConsignatarioNome },
                    PostalStructuredAddress = new HouseWaybill.StructuredAddressType
                    {
                        PostcodeCode = new HouseWaybill.CodeType { Value = house.ConsignatarioPostal },
                        StreetName = new HouseWaybill.TextType { Value = house.ConsignatarioEndereco },
                        CityName = new HouseWaybill.TextType {  Value = house.ConsignatarioCidade },
                        CountryID = new HouseWaybill.CountryIDType
                        {
                            Value = (HouseWaybill.ISOTwoletterCountryCodeIdentifierContentType)
                            Enum.Parse(typeof(HouseWaybill.ISOTwoletterCountryCodeIdentifierContentType), house.ConsignatarioPaisCodigo)
                        }
                    }
                },
                OriginLocation = new HouseWaybill.OriginLocationType
                {
                    ID = new HouseWaybill.IDType { Value = house.AeroportoOrigemInfo.Codigo },
                    Name = new HouseWaybill.TextType {  Value = house.AeroportoOrigemInfo.Nome }
                },
                FinalDestinationLocation = new HouseWaybill.FinalDestinationLocationType
                {
                    ID = new HouseWaybill.IDType { Value = house.AeroportoDestinoInfo.Codigo },
                    Name = new HouseWaybill.TextType { Value = house.AeroportoDestinoInfo.Nome }
                },
                HandlingOSIInstructions =  new HouseWaybill.OSIInstructionsType[1],
                IncludedHouseConsignmentItem = new HouseWaybill.HouseConsignmentItemType[1],
                IncludedCustomsNote = new HouseWaybill.CustomsNoteType[2],
                //SpecifiedLogisticsTransportMovement = new HouseManifest.LogisticsTransportMovementType[1]
            };
            if (house.Volume != null)
                manhouse.MasterConsignment.IncludedHouseConsignment.GrossVolumeMeasure = new HouseWaybill.MeasureType
                {
                    unitCode = volumeUN,
                    unitCodeSpecified = house.VolumeUN != null,
                    Value = Convert.ToDecimal(house.Volume)
                };

            manhouse.MasterConsignment.IncludedHouseConsignment.HandlingOSIInstructions[0] = new HouseWaybill.OSIInstructionsType
            {
                Description = new HouseWaybill.TextType { Value = house.DescricaoMercadoria }
            };
            manhouse.MasterConsignment.IncludedHouseConsignment.IncludedHouseConsignmentItem[0] = new HouseWaybill.HouseConsignmentItemType
            {
                SequenceNumeric = 1,
                GrossWeightMeasure = new HouseWaybill.MeasureType
                {
                    Value = Convert.ToDecimal(house.PesoTotalBruto),
                    unitCode = (HouseWaybill.MeasurementUnitCommonCodeContentType)
                    Enum.Parse(typeof(HouseWaybill.MeasurementUnitCommonCodeContentType), house.PesoTotalBrutoUN),
                    unitCodeSpecified = true
                },
                TotalChargeAmount = new HouseWaybill.AmountType
                {
                    Value = house.ValorFretePP + house.ValorFreteFC,
                    currencyIDSpecified = true,
                    currencyID = (HouseWaybill.ISO3AlphaCurrencyCodeContentType)
                        Enum.Parse(typeof(HouseWaybill.ISO3AlphaCurrencyCodeContentType), house.ValorFretePPUN)
                },
                PieceQuantity = new HouseWaybill.QuantityType { Value = house.TotalVolumes },
                Information = new HouseWaybill.TextType { Value = "NDA" },
                NatureIdentificationTransportCargo = new HouseWaybill.TransportCargoType
                {
                    Identification = new HouseWaybill.TextType { Value = house.DescricaoMercadoria }
                },
                ApplicableFreightRateServiceCharge = new HouseWaybill.FreightRateServiceChargeType[1],
            };
            if(house.NCMLista != null)
            {
                var ncmArray = house.NCMLista.Split(",");
                var ncmArrayObject = from c in ncmArray
                                     select new HouseWaybill.CodeType
                                     {
                                         Value = c.Replace(".","")
                                     };

                manhouse.MasterConsignment.IncludedHouseConsignment.IncludedHouseConsignmentItem[0].TypeCode =
                    ncmArrayObject.ToArray();
            }
            manhouse.MasterConsignment.IncludedHouseConsignment.IncludedHouseConsignmentItem[0].ApplicableFreightRateServiceCharge[0]
                = new HouseWaybill.FreightRateServiceChargeType
                {
                    ChargeableWeightMeasure = new HouseWaybill.MeasureType
                    {
                        Value = Convert.ToDecimal(house.PesoTotalBruto),
                        unitCode = (HouseWaybill.MeasurementUnitCommonCodeContentType)
                            Enum.Parse(typeof(HouseWaybill.MeasurementUnitCommonCodeContentType), house.PesoTotalBrutoUN),
                        unitCodeSpecified = true
                    },
                    AppliedRate = (house.ValorFretePP + house.ValorFreteFC),
                    AppliedAmount = new HouseWaybill.AmountType { currencyID = valorPPUN , Value = (house.ValorFretePP + house.ValorFreteFC) }
                };
            manhouse.MasterConsignment.IncludedHouseConsignment.IncludedCustomsNote[0] = new HouseWaybill.CustomsNoteType
            {
                CountryID = new HouseWaybill.CountryIDType { Value = HouseWaybill.ISOTwoletterCountryCodeIdentifierContentType.BR },
                SubjectCode = new HouseWaybill.CodeType { Value = "CNE" },
                ContentCode = new HouseWaybill.CodeType { Value = "T" },
                Content = new HouseWaybill.TextType { Value = $"CNPJ{house.ConsignatarioCNPJ}" }
            };
            manhouse.MasterConsignment.IncludedHouseConsignment.IncludedCustomsNote[1] = new HouseWaybill.CustomsNoteType
            {
                CountryID = new HouseWaybill.CountryIDType { Value = HouseWaybill.ISOTwoletterCountryCodeIdentifierContentType.BR },
                SubjectCode = new HouseWaybill.CodeType { Value = "CCL" },
                ContentCode = new HouseWaybill.CodeType { Value = "M" },
                Content = new HouseWaybill.TextType { Value = $"CUSTOMSWAREHOUSE{house.CodigoRecintoAduaneiro.ToString()}" }
            };
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
            return SerializeFromStream<HouseWaybill.HouseWaybillType>(manhouse, ns);
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
}
