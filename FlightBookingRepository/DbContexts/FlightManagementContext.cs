using System;
using System.Collections.Generic;
using FlightBookingRepository.DataModels;
using Microsoft.EntityFrameworkCore;

namespace FlightBookingRepository.DbContexts;

public partial class FlightManagementContext : DbContext
{    
    public FlightManagementContext()
    {
    }

    public FlightManagementContext(DbContextOptions<FlightManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Airline> Airlines { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<FlightUserMapping> FlightUserMappings { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<UsedInstrument> UsedInstruments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserBookingDatum> UserBookingData { get; set; }

    public virtual DbSet<UserPassengerDatum> UserPassengerData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Airline>(entity =>
        {
            entity.ToTable("Airline");

            entity.Property(e => e.AirlineName).HasMaxLength(50);
            entity.Property(e => e.Logo).HasMaxLength(50);
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.ToTable("Flight");

            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.FlightNumber).HasMaxLength(1000);
            entity.Property(e => e.FromPlace).HasMaxLength(1000);
            entity.Property(e => e.ScheduledDays).HasMaxLength(50);
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.ToPlace).HasMaxLength(1000);

            entity.HasOne(d => d.Airline).WithMany(p => p.Flights)
                .HasForeignKey(d => d.AirlineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Flight__AirlineId");
        });

        modelBuilder.Entity<FlightUserMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FlightUserMapping_Id");

            entity.ToTable("FlightUserMapping");

            entity.HasOne(d => d.Flight).WithMany(p => p.FlightUserMappings)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FlightUserMapping_FlightId");

            entity.HasOne(d => d.User).WithMany(p => p.FlightUserMappings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FlightUserMapping_UserId");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.ToTable("Inventory");

            entity.Property(e => e.EndTime).HasColumnType("datetime");            
            entity.Property(e => e.FromPlace).HasMaxLength(1000);
            entity.Property(e => e.Meal).HasMaxLength(50);
            entity.Property(e => e.ScheduledDays).HasMaxLength(50);
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.ToPlace).HasMaxLength(1000);
            entity.Property(e => e.TotalTicketCost).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Flight).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventory_FlightId");            
        });

        modelBuilder.Entity<UsedInstrument>(entity =>
        {
            entity.HasKey(e => e.InstrumentId);

            entity.ToTable("UsedInstrument");

            entity.Property(e => e.InstrumentName).HasMaxLength(1000);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        modelBuilder.Entity<UserBookingDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserBookingData_Id");

            entity.Property(e => e.Meal).HasMaxLength(50);
            entity.Property(e => e.PnrNumber).HasMaxLength(50);
            entity.Property(e => e.SeatNumbers).HasMaxLength(50);
            entity.Property(e => e.UserEmail).HasMaxLength(1000);
            entity.Property(e => e.UserName).HasMaxLength(1000);
        });

        modelBuilder.Entity<UserPassengerDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserPassengerData_Id");

            entity.Property(e => e.Gender).HasMaxLength(1000);
            entity.Property(e => e.PassengerName).HasMaxLength(1000);

            entity.HasOne(d => d.UserBookingData).WithMany(p => p.UserPassengerData)
                .HasForeignKey(d => d.UserBookingDataId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPassengerData_UserBookingDataId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
