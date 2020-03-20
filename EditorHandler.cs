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
        private PictureBox picBox;

        public string Name { get => txtName.Text; }
        public string Description { get => txtDescription.Text; }
        public float Price { get => float.Parse(txtPrice.Text); }
        public int Quantity { get => int.Parse(txtQuantity.Text); }
        public string ImageLocation { get => picBox.ImageLocation; }

        public EditorHandler(TextBox txtName, TextBox txtPrice, TextBox txtDescription, TextBox txtQuantity, PictureBox picBox)
        {
            this.txtName = txtName;
            this.txtPrice = txtPrice;
            this.txtDescription = txtDescription;
            this.txtQuantity = txtQuantity;
            this.picBox = picBox;
        }

        internal void ResetFields()
        {
            txtName.Text = "";
            txtPrice.Text = "";
            txtDescription.Text = "";
            txtQuantity.Text = "";
            picBox.ImageLocation = null;
        }

        internal void FillFields(Item item)
        {
            txtName.Text = item.Name;
            txtPrice.Text = item.Price.ToString();
            txtDescription.Text = item.Description;
            txtQuantity.Text = item.Quantity.ToString();
            picBox.ImageLocation = item.Picture;
        }

        internal void HandlePriceInput(KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !(e.KeyChar == ',');
        }

        internal void HandleQuantityInput(KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        internal bool CheckFields()
        {
            return !string.IsNullOrEmpty(txtName.Text)
                && !string.IsNullOrEmpty(txtPrice.Text)
                && !string.IsNullOrEmpty(txtQuantity.Text);
        }
    }
}
