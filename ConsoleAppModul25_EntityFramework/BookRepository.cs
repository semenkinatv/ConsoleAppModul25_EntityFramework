using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleAppModul25_EntityFramework
{
    public class BookRepository : IBookRepository
    {
        private AppContext db;

        public BookRepository(AppContext vdb)
        {
            this.db = vdb;
        }

        public IEnumerable<Book> GetList()
        {
            return db.Books.ToList();
        }

        public Book GetBook(int id)
        {
            return db.Books.FirstOrDefault(book => book.Id == id);
        }

        public void Create(Book book)
        {
            db.Books.Add(book);
        }

        public void Update(int id, int year)
        {
            var Book = db.Books.Find(id);
            Book.Year = year;
        }

        public void Delete(int id)
        {
            Book book = db.Books.Find(id);
            if (book != null)
                db.Books.Remove(book);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        // Получать список книг определенного жанра и вышедших между определенными годами.
        public IEnumerable<Book> ListBooksStyle(string style, int yearS, int yearE)
        {
            var list1 = db.Books.Where(b => b.Year >= yearS && b.Year <= yearE)
                .Join(db.Styles, b => b.StyleId, s => s.Id, (b, s) =>
           new { NameBook = b.Name, YearBook = b.Year,StyleName = s.Name })
                .Where(l => l.StyleName.Contains(style)).ToList();

            var list2 = list1.Select(row => new Book {Name = row.NameBook, Year = row.YearBook});

            return (IEnumerable<Book>)list2;
        }

        //Получать количество книг определенного автора в библиотеке.
        public int CountBooksAutor(string autor)
        {
            //return db.Books.Count(b => b.Autor == autor);
            var rez = db.Books.Join(db.Autors, b => b.AutorId, a => a.Id, (b, a) =>
           new { AutorBook = a.Name })
                .Count(l => l.AutorBook == autor);
            return rez;
        }

        // Получать количество книг определенного жанра в библиотеке.
        public int CountBooksStyle(string style)
        {
             var rez = db.Books.Join(db.Styles, b => b.StyleId, s => s.Id, (b, s) =>
           new { NameBook = b.Name, StyleName = s.Name })
                .Count(l => l.StyleName.Contains(style));
            return rez;
        }

        //Получать булевый флаг о том, есть ли книга определенного автора и с определенным названием в библиотеке.
        public bool ExistsBookAutorName(string autor, string name)
        {
           var rez = db.Books.Join(db.Autors, b => b.AutorId, a => a.Id, (b, a) =>
          new { AutorBook = a.Name, NameBook = b.Name })
               .Any(l => l.AutorBook == autor && l.NameBook == name);

            return rez;
        }

        //Получать булевый флаг о том, есть ли определенная книга на руках у пользователя.
        public bool UserHasBook(string nameBook, string nameUser)
        {
            var rez =  db.Books.Where(b => b.Name == nameBook)
                .Join(db.Users, b => b.UserId, u => u.Id, (b,u) => 
            new { nameU = u.Name }).Any(u => u.nameU == nameUser);
            return rez;
        }

        //Получать количество книг на руках у пользователя.
        public int CountBookUser(string nameUser)
        {
            //return db.Books.Count(b => b.UserId == iduser);
            var rez = db.Books.Join(db.Users, b => b.UserId, u => u.Id, (b, u) =>
                new { NameUser = u.Name})
              .Count(l => l.NameUser == nameUser);
            return rez;
        }

        //Получение последней вышедшей книги.
        public IEnumerable<Book> BookLast()
        {
            var maxYear = db.Books.Max(y => y.Year);
            var maxYear1 = db.Books.Max(y => y.Year).ToString();
            var list = db.Books.Where(b => b.Year == maxYear).ToList();
            return list;
        }

        //Получение списка всех книг, отсортированного в алфавитном порядке по названию.
        public IEnumerable<Book> ListBooksOrderName()
        {
            var list = db.Books.OrderBy(b => b.Name).ToList();
            return list;
        }

        //Получение списка всех книг, отсортированного в порядке убывания года их выхода.
        public IEnumerable<Book> ListBooksOrderYear()
        {
            var list = db.Books.OrderByDescending(b => b.Year).ToList();
            return list;
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Update(Book item)
        {
            throw new NotImplementedException();
        }
    }
}
