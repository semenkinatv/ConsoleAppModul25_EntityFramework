using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppModul25_EntityFramework
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Autor { get; set; }
        public string Styles { get; set; }


        // Внешний ключ
        public int? UserId { get; set; }
        // Навигационное свойство
        public virtual User User { get; set; }
    }
}
