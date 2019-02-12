using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NovelSpider.Core.Models
{
    public class Book
    {
        public long Id { get; set; }

        /// <summary>
        /// 来源站点URl
        /// </summary>
        [MaxLength(100)]
        public string SrcSiteUrl { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public Author Author { get; set; }

        [MaxLength(80)]
        public string AuthorName { get; set; }

        [MaxLength(100)]
        public string CoverImgUrl { get; set; }

        public DateTime LastUpdateTime { get; set; }
        
        public string Description { get; set; }

        /// <summary>
        /// 状态（连载中、已完结)
        /// </summary>
        [MaxLength(50)]
        public string Status { get; set; }

        public Category Category { get; set; }

        [MaxLength(80)]
        public string CategoryName { get; set; }

        public List<Capter> Capters { get; set; }

        public DateTime? LastCrawTime { get; set; }
    }

}
