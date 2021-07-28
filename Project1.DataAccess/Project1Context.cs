using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Project1.DataAccess
{
    public partial class Project1Context : DbContext
    {
        public Project1Context()
        {
        }

        public Project1Context(DbContextOptions<Project1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Cupcake> Cupcakes { get; set; }
        public virtual DbSet<CupcakeOrder> CupcakeOrders { get; set; }
        public virtual DbSet<CupcakeOrderItem> CupcakeOrderItems { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LocationInventory> LocationInventories { get; set; }
        public virtual DbSet<RecipeItem> RecipeItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cupcake>(entity =>
            {
                entity.ToTable("Cupcake", "Project1");

                entity.HasIndex(e => e.Type, "UQ__Cupcake__F9B8A48BBF33A5C9")
                    .IsUnique();

                entity.Property(e => e.Cost)
                    .HasColumnType("decimal(8, 2)")
                    .HasDefaultValueSql("((6.00))");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<CupcakeOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__CupcakeO__C3905BCFC5A85BD9");

                entity.ToTable("CupcakeOrder", "Project1");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CupcakeOrders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Order_Customer");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.CupcakeOrders)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_Order_Location");
            });

            modelBuilder.Entity<CupcakeOrderItem>(entity =>
            {
                entity.ToTable("CupcakeOrderItem", "Project1");

                entity.HasIndex(e => new { e.OrderId, e.CupcakeId }, "OrderToCupcake")
                    .IsUnique();

                entity.Property(e => e.CupcakeId).HasColumnName("CupcakeID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.Cupcake)
                    .WithMany(p => p.CupcakeOrderItems)
                    .HasForeignKey(d => d.CupcakeId)
                    .HasConstraintName("FK_OrderItem_Cupcake");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.CupcakeOrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderItem_CupcakeOrder");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "Project1");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(e => new { e.FirstName, e.LastName })
                    .HasName("FullName")
                    .IsUnique();

                entity.HasOne(d => d.DefaultLocationNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.DefaultLocation)
                    .HasConstraintName("FK_Default_Location");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("Ingredient", "Project1");

                entity.HasIndex(e => e.Type, "UQ__Ingredie__F9B8A48BFB6A0D37")
                    .IsUnique();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Units)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location", "Project1");
            });

            modelBuilder.Entity<LocationInventory>(entity =>
            {
                entity.ToTable("LocationInventory", "Project1");

                entity.HasIndex(e => new { e.LocationId, e.IngredientId }, "InventoryIngredient")
                    .IsUnique();

                entity.Property(e => e.Amount).HasColumnType("decimal(16, 6)");

                entity.Property(e => e.IngredientId).HasColumnName("IngredientID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.LocationInventories)
                    .HasForeignKey(d => d.IngredientId)
                    .HasConstraintName("FK_Ingredient");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.LocationInventories)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_Location");
            });

            modelBuilder.Entity<RecipeItem>(entity =>
            {
                entity.ToTable("RecipeItem", "Project1");

                entity.HasIndex(e => new { e.CupcakeId, e.IngredientId }, "CupcakeIngredient")
                    .IsUnique();

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.CupcakeId).HasColumnName("CupcakeID");

                entity.Property(e => e.IngredientId).HasColumnName("IngredientID");

                entity.HasOne(d => d.Cupcake)
                    .WithMany(p => p.RecipeItems)
                    .HasForeignKey(d => d.CupcakeId)
                    .HasConstraintName("FK_Recipe_Cupcake");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.RecipeItems)
                    .HasForeignKey(d => d.IngredientId)
                    .HasConstraintName("FK_Recipe_Ingredient");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
