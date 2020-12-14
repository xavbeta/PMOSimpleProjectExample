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

        private readonly string DBPath;

        private static DBHandler instance;
        public static DBHandler GetInstance(string databaseFilePath) { 
            
                if (instance == null) {
                    instance = new DBHandler(databaseFilePath);
                }

                return instance; 
            
        }

        protected DBHandler (string datafile) {
            DBPath = datafile;
        }

        public async Task<IList<Models.Item>> GetData()
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

        public async Task SaveData(IList<Models.Item> items)
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
