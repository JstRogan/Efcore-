using System;
using System.Linq;
using ConsoleApp1.Data;
using GameStore.Entities;

namespace ConsoleApp1.Manipulation
{
    public class AuthorizationUsers
    {
        private readonly GameStoreContext _context;

        public AuthorizationUsers(GameStoreContext context)
        {
            _context = context;
        }

        public void RegisterUser(string name, string email)
        {
            if (_context.Users.Any(u => u.Email == email))
            {
                Console.WriteLine("Пользователь с таким email уже существует.");
                return;
            }

            var user = new User { Name = name, Email = email, Balance = 0 };
            _context.Users.Add(user);
            _context.SaveChanges();
            Console.WriteLine("Регистрация прошла успешно.");
        }

        public User? LoginUser(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                Console.WriteLine("Пользователь не найден.");
            }
            return user;
        }
    }
}
