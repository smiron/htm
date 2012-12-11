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

        public bool IsActiveState
        {
            get;
            set;
        }

        public bool IsLearnState
        {
            get;
            set;
        }

        public IEnumerable<Segment> Segments
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public bool GetPredictiveState(Time time)
        {
            throw new NotImplementedException();
        }

        public Segment GetActiveSegment(ActiveMode mode, Time time)
        {
            //return Segments.FirstOrDefault(segment => segment.GetIsSegmentActive(mode));
            throw new NotImplementedException();
        }

        #endregion
    }
}
