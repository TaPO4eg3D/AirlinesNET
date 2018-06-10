namespace AirlinesNET.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
        }

        public virtual DbSet<Airport> Airports { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<PastFlight> PastFlights { get; set; }
        public virtual DbSet<PastPurchase> PastPurchases { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airport>()
                .HasMany(e => e.Flights)
                .WithRequired(e => e.Airport)
                .HasForeignKey(e => e.StartPoint)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Airport>()
                .HasMany(e => e.Flights1)
                .WithRequired(e => e.Airport1)
                .HasForeignKey(e => e.EndPoint)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Airport>()
                .HasMany(e => e.PastFlights)
                .WithRequired(e => e.Airport)
                .HasForeignKey(e => e.StartPoint)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Airport>()
                .HasMany(e => e.PastFlights1)
                .WithRequired(e => e.Airport1)
                .HasForeignKey(e => e.EndPoint)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Flights)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.PastFlights)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DocumentType>()
                .HasMany(e => e.Profiles)
                .WithRequired(e => e.DocumentType1)
                .HasForeignKey(e => e.DocumentType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Flight>()
                .HasMany(e => e.Purchases)
                .WithRequired(e => e.Flight)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PastFlight>()
                .HasMany(e => e.PastPurchases)
                .WithRequired(e => e.PastFlight)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.PastPurchases)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Profiles)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Purchases)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
