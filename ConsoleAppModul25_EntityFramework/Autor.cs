﻿using System.Collections.Generic;

namespace ConsoleAppModul25_EntityFramework
{
    public class Autor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();
    }
}