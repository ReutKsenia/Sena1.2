//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class ListsUser
    {
        public int id { get; set; }
        public Nullable<int> IdList { get; set; }
        public Nullable<int> IdUser { get; set; }
    
        public virtual List List { get; set; }
        public virtual User User { get; set; }
    }
}
