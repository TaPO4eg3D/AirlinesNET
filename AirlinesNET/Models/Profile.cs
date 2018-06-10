namespace AirlinesNET.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Profile")]
    public partial class Profile
    {
        public int ProfileID { get; set; }

        [Required]
        [StringLength(110)]
        public string FullName { get; set; }

        public int DocumentType { get; set; }

        [Required]
        [StringLength(110)]
        public string SeriesAndNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        [StringLength(110)]
        public string PhoneNumber { get; set; }

        public int UserID { get; set; }

        public virtual DocumentType DocumentType1 { get; set; }

        public virtual User User { get; set; }

        // Computed properties

        [NotMapped]
        public object AllTypes
        {
            get
            {
                DataContext db = new DataContext();
                return db.DocumentTypes.ToList();
            }
            protected set { }
        }

    }
}
