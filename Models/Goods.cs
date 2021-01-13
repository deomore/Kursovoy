//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CompShop2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Goods
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Goods()
        {
            this.Transaction = new HashSet<Transaction>();
        }
    
        public int GoodsID { get; set; }
        [Display(Name = "Категория")]
        public int Category { get; set; }
        [Display(Name = "Цена")]
        public double Price { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Колличество")]
        public int Quantity { get; set; }
        [Display(Name = "Поставщик")]
        public int ProvidedBy { get; set; }
        [Display(Name = "ц")]

        public virtual Categorys Categorys { get; set; }
        public virtual Providers Providers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
