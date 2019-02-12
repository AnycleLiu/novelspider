using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NovelSpider.Core.Models
{
    public class NovelContext : DbContext
    {
        public NovelContext() { }

        public NovelContext(DbContextOptions options) : base(options) { }

        public DbSet<Category> Category { get; set; }

        public DbSet<Author> Author { get; set; }

        public DbSet<Capter> Capter { get; set; }

        public DbSet<Book> Book { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=Novel;Integrated Security=SSPI ");
            //Console.WriteLine($"NovelContext.OnConfiguring, 设置数据库连接：{Configuration.config.GetConnectionString("Default")}");
            optionsBuilder.UseSqlServer(Configuration.config.GetConnectionString("Default"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
