namespace AirlinesNET.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Flight")]
    public partial class Flight
    {

        private DataContext db = new DataContext();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flight()
        {
            Purchases = new HashSet<Purchase>();
        }

        public int FlightID { get; set; }

        [Required]
        [StringLength(64)]
        public string FlightName { get; set; }

        public int CompanyID { get; set; }

        public int StartPoint { get; set; }

        public int EndPoint { get; set; }

        public int Seats { get; set; }

        public int? TakenSeats { get; set; }

        public virtual Airport Airport { get; set; }

        public virtual Airport Airport1 { get; set; }

        public virtual Company Company { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Purchase> Purchases { get; set; }

        [NotMapped]
        public int SeatsLeft
        {
            get
            {
                var purchasesAmount = db.Purchases.Where(p => p.FlightID == this.FlightID).ToList().Count;
                return Seats - purchasesAmount; 
            }
            protected set { }
        }

    }
}
