using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StockManagementSystemMVCWebApp.Gateway;
using StockManagementSystemMVCWebApp.Models;

namespace StockManagementSystemMVCWebApp.Manager
{
    public class StockOutManager
    {
        public StockOutGateway StockOutGateway { get; set; }

        public StockOutManager()
        {
            StockOutGateway=new StockOutGateway();
        }
        public List<StockOut> GetReportDates(string fdate, string tdate)
        {
            return StockOutGateway.GetReportDates(fdate, tdate);
        }

        public string Save(StockOut stockOut)
        {
            if (StockOutGateway.Save(stockOut) > 0)
            {
                return "Saved";
            }
            else
            {
                return "Failed";
            }
        }
    }
}