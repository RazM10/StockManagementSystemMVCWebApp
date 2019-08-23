using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using StockManagementSystemMVCWebApp.Models;

namespace StockManagementSystemMVCWebApp.Gateway
{
    public class ItemGateway : BaseGateway
    {
        
        public int Save(Item item)
        {
            string query =
                "INSERT INTO Item(Name, CategoryId, CompanyId, AvailableQuantity, ReorderLevel) VALUES(@name, @caId, @coId, @aQ, @rL)";
            SqlCommand = new SqlCommand(query, SqlConnection);
            SqlCommand.Parameters.AddWithValue("@name", item.Name);
            SqlCommand.Parameters.AddWithValue("@caId", item.CategoryId);
            SqlCommand.Parameters.AddWithValue("@coId", item.CompanyId);
            SqlCommand.Parameters.AddWithValue("@aQ", 0);
            SqlCommand.Parameters.AddWithValue("@rL", item.ReorderLevel);
            
            SqlConnection.Open();
            int rowEffect = SqlCommand.ExecuteNonQuery();
            SqlConnection.Close();
            return rowEffect;
        }

        public bool IsExistsName(string name)
        {

            string query = "SELECT * FROM Item WHERE Name=@name";
            SqlCommand = new SqlCommand(query, SqlConnection);
            SqlCommand.Parameters.AddWithValue("@name", name);
            SqlConnection.Open();
            SqlDataReader = SqlCommand.ExecuteReader();
            bool isExists = SqlDataReader.HasRows;
            SqlConnection.Close();
            return isExists;
        }

        public bool IsEmpty(string courseName)
        {
            bool isEmpty = string.IsNullOrEmpty(courseName);
            return isEmpty;
        }

        public List<Item> GetAllItems()
        {

            string query = "SELECT * FROM Item";
            SqlCommand = new SqlCommand(query, SqlConnection);
            SqlConnection.Open();
            SqlDataReader = SqlCommand.ExecuteReader();
            List<Item> itemList = new List<Item>();

            while (SqlDataReader.Read())
            {
                Item input = new Item();
                input.Id = Convert.ToInt32(SqlDataReader["Id"]);
                input.Name = SqlDataReader["Name"].ToString();
                input.CategoryId = Convert.ToInt32(SqlDataReader["CategoryId"]);
                input.CompanyId = Convert.ToInt32(SqlDataReader["CompanyId"]);
                input.AvailableQuantity = Convert.ToInt32(SqlDataReader["AvailableQuantity"]);
                input.ReorderLevel = Convert.ToInt32(SqlDataReader["ReorderLevel"]);
                itemList.Add(input);
            }
            SqlDataReader.Close();
            SqlConnection.Close();
            return itemList;
        }

        public List<Item> GetItemsByCompanyId(int comId)
        {
            string query = "SELECT * FROM Item WHERE CompanyId=@cId";
            SqlCommand = new SqlCommand(query, SqlConnection);
            SqlCommand.Parameters.AddWithValue("@cId", comId);
            SqlConnection.Open();
            SqlDataReader = SqlCommand.ExecuteReader();
            List<Item> itemList = new List<Item>();

            while (SqlDataReader.Read())
            {
                Item input = new Item();
                input.Id = Convert.ToInt32(SqlDataReader["Id"]);
                input.Name = SqlDataReader["Name"].ToString();
                input.CategoryId = Convert.ToInt32(SqlDataReader["CategoryId"]);
                input.CompanyId = Convert.ToInt32(SqlDataReader["CompanyId"]);
                input.AvailableQuantity = Convert.ToInt32(SqlDataReader["AvailableQuantity"]);
                input.ReorderLevel = Convert.ToInt32(SqlDataReader["ReorderLevel"]);
                itemList.Add(input);
            }
            SqlDataReader.Close();
            SqlConnection.Close();
            return itemList;
        }

        public Item GetInfoByItemId(int id)
        {
            string query = "SELECT * FROM Item WHERE Id=@id";
            SqlCommand = new SqlCommand(query, SqlConnection);
            SqlCommand.Parameters.AddWithValue("@id", id);
            SqlConnection.Open();
            SqlDataReader = SqlCommand.ExecuteReader();
            Item input = new Item();

            if (SqlDataReader.Read())
            {
                
                input.Id = Convert.ToInt32(SqlDataReader["Id"]);
                input.Name = SqlDataReader["Name"].ToString();
                input.CategoryId = Convert.ToInt32(SqlDataReader["CategoryId"]);
                input.CompanyId = Convert.ToInt32(SqlDataReader["CompanyId"]);
                input.AvailableQuantity = Convert.ToInt32(SqlDataReader["AvailableQuantity"]);
                input.ReorderLevel = Convert.ToInt32(SqlDataReader["ReorderLevel"]);
                
            }
            SqlDataReader.Close();
            SqlConnection.Close();
            return input;
        }

        public string UpdateQuantity(Item item)
        {
            string query = "UPDATE Item SET AvailableQuantity+=@quantity WHERE Id=@id";
            SqlCommand = new SqlCommand(query, SqlConnection);
            SqlCommand.Parameters.AddWithValue("@quantity", item.StockQuantity);
            SqlCommand.Parameters.AddWithValue("@id", item.Id);

            SqlConnection.Open();
            SqlCommand.ExecuteNonQuery();
            SqlConnection.Close();

            return "Update Successful";
        }

        public void UpdateQuantitySub(int id, int aQuantity)
        {
            string query = "UPDATE Item SET AvailableQuantity-=@quantity WHERE Id=@id";
            SqlCommand = new SqlCommand(query, SqlConnection);
            SqlCommand.Parameters.AddWithValue("@quantity", aQuantity);
            SqlCommand.Parameters.AddWithValue("@id", id);

            SqlConnection.Open();
            SqlCommand.ExecuteNonQuery();
            SqlConnection.Close();
        }

        public List<Item> GetAllItemsForReport(Item item)
        {
            string query = "SELECT * FROM Item WHERE CategoryId=@caid AND CompanyId=@cid";
            SqlCommand = new SqlCommand(query, SqlConnection);
            SqlCommand.Parameters.AddWithValue("@caid", item.CategoryId);
            SqlCommand.Parameters.AddWithValue("@cid", item.CompanyId);
            SqlConnection.Open();
            SqlDataReader = SqlCommand.ExecuteReader();
            List<Item> itemList = new List<Item>();
            int sl = 0;
            while (SqlDataReader.Read())
            {
                sl = sl + 1;
                Item input=new Item();
                input.Id = Convert.ToInt32(SqlDataReader["Id"]);
                input.Name = SqlDataReader["Name"].ToString();
                input.CategoryId = Convert.ToInt32(SqlDataReader["CategoryId"]);
                input.CompanyId = Convert.ToInt32(SqlDataReader["CompanyId"]);
                input.AvailableQuantity = Convert.ToInt32(SqlDataReader["AvailableQuantity"]);
                input.ReorderLevel = Convert.ToInt32(SqlDataReader["ReorderLevel"]);
                input.Sl = sl;
                itemList.Add(input);

            }
            SqlDataReader.Close();
            SqlConnection.Close();
            //foreach (Item i in itemList)
            //{
            //    int id = i.Id;
            //    i.CompanyName = CompanyGateway.GetCompanyName(id);
            //}
            return itemList;

        }
        public string GetItemName(int id)
        {
            string query = "SELECT * FROM Item WHERE Id=@id";
            SqlCommand = new SqlCommand(query, SqlConnection);
            SqlCommand.Parameters.AddWithValue("@id", id);
            SqlConnection.Open();
            SqlDataReader = SqlCommand.ExecuteReader();
            string name = "";

            if (SqlDataReader.Read())
            {
                name = SqlDataReader["Name"].ToString();
            }
            SqlDataReader.Close();
            SqlConnection.Close();
            return name;
        }

        public int GetCompanyId(int id)
        {
            string query = "SELECT * FROM Item WHERE Id=@id";
            SqlCommand = new SqlCommand(query, SqlConnection);
            SqlCommand.Parameters.AddWithValue("@id", id);
            SqlConnection.Open();
            SqlDataReader = SqlCommand.ExecuteReader();
            int cid = 0;

            if (SqlDataReader.Read())
            {
                cid = Convert.ToInt32(SqlDataReader["CompanyId"]);
            }
            SqlDataReader.Close();
            SqlConnection.Close();
            return cid;
        }
    }
}