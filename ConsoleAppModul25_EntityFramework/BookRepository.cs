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

        //Получать список книг определенного жанра и вышедших между определенными годами.
        public IEnumerable<Book> GetListParm(string styles, int yearS, int yearE)
        {
            var list = db.Books.Where(b => b.Styles == styles)
                .Where(b => b.Year >= yearS & b.Year <= yearE).ToList();
            return list;
        }

        //Получать количество книг определенного автора в библиотеке.
        public int CountBookAutor(string autor)
        {
            return db.Books.Count(b => b.Autor == autor);
        }

        // Получать количество книг определенного жанра в библиотеке.
        public int CountBookStyles(string styles)
        {
           // return db.Books.Count(b => b.Styles == styles);
            return db.Books.Count(b => b.Styles.Contains(styles));
        }

        //Получать булевый флаг о том, есть ли книга определенного автора и с определенным названием в библиотеке.
        public bool ExistsBookAutorName(string autor, string name)
        {
            return db.Books.Any(b => b.Autor == autor & b.Name == name);
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
        public int CountBookUser(int iduser)
        {
            return db.Books.Count(b => b.Id == iduser);
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
