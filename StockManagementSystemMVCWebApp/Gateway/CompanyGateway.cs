using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using StockManagementSystemMVCWebApp.Models;

namespace StockManagementSystemMVCWebApp.Gateway
{
    public class CompanyGateway:BaseGateway
    {
        public ItemGateway ItemGateway { get; set; }
        public CompanyGateway()
        {
            ItemGateway=new ItemGateway();
        }
        public int Save(Company company)
        {
            string query =
                "INSERT INTO Company(Name) VALUES(@name)";
            SqlCommand = new SqlCommand(query, SqlConnection);
            SqlCommand.Parameters.AddWithValue("@Name", company.Name);
            SqlConnection.Open();
            int rowEffect = SqlCommand.ExecuteNonQuery();
            SqlConnection.Close();
            return rowEffect;
        }

        public bool IsExistsName(string name)
        {

            string query = "SELECT * FROM Company WHERE Name=@name";
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

        public List<Company> GetAllCompanys()
        {

            string query = "SELECT * FROM Company";
            SqlCommand = new SqlCommand(query, SqlConnection);
            SqlConnection.Open();
            SqlDataReader = SqlCommand.ExecuteReader();
            List<Company> companyList = new List<Company>();
            int sl = 0;
            while (SqlDataReader.Read())
            {
                sl = sl + 1;
                Company input = new Company();
                input.Id = Convert.ToInt32(SqlDataReader["Id"]);
                input.Name = SqlDataReader["Name"].ToString();
                input.Sl = sl;
                companyList.Add(input);
            }
            SqlDataReader.Close();
            SqlConnection.Close();
            return companyList;
        }

        public string UpdateName(Company company)
        {
            string query = "UPDATE Company SET Name=@name WHERE Id=@id";
            SqlCommand = new SqlCommand(query, SqlConnection);
            SqlCommand.Parameters.AddWithValue("@name", company.Name);
            SqlCommand.Parameters.AddWithValue("@id", company.Id);

            SqlConnection.Open();
            SqlCommand.ExecuteNonQuery();
            SqlConnection.Close();

            return "Update Successful";
        }
        public string GetCompanyName(int id)
        {
            int cid = ItemGateway.GetCompanyId(id);
            string query = "SELECT * FROM Company WHERE Id=@id";
            SqlCommand = new SqlCommand(query, SqlConnection);
            SqlCommand.Parameters.AddWithValue("@id", cid);
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
        public string GetCompanyNameTwo(int id)
        {
            string query = "SELECT * FROM Company WHERE Id=@id";
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
    }
}