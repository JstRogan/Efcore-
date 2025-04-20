using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using GameStore.Entities;

namespace ConsoleApp1.Manipulation
{
    public static class UserActions
    {
        public static void TopUpBalance(Data.GameStoreContext context, User user)
        {
            Console.Write("Введите сумму для пополнения: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            user.Balance += amount;
            context.SaveChanges();
            Console.WriteLine($"Баланс успешно пополнен. Текущий баланс: {user.Balance}₽");
        }

        public static void ViewGameCatalog(Data.GameStoreContext context)
        {
            var games = context.Games.Include(g => g.Genre).Include(g => g.Platform).ToList();
            if (!games.Any())
            {
                Console.WriteLine("Каталог игр пуст.");
                return;
            }

            Console.WriteLine("Каталог игр:");
            foreach (var game in games)
            {
                string genreName = game.Genre?.Name ?? "Не указано";
                string platformName = game.Platform?.Name ?? "Не указано";
                Console.WriteLine($"ID: {game.Id} | Название: {game.Title} | Жанр: {genreName} | Платформа: {platformName} | Цена: {game.Price}₽");
            }
        }


        public static void BuyGame(Data.GameStoreContext context, User user)
        {
            ViewGameCatalog(context);
            Console.Write("Введите ID игры для покупки: ");
            int gameId = Convert.ToInt32(Console.ReadLine());

            var game = context.Games.Find(gameId);
            if (game == null)
            {
                Console.WriteLine("Игра не найдена.");
                return;
            }

            if (user.Balance < game.Price)
            {
                Console.WriteLine("Недостаточно средств на балансе.");
                return;
            }

            var order = new Order
            {
                UserId = user.Id,
                Date = DateTime.Now,
                TotalAmount = game.Price,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        GameId = game.Id,
                        Quantity = 1,
                        TotalPrice = game.Price
                    }
                }
            };

            context.Orders.Add(order);
            user.Balance -= game.Price;
            context.SaveChanges();
            Console.WriteLine($"Вы успешно купили игру: {game.Title}");
        }

        public static void ViewPurchaseHistory(Data.GameStoreContext context, User user)
        {
            var orders = context.Orders
                .Where(o => o.UserId == user.Id)
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.Game)
                .ToList();

            if (!orders.Any())
            {
                Console.WriteLine("История покупок пуста.");
                return;
            }

            Console.WriteLine("История покупок:");
            foreach (var order in orders)
            {
                Console.WriteLine($"Дата: {order.Date}, Сумма: {order.TotalAmount}₽");
                foreach (var item in order.OrderItems)
                {
                    Console.WriteLine($"  - {item.Game.Title} | Цена: {item.TotalPrice}₽");
                }
            }
        }
    }
}
