using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CtaCargo.CctImportacao.Infrastructure.Data;

public class DatabaseDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public DatabaseDesignTimeDbContextFactory()
    {
    }

    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        builder.UseSqlServer("Server=tcp:hom-ctacargosql-server-01.database.windows.net,1433;Initial Catalog=cctimportacao;Persist Security Info=False;User ID=ctacargodatabase;Password=Access_SQL_@99*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        return new ApplicationDbContext(builder.Options);
    }
}