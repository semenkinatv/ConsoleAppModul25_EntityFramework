using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleAppModul25_EntityFramework
{
    class Program
    {

        static void Main(string[] args)
        {
            using (var db = new AppContext())
            {
                IUserRepository dbu;
                dbu = new UserRepository(db);

                var user1 = new User { Name = "Dina", Email = "dina@mail.ru" };
                var user2 = new User { Name = "Klim", Email = "klim@mail.ru" };
                var user3 = new User { Name = "Alice", Email = "Alice@mail.ru", Role = "User" };
                var user4 = new User { Name = "Bob", Role = "Admin" };

                //добавление пользователей в БД 
                dbu.Create(user1);
                dbu.Create(user2);
                dbu.Create(user3);
                dbu.Create(user4);
                dbu.Save();
                //удаление из БД
                dbu.Delete(4);
                dbu.Save();

                //выбор объекта из БД по его идентификатору
                User user = dbu.GetUser(2);

                //выбор всех объектов
                var allUsers = dbu.GetList();

                //обновление Имени пользоваителя (по Id).
                dbu.Update(1, "Дина");
                dbu.Save();

                //--------добавление книги в БД ---------
                IBookRepository dbb;
                dbb = new BookRepository(db);

                var book1 = new Book { Name = "12 стульев", Year = 1974, Autor = "Ильф и Петров", Styles = "Сатирический Роман" };
                var book2 = new Book { Name = "Дубровский", Year = 1965, Autor = "Пушкин", Styles = "Роман" };
                var book3 = new Book { Name = "Маугли", Year = 1985, Autor = "Киплинг", Styles = "Приключения" };
                var book4 = new Book { Name = "Война и мир", Year = 1961, Autor = "Толстой", Styles = "Роман-эпопея" };
                var book5 = new Book { Name = "Левша", Year = 1970, Autor = "Лесков", Styles = "Повесть" };

                //Выдача книги на руки (один ко многим)
                book1.User = user1;
                dbb.Create(book1);
                dbb.Save();

                user1.Books.Add(book2);
                dbb.Create(book2);
                dbb.Save();

                dbb.Create(book3);
                dbb.Save();

                book4.UserId = user3.Id;
                dbb.Create(book4);
                dbb.Save();

                //удаление из БД
                dbb.Delete(5);
                dbb.Save();

                //выбор объекта из БД по его идентификатору
                Book book = dbb.GetBook(2);

                //выбор всех объектов
                var allBooks = dbb.GetList();

                //обновление года выпуска книги(по Id).
                dbb.Update(1, 2000);
                dbb.Save();

                Console.WriteLine("================");
                var listBook = dbb.GetListParm("Роман", 1960, 1975);

                foreach (var b in listBook.ToList())
                {
                    Console.WriteLine(b.Name + ", " + b.Autor + ", " + b.Year);
                }

                Console.WriteLine("Кол-во книг автора Пушкин А.С.: " + dbb.CountBookAutor("Пушкин"));

                Console.WriteLine("Кол-во книг в жанре Роман: " + dbb.CountBookStyles("Роман"));

                bool rez = dbb.ExistsBookAutorName("Ильф и Петров", "12 стульев");
                if (rez)
                    Console.WriteLine("В библиотеке имеется книга '12 стульев, Ильф и Петров'");
                else
                    Console.WriteLine("В библиотеке нет книги '12 стульев, Ильф и Петров'");


                bool rez1 = dbb.UserHasBook("Война и мир", "Alice");
                if (rez1)
                    Console.WriteLine("Книга 'Война и мир' выдана на руки пользователю Alice.");
                else
                    Console.WriteLine("Книга 'Война и мир' не выдана на руки данному пользователю.");

                Console.WriteLine("Кол-во книг на руках у данного пользователя: " + dbb.CountBookUser(1) + " шт.");

                var listBook1 = dbb.BookLast();
                foreach (var b in listBook1.ToList())
                {
                    Console.WriteLine("Последняя вышедшая книга:" + b.Name + ", " + b.Autor + ", " + b.Year);
                }

                Console.WriteLine("\r\nКниги отсортированы в алфавитном порядке по названию:");
                foreach (var b in dbb.ListBooksOrderName().ToList())
                {
                    Console.WriteLine(b.Name + ", " + b.Autor + ", " + b.Year);
                }

                Console.WriteLine("\r\nКниги отсортированы в порядке убывания года их выхода:");
                foreach (var b in dbb.ListBooksOrderYear().ToList())
                {
                    Console.WriteLine(b.Name + ", " + b.Autor + ", " + b.Year);
                }
            }
        }

    }
}
