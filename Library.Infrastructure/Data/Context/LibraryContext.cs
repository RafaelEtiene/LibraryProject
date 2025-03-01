using System;
using System.Collections.Generic;
using Library.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Library.Infrastructure.Data.Context;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Bookgenre> Bookgenres { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<Statusloan> Statusloans { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=library;user=usr_rafiusk;password=rafa05", ServerVersion.Parse("8.0.12-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.IdBook).HasName("PRIMARY");

            entity.ToTable("book");

            entity.HasIndex(e => e.IdBookGenre, "fk_idBookGenre");

            entity.Property(e => e.IdBook)
                .HasColumnType("int(10)")
                .HasColumnName("idBook");
            entity.Property(e => e.Author)
                .HasMaxLength(45)
                .HasColumnName("author");
            entity.Property(e => e.IdBookGenre)
                .HasColumnType("int(11)")
                .HasColumnName("idBookGenre");
            entity.Property(e => e.NameBook)
                .HasMaxLength(45)
                .HasColumnName("nameBook");
            entity.Property(e => e.PublicationDate).HasColumnName("publicationDate");

            entity.HasOne(d => d.IdBookGenreNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.IdBookGenre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_idBookGenre");
        });

        modelBuilder.Entity<Bookgenre>(entity =>
        {
            entity.HasKey(e => e.IdBookGenre).HasName("PRIMARY");

            entity.ToTable("bookgenre");

            entity.Property(e => e.IdBookGenre)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("idBookGenre");
            entity.Property(e => e.NameGenre)
                .HasMaxLength(45)
                .HasColumnName("nameGenre");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("PRIMARY");

            entity.ToTable("client");

            entity.HasIndex(e => e.Email, "idx_email");

            entity.HasIndex(e => e.IdClient, "idx_id");

            entity.HasIndex(e => e.PhoneNumber, "idx_phoneNumber");

            entity.Property(e => e.IdClient)
                .HasColumnType("int(11)")
                .HasColumnName("idClient");
            entity.Property(e => e.Address)
                .HasMaxLength(45)
                .HasColumnName("address");
            entity.Property(e => e.Age)
                .HasColumnType("int(2)")
                .HasColumnName("age");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Gender)
                .HasMaxLength(45)
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .HasColumnName("phoneNumber");
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.IdLoan).HasName("PRIMARY");

            entity.ToTable("loan");

            entity.HasIndex(e => e.IdBook, "fk_idBook_idx");

            entity.HasIndex(e => e.IdClient, "fk_idClient");

            entity.HasIndex(e => e.IdStatusLoan, "fk_idStatusLoan");

            entity.Property(e => e.IdLoan)
                .HasColumnType("int(11)")
                .HasColumnName("idLoan");
            entity.Property(e => e.DateInitialLoan).HasColumnName("dateInitialLoan");
            entity.Property(e => e.IdBook)
                .HasColumnType("int(11)")
                .HasColumnName("idBook");
            entity.Property(e => e.IdClient)
                .HasColumnType("int(11)")
                .HasColumnName("idClient");
            entity.Property(e => e.IdStatusLoan)
                .HasColumnType("int(1)")
                .HasColumnName("idStatusLoan");
            entity.Property(e => e.LastStatusDate).HasColumnName("lastStatusDate");
            entity.Property(e => e.LateFine)
                .HasPrecision(15, 2)
                .HasColumnName("lateFine");
            entity.Property(e => e.Note)
                .HasMaxLength(45)
                .HasColumnName("note");

            entity.HasOne(d => d.IdBookNavigation).WithMany(p => p.Loans)
                .HasForeignKey(d => d.IdBook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_idBook");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Loans)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_idClient");

            entity.HasOne(d => d.IdStatusLoanNavigation).WithMany(p => p.Loans)
                .HasForeignKey(d => d.IdStatusLoan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_idStatusLoan");
        });

        modelBuilder.Entity<Statusloan>(entity =>
        {
            entity.HasKey(e => e.IdStatusLoan).HasName("PRIMARY");

            entity.ToTable("statusloan");

            entity.Property(e => e.IdStatusLoan)
                .ValueGeneratedNever()
                .HasColumnType("int(2)")
                .HasColumnName("idStatusLoan");
            entity.Property(e => e.Status)
                .HasMaxLength(45)
                .HasColumnName("status");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.IdUser)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("idUser");
            entity.Property(e => e.Password)
                .HasMaxLength(45)
                .HasColumnName("password");
            entity.Property(e => e.User1)
                .HasMaxLength(45)
                .HasColumnName("user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
