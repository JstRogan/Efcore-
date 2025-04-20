using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ConsoleApp1.Data;
using Microsoft.Extensions.Configuration;

public class GameStoreContextFactory : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build()
            .GetConnectionString("Default");
        optionsBuilder.UseSqlServer(configurationBuilder);

    }
}
