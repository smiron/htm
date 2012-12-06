using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.Misc;

namespace Main.Temporal
{
    public class TemporalPoolerInputPipe
    {
        public IEnumerable<Point2D> ActiveColumns
        {
            get;
            set;
        }
    }
}
