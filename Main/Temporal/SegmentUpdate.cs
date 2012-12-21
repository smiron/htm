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

        public Cell Cell
        {
            get;
            private set;
        }

        public Segment Segment
        {
            get;
            private set;
        }

        #endregion

        #region Instance

        public SegmentUpdate(Cell cell, Segment segment, IEnumerable<Synapse> activeSynapses)
        {
            Cell = cell;
            ActiveSynapses = activeSynapses;
            Segment = segment;
        }

        #endregion
    }
}
