using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ZoomCars.Models
{
    public partial class newCarContext : DbContext
    {
        public newCarContext()
        {
        }

        public newCarContext(DbContextOptions<newCarContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Customer1> Customer1s { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Rental> Rentals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source =(localdb)\\MSSQLLocalDB;Initial Catalog=newCar; Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.Vin)
                    .HasName("PK__Cars__C5DF234D53DF5E73");

                entity.Property(e => e.Vin).HasColumnName("VIN");

                entity.Property(e => e.Brand)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CarAvailable)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Car_available");

                entity.Property(e => e.CarPrice).HasColumnName("Car_price");

                entity.Property(e => e.CarSeats).HasColumnName("Car_Seats");

                entity.Property(e => e.CarType)
                    .HasMaxLength(225)
                    .IsUnicode(false)
                    .HasColumnName("Car_type");

                entity.Property(e => e.Model)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer1>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__customer__8CB286B9D6E72D38");

                entity.ToTable("customer1");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MobilePhone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Mobile_phone");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password).HasMaxLength(100);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.LocationId).HasColumnName("Location_ID");

                entity.Property(e => e.City)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rental>(entity =>
            {
                entity.HasKey(e => e.ReservationNumber)
                    .HasName("PK__Rental__3E712F1C90451F62");

                entity.ToTable("Rental");

                entity.Property(e => e.ReservationNumber).HasColumnName("Reservation_Number");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.LocationId).HasColumnName("Location_ID");

                entity.Property(e => e.PickUpDate)
                    .HasColumnType("date")
                    .HasColumnName("Pick_up_date");

                entity.Property(e => e.ReturnDate)
                    .HasColumnType("date")
                    .HasColumnName("Return_date");

                entity.Property(e => e.Vin).HasColumnName("VIN");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rental__Customer__70DDC3D8");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rental__Location__71D1E811");

                entity.HasOne(d => d.VinNavigation)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.Vin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rental__VIN__6FE99F9F");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
