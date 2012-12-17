using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Temporal
{
    public class SegmentUpdate
    {
        #region Properties

        public IEnumerable<Synapse> ActiveSynapses
        {
            get;
            private set;
        }

        public bool SequenceSegment
        {
            get;
            set;
        }

        #endregion

        #region Instance

        public SegmentUpdate(IEnumerable<Synapse> activeSynapses)
        {
            ActiveSynapses = activeSynapses;
        }

        #endregion
    }
}
