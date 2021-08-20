using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppModul25_EntityFramework
{
    interface IUserRepository : IDisposable
    {
        IEnumerable<User> GetList(); // получение всех объектов
        User GetUser(int id); // получение одного объекта по id
        void Create(User item); // создание объекта
        void Update(int id, string name); // обновление объекта
        void Delete(int id); // удаление объекта по id
        void Save();  // сохранение изменений
    }
}
