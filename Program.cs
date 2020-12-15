#define LOCAL

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
#if (LOCAL)
                    Database.LocalFileDB.GetInstance(@"fileDB.txt")
#else
                    new Database.LocalDB()
#endif
                )
            );
        }
    }
}
