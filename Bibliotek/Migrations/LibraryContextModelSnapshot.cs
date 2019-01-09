﻿// <auto-generated />
using System;
using Bibliotek.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bibliotek.Migrations
{
    [DbContext(typeof(LibraryContext))]
    partial class LibraryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bibliotek.Models.Author", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("ID");

                    b.ToTable("Authors");

                    b.HasData(
                        new { ID = 1, FirstName = "William", LastName = "Shakespeare" }
                    );
                });

            modelBuilder.Entity("Bibliotek.Models.Book", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorID");

                    b.Property<string>("Description");

                    b.Property<string>("ISBN");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.HasIndex("AuthorID");

                    b.ToTable("Books");

                    b.HasData(
                        new { ID = 1, AuthorID = 1, Description = "Arguably Shakespeare's greatest tragedy", ISBN = "1472518381", Title = "Hamlet" },
                        new { ID = 2, AuthorID = 1, Description = "King Lear is a tragedy written by William Shakespeare. It depicts the gradual descent into madness of the title character, after he disposes of his kingdom by giving bequests to two of his three daughters egged on by their continual flattery, bringing tragic consequences for all.", ISBN = "9780141012292", Title = "King Lear" },
                        new { ID = 3, AuthorID = 1, Description = "An intense drama of love, deception, jealousy and destruction.", ISBN = "1853260185", Title = "Othello" }
                    );
                });

            modelBuilder.Entity("Bibliotek.Models.BookCopy", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookID");

                    b.Property<int>("IsAvailable");

                    b.HasKey("ID");

                    b.HasIndex("BookID");

                    b.ToTable("BookCopies");
                });

            modelBuilder.Entity("Bibliotek.Models.Loan", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookID");

                    b.Property<DateTime>("DateLoan");

                    b.Property<DateTime>("DateReturn");

                    b.Property<int>("MemberID");

                    b.HasKey("ID");

                    b.HasIndex("MemberID");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("Bibliotek.Models.Member", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("ID");

                    b.ToTable("Members");

                    b.HasData(
                        new { ID = 1, FirstName = "Fredrik", LastName = "Gustafsson" }
                    );
                });

            modelBuilder.Entity("Bibliotek.Models.Book", b =>
                {
                    b.HasOne("Bibliotek.Models.Author", "Author")
                        .WithMany("AuthorBooks")
                        .HasForeignKey("AuthorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Bibliotek.Models.BookCopy", b =>
                {
                    b.HasOne("Bibliotek.Models.Book")
                        .WithMany("BookCopeis")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Bibliotek.Models.Loan", b =>
                {
                    b.HasOne("Bibliotek.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookID");

                    b.HasOne("Bibliotek.Models.Member", "Member")
                        .WithMany("Loans")
                        .HasForeignKey("MemberID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
