using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Temporal
{
    public class Segment
    {
        #region Properties

        public IEnumerable<Synapse> Synapses
        {
            get;
            private set;
        }

        public bool IsSequenceSegment
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public bool GetIsSegmentActive(ActiveMode mode, Time time)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
