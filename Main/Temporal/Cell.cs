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

        public IEnumerable<Segment> Segments
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public bool GetIsActiveState(Time time)
        {
            throw new NotImplementedException();
        }

        public void SetIsActiveState(Time time, bool value)
        {
            throw new NotImplementedException();
        }

        public void SetIsLearningState(Time time, bool value)
        {
            throw new NotImplementedException();
        }

        public bool GetIsLearningState(Time time)
        {
            throw new NotImplementedException();
        }

        public bool GetPredictiveState(Time time)
        {
            throw new NotImplementedException();
        }

        public bool SetPredictiveState(Time time, bool value)
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
