using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using senia1._2.Model;

namespace senia1._2.Repositories
{
    class EFListRepository : IListRepository
    {
        private ToDoEntities1 context;

        public EFListRepository()
        {
            context = new ToDoEntities1();
        }

        public void add(List list)
        {
            context.List.Add(list);
            context.SaveChanges();
        }

        public void delete(List list)
        {
            context.List.Remove(context.List.FirstOrDefault(x => x.id == list.id));
            context.SaveChanges();
        }

        public IEnumerable<List> getAll()
        {
            return context.List;
        }

        public List getByName(string name)
        {
            return context.List.FirstOrDefault(x => x.Title == name);
        }

        public IEnumerable<List> getByUserId(int id)
        {
            return context.List.Where(x => x.UserId == id).ToList();
        }

        public List<string> getNameByUserId(int id)
        {
            List<string> vs = new List<string>();
            List<List> lists;
            lists = context.List.Where(x => x.UserId == id).ToList();
            for(int i=0; i<lists.Count(); i++)
            {
                if(lists[i].Title != "Today" &&
                    lists[i].Title != "NextDay1" &&
                    lists[i].Title != "NextDay2" &&
                    lists[i].Title != "NextDay3" &&
                    lists[i].Title != "NextDay4" &&
                    lists[i].Title != "NextDay5" &&
                    lists[i].Title != "NextDay6" &&
                    lists[i].Title != "Calendar" &&
                    lists[i].Title != "Notepad")
                {
                    vs.Add(lists[i].ToString());
                }
            }
            return vs;
        }

        public void update(List oldList, List newList)
        {
            var tmp = context.List.FirstOrDefault(x => x.id == oldList.id);

            if (tmp != null)
            {
                tmp.Title = newList.Title;
                tmp.UserId = newList.UserId;
                tmp.DateCreate = newList.DateCreate;
            }
            context.SaveChanges();
        }
    }
}
