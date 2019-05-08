using senia1._2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace senia1._2.Repositories
{
    interface ITaskRepository
    {
        IEnumerable<Task> getAll();
        void delete(Task task);
        void add(Task task);
        void update(Task oldTask, Task newTask);
        IEnumerable<Task> getByCategory(string category);
        IEnumerable<Task> getByListId(int id);
    }
}
