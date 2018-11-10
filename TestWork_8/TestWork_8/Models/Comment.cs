using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestWork_8.Models
{
    public class Comment : Entity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string ThemsId { get; set; }
        public Thems Thems { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }
        public string NameThemsComment { get; set; }
    }
}
