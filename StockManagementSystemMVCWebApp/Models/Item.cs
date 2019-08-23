using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StockManagementSystemMVCWebApp.Models
{
    public class Item
    {
        public int Id { get; set; }
        //[Required]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please provide Category")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Please provide Company")]
        public int CompanyId { get; set; }
        [Required]
        public int AvailableQuantity { get; set; }
        [Required]
        public int ReorderLevel { get; set; }
        [Required]
        public int StockQuantity { get; set; }
        public int Stockout { get; set; }
        public int Sl { get; set; }
        public string CompanyName { get; set; }
    }
}