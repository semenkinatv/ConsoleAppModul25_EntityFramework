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
        IEnumerable<Book> GetListParm(string styles, int yearS, int yearE);
        int CountBookAutor(string autor);
        int CountBookStyles(string styles);
        bool ExistsBookAutorName(string autor, string name);
        bool UserHasBook(string nameBook, string nameUser);
        int CountBookUser(int iduser);
        IEnumerable<Book> BookLast();
        IEnumerable<Book> ListBooksOrderName();
        IEnumerable<Book> ListBooksOrderYear(); 
    }
}
