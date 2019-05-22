using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using senia1._2.Model;

namespace senia1._2.Repositories
{
    interface IRepository<T> where T : class
    {
        IEnumerable<T> getAll();
        void add(T t);
        void update(T oldT, T newT);
        void delete(T t);
    }
}
