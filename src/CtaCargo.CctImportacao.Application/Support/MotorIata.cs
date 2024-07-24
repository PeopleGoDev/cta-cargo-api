using CtaCargo.CctImportacao.Application.Support.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Enums;
using CtaCargo.CctImportacao.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Flight = CtaCargo.CctImportacao.Domain.Model.Iata.FlightManifest;
using Waybill = CtaCargo.CctImportacao.Domain.Model.Iata.WaybillManifest;

namespace CtaCargo.CctImportacao.Application.Support;

public class MotorIata : IMotorIata
{
    public string GenFlightManifest(Voo voo, int? trechoId = null, DateTime? actualDateTime = null, bool isScheduled = false)
    {
        try
        {
            Flight.FlightManifestType manvoo = new();

            // MessageHeaderDocument
            Flight.MessageHeaderDocumentType headerdoc = new()
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
            Flight.BusinessHeaderDocumentType busdoc = new()
            {
                ID = new Flight.IDType { Value = $"{voo.Numero}{((DateTime)voo.DataHoraSaidaEstimada).ToString("yyyyMMdd")}{voo.PortoIataOrigemInfo.Codigo}" },
                IncludedHeaderNote = new Flight.HeaderNoteType[1]
            };
            busdoc.IncludedHeaderNote[0] = new Flight.HeaderNoteType
            {
                Content = new Flight.TextType { Value = "FlightManifest" }
            };
            manvoo.BusinessHeaderDocument = busdoc;

            // LogisticsTransportMovement
            Flight.LogisticsTransportMovementType logmov = new()
            {
                StageCode = new Flight.CodeType { Value = "Main-Carriage" },
                ModeCode = new Flight.TransportModeCodeType { Value = Flight.TransportModeCodeContentType.Item4 },
                Mode = new Flight.TextType { Value = "AIR TRANSPORT" },
                ID = new Flight.IDType { Value = voo.Numero },
                SequenceNumeric = 1
            };
            if (voo.PrefixoAeronave != null)
            {
                logmov.UsedLogisticsTransportMeans = new Flight.LogisticsTransportMeansType
                {
                    Name = new Flight.TextType { Value = voo.PrefixoAeronave }
                };
            };

            double totalWeightGross = voo.Masters.Sum(x => x.PesoTotalBruto);
            int totalPecas = voo.Masters.Sum(x => x.TotalPecas);
            int totalPacotes = voo.Masters.Count;
            logmov.TotalGrossWeightMeasure = new Flight.MeasureType
            {
                unitCode = Flight.MeasurementUnitCommonCodeContentType.KGM,
                unitCodeSpecified = true,
                Value = Convert.ToDecimal(totalWeightGross)
            };
            logmov.TotalPackageQuantity = new Flight.QuantityType { Value = totalPacotes };
            logmov.TotalPieceQuantity = new Flight.QuantityType { Value = totalPecas };
            logmov.DepartureEvent = new Flight.DepartureEventType();

            if (isScheduled)
            {
                logmov.DepartureEvent.DepartureOccurrenceDateTime = (DateTime)voo.DataHoraSaidaEstimada;
                logmov.DepartureEvent.DepartureDateTimeTypeCode = new Flight.CodeType { Value = "S" };
            }
            else
            {
                if (voo.DataHoraSaidaReal != null)
                {
                    logmov.DepartureEvent.DepartureOccurrenceDateTime = (DateTime)voo.DataHoraSaidaReal;
                    logmov.DepartureEvent.DepartureDateTimeTypeCode = new Flight.CodeType { Value = "A" };
                }
                else
                {
                    logmov.DepartureEvent.DepartureOccurrenceDateTime = (DateTime)voo.DataHoraSaidaEstimada;
                    logmov.DepartureEvent.DepartureDateTimeTypeCode = new Flight.CodeType { Value = "S" };
                }
            }

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
                }
                if (trechoId == trecho.Id && actualDateTime != null)
                {
                    arrevent.DepartureOccurrenceDateTime = actualDateTime;
                    arrevent.DepartureDateTimeTypeCode = new Flight.CodeType { Value = "A" };
                    arrevent.DepartureOccurrenceDateTimeSpecified = true;
                }
                else
                {
                    if (trecho.DataHoraSaidaEstimada != null)
                    {
                        arrevent.DepartureOccurrenceDateTime = trecho.DataHoraSaidaEstimada;
                        arrevent.DepartureDateTimeTypeCode = new() { Value = "S" };
                        arrevent.DepartureOccurrenceDateTimeSpecified = true;
                    }
                }
                arrevent.OccurrenceArrivalLocation = new Flight.ArrivalLocationType
                {
                    ID = new() { Value = trecho.PortoIataDestinoInfo.Codigo },
                    Name = new() { Value = trecho.PortoIataDestinoInfo.Nome },
                    TypeCode = new() { Value = "Airport" }
                };

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

                        Flight.TransportCargoType transpuld = new()
                        {
                            TypeCode = new() { Value = "ULD" }
                        };

                        _ = Enum.TryParse("KGM", out Flight.MeasurementUnitCommonCodeContentType pesoUN);

                        Flight.UnitLoadTransportEquipmentType unitequip = new Flight.UnitLoadTransportEquipmentType
                        {
                            ID = new()
                            {
                                Value = item.ULDId
                            },
                            CharacteristicCode = new()
                            {
                                Value = item.ULDCaracteristicaCodigo
                            },
                            OperatingParty = new()
                            {
                                PrimaryID = new() { Value = item.ULDIdPrimario }
                            },
                            PieceQuantity = new() 
                            { 
                                Value = Convert.ToDecimal(item.Pecas) 
                            },
                            GrossWeightMeasure = new()
                            {
                                Value = Convert.ToDecimal(item.Peso),
                                unitCodeSpecified = true,
                                unitCode = pesoUN
                            }
                        };

                        transpuld.UtilizedUnitLoadTransportEquipment = unitequip;

                        // Monta a lista de Master
                        List<Flight.MasterConsignmentType> masterConsigments = new();

                        foreach (UldMaster uldMaster in ulds)
                        {
                            Flight.MasterConsignmentType masterC = new();

                            _ = Enum.TryParse(uldMaster.PesoUN, out Flight.MeasurementUnitCommonCodeContentType pesoTotalUN);

                            var isNonIataAwb = IsAwbNonIata(uldMaster.MasterNumero);

                            masterC.GrossWeightMeasure = new()
                            {
                                Value = Convert.ToDecimal(uldMaster.Peso),
                                unitCodeSpecified = true,
                                unitCode = pesoTotalUN
                            };

                            masterC.PackageQuantity = new() { Value = 1 };
                            masterC.TotalPieceQuantity = new() { Value = Convert.ToDecimal(uldMaster.QuantidadePecas) };
                            masterC.SummaryDescription = new() { Value = uldMaster.SummaryDescription };
                            masterC.TransportSplitDescription = new() { Value = uldMaster.TotalParcial };
                            masterC.TransportContractDocument = new()
                            {
                                ID = new Flight.IDType
                                {
                                    Value = isNonIataAwb ? uldMaster.MasterNumero : uldMaster.MasterNumero.Insert(3, "-")
                                }
                            };

                            masterC.OriginLocation = new()
                            {
                                ID = new Flight.IDType { Value = uldMaster.PortOfOrign },
                            };

                            masterC.FinalDestinationLocation = new()
                            {
                                ID = new Flight.IDType { Value = uldMaster.PortOfDestiny },
                            };

                            if (isNonIataAwb)
                            {
                                masterC.IncludedCustomsNote = new Flight.CustomsNoteType[1];
                                masterC.IncludedCustomsNote[0] = new()
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

                if (blk.Any())
                {
                    Flight.TransportCargoType transpuld = new()
                    {
                        TypeCode = new Flight.CodeType { Value = "BLK" }
                    };

                    List<Flight.MasterConsignmentType> masterConsigments = new();

                    foreach (UldMaster uldMaster in blk)
                    {
                        Flight.MasterConsignmentType masterC = new();

                        _ = Enum.TryParse(uldMaster.PesoUN, out Flight.MeasurementUnitCommonCodeContentType pesoTotalUN);

                        var isNonIataAwb = IsAwbNonIata(uldMaster.MasterNumero);

                        masterC.GrossWeightMeasure = new()
                        {
                            Value = Convert.ToDecimal(uldMaster.Peso),
                            unitCodeSpecified = true,
                            unitCode = pesoTotalUN
                        };

                        masterC.PackageQuantity = new() { Value = 1 };
                        masterC.TotalPieceQuantity = new() { Value = Convert.ToDecimal(uldMaster.QuantidadePecas) };
                        masterC.SummaryDescription = new() { Value = uldMaster.SummaryDescription };
                        masterC.TransportSplitDescription = new() { Value = uldMaster.TotalParcial };
                        masterC.TransportContractDocument = new()
                        {
                            ID = new Flight.IDType { Value = isNonIataAwb ? uldMaster.MasterNumero : uldMaster.MasterNumero.Insert(3, "-") }
                        };

                        masterC.OriginLocation = new()
                        {
                            ID = new Flight.IDType { Value = uldMaster.PortOfOrign },
                        };

                        masterC.FinalDestinationLocation = new()
                        {
                            ID = new Flight.IDType { Value = uldMaster.PortOfDestiny },
                        };

                        if (isNonIataAwb)
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

                if (blk.Count() == 0 && uldsMaster.Count() == 0)
                {
                    Flight.TransportCargoType transpuld = new()
                    {
                        TypeCode = new Flight.CodeType { Value = "NIL" }
                    };

                    transportes.Add(transpuld);
                }

                arrevent.AssociatedTransportCargo = transportes.ToArray();

                arrivalEvents.Add(arrevent);
            }

            manvoo.ArrivalEvent = arrivalEvents.ToArray();

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "iata:datamodel:3");
            ns.Add("ns2", "iata:flightmanifest:1");

            return SerializeFromStream(manvoo, ns);
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Não foi possivel gerar o arquivo XML do voo ({ex.Message}).");
        }
    }

    public string GenMasterManifest(Master master, IataXmlPurposeCode purposeCode)
    {
        Waybill.WaybillType manmaster = new();

        #region MessageHeaderDocument
        if (master.XmlIssueDate == null)
            master.XmlIssueDate = DateTime.UtcNow;

        Waybill.MessageHeaderDocumentType headerDocument = new()
        {
            ID = new Waybill.IDType() { Value = $"{master.Numero}_{master.DataEmissaoXML.Value.ToString("ddMMyyyyhhmmss")}" },
            Name = new Waybill.TextType() { Value = "MASTER AIR WAYBILL" }
        };

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
        Waybill.MasterConsignmentType masterConsigment = new();
        masterConsigment.TotalPieceQuantity = new Waybill.QuantityType() { Value = master.TotalPecas };
        masterConsigment.IncludedTareGrossWeightMeasure = new Waybill.MeasureType()
        {
            Value = Convert.ToDecimal(master.PesoTotalBruto),
            unitCodeSpecified = master.PesoTotalBrutoUN == null ? false : true,
            unitCode = pesoTotalUN
        };
        masterConsigment.ProductID = new Waybill.IDType { Value = "GEN" };
        if (master.ValorFretePP > 0)
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
                CountryID = new Waybill.CountryIDType { Value = codigoPaisExpedidor },
                PostOfficeBox = new Waybill.TextType { Value = master.ExpedidorPostal }
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
                CountryID = new Waybill.CountryIDType { Value = codigoPaisConsignatario },
                PostOfficeBox = new Waybill.TextType { Value = master.ConsignatarioPostal }
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

        if (master.VooInfo.Trechos?.Count > 0)
        {
            var firstLag = (master.VooInfo.Trechos.FirstOrDefault(x => x.DataExclusao == null));

            if (firstLag is not null)
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
                ID = new Waybill.IDType { Value = master.VooInfo.AeroportoOrigemCodigo },
                Name = new Waybill.TextType { Value = master.VooInfo.PortoIataOrigemInfo?.Nome ?? null },
                TypeCode = new Waybill.CodeType { Value = "Airport" }
            }
        };
        #endregion

        #region MasterConsigment > HandlingSPHInstructions
        if (master.NaturezaCarga != null)
        {
            var naturezaCargas = master.NaturezaCarga.Split(",");
            List<Waybill.SPHInstructionsType> instructions = new List<Waybill.SPHInstructionsType>();

            foreach (var nat in naturezaCargas)
            {
                instructions.Add(new Waybill.SPHInstructionsType
                {
                    DescriptionCode = new Waybill.CodeType { Value = nat }
                });
            }
            masterConsigment.HandlingSPHInstructions = instructions.ToArray();
        }
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
        }
        else
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

        if (master.ValorFretePP > 0)
        {
            sumArray.Add(new Waybill.PrepaidCollectMonetarySummationType
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

        if (master.ValorFreteFC > 0)
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
                PrepaidIndicator = false
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

    private bool IsAwbNonIata(string e)
    {
        if (e.Length != 11)
            return true;

        if (!long.TryParse(e, out _))
            return true;

        var digitos7 = Convert.ToInt32(e.Substring(3, 7));
        var digito = Convert.ToInt32(e.Substring(10, 1));
        var digitoesperado = digitos7 % 7;
        if (digito == digitoesperado)
            return false;
        return true;
    }
}
