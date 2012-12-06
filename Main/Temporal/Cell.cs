using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Temporal
{
    public class Cell
    {
        #region Properties

        private bool PredictiveState
        {
            get;
            private set;
        }

        public IEnumerable<Segment> Segments
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public Segment GetActiveSegment(ActiveSegmentMode mode)
        {
        }

        #endregion
    }
}
