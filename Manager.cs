using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PMOTestProject.Models;
using PMOTestProject.Extensions;
using PMOTestProject.Calculations;

namespace PMOTestProject
{
    public partial class Manager : Form
    {
        private IList<Item> items = new List<Item>();
        private readonly DBHandler db;
        private Dictionary<IVisitor, TextBox> visitors;
        private EditorHandler editor;

        public Manager()
        {
            InitializeComponent();
            InitializeCalulations();

            db = DBHandler.Instance;
            editor = new EditorHandler(txtName, txtPrice,txtDescription, txtQuantity);
            LoadStorage();
        }

        private void InitializeCalulations()
        {
            visitors = new Dictionary<IVisitor, TextBox> {
                { new ItemCounter(), this.txtItemCount },
                { new ItemAvgPrice(), this.txtAvgValue },
                { new ItemTotalValue(), this.txtTotalValue },
                { new ItemTotalQuantity(), this.txtTotalItemCount }
            };
    }

        private void LoadStorage()
        {
            items = db.GetData();
            this.listItems.Items.Clear();
            this.listItems.Items.AddRange(items.Select(i => i.ToListViewItem()).ToArray());
            UpdateCalculations();
        }

        private void UpdateCalculations()
        {
            foreach (var pair in visitors)
            {
                var visitor = pair.Key;
                var textBox = pair.Value;
                visitor.Reset();

                items.ToList().ForEach(i => visitor.Visit(i));
                textBox.Text = visitor.Result.ToString();
            }
        }

        private void SaveStorage()
        {
            db.SaveData(items);
        }

        private void Manager_Load(object sender, EventArgs e)
        {
            LoadStorage();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveStorage();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadStorage();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (CheckItemField(this.txtName, this.txtDescription, this.txtPrice, this.txtQuantity))
            {
                MemorizeItem(this.txtName.Text,
                    this.txtDescription.Text,
                    float.Parse(this.txtPrice.Text),
                    int.Parse(this.txtQuantity.Text));
                ResetItemEditFields();
            } else
            {
                MessageBox.Show("Please fill in at least Name, Quantity, and Price fields!");
            }
        }

        private bool CheckItemField(TextBox txtName, TextBox txtDescription, TextBox txtPrice, TextBox txtQuantity)
        {
            return !string.IsNullOrEmpty(txtName.Text)
                && !string.IsNullOrEmpty(txtPrice.Text)
                && !string.IsNullOrEmpty(txtQuantity.Text);
        }

        private void MemorizeItem(string name, string description, float price, int quantity)
        {
            var item = new Item { Name = name, Description = description, Price = price, Quantity = quantity };
            int index;
            if ((index = items.IndexOf(item)) >= 0)
            {
                items[index] = item;
            }
            else
            {
                items.Add(item);
            }
            
            SaveStorage();
            LoadStorage();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            editor.HandlePriceInput(e);
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            editor.HandleQuantityInput(e);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetItemEditFields();
        }

        private void FillItemEditFields(Item item)
        {
            editor.FillFields(item);
        }

        private void ResetItemEditFields()
        {
            editor.ResetFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listItem in this.listItems.SelectedItems) {
                this.items.Remove(listItem.ToItem());
                break;
            }

            SaveStorage();
            LoadStorage();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem listItem in this.listItems.SelectedItems)
            {
                FillItemEditFields(listItem.ToItem());
                break; 
            }
        }

        private void listItems_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            FillItemEditFields(e.Item.ToItem());
        }
    }
}
