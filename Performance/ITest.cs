using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance
{
    interface ITest
    {
        String Name { get; set; }

        String Source { get; }
    }
}
