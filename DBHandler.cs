using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMOTestProject.Models;

namespace PMOTestProject
{
    class DBHandler
    {

        private const string DBPath = @"C:\Users\saver\OneDrive\Desktop\db.txt";

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

            } catch (Newtonsoft.Json.JsonException) {
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
