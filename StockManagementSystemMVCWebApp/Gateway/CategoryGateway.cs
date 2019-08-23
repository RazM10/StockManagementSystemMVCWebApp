using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using StockManagementSystemMVCWebApp.Models;

namespace StockManagementSystemMVCWebApp.Gateway
{
    public class CategoryGateway : BaseGateway
    {
        public int Save(Category category)
        {
            string query =
                "INSERT INTO Category(Name) VALUES(@name)";
            SqlCommand = new SqlCommand(query, SqlConnection);
            SqlCommand.Parameters.AddWithValue("@name", category.Name);
            SqlConnection.Open();
            int rowEffect = SqlCommand.ExecuteNonQuery();
            SqlConnection.Close();
            return rowEffect;
        }

        public bool IsExistsName(string name)
        {

            string query = "SELECT * FROM Category WHERE Name=@name";
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

        public List<Category> GetAllCategories()
        {

            string query = "SELECT * FROM Category";
            SqlCommand = new SqlCommand(query, SqlConnection);
            SqlConnection.Open();
            SqlDataReader = SqlCommand.ExecuteReader();
            List<Category> categoryList = new List<Category>();
            int sl = 0;
            while (SqlDataReader.Read())
            {
                sl = sl + 1;
                Category input = new Category();
                input.Id = Convert.ToInt32(SqlDataReader["Id"]);
                input.Name = SqlDataReader["Name"].ToString();
                input.Sl = sl;
                categoryList.Add(input);
            }
            SqlDataReader.Close();
            SqlConnection.Close();
            return categoryList;
        }
        public string UpdateName(Category category)
        {
            string query = "UPDATE Category SET Name=@name WHERE Id=@id";
            SqlCommand = new SqlCommand(query, SqlConnection);
            SqlCommand.Parameters.AddWithValue("@name", category.Name);
            SqlCommand.Parameters.AddWithValue("@id", category.Id);

            SqlConnection.Open();
            SqlCommand.ExecuteNonQuery();
            SqlConnection.Close();

            return "Update Successful";
        }
        public string GetCategoryName(int id)
        {
            string query = "SELECT * FROM Category WHERE Id=@id";
            SqlCommand = new SqlCommand(query, SqlConnection);
            SqlCommand.Parameters.AddWithValue("@id", id);
            SqlConnection.Open();
            SqlDataReader = SqlCommand.ExecuteReader();
            string name="";

            if (SqlDataReader.Read())
            {
                name = SqlDataReader["Name"].ToString();
            }
            SqlDataReader.Close();
            SqlConnection.Close();
            return name;
        }
    }
}