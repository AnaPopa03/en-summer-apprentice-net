using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Ticket_Management_System_API.Models;

public partial class TicketManagementSystemContext : DbContext
{
    public TicketManagementSystemContext()
    {
    }

    public TicketManagementSystemContext(DbContextOptions<TicketManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<TicketCategory> TicketCategories { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-AQSE0NH\\MSSQLSERVER01;Initial Catalog=ticket_management_system;Integrated Security=True;TrustServerCertificate=True;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__customer__CD65CB85E0EA26FA");

            entity.ToTable("customer", "ticket_management_system");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("customer_name");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__event__2370F727B41B5193");

            entity.ToTable("event", "ticket_management_system");

            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.EndDate)
                .HasPrecision(6)
                .HasColumnName("end_date");
            entity.Property(e => e.EventDescription)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("event_description");
            entity.Property(e => e.EventName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("event_name");
            entity.Property(e => e.EventTypeId).HasColumnName("event_type_id");
            entity.Property(e => e.StartDate)
                .HasPrecision(6)
                .HasColumnName("start_date");
            entity.Property(e => e.VenueId).HasColumnName("venue_id");

            entity.HasOne(d => d.EventType).WithMany(p => p.Events)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_event_event_type");

            entity.HasOne(d => d.Venue).WithMany(p => p.Events)
                .HasForeignKey(d => d.VenueId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_event_venue");
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.EventTypeId).HasName("PK__event_ty__BB84C6F345A60446");

            entity.ToTable("event_type", "ticket_management_system");

            entity.Property(e => e.EventTypeId).HasColumnName("event_type_id");
            entity.Property(e => e.EventTypeName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("event_type_name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__orders__46596229872A5AA4");

            entity.ToTable("orders", "ticket_management_system");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.NumberOfTickets).HasColumnName("number_of_tickets");
            entity.Property(e => e.OrderedAt)
                .HasPrecision(6)
                .HasColumnName("ordered_at");
            entity.Property(e => e.TicketCategoryId).HasColumnName("ticket_category_id");
            entity.Property(e => e.TotalPrice).HasColumnName("total_price");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_orders_user");

            entity.HasOne(d => d.TicketCategory).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TicketCategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_orders_ticket_category");
        });

        modelBuilder.Entity<TicketCategory>(entity =>
        {
            entity.HasKey(e => e.TicketCategoryId).HasName("PK__ticket_c__3FC8DEA29A78C27D");

            entity.ToTable("ticket_category", "ticket_management_system");

            entity.Property(e => e.TicketCategoryId).HasColumnName("ticket_category_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.Event).WithMany(p => p.TicketCategories)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ticket_category_event");
        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.HasKey(e => e.VenueId).HasName("PK__venue__82A8BE8D9E5F38D1");

            entity.ToTable("venue", "ticket_management_system");

            entity.Property(e => e.VenueId).HasColumnName("venue_id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
