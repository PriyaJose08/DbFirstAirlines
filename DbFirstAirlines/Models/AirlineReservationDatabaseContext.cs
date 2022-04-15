using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DbFirstAirlines.Models
{
    public partial class AirlineReservationDatabaseContext : DbContext
    {
        public AirlineReservationDatabaseContext()
        {
        }

        public AirlineReservationDatabaseContext(DbContextOptions<AirlineReservationDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Flight> Flights { get; set; } = null!;
        public virtual DbSet<Flightsmaster> Flightsmasters { get; set; } = null!;
        public virtual DbSet<Passenger> Passengers { get; set; } = null!;
        public virtual DbSet<Seat> Seats { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<UsersDb> UsersDbs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-4B2S9BA4\\SQLEXPRESS;Database=AirlineReservationDatabase;integrated security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.HasOne(d => d.FlightRegistration)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.FlightRegistrationId)
                    .HasConstraintName("FK__Booking__FlightR__3F466844");

                entity.HasOne(d => d.Journey)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.JourneyId)
                    .HasConstraintName("FK__Booking__Journey__3E52440B");
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasKey(e => e.JourneyId)
                    .HasName("PK__flights__4159B9EF0CF3635C");

                entity.ToTable("flights");

                entity.Property(e => e.JourneyId).ValueGeneratedNever();

                entity.Property(e => e.Arrivaltime).HasColumnName("arrivaltime");

                entity.Property(e => e.Availablebusinessseats).HasColumnName("availablebusinessseats");

                entity.Property(e => e.Availableeconomyseats).HasColumnName("availableeconomyseats");

                entity.Property(e => e.Businessclassprice)
                    .HasColumnType("money")
                    .HasColumnName("businessclassprice");

                entity.Property(e => e.DateofJourney).HasColumnType("smalldatetime");

                entity.Property(e => e.Departuretime).HasColumnType("smalldatetime");

                entity.Property(e => e.DestinationArea)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("destinationArea");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.Economyclassprice)
                    .HasColumnType("money")
                    .HasColumnName("economyclassprice");

                entity.Property(e => e.Flightregistrationid).HasColumnName("flightregistrationid");

                entity.Property(e => e.SourceArea)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("sourceArea");

                entity.HasOne(d => d.Flightregistration)
                    .WithMany(p => p.Flights)
                    .HasForeignKey(d => d.Flightregistrationid)
                    .HasConstraintName("FK__flights__flightr__398D8EEE");
            });

            modelBuilder.Entity<Flightsmaster>(entity =>
            {
                entity.HasKey(e => e.FlightregistrationId)
                    .HasName("PK__flightsm__0D27BDBB7BF2EACD");

                entity.ToTable("flightsmaster");

                entity.Property(e => e.FlightregistrationId)
                    .ValueGeneratedNever()
                    .HasColumnName("flightregistrationId");

                entity.Property(e => e.Businessseats)
                    .HasColumnType("money")
                    .HasColumnName("businessseats");

                entity.Property(e => e.Economyseats)
                    .HasColumnType("money")
                    .HasColumnName("economyseats");

                entity.Property(e => e.Flightname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("flightname");
            });

            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.HasKey(e => new { e.PassengerId, e.FirstName, e.LastName })
                    .HasName("PK__Passenge__8AD4255F732CBDF2");

                entity.ToTable("Passenger");

                entity.Property(e => e.PassengerId).ValueGeneratedOnAdd();

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ClassType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Passengers)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Passenger__Booki__75A278F5");

                entity.HasOne(d => d.Seat)
                    .WithMany(p => p.Passengers)
                    .HasForeignKey(d => d.SeatId)
                    .HasConstraintName("FK__Passenger__SeatI__76969D2E");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.ToTable("Seat");

                entity.Property(e => e.SeatId).ValueGeneratedNever();

                entity.Property(e => e.FlightregistrationId).HasColumnName("flightregistrationId");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK__Seat__BookingId__5AEE82B9");

                entity.HasOne(d => d.Flightregistration)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.FlightregistrationId)
                    .HasConstraintName("FK__Seat__flightregi__440B1D61");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__BookingI__09A971A2");

                entity.HasOne(d => d.Seat)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.SeatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__SeatId__08B54D69");

                entity.HasOne(d => d.Passenger)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => new { d.PassengerId, d.FirstName, d.LastName })
                    .HasConstraintName("FK__Ticket__07C12930");
            });

            modelBuilder.Entity<UsersDb>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("PK__UsersDb__CBA1B2572758C5F6");

                entity.ToTable("UsersDb");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber).HasMaxLength(10);

                entity.Property(e => e.Title)
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
