using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMOTestProject.Models;

namespace PMOTestProject.Database
{
    class DBHandler: IDatabase
    {

        private const string DBPath = @"C:\Users\saver\OneDrive\Desktop\db2.txt";

        private static DBHandler instance;
        public static DBHandler Instance { 
            get {
                if (instance == null) {
                    instance = new DBHandler();
                }

                return instance; 
            }
        }

        protected DBHandler () {}

        public IList<Models.Item> GetData()
        {
            IList<Item> items = null;
            try
            {
                JArray jsonArray = JArray.Parse(File.ReadAllText(DBPath));
                items = jsonArray.ToObject<IList<Item>>();

            } catch (Exception) {
                items = new List<Item>();
            }

            return items;
        }

        public void SaveData(IList<Models.Item> items)
        {
            JArray itemsArray = new JArray(
                items.Select(i => new JObject
                {
                    { "Name", i.Name },
                    { "Description", i.Description },
                    { "Price", i.Price },
                    { "Quantity", i.Quantity },
                    { "Picture", i.Picture },
                })
            );

            File.WriteAllText(DBPath, itemsArray.ToString());
        }

    }
}
