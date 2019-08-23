using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StockManagementSystemMVCWebApp.Models
{
    public class StockOut
    {
        public int Id { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string Date { get; set; }
        public string OutAction { get; set; }
        public string Msg { get; set; }
        public int Sl { get; set; }
        public string ItemName { get; set; }
        public string CompanyName { get; set; }
    }
}