using PMOTestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMOTestProject.Calculations
{
    class ItemAvgPrice : IVisitor
    {
        private List<float> values = new List<float>();

        public float Result
        {
            get => values.Count > 0 ? values.Average() : 0;
        }

        public void Reset() => values.Clear();

        public void Visit(Item item) => values.Add(item.Price);
    }
}
