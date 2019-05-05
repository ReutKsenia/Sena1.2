using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using senia1._2.Model;

namespace senia1._2.Repositories
{
    interface IUserRepository
    {
        IEnumerable<User> getAll();
        void add(User user);
        User getByLogin(string login);
        void update(User oldUser, User newUser);
        List<string> getAllNames();
    }
}
