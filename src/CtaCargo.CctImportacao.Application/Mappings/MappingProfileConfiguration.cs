using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Domain.Entities;

namespace CtaCargo.CctImportacao.Application.Mappings;

public class MappingProfileConfiguration : Profile
{
    
    public MappingProfileConfiguration()
    {
        #region Login
        // Mapeamento de Usuario e Informação de Login
        CreateMap<Usuario, UsuarioInfoResponse>()
            .ForMember(dest => dest.UsuarioId, m => m.MapFrom(a => a.Id))
            .ForMember(dest => dest.EmpresaId, m => m.MapFrom(a => a.EmpresaId))
            .ForMember(dest => dest.Nome, m => m.MapFrom(a => a.Nome))
            .ForMember(dest => dest.Sobrenome, m => m.MapFrom(a => a.Sobrenome))
            .ForMember(dest => dest.EmpresaNome, m => m.MapFrom(a => a.Empresa.RazaoSocial))
            .ForMember(dest => dest.EmpresaLogoUrl, m => m.MapFrom(a => a.Empresa.LogoUrl))
            .ForMember(dest => dest.Email, m => m.MapFrom(a => a.EMail))
            .ForMember(dest => dest.AlteraCompanhia, m => m.MapFrom(a => a.AlteraCia))
            .ForMember(dest => dest.AcessoUsuarios, m => m.MapFrom(a => a.AcessaUsuarios))
            .ForMember(dest => dest.AcessoClientes, m => m.MapFrom(a => a.AcessaClientes))
            .ForMember(dest => dest.AcessoCompanhias, m => m.MapFrom(a => a.AcessaCiasAereas))
            .ForMember(dest => dest.DataAlteracao, m => m.MapFrom(a => a.ModifiedDateTimeUtc))
            .ForMember(dest => dest.CompanhiaId, m => m.MapFrom(a => a.CiaAereaId))
            .ForMember(dest => dest.CompanhiaNome, m => m.MapFrom(a => a.CiaAereaNome));
        #endregion

        #region Companhia Aerea
        // Mapeamentos de Companhias Aéreas
        CreateMap<CiaAerea, CiaAereaResponseDto>()
            .ForMember(dest => dest.CiaId, m => m.MapFrom(a => a.Id))
            .ForMember(dest => dest.Nome, m => m.MapFrom(a => a.Nome))
            .ForMember(dest => dest.Endereco1, m => m.MapFrom(a => a.Endereco))
            .ForMember(dest => dest.Endereco2, m => m.MapFrom(a => a.Complemento))
            .ForMember(dest => dest.Cidade, m => m.MapFrom(a => a.Cidade))
            .ForMember(dest => dest.Estado, m => m.MapFrom(a => a.UF))
            .ForMember(dest => dest.Pais, m => m.MapFrom(a => a.Pais))
            .ForMember(dest => dest.CNPJ, m => m.MapFrom(a => a.CNPJ))
            .ForMember(dest => dest.Numero, m => m.MapFrom(a => a.Numero))
            .ForMember(dest => dest.ArquivoCertificado, m => m.MapFrom(a => a.CertificadoDigital.Arquivo))
            .ForMember(dest => dest.DataExpiracaoCertificado, m => m.MapFrom(a => a.CertificadoDigital.DataVencimento));

        CreateMap<CiaAereaInsertRequest, CiaAerea>()
            .ForMember(dest => dest.EmpresaId, m => m.MapFrom(a => a.EmpresaId))
            .ForMember(dest => dest.Nome, m => m.MapFrom(a => a.Nome))
            .ForMember(dest => dest.Endereco, m => m.MapFrom(a => a.Endereco1))
            .ForMember(dest => dest.Complemento, m => m.MapFrom(a => a.Endereco2))
            .ForMember(dest => dest.Cidade, m => m.MapFrom(a => a.Cidade))
            .ForMember(dest => dest.UF, m => m.MapFrom(a => a.Estado))
            .ForMember(dest => dest.Pais, m => m.MapFrom(a => a.Pais))
            .ForMember(dest => dest.CNPJ, m => m.MapFrom(a => a.CNPJ))
            .ForMember(dest => dest.Numero, m => m.MapFrom(a => a.Numero));

        CreateMap<CiaAereaUpdateRequest, CiaAerea>()
            .ForMember(dest => dest.Id, m => m.MapFrom(a => a.CiaId))
            .ForMember(dest => dest.Nome, m => m.MapFrom(a => a.Nome))
            .ForMember(dest => dest.Endereco, m => m.MapFrom(a => a.Endereco1))
            .ForMember(dest => dest.Complemento, m => m.MapFrom(a => a.Endereco2))
            .ForMember(dest => dest.Cidade, m => m.MapFrom(a => a.Cidade))
            .ForMember(dest => dest.UF, m => m.MapFrom(a => a.Estado))
            .ForMember(dest => dest.Pais, m => m.MapFrom(a => a.Pais))
            .ForMember(dest => dest.CNPJ, m => m.MapFrom(a => a.CNPJ))
            .ForMember(dest => dest.Numero, m => m.MapFrom(a => a.Numero));

        CreateMap<CiaAerea, CiaAreaListaSimplesResponse>()
            .ForMember(dest => dest.CiaId, m => m.MapFrom(a => a.Id))
            .ForMember(dest => dest.Nome, m => m.MapFrom(a => a.Nome));
        #endregion

        #region Agente de Carga
        // Mapeamentos de Agentes de Carga
        CreateMap<AgenteDeCarga, AgenteDeCargaResponseDto>()
            .ForMember(dest => dest.AgenteDeCargaId, m => m.MapFrom(a => a.Id))
            .ForMember(dest => dest.Nome, m => m.MapFrom(a => a.Nome))
            .ForMember(dest => dest.Endereco1, m => m.MapFrom(a => a.Endereco))
            .ForMember(dest => dest.Endereco2, m => m.MapFrom(a => a.Complemento))
            .ForMember(dest => dest.Cidade, m => m.MapFrom(a => a.Cidade))
            .ForMember(dest => dest.Estado, m => m.MapFrom(a => a.UF))
            .ForMember(dest => dest.Pais, m => m.MapFrom(a => a.Pais))
            .ForMember(dest => dest.CNPJ, m => m.MapFrom(a => a.CNPJ))
            .ForMember(dest => dest.Numero, m => m.MapFrom(a => a.Numero))
            .ForMember(dest => dest.ArquivoCertificado, m => m.MapFrom(a => a.CertificadoDigital.Arquivo))
            .ForMember(dest => dest.DataExpiracaoCertificado, m => m.MapFrom(a => a.CertificadoDigital.DataVencimento));

        CreateMap<AgenteDeCargaInsertRequest, AgenteDeCarga>()
            .ForMember(dest => dest.EmpresaId, m => m.MapFrom(a => a.EmpresaId))
            .ForMember(dest => dest.Nome, m => m.MapFrom(a => a.Nome))
            .ForMember(dest => dest.Endereco, m => m.MapFrom(a => a.Endereco1))
            .ForMember(dest => dest.Complemento, m => m.MapFrom(a => a.Endereco2))
            .ForMember(dest => dest.Cidade, m => m.MapFrom(a => a.Cidade))
            .ForMember(dest => dest.UF, m => m.MapFrom(a => a.Estado))
            .ForMember(dest => dest.Pais, m => m.MapFrom(a => a.Pais))
            .ForMember(dest => dest.CNPJ, m => m.MapFrom(a => a.CNPJ))
            .ForMember(dest => dest.Numero, m => m.MapFrom(a => a.Numero));

        CreateMap<AgenteDeCargaUpdateRequest, AgenteDeCarga>()
            .ForMember(dest => dest.Id, m => m.MapFrom(a => a.AgenteDeCargaId))
            .ForMember(dest => dest.Nome, m => m.MapFrom(a => a.Nome))
            .ForMember(dest => dest.Endereco, m => m.MapFrom(a => a.Endereco1))
            .ForMember(dest => dest.Complemento, m => m.MapFrom(a => a.Endereco2))
            .ForMember(dest => dest.Cidade, m => m.MapFrom(a => a.Cidade))
            .ForMember(dest => dest.UF, m => m.MapFrom(a => a.Estado))
            .ForMember(dest => dest.Pais, m => m.MapFrom(a => a.Pais))
            .ForMember(dest => dest.CNPJ, m => m.MapFrom(a => a.CNPJ))
            .ForMember(dest => dest.Numero, m => m.MapFrom(a => a.Numero));

        CreateMap<AgenteDeCarga, AgenteDeCargaListaSimplesResponse>()
            .ForMember(dest => dest.AgenteDeCargaId, m => m.MapFrom(a => a.Id))
            .ForMember(dest => dest.Nome, m => m.MapFrom(a => a.Nome));
        #endregion

        #region Usuario
        // Mapeamentos de Usuários
        CreateMap<Usuario, UsuarioResponseDto>()
            .ForMember(dest => dest.UsuarioId, m => m.MapFrom(a => a.Id))
            .ForMember(dest => dest.Nome, m => m.MapFrom(a => a.Nome))
            .ForMember(dest => dest.Sobrenome, m => m.MapFrom(a => a.Sobrenome))
            .ForMember(dest => dest.Email, m => m.MapFrom(a => a.EMail))
            .ForMember(dest => dest.CompanhiaId, m => m.MapFrom(a => a.CiaAereaId))
            .ForMember(dest => dest.CompanhiaNome, m => m.MapFrom(a => a.CiaAereaNome))
            .ForMember(dest => dest.AlteraCompanhia, m => m.MapFrom(a => a.AlteraCia))
            .ForMember(dest => dest.AcessoCompanhias, m => m.MapFrom(a => a.AcessaCiasAereas))
            .ForMember(dest => dest.AcessoUsuarios, m => m.MapFrom(a => a.AcessaUsuarios))
            .ForMember(dest => dest.AcessoClientes, m => m.MapFrom(a => a.AcessaClientes))
            .ForMember(dest => dest.DataCriacao, m => m.MapFrom(a => a.CreatedDateTimeUtc))
            .ForMember(dest => dest.CertificadoDigitalId, m => m.MapFrom(a => a.CertificadoId));

        CreateMap<UsuarioInsertRequest, Usuario>()
            .ForMember(dest => dest.EmpresaId, m => m.MapFrom(a => a.EmpresaId))
            .ForMember(dest => dest.Nome, m => m.MapFrom(a => a.Nome))
            .ForMember(dest => dest.Sobrenome, m => m.MapFrom(a => a.Sobrenome))
            .ForMember(dest => dest.EMail, m => m.MapFrom(a => a.Email))
            .ForMember(dest => dest.CiaAereaId, m => m.MapFrom(a => a.CompanhiaId))
            .ForMember(dest => dest.AlteraCia, m => m.MapFrom(a => a.AlteraCompanhia))
            .ForMember(dest => dest.AcessaCiasAereas, m => m.MapFrom(a => a.AcessoCompanhias))
            .ForMember(dest => dest.AcessaClientes, m => m.MapFrom(a => a.AcessoClientes))
            .ForMember(dest => dest.AcessaUsuarios, m => m.MapFrom(a => a.AcessoUsuarios))
            .ForMember(dest => dest.CriadoPeloId, m => m.MapFrom(a => a.UsuarioInsercaoId))
            .ForMember(dest => dest.CertificadoId, m => m.MapFrom(a => a.CertificadoDigitalId));

        CreateMap<UsuarioUpdateRequest, Usuario>()
            .ForMember(dest => dest.Id, m => m.MapFrom(a => a.UsuarioId))
            .ForMember(dest => dest.Nome, m => m.MapFrom(a => a.Nome))
            .ForMember(dest => dest.Sobrenome, m => m.MapFrom(a => a.Sobrenome))
            .ForMember(dest => dest.CiaAereaId, m => m.MapFrom(a => a.CompanhiaId))
            .ForMember(dest => dest.AlteraCia, m => m.MapFrom(a => a.AlteraCompanhia))
            .ForMember(dest => dest.AcessaCiasAereas, m => m.MapFrom(a => a.AcessoCompanhias))
            .ForMember(dest => dest.AcessaClientes, m => m.MapFrom(a => a.AcessoClientes))
            .ForMember(dest => dest.AcessaUsuarios, m => m.MapFrom(a => a.AcessoUsuarios))
            .ForMember(dest => dest.Bloqueado, m => m.MapFrom(a => a.Bloqueado))
            .ForMember(dest => dest.ModificadoPeloId, m => m.MapFrom(a => a.UsuarioModificadorId))
            .ForMember(dest => dest.CertificadoId, m => m.MapFrom(a => a.CertificadoDigitalId));
        #endregion

        #region voo
        // Mapeamentos de Voo
        CreateMap<VooListaQuery, VooListaResponseDto>()
            .ForMember(dest => dest.SituacaoVoo, m => m.MapFrom(a => a.SituacaoVoo))
            .ForMember(dest => dest.CiaAereaNome, m => m.MapFrom(a => a.CiaAereaNome));
        #endregion

        #region Porto Iata
        CreateMap<PortoIata, PortoIataResponseDto>()
            .ForMember(dest => dest.PortoId, m => m.MapFrom(a => a.Id))
            .ForMember(dest => dest.Codigo, m => m.MapFrom(a => a.Codigo))
            .ForMember(dest => dest.CountryCode, m => m.MapFrom(a => a.SiglaPais))
            .ForMember(dest => dest.Nome, m => m.MapFrom(a => a.Nome));

        CreateMap<PortoIataInsertRequestDto, PortoIata>()
            .ForMember(dest => dest.Codigo, m => m.MapFrom(a => a.Codigo))
            .ForMember(dest => dest.Nome, m => m.MapFrom(a => a.Nome))
            .ForMember(dest => dest.SiglaPais, m => m.MapFrom(a => a.CountryCode));

        CreateMap<PortoIataUpdateRequestDto, PortoIata>()
            .ForMember(dest => dest.Nome, m => m.MapFrom(a => a.Nome))
            .ForMember(dest => dest.SiglaPais, m => m.MapFrom(a => a.CountryCode));
        #endregion

        #region Natureza Carga
        CreateMap<NaturezaCarga, NaturezaCargaResponseDto>()
            .ForMember(dest => dest.NaturezaCargaId, m => m.MapFrom(a => a.Id))
            .ForMember(dest => dest.Codigo, m => m.MapFrom(a => a.Codigo))
            .ForMember(dest => dest.Descricao, m => m.MapFrom(a => a.Descricao));

        CreateMap<NaturezaCargaInsertRequestDto, NaturezaCarga>()
            .ForMember(dest => dest.Codigo, m => m.MapFrom(a => a.Codigo))
            .ForMember(dest => dest.Descricao, m => m.MapFrom(a => a.Descricao))
            .ForMember(dest => dest.CriadoPeloId, m => m.MapFrom(a => a.UsuarioInsercaoId))
            .ForMember(dest => dest.EmpresaId, m => m.MapFrom(a => a.EmpresaId));

        CreateMap<NaturezaCargaUpdateRequestDto, NaturezaCarga>()
            .ForMember(dest => dest.Descricao, m => m.MapFrom(a => a.Descricao))
            .ForMember(dest => dest.ModificadoPeloId, m => m.MapFrom(a => a.UsuarioModificadorId));
        #endregion

        #region Master
        var configuration = new MapperConfiguration(c =>
        {
            c.AllowNullCollections = true;
        });

        CreateMap<Master, MasterResponseDto>()
        .ForMember(dest => dest.MasterId, m => m.MapFrom(a => a.Id))
        .ForMember(dest => dest.StatusId, m => m.MapFrom(a => a.StatusId))
        .ForMember(dest => dest.Numero, m => m.MapFrom(a => a.Numero))
        .ForMember(dest => dest.PesoTotalBruto, m => m.MapFrom(a => a.PesoTotalBruto))
        .ForMember(dest => dest.PesoTotalBrutoUN, m => m.MapFrom(a => a.PesoTotalBrutoUN))
        .ForMember(dest => dest.TotalParcial, m => m.MapFrom(a => a.TotalParcial))
        .ForMember(dest => dest.TotalPecas, m => m.MapFrom(a => a.TotalPecas))
        .ForMember(dest => dest.ValorFOB, m => m.MapFrom(a => a.ValorFOB))
        .ForMember(dest => dest.ValorFOBUN, m => m.MapFrom(a => a.ValorFOBUN))
        .ForMember(dest => dest.ValorFretePP, m => m.MapFrom(a => a.ValorFretePP))
        .ForMember(dest => dest.ValorFretePPUN, m => m.MapFrom(a => a.ValorFretePPUN))
        .ForMember(dest => dest.ValorFreteFC, m => m.MapFrom(a => a.ValorFreteFC))
        .ForMember(dest => dest.ValorFreteFCUN, m => m.MapFrom(a => a.ValorFreteFCUN))
        .ForMember(dest => dest.IndicadorMadeiraMacica, m => m.MapFrom(a => a.IndicadorMadeiraMacica))
        .ForMember(dest => dest.IndicadorNaoDesunitizacao, m => m.MapFrom(a => a.IndicadorNaoDesunitizacao))
        .ForMember(dest => dest.IndicadorAwbNaoIata, m => m.MapFrom(a => a.IndicadorAwbNaoIata))
        .ForMember(dest => dest.DescricaoMercadoria, m => m.MapFrom(a => a.DescricaoMercadoria))
        .ForMember(dest => dest.CodigoRecintoAduaneiro, m => m.MapFrom(a => a.CodigoRecintoAduaneiro))
        .ForMember(dest => dest.RUC, m => m.MapFrom(a => a.RUC))
        .ForMember(dest => dest.RemetenteNome, m => m.MapFrom(a => a.ExpedidorNome))
        .ForMember(dest => dest.RemetenteEndereco, m => m.MapFrom(a => a.ExpedidorEndereco))
        .ForMember(dest => dest.RemetentePostal, m => m.MapFrom(a => a.ExpedidorPostal))
        .ForMember(dest => dest.RemetenteCidade, m => m.MapFrom(a => a.ExpedidorCidade))
        .ForMember(dest => dest.RemetentePaisCodigo, m => m.MapFrom(a => a.ExpedidorPaisCodigo))
        .ForMember(dest => dest.RemetenteSubdivisao, m => m.MapFrom(a => a.ExpedidorSubdivisao))
        .ForMember(dest => dest.ConsignatarioNome, m => m.MapFrom(a => a.ConsignatarioNome))
        .ForMember(dest => dest.ConsignatarioEndereco, m => m.MapFrom(a => a.ConsignatarioEndereco))
        .ForMember(dest => dest.ConsignatarioPostal, m => m.MapFrom(a => a.ConsignatarioPostal))
        .ForMember(dest => dest.ConsignatarioCidade, m => m.MapFrom(a => a.ConsignatarioCidade))
        .ForMember(dest => dest.ConsignatarioPaisCodigo, m => m.MapFrom(a => a.ConsignatarioPaisCodigo))
        .ForMember(dest => dest.ConsignatarioSubdivisao, m => m.MapFrom(a => a.ConsignatarioSubdivisao))
        .ForMember(dest => dest.ConsignatarioCNPJ, m => m.MapFrom(a => a.ConsignatarioCNPJ))
        .ForMember(dest => dest.AeroportoOrigemCodigo, m => m.MapFrom(a => a.AeroportoOrigemCodigo))
        .ForMember(dest => dest.AeroportoDestinoCodigo, m => m.MapFrom(a => a.AeroportoDestinoCodigo))
        .ForMember(dest => dest.NumeroVooXML, m => m.MapFrom(a => a.VooNumeroXML))
        .ForMember(dest => dest.DataEmissaoXML, m => m.MapFrom(a => a.DataEmissaoXML))
        .ForMember(dest => dest.NCMLista, m => m.MapFrom(a => a.GetNCMLista()))
        .ForMember(dest => dest.SituacaoRFB, m => m.MapFrom(a => a.SituacaoRFBId))
        .ForMember(dest => dest.ProtocoloRFB, m => m.MapFrom(a => a.ProtocoloRFB))
        .ForMember(dest => dest.CodigoErroRFB, m => m.MapFrom(a => a.CodigoErroRFB))
        .ForMember(dest => dest.DescricoErroRFB, m => m.MapFrom(a => a.DescricaoErroRFB))
        .ForMember(dest => dest.DataProtocoloRFB, m => m.MapFrom(a => a.DataProtocoloRFB))
        .ForMember(dest => dest.UsuarioCriacao, m => m.MapFrom(a => a.UsuarioCriacaoInfo.Nome))
        .ForMember(dest => dest.DataCriacao, m => m.MapFrom(a => a.CreatedDateTimeUtc))
        .ForMember(dest => dest.ConsolidadoDireto, m => m.MapFrom(a => a.CodigoConteudo))
        .ForMember(dest => dest.NaturezaCarga, m => m.MapFrom(a => a.GetNaturezaCargaLista()))
        .ForMember(dest => dest.StatusVoo, m => m.MapFrom(a => a.VooInfo.StatusId))
        .ForMember(dest => dest.Reenviar, m => m.MapFrom(a => a.Reenviar))
        .ForMember(dest => dest.Erros, m => m.MapFrom(a => a.ErrosMaster))
        .ForMember(dest => dest.Volume, m => m.MapFrom(a => a.Volume))
        .ForMember(dest => dest.VolumeUN, m => m.MapFrom(a => a.VolumeUN));

        CreateMap<ErroMaster, MasterErroDto>()
            .ForMember(dest => dest.Erro, m => m.MapFrom(a => a.Erro));

        CreateMap<MasterInsertRequestDto, Master>()
            .ForMember(dest => dest.VooId, m => m.MapFrom(a => a.VooId))
            .ForMember(dest => dest.Numero, m => m.MapFrom(a => a.Numero))
            .ForMember(dest => dest.PesoTotalBruto, m => m.MapFrom(a => a.PesoTotalBruto))
            .ForMember(dest => dest.PesoTotalBrutoUN, m => m.MapFrom(a => a.PesoTotalBrutoUN))
            .ForMember(dest => dest.ValorFOB, m => m.MapFrom(a => a.ValorFOB))
            .ForMember(dest => dest.ValorFOBUN, m => m.MapFrom(a => a.ValorFOBUN))
            .ForMember(dest => dest.TotalPecas, m => m.MapFrom(a => a.TotalPecas))
            .ForMember(dest => dest.TotalParcial, m => m.MapFrom(a => a.TotalParcial))
            .ForMember(dest => dest.ValorFretePP, m => m.MapFrom(a => a.ValorFretePP))
            .ForMember(dest => dest.ValorFretePPUN, m => m.MapFrom(a => a.ValorFretePPUN))
            .ForMember(dest => dest.ValorFreteFC, m => m.MapFrom(a => a.ValorFreteFC))
            .ForMember(dest => dest.ValorFreteFCUN, m => m.MapFrom(a => a.ValorFreteFCUN))
            .ForMember(dest => dest.IndicadorMadeiraMacica, m => m.MapFrom(a => a.IndicadorMadeiraMacica))
            .ForMember(dest => dest.IndicadorNaoDesunitizacao, m => m.MapFrom(a => a.IndicadorNaoDesunitizacao))
            .ForMember(dest => dest.IndicadorAwbNaoIata, m => m.MapFrom(a => a.IndicadorAwbNaoIata))
            .ForMember(dest => dest.DescricaoMercadoria, m => m.MapFrom(a => a.DescricaoMercadoria))
            .ForMember(dest => dest.CodigoRecintoAduaneiro, m => m.MapFrom(a => a.CodigoRecintoAduaneiro))
            .ForMember(dest => dest.RUC, m => m.MapFrom(a => a.RUC))
            .ForMember(dest => dest.ExpedidorNome, m => m.MapFrom(a => a.RemetenteNome))
            .ForMember(dest => dest.ExpedidorEndereco, m => m.MapFrom(a => a.RemetenteEndereco))
            .ForMember(dest => dest.ExpedidorPostal, m => m.MapFrom(a => a.RemetentePostal))
            .ForMember(dest => dest.ExpedidorCidade, m => m.MapFrom(a => a.RemetenteCidade))
            .ForMember(dest => dest.ExpedidorPaisCodigo, m => m.MapFrom(a => a.RemetentePaisCodigo))
            .ForMember(dest => dest.ExpedidorSubdivisao, m => m.MapFrom(a => a.RemetenteSubdivisao))
            .ForMember(dest => dest.ConsignatarioNome, m => m.MapFrom(a => a.ConsignatarioNome))
            .ForMember(dest => dest.ConsignatarioEndereco, m => m.MapFrom(a => a.ConsignatarioEndereco))
            .ForMember(dest => dest.ConsignatarioPostal, m => m.MapFrom(a => a.ConsignatarioPostal))
            .ForMember(dest => dest.ConsignatarioCidade, m => m.MapFrom(a => a.ConsignatarioCidade))
            .ForMember(dest => dest.ConsignatarioPaisCodigo, m => m.MapFrom(a => a.ConsignatarioPaisCodigo))
            .ForMember(dest => dest.ConsignatarioSubdivisao, m => m.MapFrom(a => a.ConsignatarioSubdivisao))
            .ForMember(dest => dest.ConsignatarioCNPJ, m => m.MapFrom(a => a.ConsignatarioCNPJ))
            .ForMember(dest => dest.NCMLista, m => m.MapFrom(a => a.GetNCMListaString()))
            .ForMember(dest => dest.CodigoConteudo, m => m.MapFrom(a => a.ConsolidadoDireto))
            .ForMember(dest => dest.AeroportoOrigemCodigo, m => m.MapFrom(a => a.AeroportoOrigemCodigo))
            .ForMember(dest => dest.AeroportoDestinoCodigo, m => m.MapFrom(a => a.AeroportoDestinoCodigo))
            .ForMember(dest => dest.NaturezaCarga, m => m.MapFrom(a => a.GetNaturezaCargaListaString()))
            .ForMember(dest => dest.Volume, m => m.MapFrom(a => a.Volume))
            .ForMember(dest => dest.VolumeUN, m => m.MapFrom(a => a.VolumeUN));

        CreateMap<MasterUpdateRequestDto, Master>()
            .ForMember(dest => dest.Id, m => m.MapFrom(a => a.MasterId))
            .ForMember(dest => dest.Numero, m => m.MapFrom(a => a.Numero))
            .ForMember(dest => dest.PesoTotalBruto, m => m.MapFrom(a => a.PesoTotalBruto))
            .ForMember(dest => dest.PesoTotalBrutoUN, m => m.MapFrom(a => a.PesoTotalBrutoUN))
            .ForMember(dest => dest.ValorFOB, m => m.MapFrom(a => a.ValorFOB))
            .ForMember(dest => dest.ValorFOBUN, m => m.MapFrom(a => a.ValorFOBUN))
            .ForMember(dest => dest.TotalPecas, m => m.MapFrom(a => a.TotalPecas))
            .ForMember(dest => dest.TotalParcial, m => m.MapFrom(a => a.TotalParcial))
            .ForMember(dest => dest.ValorFretePP, m => m.MapFrom(a => a.ValorFretePP))
            .ForMember(dest => dest.ValorFretePPUN, m => m.MapFrom(a => a.ValorFretePPUN))
            .ForMember(dest => dest.ValorFreteFC, m => m.MapFrom(a => a.ValorFreteFC))
            .ForMember(dest => dest.ValorFreteFCUN, m => m.MapFrom(a => a.ValorFreteFCUN))
            .ForMember(dest => dest.IndicadorMadeiraMacica, m => m.MapFrom(a => a.IndicadorMadeiraMacica))
            .ForMember(dest => dest.IndicadorNaoDesunitizacao, m => m.MapFrom(a => a.IndicadorNaoDesunitizacao))
            .ForMember(dest => dest.IndicadorAwbNaoIata, m => m.MapFrom(a => a.IndicadorAwbNaoIata))
            .ForMember(dest => dest.DescricaoMercadoria, m => m.MapFrom(a => a.DescricaoMercadoria))
            .ForMember(dest => dest.CodigoRecintoAduaneiro, m => m.MapFrom(a => a.CodigoRecintoAduaneiro))
            .ForMember(dest => dest.RUC, m => m.MapFrom(a => a.RUC))
            .ForMember(dest => dest.ExpedidorNome, m => m.MapFrom(a => a.RemetenteNome))
            .ForMember(dest => dest.ExpedidorEndereco, m => m.MapFrom(a => a.RemetenteEndereco))
            .ForMember(dest => dest.ExpedidorPostal, m => m.MapFrom(a => a.RemetentePostal))
            .ForMember(dest => dest.ExpedidorCidade, m => m.MapFrom(a => a.RemetenteCidade))
            .ForMember(dest => dest.ExpedidorPaisCodigo, m => m.MapFrom(a => a.RemetentePaisCodigo))
            .ForMember(dest => dest.ExpedidorSubdivisao, m => m.MapFrom(a => a.RemetenteSubdivisao))
            .ForMember(dest => dest.ConsignatarioNome, m => m.MapFrom(a => a.ConsignatarioNome))
            .ForMember(dest => dest.ConsignatarioEndereco, m => m.MapFrom(a => a.ConsignatarioEndereco))
            .ForMember(dest => dest.ConsignatarioPostal, m => m.MapFrom(a => a.ConsignatarioPostal))
            .ForMember(dest => dest.ConsignatarioCidade, m => m.MapFrom(a => a.ConsignatarioCidade))
            .ForMember(dest => dest.ConsignatarioPaisCodigo, m => m.MapFrom(a => a.ConsignatarioPaisCodigo))
            .ForMember(dest => dest.ConsignatarioSubdivisao, m => m.MapFrom(a => a.ConsignatarioSubdivisao))
            .ForMember(dest => dest.ConsignatarioCNPJ, m => m.MapFrom(a => a.ConsignatarioCNPJ))
            .ForMember(dest => dest.NCMLista, m => m.MapFrom(a => a.GetNCMListaString()))
            .ForMember(dest => dest.CodigoConteudo, m => m.MapFrom(a => a.ConsolidadoDireto))
            .ForMember(dest => dest.AeroportoOrigemCodigo, m => m.MapFrom(a => a.AeroportoOrigemCodigo))
            .ForMember(dest => dest.AeroportoDestinoCodigo, m => m.MapFrom(a => a.AeroportoDestinoCodigo))
            .ForMember(dest => dest.NaturezaCarga, m => m.MapFrom(a => a.GetNaturezaCargaListaString()))
            .ForMember(dest => dest.Volume, m => m.MapFrom(a => a.Volume))
            .ForMember(dest => dest.VolumeUN, m => m.MapFrom(a => a.VolumeUN));

        CreateMap<MasterVooQuery, MasterVooResponseDto>();
        CreateMap<MasterListaQuery, MasterListaResponseDto>();

        #endregion

        #region Uld Master
        CreateMap<UldMaster, UldMasterResponseDto>()
            .ForMember(dest => dest.Id, m => m.MapFrom(a => a.Id))
            .ForMember(dest => dest.UldId, m => m.MapFrom(a => a.ULDId))
            .ForMember(dest => dest.UldCaracteristicaCodigo, m => m.MapFrom(a => a.ULDCaracteristicaCodigo))
            .ForMember(dest => dest.UldIdPrimario, m => m.MapFrom(a => a.ULDIdPrimario))
            .ForMember(dest => dest.UsuarioCriacao, m => m.MapFrom(a => a.UsuarioCriacaoInfo.Nome))
            .ForMember(dest => dest.MasterId, m => m.MapFrom(a => a.MasterId))
            .ForMember(dest => dest.MasterNumero, m => m.MapFrom(a => a.MasterInfo.Numero))
            .ForMember(dest => dest.Peso, m => m.MapFrom(a => a.Peso))
            .ForMember(dest => dest.QuantidadePecas, m => m.MapFrom(a => a.QuantidadePecas))
            .ForMember(dest => dest.DataCricao, m => m.MapFrom(a => a.CreatedDateTimeUtc))
            .ForMember(dest => dest.AeroportoOrigem, m => m.MapFrom(a => a.PortOfOrign))
            .ForMember(dest => dest.AeroportoDestino, m => m.MapFrom(a => a.PortOfDestiny))
            .ForMember(dest => dest.DescricaoMercadoria, m => m.MapFrom(a => a.SummaryDescription));

        CreateMap<UldMasterUpdateRequest, UldMaster>()
            .ForMember(dest => dest.Id, m => m.MapFrom(a => a.Id))
            .ForMember(dest => dest.ULDId, m => m.MapFrom(a => a.UldId))
            .ForMember(dest => dest.ULDCaracteristicaCodigo, m => m.MapFrom(a => a.UldCaracteristicaCodigo))
            .ForMember(dest => dest.ULDIdPrimario, m => m.MapFrom(a => a.UldIdPrimario))
            .ForMember(dest => dest.Peso, m => m.MapFrom(a => a.Peso))
            .ForMember(dest => dest.QuantidadePecas, m => m.MapFrom(a => a.QuantidadePecas));
        #endregion

        #region House
        CreateMap<House, HouseResponseDto>()
            .ForMember(dest => dest.HouseId, m => m.MapFrom(a => a.Id))
            .ForMember(dest => dest.StatusId, m => m.MapFrom(a => a.StatusId))
            .ForMember(dest => dest.SituacaoRFB, m => m.MapFrom(a => a.SituacaoRFBId))
            .ForMember(dest => dest.ProtocoloRFB, m => m.MapFrom(a => a.ProtocoloRFB))
            .ForMember(dest => dest.Numero, m => m.MapFrom(a => a.Numero.Trim()))
            .ForMember(dest => dest.PesoTotalBruto, m => m.MapFrom(a => a.PesoTotalBruto))
            .ForMember(dest => dest.PesoTotalBrutoUN, m => m.MapFrom(a => a.PesoTotalBrutoUN))
            .ForMember(dest => dest.TotalVolumes, m => m.MapFrom(a => a.TotalVolumes))
            .ForMember(dest => dest.Volume, m => m.MapFrom(a => a.Volume))
            .ForMember(dest => dest.VolumeUN, m => m.MapFrom(a => a.VolumeUN))
            .ForMember(dest => dest.ValorFretePP, m => m.MapFrom(a => a.ValorFretePP))
            .ForMember(dest => dest.ValorFretePPUN, m => m.MapFrom(a => a.ValorFretePPUN))
            .ForMember(dest => dest.ValorFreteFC, m => m.MapFrom(a => a.ValorFreteFC))
            .ForMember(dest => dest.ValorFreteFCUN, m => m.MapFrom(a => a.ValorFreteFCUN))
            .ForMember(dest => dest.IndicadorMadeiraMacica, m => m.MapFrom(a => a.IndicadorMadeiraMacica))
            .ForMember(dest => dest.DescricaoMercadoria, m => m.MapFrom(a => a.DescricaoMercadoria))
            .ForMember(dest => dest.CodigoRecintoAduaneiro, m => m.MapFrom(a => a.CodigoRecintoAduaneiro.Trim()))
            .ForMember(dest => dest.RUC, m => m.MapFrom(a => a.RUC))
            .ForMember(dest => dest.ConsignatarioNome, m => m.MapFrom(a => a.ConsignatarioNome.Trim()))
            .ForMember(dest => dest.ConsignatarioEndereco, m => m.MapFrom(a => a.ConsignatarioEndereco.Trim()))
            .ForMember(dest => dest.ConsignatarioPostal, m => m.MapFrom(a => a.ConsignatarioPostal.Trim()))
            .ForMember(dest => dest.ConsignatarioCidade, m => m.MapFrom(a => a.ConsignatarioCidade.Trim()))
            .ForMember(dest => dest.ConsignatarioPaisCodigo, m => m.MapFrom(a => a.ConsignatarioPaisCodigo.Trim()))
            .ForMember(dest => dest.ConsignatarioSubdivisao, m => m.MapFrom(a => a.ConsignatarioSubdivisao.Trim()))
            .ForMember(dest => dest.ConsignatarioCNPJ, m => m.MapFrom(a => a.ConsignatarioCNPJ.Trim()))
            .ForMember(dest => dest.RemetenteNome, m => m.MapFrom(a => a.ExpedidorNome.Trim()))
            .ForMember(dest => dest.RemetenteEndereco, m => m.MapFrom(a => a.ExpedidorEndereco.Trim()))
            .ForMember(dest => dest.RemetentePostal, m => m.MapFrom(a => a.ExpedidorPostal.Trim()))
            .ForMember(dest => dest.RemetenteCidade, m => m.MapFrom(a => a.ExpedidorCidade.Trim()))
            .ForMember(dest => dest.RemetentePaisCodigo, m => m.MapFrom(a => a.ExpedidorPaisCodigo.Trim()))
            .ForMember(dest => dest.AgenteDeCargaNumero, m => m.MapFrom(a => a.NumeroAgenteDeCarga))
            .ForMember(dest => dest.AeroportoOrigem, m => m.MapFrom(a => a.AeroportoOrigemCodigo))
            .ForMember(dest => dest.AeroportoDestino, m => m.MapFrom(a => a.AeroportoDestinoCodigo))
            .ForMember(dest => dest.DataEmissaoXML, m => m.MapFrom(a => a.DataEmissaoXML))
            .ForMember(dest => dest.MasterNumeroXML, m => m.MapFrom(a => a.MasterNumeroXML.Trim()))
            .ForMember(dest => dest.DataProcessamento, m => m.MapFrom(a => a.DataProcessamento))
            .ForMember(dest => dest.RFBCancelationStatus, m => m.MapFrom(a => a.SituacaoDeletionRFBId))
            .ForMember(dest => dest.RFBCancelationProtocol, m => m.MapFrom(a => a.ProtocoloDeletionRFB))
            .ForMember(dest => dest.NCMLista, m => m.MapFrom(a => a.NcmArray()))
            .ForMember(dest => dest.NaturezaCarga , m => m.MapFrom(a => a.NaturezaCargaArray()));

        CreateMap<HouseInsertRequestDto, House>()
            .ForMember(dest => dest.Numero, m => m.MapFrom(a => a.Numero))
            .ForMember(dest => dest.PesoTotalBruto, m => m.MapFrom(a => a.PesoTotalBruto))
            .ForMember(dest => dest.PesoTotalBrutoUN, m => m.MapFrom(a => a.PesoTotalBrutoUN))
            .ForMember(dest => dest.TotalVolumes, m => m.MapFrom(a => a.TotalVolumes))
            .ForMember(dest => dest.Volume, m => m.MapFrom(a => a.VolumeUN))
            .ForMember(dest => dest.VolumeUN, m => m.MapFrom(a => a.VolumeUN))
            .ForMember(dest => dest.ValorFretePP, m => m.MapFrom(a => a.ValorFretePP))
            .ForMember(dest => dest.ValorFretePPUN, m => m.MapFrom(a => a.ValorFretePPUN))
            .ForMember(dest => dest.ValorFreteFC, m => m.MapFrom(a => a.ValorFreteFC))
            .ForMember(dest => dest.ValorFreteFCUN, m => m.MapFrom(a => a.ValorFreteFCUN))
            .ForMember(dest => dest.IndicadorMadeiraMacica, m => m.MapFrom(a => a.IndicadorMadeiraMacica))
            .ForMember(dest => dest.DescricaoMercadoria, m => m.MapFrom(a => a.DescricaoMercadoria))
            .ForMember(dest => dest.CodigoRecintoAduaneiro, m => m.MapFrom(a => a.CodigoRecintoAduaneiro))
            .ForMember(dest => dest.RUC, m => m.MapFrom(a => a.RUC))
            .ForMember(dest => dest.ConsignatarioNome, m => m.MapFrom(a => a.ConsignatarioNome))
            .ForMember(dest => dest.ConsignatarioEndereco, m => m.MapFrom(a => a.ConsignatarioEndereco))
            .ForMember(dest => dest.ConsignatarioPostal, m => m.MapFrom(a => a.ConsignatarioPostal))
            .ForMember(dest => dest.ConsignatarioCidade, m => m.MapFrom(a => a.ConsignatarioCidade))
            .ForMember(dest => dest.ConsignatarioPaisCodigo, m => m.MapFrom(a => a.ConsignatarioPaisCodigo))
            .ForMember(dest => dest.ConsignatarioSubdivisao, m => m.MapFrom(a => a.ConsignatarioSubdivisao))
            .ForMember(dest => dest.ConsignatarioCNPJ, m => m.MapFrom(a => a.ConsignatarioCNPJ))
            .ForMember(dest => dest.ExpedidorNome, m => m.MapFrom(a => a.RemetenteNome))
            .ForMember(dest => dest.ExpedidorEndereco, m => m.MapFrom(a => a.RemetenteEndereco))
            .ForMember(dest => dest.ExpedidorPostal, m => m.MapFrom(a => a.RemetentePostal))
            .ForMember(dest => dest.ExpedidorCidade, m => m.MapFrom(a => a.RemetenteCidade))
            .ForMember(dest => dest.ExpedidorPaisCodigo, m => m.MapFrom(a => a.RemetentePaisCodigo))
            .ForMember(dest => dest.NumeroAgenteDeCarga, m => m.MapFrom(a => a.AgenteDeCargaNumero))
            .ForMember(dest => dest.AeroportoOrigemCodigo, m => m.MapFrom(a => a.AeroportoOrigem))
            .ForMember(dest => dest.AeroportoDestinoCodigo, m => m.MapFrom(a => a.AeroportoDestino))
            .ForMember(dest => dest.DataProcessamento, m => m.MapFrom(a => a.DataProcessamento))
            .ForMember(dest => dest.NCMLista, m => m.MapFrom(a => a.GetNCMListaString()))
            .ForMember(dest => dest.NaturezaCargaLista, m => m.MapFrom(a => a.GetNaturezaCargaString()));

        CreateMap<HouseUpdateRequestDto, House>()
            .ForMember(dest => dest.Id, m => m.MapFrom(a => a.HouseId))
            .ForMember(dest => dest.Numero, m => m.MapFrom(a => a.Numero))
            .ForMember(dest => dest.PesoTotalBruto, m => m.MapFrom(a => a.PesoTotalBruto))
            .ForMember(dest => dest.PesoTotalBrutoUN, m => m.MapFrom(a => a.PesoTotalBrutoUN))
            .ForMember(dest => dest.TotalVolumes, m => m.MapFrom(a => a.TotalVolumes))
            .ForMember(dest => dest.Volume, m => m.MapFrom(a => a.Volume))
            .ForMember(dest => dest.VolumeUN, m => m.MapFrom(a => a.VolumeUN))
            .ForMember(dest => dest.ValorFretePP, m => m.MapFrom(a => a.ValorFretePP))
            .ForMember(dest => dest.ValorFretePPUN, m => m.MapFrom(a => a.ValorFretePPUN))
            .ForMember(dest => dest.ValorFreteFC, m => m.MapFrom(a => a.ValorFreteFC))
            .ForMember(dest => dest.ValorFreteFCUN, m => m.MapFrom(a => a.ValorFreteFCUN))
            .ForMember(dest => dest.IndicadorMadeiraMacica, m => m.MapFrom(a => a.IndicadorMadeiraMacica))
            .ForMember(dest => dest.DescricaoMercadoria, m => m.MapFrom(a => a.DescricaoMercadoria))
            .ForMember(dest => dest.CodigoRecintoAduaneiro, m => m.MapFrom(a => a.CodigoRecintoAduaneiro))
            .ForMember(dest => dest.RUC, m => m.MapFrom(a => a.RUC))
            .ForMember(dest => dest.ConsignatarioNome, m => m.MapFrom(a => a.ConsignatarioNome))
            .ForMember(dest => dest.ConsignatarioEndereco, m => m.MapFrom(a => a.ConsignatarioEndereco))
            .ForMember(dest => dest.ConsignatarioPostal, m => m.MapFrom(a => a.ConsignatarioPostal))
            .ForMember(dest => dest.ConsignatarioCidade, m => m.MapFrom(a => a.ConsignatarioCidade))
            .ForMember(dest => dest.ConsignatarioPaisCodigo, m => m.MapFrom(a => a.ConsignatarioPaisCodigo))
            .ForMember(dest => dest.ConsignatarioSubdivisao, m => m.MapFrom(a => a.ConsignatarioSubdivisao))
            .ForMember(dest => dest.ConsignatarioCNPJ, m => m.MapFrom(a => a.ConsignatarioCNPJ))
            .ForMember(dest => dest.ExpedidorNome, m => m.MapFrom(a => a.RemetenteNome))
            .ForMember(dest => dest.ExpedidorEndereco, m => m.MapFrom(a => a.RemetenteEndereco))
            .ForMember(dest => dest.ExpedidorPostal, m => m.MapFrom(a => a.RemetentePostal))
            .ForMember(dest => dest.ExpedidorCidade, m => m.MapFrom(a => a.RemetenteCidade))
            .ForMember(dest => dest.ExpedidorPaisCodigo, m => m.MapFrom(a => a.RemetentePaisCodigo))
            .ForMember(dest => dest.NumeroAgenteDeCarga, m => m.MapFrom(a => a.AgenteDeCargaNumero))
            .ForMember(dest => dest.AeroportoOrigemCodigo, m => m.MapFrom(a => a.AeroportoOrigem))
            .ForMember(dest => dest.AeroportoDestinoCodigo, m => m.MapFrom(a => a.AeroportoDestino))
            .ForMember(dest => dest.NCMLista, m => m.MapFrom(a => a.GetNCMListaString()))
            .ForMember(dest => dest.NaturezaCargaLista, m => m.MapFrom(a => a.GetNaturezaCargaString()));

        CreateMap<HouseMasterQuery, HouseMasterResponseDto>();
        #endregion

        #region CertificadoDigital
        CreateMap<CertificadoDigital, CertificadoDigitalResponseDto>()
            .ForMember(dest => dest.Id, m => m.MapFrom(a => a.Id))
            .ForMember(dest => dest.Arquivo, m => m.MapFrom(a => a.Arquivo))
            .ForMember(dest => dest.DataCriacao, m => m.MapFrom(a => a.CreatedDateTimeUtc))
            .ForMember(dest => dest.DataModificacao, m => m.MapFrom(a => a.ModifiedDateTimeUtc))
            .ForMember(dest => dest.DataVencimento, m => m.MapFrom(a => a.DataVencimento))
            .ForMember(dest => dest.NomeDono, m => m.MapFrom(a => a.NomeDono))
            .ForMember(dest => dest.SerialNumber, m => m.MapFrom(a => a.SerialNumber))
            .ForMember(dest => dest.UsuarioCriacao, m => m.MapFrom(a => a.UsuarioCriacaoInfo.Nome))
            .ForMember(dest => dest.UsuarioCriacaoId, m => m.MapFrom(a => a.CriadoPeloId))
            .ForMember(dest => dest.UsuarioModificacao, m => m.MapFrom(a => a.UsuarioModificacaoInfo.Nome))
            .ForMember(dest => dest.UsuarioModificadorId, m => m.MapFrom(a => a.ModificadoPeloId));
        #endregion
    }

}
