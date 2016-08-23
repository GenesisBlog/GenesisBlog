using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCBlog.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Author { get; set; }

        [Required]
        public string Body { get; set; }


        [Required]
        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}