using PMOTestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMOTestProject
{
    class EditorHandler
    {
        private TextBox txtName;
        private TextBox txtPrice;
        private TextBox txtDescription;
        private TextBox txtQuantity;

        public EditorHandler(TextBox txtName, TextBox txtPrice, TextBox txtDescription, TextBox txtQuantity)
        {
            this.txtName = txtName;
            this.txtPrice = txtPrice;
            this.txtDescription = txtDescription;
            this.txtQuantity = txtQuantity;
        }

        internal void ResetFields()
        {
            txtName.Text = "";
            txtPrice.Text = "";
            txtDescription.Text = "";
            txtQuantity.Text = "";
        }

        internal void FillFields(Item item)
        {
            txtName.Text = item.Name;
            txtPrice.Text = item.Price.ToString();
            txtDescription.Text = item.Description;
            txtQuantity.Text = item.Quantity.ToString();
        }

        internal void HandlePriceInput(KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !(e.KeyChar == ',');
        }

        internal void HandleQuantityInput(KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
