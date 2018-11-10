using System;
using System.Collections.Generic;

namespace TestWork_8.Models
{
    public class Thems : Entity
    {
        public string NameThem { get; set; }
        public string ContentThems { get; set; }
        public DateTime DateCreateThem { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public List<Thems> ThemsesList { get; set; }
    }
}