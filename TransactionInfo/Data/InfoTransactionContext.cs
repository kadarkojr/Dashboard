using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TransactionInfo.Models;

namespace TransactionInfo.Data;

public partial class InfoTransactionContext : DbContext
{
    public InfoTransactionContext()
    {
    }

    public InfoTransactionContext(DbContextOptions<InfoTransactionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TransactionsData> TransactionsDatas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Database=InfoTransaction;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransactionsData>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC271E7F7C9A");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Product_Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Product_Name");
            entity.Property(e => e.Transaction_Count).HasColumnName("Transaction_count");
            entity.Property(e => e.Transaction_Values).HasColumnName("Transaction_Values");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
