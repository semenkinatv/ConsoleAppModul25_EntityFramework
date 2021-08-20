using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleAppModul25_EntityFramework
{
    public class UserRepository : IUserRepository
    {
        private AppContext db;

        public UserRepository(AppContext vdb)
        {
            this.db = vdb;
        }

        public IEnumerable<User> GetList()
        {
            return db.Users.ToList();
        }

        public User GetUser(int id)
        {
            return db.Users.FirstOrDefault(user => user.Id == id);
        }

        public void Create(User user)
        {
            db.Users.Add(user);
        }

        public void Update(int id, string name)
        {
            var User = db.Users.Find(id);
            User.Name = name;
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }

        public void Save()
        {
            db.SaveChanges();
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
