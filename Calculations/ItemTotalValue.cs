using PMOTestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMOTestProject.Calculations
{
    class ItemTotalValue : IVisitor
    {
        private float value = 0;

        public float Result => value;

        public void Reset() => value = 0;

        public void Visit(Item item) => value += item.Price * item.Quantity;
        
    }
}
