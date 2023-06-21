using CtaCargo.CctImportacao.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Configura> Configura { get; set; }
        public DbSet<MasterInstrucaoManuseio> MasterInstricoesManuseio { get; set; }
        public DbSet<CertificadoDigital> Certificados { get; set; }
        public DbSet<PortoIata> PortosIATA { get; set; }
        public DbSet<CiaAerea> CiasAereas { get; set; }
        public DbSet<Voo> Voos { get; set; }
        public DbSet<Master> Masters { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<CnpjCliente> CnpjClientes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<UldMaster> ULDMasters { get; set; }
        public DbSet<NaturezaCarga> NaturezasCarga { get; set; }
        public DbSet<ErroMaster> ErrosMaster { get; set; }
        public DbSet<AgenteDeCarga> AgentesDeCarga { get; set; }
        public DbSet<ResumoVooUldView> ResumoVooUldsView { get; set; }
        public DbSet<Fatura> Faturas { get; set; }
        public DbSet<ReceitaFederalTransacao> ReceitaFederalTransacoes { get; set; }
        public DbSet<NCM> NCMs { get; set; }
        public DbSet<MasterHouseAssociacao> MasterHouseAssociacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empresa>().ToTable("Empresa"); 
            modelBuilder.Entity<Empresa>()
                .HasIndex(u => u.RazaoSocial);

            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => new { u.EmpresaId, u.EMail, u.DataExclusao } )
                .IsUnique()
                .Metadata.SetAnnotation(RelationalAnnotationNames.Filter, null);

            modelBuilder.Entity<Configura>().ToTable("Configura");

            modelBuilder.Entity<MasterInstrucaoManuseio>().ToTable("MasterInstrucaoManuseio");
            modelBuilder.Entity<MasterInstrucaoManuseio>()
                .HasIndex(u => new { u.Codigo })
                .IsUnique()
                .Metadata.SetAnnotation(RelationalAnnotationNames.Filter, null);

            modelBuilder.Entity<CertificadoDigital>().ToTable("CertificadoDigital");
            modelBuilder.Entity<CertificadoDigital>()
                .HasIndex(u => new { u.EmpresaId, u.SerialNumber, u.DataExclusao })
                .IsUnique()
                .Metadata.SetAnnotation(RelationalAnnotationNames.Filter, null);

            modelBuilder.Entity<PortoIata>()
                .ToTable("PortoIATA");
            modelBuilder.Entity<PortoIata>()
                .HasIndex(u => new { u.EmpresaId, u.Codigo, u.DataExclusao })
                .IsUnique()
                .Metadata.SetAnnotation(RelationalAnnotationNames.Filter, null);

            modelBuilder.Entity<CiaAerea>()
                .ToTable("CiaAerea");
            modelBuilder.Entity<CiaAerea>()
                .HasIndex(u => new { u.EmpresaId, u.Numero, u.DataExclusao } )
                .IsUnique()
                .Metadata.SetAnnotation(RelationalAnnotationNames.Filter, null);

            modelBuilder.Entity<AgenteDeCarga>()
                .ToTable("AgenteDeCarga");
            modelBuilder.Entity<AgenteDeCarga>()
                .HasIndex(u => new { u.EmpresaId, u.Numero, u.DataExclusao })
                .IsUnique()
                .Metadata.SetAnnotation(RelationalAnnotationNames.Filter, null);

            modelBuilder.Entity<Voo>()
                .ToTable("Voo");
            modelBuilder.Entity<Voo>()
                .HasIndex(u => new { u.CiaAereaId, u.DataVoo, u.Numero, u.DataExclusao })
                .IsUnique()
                .Metadata.SetAnnotation(RelationalAnnotationNames.Filter, null);

            modelBuilder.Entity<Master>()
                .ToTable("Master");
            modelBuilder.Entity<Master>()
                .HasIndex(u => new { u.VooId, u.Numero, u.DataExclusao })
                .IsUnique()
                .Metadata.SetAnnotation(RelationalAnnotationNames.Filter, null);

            modelBuilder.Entity<ErroMaster>()
                .ToTable("ErroMaster");
            modelBuilder.Entity<ErroMaster>()
                .HasOne<Master>(e => e.MasterInfo)
                .WithMany(d => d.ErrosMaster)
                .HasForeignKey(e => e.MasterId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ErroMaster>()
                .HasIndex(u => new { u.MasterId, u.DataExclusao })
                .Metadata.SetAnnotation(RelationalAnnotationNames.Filter, null);

            modelBuilder.Entity<UldMaster>()
                .ToTable("ULDMaster");
            modelBuilder.Entity<UldMaster>()
                .HasIndex(u => new { u.MasterId, u.ULDCaracteristicaCodigo, u.ULDId, u.ULDIdPrimario, u.DataExclusao })
                .IsUnique()
                .Metadata.SetAnnotation(RelationalAnnotationNames.Filter, null);

            modelBuilder.Entity<House>()
                .ToTable("House");
            modelBuilder.Entity<House>()
                .HasIndex(u => new { u.MasterNumeroXML , u.Numero, u.DataExclusao })
                .IsUnique()
                .Metadata.SetAnnotation(RelationalAnnotationNames.Filter, null);

            modelBuilder.Entity<CnpjCliente>().ToTable("CnpjCliente");
            modelBuilder.Entity<CnpjCliente>()
                .HasIndex(u => new { u.EmpresaId, u.Cnpj })
                .IsUnique()
                .Metadata.SetAnnotation(RelationalAnnotationNames.Filter, null);

            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Cliente>()
                .HasIndex(u => new { u.EmpresaId, u.Nome, u.Endereco, u.Cidade, u.PaisCodigo, u.Postal, u.Subdivisao })
                .IsUnique()
                .Metadata.SetAnnotation(RelationalAnnotationNames.Filter, null);

            modelBuilder.Entity<NaturezaCarga>().ToTable("NaturezaCarga");
            modelBuilder.Entity<NaturezaCarga>()
                .HasIndex(u => new { u.EmpresaId, u.Codigo, u.DataExclusao })
                .IsUnique()
                .Metadata.SetAnnotation(RelationalAnnotationNames.Filter, null);

            modelBuilder.Entity<Fatura>().ToTable("Fatura");
            modelBuilder.Entity<Fatura>().HasIndex(u => new { u.EmpresaId, u.TipoFatura, u.DataExclusao })
                .Metadata.SetAnnotation(RelationalAnnotationNames.Filter, null);

            modelBuilder.Entity<ReceitaFederalTransacao>().ToTable("ReceitaFederalTransacao");
            modelBuilder.Entity<ReceitaFederalTransacao>().HasIndex( u => new { u.EmpresaId, u.FaturaId })
                .Metadata.SetAnnotation(RelationalAnnotationNames.Filter, null);

            modelBuilder.Entity<NCM>().ToTable("NCMs");
            modelBuilder.Entity<NCM>().HasIndex(u => new { u.Seleciona, u.Descricao })
                .Metadata.SetAnnotation(RelationalAnnotationNames.Filter, null);
            
            modelBuilder.Entity<MasterHouseAssociacao>().ToTable("MasterHouseAssociacao");
            modelBuilder.Entity<MasterHouseAssociacao>().HasIndex(u => new { u.DataExclusao, u.MasterNumber })
                .Metadata.SetAnnotation(RelationalAnnotationNames.Filter, null);

            modelBuilder.Entity<ResumoVooUldView>(eb =>
            {
                eb.ToView("ResumoVooUldView");
            });

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

    }
}
