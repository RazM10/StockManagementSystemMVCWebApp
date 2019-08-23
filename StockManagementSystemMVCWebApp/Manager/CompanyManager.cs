using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StockManagementSystemMVCWebApp.Gateway;
using StockManagementSystemMVCWebApp.Models;

namespace StockManagementSystemMVCWebApp.Manager
{
    public class CompanyManager
    {
        public CompanyGateway CompanyGateway { get; set; }

        public CompanyManager()
        {
            CompanyGateway=new CompanyGateway();
        }

        public string Save(Company company)
        {
            if (CompanyGateway.IsEmpty(company.Name))
            {
                return "Name Cann't be null";
            }
            else if (CompanyGateway.IsExistsName(company.Name))
            {
                return "Already Exist";
            }
            else
            {
                if (CompanyGateway.Save(company) > 0)
                {
                    return "Saved Successfully";
                }
                else
                {
                    return "Saved Failed";
                }
            }
        }

        public List<Company> GetAllCompanys()
        {
            return CompanyGateway.GetAllCompanys();
        }

        public string UpdateName(Company company)
        {
            return CompanyGateway.UpdateName(company);
        }

        public string GetCompanyName(int iId)
        {
            return CompanyGateway.GetCompanyName(iId);
        }

        public string GetCompanyNameTwo(int companyId)
        {
            return CompanyGateway.GetCompanyNameTwo(companyId);
        }
    }
}