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
                var user3 = new User { Name = "Alice", Email = "Alice@mail.ru" };
                var user4 = new User { Name = "Bob", Email = "Bob@mail.ru" };

                //добавление пользователей в БД 
                dbu.Create(user1);
                dbu.Create(user2);
                dbu.Create(user3);
                dbu.Create(user4);
                dbu.Save();
                
                //выбор объекта из БД по его идентификатору
                User user = dbu.GetUser(2);

                //выбор всех объектов
                var allUsers = dbu.GetList();

                //обновление Имени пользоваителя (по Id).
                dbu.Update(2, "Клим");
                dbu.Save();

                //удаление из БД
                dbu.Delete(4);
                dbu.Save();

                //--------добавление книги в БД ---------
                IBookRepository dbb;
                dbb = new BookRepository(db);

                var style1 = new Style { Name = "Сатирический роман" };
                var style2 = new Style { Name = "Роман-эпопея" };
                var style3 = new Style { Name = "Социальный роман" };
                var style4 = new Style { Name = "Мистика" };
                var style5 = new Style { Name = "Приключения" };

                var autor1 = new Autor { Name = "Пушкин" };
                var autor2 = new Autor { Name = "Толстой" };
                var autor3 = new Autor { Name = "Киплинг" };
                var autor4 = new Autor { Name = "Ильф и Петров" };
                var autor5 = new Autor { Name = "Гоголь" };

                db.Styles.AddRange(style1, style2, style3, style4, style5);
                db.SaveChanges();

                db.Autors.AddRange(autor1, autor2, autor3, autor4, autor5);
                db.SaveChanges();

                var book1 = new Book { Name = "12 стульев", Year = 1974 };
                var book2 = new Book { Name = "Дубровский", Year = 1965 };
                var book3 = new Book { Name = "Маугли", Year = 1985 };
                var book4 = new Book { Name = "Война и мир", Year = 1961 };
                var book5 = new Book { Name = "Вий", Year = 1970 };

                //Выдача книги на руки (один ко многим)
                book1.User = user1;
                book1.Style = style1;
                book1.Autor = autor4;
                dbb.Create(book1);
                dbb.Save();
              
                user1.Books.Add(book2);
                style3.Books.Add(book2);
                book2.Autor = autor1;
                dbb.Create(book2);
                dbb.Save();

                style5.Books.Add(book3);
                dbb.Create(book3);
                book3.Autor = autor3;
                dbb.Save();

                book4.UserId = user3.Id;
                book4.StyleId = style2.Id;
                book4.Autor = autor2;
                dbb.Create(book4);
                dbb.Save();

                style4.Books.Add(book5);
                book5.Autor = autor5;
                dbb.Create(book5);
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

                Console.WriteLine("Книги между 1960 и 1995 годом выпуска:");
                var listBook = dbb.ListBooksStyle("роман", 1960, 1995);

                foreach (var b in listBook.ToList())
                {
                    Console.WriteLine(b.Name + ", " + b.Year);
                }

                Console.WriteLine("Кол-во книг, автор Пушкин: " + dbb.CountBooksAutor("Пушкин"));

                Console.WriteLine("Кол-во книг, жанр Роман: " + dbb.CountBooksStyle("Роман"));

                bool rez = dbb.ExistsBookAutorName("Ильф и Петров", "12 стульев");
                if (rez)
                    Console.WriteLine("В библиотеке имеется книга '12 стульев, Ильф и Петров'");
                else
                    Console.WriteLine("В библиотеке нет книги '12 стульев, Ильф и Петров'");


                bool rez1 = dbb.UserHasBook("Война и мир", "Alice");
                if (rez1)
                    Console.WriteLine("Книга 'Война и мир' действительно выдана на руки пользователю Alice.");
                else
                    Console.WriteLine("Книга 'Война и мир' не выдана на руки данному пользователю.");

                Console.WriteLine("Кол-во книг на руках у пользователя Dina: " + dbb.CountBookUser("Dina") + " шт.");

                var listBook1 = dbb.BookLast();
                foreach (var b in listBook1.ToList())
                {
                    Console.WriteLine("Последняя вышедшая книга: " + b.Name + ", " + b.Year);
                }

                Console.WriteLine("\r\nКниги отсортированы в алфавитном порядке по названию:");
                foreach (var b in dbb.ListBooksOrderName().ToList())
                {
                    Console.WriteLine(b.Name + ", " + b.Year);
                }

                Console.WriteLine("\r\nКниги отсортированы в порядке убывания года их выхода:");
                foreach (var b in dbb.ListBooksOrderYear().ToList())
                {
                    Console.WriteLine(b.Name + ", " + b.Year);
                }
            }
        }

    }
}
