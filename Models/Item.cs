using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMOTestProject.Calculations;

namespace PMOTestProject.Models
{
    public class Item: IVisitorHost
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string Picture { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) {
                return false;
            }
            
            if (obj.GetType() != this.GetType()) { 
                return base.Equals(obj); 
            }

            var other = obj as Item;

            return string.Compare(Identifier(this), 
                Identifier(other)) == 0;

        }

        private string Identifier(Item item) {
            return item.Name;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
