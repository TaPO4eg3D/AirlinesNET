namespace AirlinesNET.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("User")]
    public partial class User
    {

        private DataContext db = new DataContext();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            PastPurchases = new HashSet<PastPurchase>();
            Profiles = new HashSet<Profile>();
            Purchases = new HashSet<Purchase>();
        }

        public int UserID { get; set; }

        [Required]
        [StringLength(110)]
        public string Username { get; set; }

        [Required]
        [StringLength(110)]
        public string Password { get; set; }

        public int Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PastPurchase> PastPurchases { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Profile> Profiles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Purchase> Purchases { get; set; }

        [NotMapped]
        public Profile Profile {
            get
            {
                return db.Profiles.Where(p => p.UserID == this.UserID).FirstOrDefault();
            }
            protected set { }
        }
    }
}
