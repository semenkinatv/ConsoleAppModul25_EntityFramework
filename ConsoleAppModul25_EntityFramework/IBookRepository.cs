using System;
using System.Collections.Generic;

namespace ConsoleAppModul25_EntityFramework 
{
    interface IBookRepository : IDisposable    
    {
        IEnumerable<Book> GetList(); // получение всех объектов
        Book GetBook(int id); // получение одного объекта по id
        void Create(Book item); // создание объекта
        void Update(int id, int year); // обновление объекта
        void Delete(int id); // удаление объекта по id
        void Save();  // сохранение изменений
        IEnumerable<Book> ListBooksStyle(string style, int yearS, int yearE);
        int CountBooksAutor(string autor);
        int CountBooksStyle(string style);
        bool ExistsBookAutorName(string autor, string name);
        bool UserHasBook(string nameBook, string nameUser);
        int CountBookUser(string nameUser);
        IEnumerable<Book> BookLast();
        IEnumerable<Book> ListBooksOrderName();
        IEnumerable<Book> ListBooksOrderYear(); 
    }
}
