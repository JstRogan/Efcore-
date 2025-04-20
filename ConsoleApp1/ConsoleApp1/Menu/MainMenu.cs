using ConsoleApp1;
using ConsoleApp1.Data;
using ConsoleApp1.Manipulation;
using Manipulation;

public class MainMenu
{
    private readonly GameStoreContext _context;

    public MainMenu(GameStoreContext context)
    {
        _context = context;
    }

    public void Run() 
    { 
        while (true) 
        { 
            Console.WriteLine("1 - Войти как Админ"); 
            Console.WriteLine("2 - Войти как Пользователь"); 
            Console.WriteLine("3 - Выйти из программы");
            
            int choice = Convert.ToInt32(Console.ReadLine());
            
            switch (choice) 
            { 
                case 1: AdminMenu();
                    break;
                case 2:
                    UserMenu(); 
                    break;
                case 3: 
                    return;
                default: 
                    Console.WriteLine("Некорректный выбор!"); 
                    break;
            }
        }
    }
    private void AdminMenu() 
    { 
        while (true)
        { 
            Console.WriteLine("1 - Добавить игру"); 
            Console.WriteLine("2 - Удалить игру"); 
            Console.WriteLine("3 - Обновить игру"); 
            Console.WriteLine("4 - Показать все игры"); 
            Console.WriteLine("5 - Выйти");
            
            int choice = Convert.ToInt32(Console.ReadLine());

            
            switch (choice) 
            { 
                case 1: 
                    AdminActions.AddGame(_context); 
                    break;
                case 2:
                    AdminActions.DeleteGame(_context); 
                    break;
                case 3: 
                    AdminActions.UpdateGame(_context); 
                    break;
                case 4: 
                    AdminActions.ShowAllGames(_context); 
                    break;
                case 5: 
                    return;
                default: 
                    Console.WriteLine("Некорректный выбор!"); 
                    break;
            }
        }
    }

    private void UserMenu()
    { 
        var auth = new AuthorizationUsers(_context);

        while (true) 
        { 
            Console.WriteLine("1 - Регистрация"); 
            Console.WriteLine("2 - Войти"); 
            Console.WriteLine("3 - Назад");
            
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice) 
            { 
                case 1: 
                    Console.Write("Введите имя: "); 
                    string name = Console.ReadLine()!; 
                    Console.Write("Введите email: "); 
                    string email = Console.ReadLine()!; 
                    auth.RegisterUser(name, email); 
                    break;
                case 2: 
                    Console.Write("Введите email: "); 
                    string loginEmail = Console.ReadLine()!; 
                    var user = auth.LoginUser(loginEmail); 
                    if (user != null) 
                    { 
                        Program.CurrentUser = user; 
                        UserActionsMenu();
                    }
                    break;
                case 3: 
                    return;
                default: 
                    Console.WriteLine("Некорректный выбор!"); 
                    break;
            }
        }
    }

    private void UserActionsMenu() 
    { 
        while (true) 
        { 
            Console.WriteLine("1 - Просмотреть каталог игр"); 
            Console.WriteLine("2 - Купить игру"); 
            Console.WriteLine("3 - Пополнить баланс"); 
            Console.WriteLine("4 - История покупок"); 
            Console.WriteLine("5 - Выйти");
            
            int choice = Convert.ToInt32(Console.ReadLine());
            
            switch (choice) 
            { 
                case 1: 
                    UserActions.ViewGameCatalog(_context); 
                    break;
                case 2:
                    UserActions.BuyGame(_context, Program.CurrentUser); 
                    break;
                case 3: 
                    UserActions.TopUpBalance(_context, Program.CurrentUser); 
                    break;
                case 4: 
                    UserActions.ViewPurchaseHistory(_context, Program.CurrentUser); 
                    break;
                case 5: 
                    return;
                default: 
                    Console.WriteLine("Некорректный выбор!"); 
                    break;
            }
        }
    }
}
