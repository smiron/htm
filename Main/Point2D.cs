using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class Point2D
    {
        #region Properties

        public int X { get; private set; }

        public int Y { get; private set; }

        #endregion

        #region Instance

        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion
    }
}
