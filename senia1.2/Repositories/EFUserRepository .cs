using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using senia1._2.Model;

namespace senia1._2.Repositories
{
    class EFUserRepository : IRepository<User>
    {
        private ToDoEntities1 context;
        public EFUserRepository(ToDoEntities1 context)
        {
            this.context = context;
        }

        public List<string> getAllNames()
        {
            List<string> tmp = new List<string>();
            foreach (User s in context.User)
                tmp.Add(s.Login);
            return tmp;
        }

        public IEnumerable<User> getAll()
        {
            return context.User;
        }

        public void add(User user)
        {
            context.User.Add(user);
            context.SaveChanges();
        }

        public void delete(User user)
        {
            context.User.Remove(user);
            context.SaveChanges();
        }

        public User getByLogin(string login)
        {
            return context.User.FirstOrDefault(x => x.Login == login);
        }

        public void update(User oldUser, User newUser)
        {
            var tmp = context.User.FirstOrDefault(x => x.Id == oldUser.Id);

            if (tmp != null)
            {
                tmp.UserName = newUser.UserName;
                tmp.Login = newUser.Login;
                tmp.Password = newUser.Password;
                tmp.Foto = newUser.Foto;
            }
            context.SaveChanges();
        }
    }
}
