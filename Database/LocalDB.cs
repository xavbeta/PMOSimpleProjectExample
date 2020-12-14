using MySqlConnector;
using PMOTestProject.Database.Query;
using PMOTestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMOTestProject.Database
{
    class LocalDB : IDatabase
    {

        private async Task<MySqlConnection> ConnectAsync()
        {
            var connection = new MySqlConnection("server=127.0.0.1;user=root;password=;database=test_project");
            await connection.OpenAsync();
            return connection;

        }

        public async Task<IList<Item>> GetData()
        {
            using (var connection = await ConnectAsync())
            {
                var command = new MySqlCommand(GenerateSelectAllQuery(), connection);
                var reader = await command.ExecuteReaderAsync();

                List<Item> list = new List<Item>();
                while (await reader.ReadAsync())
                {
                    var i = new Item
                    {
                        Name = reader.GetString(0),
                        Description = reader.GetString(1),
                        Price = reader.GetFloat(2),
                        Quantity = reader.GetUInt16(3),
                        Picture = reader.GetString(4)
                    };

                    Console.WriteLine(i);
                    list.Add(i);
                }
                return list;
            }

        }

        public async Task SaveData(IList<Item> items)
        {
            using (var connection = await ConnectAsync())
            {
                var deleteAllCommand = new MySqlCommand(GenerateDeleteAllQuery(), connection);
                var delete = await deleteAllCommand.ExecuteNonQueryAsync();

                var insertCommand = new MySqlCommand(GenerateInsertQuery(items), connection);
                var insert = await insertCommand.ExecuteNonQueryAsync();
            }
        }

        private string GenerateDeleteAllQuery()
        {
            var builder = new DeleteAllQueryBuilder();
            builder.SetTable("items");

            return builder.Query;
        }

        private string GenerateInsertQuery(IList<Item> items)
        {
            var builder = new DeleteAllQueryBuilder();
            builder.SetTable("items");
            builder.SetColumns(new List<string>
            {
                "name", "description", "price", "quantity", "picture"
            });
            builder.AddItem(items);
            return builder.Query;

        }

        private string GenerateSelectAllQuery()
        {
            var builder = new SelectAllQueryBuilder();
            builder.SetTable("items");
            builder.SetColumns(new List<string>
            {
                "name", "description", "price", "quantity", "picture"
            });
            return builder.Query;

        }

    }

}
