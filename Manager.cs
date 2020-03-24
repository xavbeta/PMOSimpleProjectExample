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
using PMOTestProject.Database;

namespace PMOTestProject
{
    public partial class Manager : Form
    {
        private IList<Item> items = new List<Item>();
        private readonly IDatabase db;
        private Dictionary<IVisitor, TextBox> visitors;
        private EditorHandler editor;

        public Manager(IDatabase database)
        {
            InitializeComponent();
            InitializeCalulations();

            db = database;
            editor = new EditorHandler(txtName, txtPrice,txtDescription, txtQuantity, picBox);
            LoadStorage();
        }

        internal EditorHandler EditorHandler
        {
            get => default;
            set
            {
            }
        }

        public Item Item
        {
            get => default;
            set
            {
            }
        }

        internal DBHandler DBHandler
        {
            get => default;
            set
            {
            }
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
            if (CheckItemField())
            {
                MemorizeItem();
                ResetItemEditFields();
            } else
            {
                MessageBox.Show("Please fill in at least Name, Quantity, and Price fields!");
            }
        }

        private bool CheckItemField()
        {
            return editor.CheckFields();
        }

        private void MemorizeItem()
        {
            var item = new Item { Name = editor.Name, Description = editor.Description, Price = editor.Price, Quantity = editor.Quantity, Picture = editor.ImageLocation };
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Wrap the creation of the OpenFileDialog instance in a using statement,
            // rather than manually calling the Dispose method to ensure proper disposal
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                //For any other formats
                dlg.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
                dlg.FileName = picBox.ImageLocation ?? null;
                    
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    picBox.ImageLocation = dlg.FileName;
                }
            }
        }
    }
}
