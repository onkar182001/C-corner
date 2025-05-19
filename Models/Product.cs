using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MahindraCrud.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Gender { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
    }
}
