﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace senia1._2.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ToDoListDBEntities1 : DbContext
    {
        public ToDoListDBEntities1()
            : base("name=ToDoListDBEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Lists> Lists { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
