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
    
    public partial class Lists
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lists()
        {
            this.Users = new HashSet<Users>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public System.DateTime DateCreate { get; set; }
        public Nullable<int> TaskId { get; set; }
    
        public virtual Tasks Tasks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users { get; set; }
    }
}
