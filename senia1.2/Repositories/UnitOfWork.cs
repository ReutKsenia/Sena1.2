using senia1._2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace senia1._2.Repositories
{
    class UnitOfWork
    {
        private ToDoEntities1 context = new ToDoEntities1();
        private EFUserRepository userRepository;
        private EFListRepository listRepository;
        private EFTaskRepository taskRepository;

        public EFUserRepository User
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new EFUserRepository(context);
                }

                return userRepository;
            }
        }

        public EFListRepository List
        {
            get
            {
                if (listRepository == null)
                {
                    listRepository = new EFListRepository(context);
                }

                return listRepository;
            }
        }

        public EFTaskRepository Task
        {
            get
            {
                if (taskRepository == null)
                {
                    taskRepository = new EFTaskRepository(context);
                }

                return taskRepository;
            }
        }
    }
}
