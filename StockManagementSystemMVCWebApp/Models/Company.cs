using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StockManagementSystemMVCWebApp.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please provide Name")]
        [StringLength(10,MinimumLength = 3)]
        public string Name { get; set; }
        public int Sl { get; set; }
    }
}