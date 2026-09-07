using LabResultsService.Repository.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LabResultsService.Repository.Data
{
    public class LabResultsDbContext(DbContextOptions<LabResultsDbContext> options) : DbContext(options)
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<LabResult> LabResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            OnPatientModelCreation(modelBuilder);
            OnLabResultModelCreation(modelBuilder);
        }

        private void OnPatientModelCreation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(p => p.FirstName)
                    .HasColumnType("nvarchar(126)")
                    .IsRequired();

                entity.Property(p => p.MiddleName)
                    .HasColumnType("nvarchar(126)")
                    .IsRequired(false);

                entity.Property(p => p.LastName)
                    .HasColumnType("nvarchar(126)")
                    .IsRequired();

                entity.HasMany(p => p.PatientTestResults)
                    .WithOne()
                    .HasForeignKey(l => l.PatientId);
            });
        }

        private void OnLabResultModelCreation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LabResult>(entity =>
            {
                entity.Property(x => x.PatientId)
                    .IsRequired();

                entity.Property(x => x.TestName)
                    .HasMaxLength(128)
                    .IsRequired();

                entity.Property(x => x.ResultValue)
                    .HasMaxLength(256)
                    .IsRequired();

                entity.Property(x => x.Unit)
                    .HasMaxLength(32)
                    .IsRequired();

                entity.Property(x => x.ObservedDate)
                    .HasColumnType("datetime2")
                    .IsRequired();
            });
        }

    }
}
