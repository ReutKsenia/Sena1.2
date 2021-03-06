﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using senia1._2.Model;

namespace senia1._2.Repositories
{
    class EFTaskRepository : IRepository<Task>
    {
        private ToDoEntities1 context;
        public EFTaskRepository(ToDoEntities1 context)
        {
            this.context = context;
        }

        public void add(Task task)
        {
            context.Task.Add(task);
            context.SaveChanges();
        }

        public void delete(Task task)
        {
            context.Task.Remove(context.Task.FirstOrDefault(x => x.id == task.id));
            context.SaveChanges();
        }

        public IEnumerable<Task> getAll()
        {
            return context.Task;
        }

        public IEnumerable<Task> getByListId(int id)
        {
            return context.Task.Where(x => x.ListId == id).ToList();
        }

        public void update(Task oldTask, Task newTask)
        {
            var tmp = context.Task.FirstOrDefault(x => x.id == oldTask.id);

            if (tmp != null)
            {
                tmp.Value = newTask.Value;
                tmp.Priority = newTask.Priority;
                tmp.ListId = newTask.ListId;
                tmp.DateExpected = newTask.DateExpected;
                tmp.Completed = newTask.Completed;
                tmp.Category = newTask.Category;
            }
            context.SaveChanges();
        }

        public Task getById(int id)
        {
            return context.Task.FirstOrDefault(x => x.id == id);

        }
    }
}
