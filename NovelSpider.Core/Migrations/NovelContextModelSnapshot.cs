using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NovelSpider.Core.Models;

namespace NovelSpider.Core.Migrations
{
    [DbContext(typeof(NovelContext))]
    partial class NovelContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NovelSpider.Core.Models.Author", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.ToTable("Author");
                });

            modelBuilder.Entity("NovelSpider.Core.Models.Book", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("AuthorId");

                    b.Property<string>("AuthorName")
                        .HasMaxLength(80);

                    b.Property<long?>("CategoryId");

                    b.Property<string>("CategoryName")
                        .HasMaxLength(80);

                    b.Property<string>("CoverImgUrl")
                        .HasMaxLength(100);

                    b.Property<string>("Description");

                    b.Property<DateTime?>("LastCrawTime");

                    b.Property<DateTime>("LastUpdateTime");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<string>("SrcSiteUrl")
                        .HasMaxLength(100);

                    b.Property<string>("Status")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("NovelSpider.Core.Models.Capter", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("BookId");

                    b.Property<int>("CapterNo");

                    b.Property<string>("CapterUrl")
                        .HasMaxLength(100);

                    b.Property<string>("Content");

                    b.Property<long>("SrcSiteId");

                    b.Property<string>("Title")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Capter");
                });

            modelBuilder.Entity("NovelSpider.Core.Models.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("NovelSpider.Core.Models.Book", b =>
                {
                    b.HasOne("NovelSpider.Core.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId");

                    b.HasOne("NovelSpider.Core.Models.Category", "Category")
                        .WithMany("Books")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("NovelSpider.Core.Models.Capter", b =>
                {
                    b.HasOne("NovelSpider.Core.Models.Book", "Book")
                        .WithMany("Capters")
                        .HasForeignKey("BookId");
                });
        }
    }
}
