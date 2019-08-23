using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockManagementSystemMVCWebApp.Gateway;
using StockManagementSystemMVCWebApp.Models;

namespace StockManagementSystemMVCWebApp.Manager
{
    public class ItemManager
    {
        public ItemGateway ItemGateway { get; set; }

        public ItemManager()
        {
            ItemGateway=new ItemGateway();
        }
        public string Save(Item item)
        {
            if (ItemGateway.Save(item) > 0)
            {
                return "Saved Successfully";
            }
            else
            {
                return "Saved Failed";
            }
        }

        public List<Item> GetAllItems()
        {
            return ItemGateway.GetAllItems();
        }

        public List<SelectListItem> GetAllItemsForDropDown()
        {
            List<Item> itemList = GetAllItems();
            List<SelectListItem> selectListItems = new List<SelectListItem>
            {
                new SelectListItem(){Text = "--Select--", Value = ""}
            };
            foreach (Item item in itemList)
            {
                SelectListItem selectListItem=new SelectListItem();
                selectListItem.Text = item.Name;
                selectListItem.Value = item.Id.ToString();
                selectListItems.Add(selectListItem);
            }
            return selectListItems;
        }

        public List<Item> GetItemsByCompanyId(int comId)
        {
            return ItemGateway.GetItemsByCompanyId(comId);
        }

        public Item GetInfoByItemId(int id)
        {
            return ItemGateway.GetInfoByItemId(id);
        }

        public string UpdateQuantity(Item item)
        {
            return ItemGateway.UpdateQuantity(item);
        }

        public List<Item> GetAllItemsForReport(Item item)
        {
            return ItemGateway.GetAllItemsForReport(item);
        }

        public string GetItemName(int iId)
        {
            return ItemGateway.GetItemName(iId);
        }
    }
}