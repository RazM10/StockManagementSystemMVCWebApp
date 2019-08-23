using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using StockManagementSystemMVCWebApp.Models;

namespace StockManagementSystemMVCWebApp.Gateway
{
    public class StockOutGateway:BaseGateway
    {
        public ItemGateway ItemGateway { get; set; }

        public StockOutGateway()
        {
            ItemGateway=new ItemGateway();
        }
        public List<StockOut> GetReportDates(string fdate, string tdate)
        {
            string query = "SELECT ItemId,SUM(Quantity) AS Total FROM StockOut WHERE Date BETWEEN @from AND @to AND OutAction='Sell' GROUP BY ItemId";
            SqlCommand = new SqlCommand(query, SqlConnection);
            SqlCommand.Parameters.AddWithValue("@from", fdate);
            SqlCommand.Parameters.AddWithValue("@to", tdate);
            SqlConnection.Open();
            SqlDataReader = SqlCommand.ExecuteReader();
            List<StockOut> stockOutList = new List<StockOut>();
            while (SqlDataReader.Read())
            {
                StockOut aStockOut = new StockOut();
                aStockOut.ItemId = Convert.ToInt32(SqlDataReader["ItemId"]);
                //aStockOut.ItemName = aItemGateway.GetItemNameById(aStockOut.ItemId);
                //aStockOut.CompanyName = aItemGateway.GetCompanyNameByItemId(aStockOut.ItemId);
                aStockOut.Quantity = Convert.ToInt32(SqlDataReader["Total"]);

                stockOutList.Add(aStockOut);
            }
            SqlDataReader.Close();
            SqlConnection.Close();
            return stockOutList;
        }

        public int Save(StockOut stockOut)
        {
            stockOut.Date = DateTime.Now.ToString("yyyy-MM-dd");
            ItemGateway.UpdateQuantitySub(stockOut.ItemId, stockOut.Quantity);
            string query = "INSERT INTO StockOut(ItemId,Quantity,Date,OutAction) VALUES(@id,@quantity,@date,@out)";
            SqlCommand = new SqlCommand(query, SqlConnection);
            SqlCommand.Parameters.AddWithValue("@id", stockOut.ItemId);
            SqlCommand.Parameters.AddWithValue("@quantity", stockOut.Quantity);
            SqlCommand.Parameters.AddWithValue("@date", stockOut.Date);
            SqlCommand.Parameters.AddWithValue("@out", stockOut.OutAction);

            SqlConnection.Open();
            int rowEffect=SqlCommand.ExecuteNonQuery();
            SqlConnection.Close();
            return rowEffect;
        }
    }
}