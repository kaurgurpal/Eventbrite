using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAccountsAPI.Domain;

namespace UserAccountsAPI.Data
{
    public class AccountsContext : DbContext
    {
        public AccountsContext(DbContextOptions options): base(options)
        { }

        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactInfo>(ConfigureContactInfo);
            modelBuilder.Entity<Address>(ConfigureAddress);
        }

        private void ConfigureAddress(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");
            builder.Property(a => a.Id).IsRequired()
                .ForSqlServerUseSequenceHiLo("address_hilo");
            builder.Property(a => a.Address1).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Address2).HasMaxLength(100);
            builder.Property(a => a.City).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Country).IsRequired().HasMaxLength(50);
            builder.Property(a => a.State).IsRequired().HasMaxLength(100);
            builder.Property(a => a.PostalCode).IsRequired();
        }

        private void ConfigureContactInfo(EntityTypeBuilder<ContactInfo> builder)
        {
            builder.ToTable("ContactInfos");
            builder.Property(c => c.Id).IsRequired()
                .ForSqlServerUseSequenceHiLo("contact_info_hilo");
            builder.Property(c => c.Email).IsRequired().HasMaxLength(100);
            builder.Property(c => c.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(c => c.LastName).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Phone).HasMaxLength(30);
            builder.Property(c => c.FbUrl).HasMaxLength(100);
            builder.Property(c => c.Company).HasMaxLength(100);
            builder.Property(c => c.PhotoUrl).IsRequired();

            //Foreign Key relation with Address
            builder.HasOne(c => c.HomeAddress).WithMany()
                .HasForeignKey(c => c.HomeAddressId);
                
        }
    }
}
