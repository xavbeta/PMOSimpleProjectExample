using PMOTestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMOTestProject.Calculations
{
    class ItemTotalQuantity : IVisitor
    {
        private int quantity = 0;

        public float Result => quantity;

        public void Reset() => quantity = 0;

        public void Visit(Item item) => quantity += item.Quantity;
    }
}
