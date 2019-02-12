using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NovelSpider.Core.Models
{
    public class Capter
    {
        public long Id { get; set; }

        /// <summary>
        /// 章节号
        /// </summary>
        public int CapterNo { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// 原站点ID
        /// </summary>
        public long SrcSiteId { get; set; }

        /// <summary>
        /// 章节详情URL
        /// </summary>
        [MaxLength(100)]
        public string CapterUrl { get; set; }

        public long BookId { get; set; }

        public Book Book { get; set; }

        /// <summary>
        /// 章节内容
        /// </summary>
        public string Content { get; set; }
    }
}
