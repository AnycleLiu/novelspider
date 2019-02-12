using NovelSpider.Core.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace NovelSpider.Core
{
    public class Storage : IDisposable
    {
        private NovelContext context;

        public Storage()
        {
            context = new NovelContext();
        }

        /// <summary>
        /// 加入创建队列
        /// </summary>
        /// <param name="book"></param>
        public void Create(Book book)
        {
            var old = context.Book.Include(x => x.Capters)
                                .FirstOrDefault(x => x.Name == book.Name && x.AuthorName == book.AuthorName);

            if (old != null)
            {
                foreach (var r in book.Capters)
                {
                    old.Capters.Add(r);
                }
                context.Update(old);
            }
            else
            {
                context.Book.Add(book);
            }

            context.SaveChanges();
            Console.WriteLine($"save book 【{book.Name}】");
        }

        public Author CreateAuthorIfNoExists(string authorName)
        {
            var author = context.Author.FirstOrDefault(x => x.Name == authorName.Trim());
            if (author == null)
            {
                author = new Author() { Name = authorName.Trim() };
                context.Add(author);
                context.SaveChanges();
            }
            return author;
        }
        public Category CreateCategoryIfNoExists(string cateoryName)
        {

            var category = context.Category.FirstOrDefault(x => x.Name == cateoryName.Trim());
            if (category == null)
            {
                category = new Category() { Name = cateoryName.Trim() };
                context.Add(category);
                context.SaveChanges();
            }
            return category;

        }

        public Book GetBook(string bookName, string authorName)
        {
            return context.Book.FirstOrDefault(x => x.Name == bookName && x.AuthorName == authorName);
        }

        public Capter GetBookLastCapter(long bookId)
        {
            return context.Capter.Where(x => x.BookId == bookId)
                    .OrderByDescending(x => x.SrcSiteId)
                    .FirstOrDefault();
        }

        public void Dispose()
        {
            Console.WriteLine("Storage Disposed.");
            try
            {
                context.Dispose();
            }
            finally
            {
            }
        }
    }
}
