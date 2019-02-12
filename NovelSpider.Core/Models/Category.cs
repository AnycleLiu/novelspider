using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NovelSpider.Core.Models
{
    public class Category
    {
        public long Id { get; set; }

        [MaxLength(80)]
        public string Name { get; set; }

        public List<Book> Books { get; set; }
    }
}
