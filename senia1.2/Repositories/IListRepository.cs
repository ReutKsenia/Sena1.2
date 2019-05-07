using senia1._2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace senia1._2.Repositories
{
    interface IListRepository
    {
        IEnumerable<List> getAll();
        void delete(List list);
        void add(List list);
        List getByName(string name);
        void update(List oldList, List newList);
        IEnumerable<List> getByUserId(int id);
        List<string> getNameByUserId(int id);
    }
}
