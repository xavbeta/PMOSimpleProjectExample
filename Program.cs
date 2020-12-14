using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMOTestProject
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(
                new Manager(
                    //Database.DBHandler.GetInstance(@"C:\Users\saver\OneDrive\Desktop\db2.txt")
                    new Database.LocalDB()
                )
            );
        }
    }
}
