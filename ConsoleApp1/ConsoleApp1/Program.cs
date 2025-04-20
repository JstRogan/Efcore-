using System;
using ConsoleApp1.Data;
using GameStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    class Program
    {
        private static string connectionString = "Data Source=localhost;Initial Catalog=GameStore;Integrated Security=True;TrustServerCertificate=True";
        public static User CurrentUser { get; set; }

        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GameStoreContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var context = new GameStoreContext(optionsBuilder.Options))
            {
                var mainMenu = new MainMenu(context);
                mainMenu.Run();
            }
        }
    }
}