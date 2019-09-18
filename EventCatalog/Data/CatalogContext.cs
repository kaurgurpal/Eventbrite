using EventCatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Data
{
    public class CatalogContext:DbContext
    {
        public CatalogContext(DbContextOptions options):base(options)
        { }

        public DbSet<CatalogCategory> CatalogCategories { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<CatalogEvent> CatalogEvents { get; set; }
        public DbSet<EventLocation> EventLocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatalogCategory>(ConfigureCatalogCategory);
            modelBuilder.Entity<CatalogType>(ConfigureCatalogType);
            modelBuilder.Entity<CatalogEvent>(ConfigureCatalogEvent);
            modelBuilder.Entity<EventLocation>(ConfigureEventLocation);

        }

        private void ConfigureEventLocation(EntityTypeBuilder<EventLocation> builder)
        {
            throw new NotImplementedException();
        }

        private void ConfigureCatalogEvent(EntityTypeBuilder<CatalogEvent> builder)
        {
            builder.ToTable("Catalog");
            builder.Property(c => c.Id)
                .IsRequired()
                .ForSqlServerUseSequenceHiLo("catalog_hilo");
            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasOne(c => c.EventLocation)
                .WithMany()
                .HasForeignKey(c => c.LocationId);
            builder.Property(c => c.ShowMap);
            builder.Property(c => c.StartDateTime);
            builder.Property(c => c.EndDateTime);
            builder.Property(c => c.ImageUrl);
            builder.Property(c => c.Description);
            builder.Property(c => c.OrganizerName)
                .HasMaxLength(100);
            builder.Property(c => c.OrganizerDescription);
            builder.Property(c => c.FacebookLink);
            builder.Property(c => c.TwitterLink);
            builder.HasOne(c => c.EventType)
                .WithMany()
                .HasForeignKey(c => c.EventTypeId);
            builder.HasOne(c => c.EventCategory)
                .WithMany()
                .HasForeignKey(c => c.EventCatagoryId);
            builder.Property(c => c.Price);

        }

        private void ConfigureCatalogType(EntityTypeBuilder<CatalogType> builder)
        {
            builder.ToTable("CatalogType");
            builder.Property(c=>c.Id)
                .IsRequired()
                .ForSqlServerUseSequenceHiLo("catalog_type_hilo");
            builder.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(100);
        }

        private void ConfigureCatalogCategory(EntityTypeBuilder<CatalogCategory> builder)
        {
            builder.ToTable("CatalogCategory");
            builder.Property(c => c.Id)
                .IsRequired()
                .ForSqlServerUseSequenceHiLo("catalog_category_hilo");
            builder.Property(t => t.Category)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
