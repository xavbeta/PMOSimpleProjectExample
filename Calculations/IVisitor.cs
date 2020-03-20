using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMOTestProject.Calculations
{
    public interface IVisitor
    {
        float Result { get; }
        void Visit(Models.Item item);
        void Reset();
    }
}
