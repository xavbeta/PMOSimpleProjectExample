using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PMOTestProject.Models;

namespace PMOTestProject.Extensions
{
    public static class ItemExtensions
    {
        public static ListViewItem ToListViewItem(this Item item)
        {
            string[] row = { item.Name, item.Price.ToString(), item.Quantity.ToString(), item.Description, item.Picture };
            return new ListViewItem(row);

        }

        public static Item ToItem(this ListViewItem item)
        {
            return new Item
            {
                Name = item.SubItems[0].Text,
                Price = float.Parse(item.SubItems[1].Text),
                Quantity = int.Parse(item.SubItems[2].Text),
                Description = item.SubItems[3].Text,
                Picture = item.SubItems[4].Text,
            };

        }
    }
}
