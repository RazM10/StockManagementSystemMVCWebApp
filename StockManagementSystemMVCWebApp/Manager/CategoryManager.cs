using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StockManagementSystemMVCWebApp.Gateway;
using StockManagementSystemMVCWebApp.Models;

namespace StockManagementSystemMVCWebApp.Manager
{
    public class CategoryManager
    {
        public CategoryGateway CategoryGateway { get; set; }

        public CategoryManager()
        {
            CategoryGateway=new CategoryGateway();
        }

        public string Save(Category category)
        {
            if (CategoryGateway.IsExistsName(category.Name))
            {
                return "Already Exist";
            }
            else
            {
                if (CategoryGateway.Save(category)>0)
                {
                    return "Saved Successfully";
                }
                else
                {
                    return "Saved Failed";
                }
            }
        }

        public List<Category> GetAllCategories()
        {
            return CategoryGateway.GetAllCategories();
        }

        public string UpdateName(Category category)
        {
            return CategoryGateway.UpdateName(category);
        }
    }
}