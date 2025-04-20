using System;
using System.Linq;
using System.Threading.Channels;
using ConsoleApp1.Data;
using Microsoft.EntityFrameworkCore;
using GameStore.Entities;

namespace Manipulation
{
    public static class AdminActions
    {
        public static void AddGame(GameStoreContext context)
        {
            Console.Write("Название игры: ");
            string title = Console.ReadLine()!;

            Console.WriteLine("Название жанра игры: ");
            string genre = Console.ReadLine()!;
            /*Console.Write("ID жанра: ");
            int genreId = int.Parse(Console.ReadLine()!);
            Console.Write("ID платформы: ");
            int platformId = int.Parse(Console.ReadLine()!);*/
            Console.Write("Цена: ");
            decimal price = decimal.Parse(Console.ReadLine()!);
            
            var game = new Game { Title = title, Price = price, Genre = new Genre() { Name = genre } };
            context.Games.Add(game);
            context.SaveChanges();
            Console.WriteLine("Игра успешно добавлена.");
        }

        public static void DeleteGame(GameStoreContext context)
        {
            Console.Write("ID игры для удаления: ");
            int id = int.Parse(Console.ReadLine()!);
            var game = context.Games.Find(id);
            if (game == null)
            {
                Console.WriteLine("Игра не найдена."); return;
            }
            context.Games.Remove(game);
            context.SaveChanges();
            Console.WriteLine("Игра удалена.");
        }

        public static void UpdateGame(GameStoreContext context)
        {
            Console.Write("ID игры для обновления: ");
            int id = int.Parse(Console.ReadLine()!);
            var game = context.Games.Find(id);
            
            if (game == null)
            {
                Console.WriteLine("Игра не найдена."); 
                return;
            }
            
            Console.Write($"Новое название (текущее: {game.Title}): ");
            game.Title = Console.ReadLine()!;
            Console.Write($"Новая цена (текущая: {game.Price}): ");
            game.Price = decimal.Parse(Console.ReadLine()!);
            context.SaveChanges();
            Console.WriteLine("Игра обновлена.");
        }

        public static void ShowAllGames(GameStoreContext context)
        {
            var games = context.Games.Include(g => g.Genre).Include(g => g.Platform).ToList();
            Console.WriteLine("Список всех игр:");
            foreach (var game in games)
            {
                Console.WriteLine($"ID: {game.Id}, Название: {game.Title}, Жанр: {game.Genre.Name}, Платформа: {game.Platform.Name}, Цена: {game.Price}₽");
            }
        }
    }
}
