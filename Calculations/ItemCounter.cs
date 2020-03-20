using PMOTestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMOTestProject.Calculations
{
    class ItemCounter : IVisitor
    {
        private int count = 0;

        public float Result
        { 
            get => count; 
        }

        public void Reset() => count = 0;

        public void Visit(Item item) => count++;
        
    }
}
