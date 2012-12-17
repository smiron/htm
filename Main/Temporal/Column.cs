using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.Misc;

namespace Main.Temporal
{
    public class Column
    {
        #region Fields

        private List<SegmentUpdate> _segmentUpdateList;
        private TemporalPooler _temporalPooler;
        private Parameters _parameters;
        private Point2D _location;

        #endregion

        #region Properties

        public TemporalPooler TemporalPooler
        {
            get;
            private set;
        }

        public Parameters Parameters
        {
            get;
            private set;
        }

        public int Location
        {
            get;
            private set;
        }

        public IEnumerable<Cell> Cells
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public void Process()
        {
            bool buPredicted = false;
            bool icChosen = false;

            foreach (var cell in Cells)
            {
                if (cell.GetPredictiveState(Time.Prev))
                {
                    var s = cell.GetActiveSegment(ActiveMode.ActiveState, Time.Prev);
                    if (s.IsSequenceSegment)
                    {
                        buPredicted = true;
                        cell.IsActiveState = true;
                        if (s.GetIsSegmentActive(ActiveMode.LearnState, Time.Prev))
                        {
                            icChosen = true;
                            cell.SetIsLearningState(Time.Current, true);
                        }
                    }
                }
            }

            // if no cell was predicted to be active .. activate all cell in the column
            if (buPredicted == false)
            {
                foreach (var cell in Cells)
                {
                    cell.IsActiveState = true;
                }
            }

            if (icChosen == false)
            {
                Cell cell = null;
                Segment segment = null;

                CalculateBestMatchingCellAndSegment(Time.Prev, out cell, out segment);
                cell.SetIsLearningState(Time.Current, true);

                var sUpdate = GetSegemntActiveSynapses(segment, cell, true);
                sUpdate.SequenceSegment = true;
            }
        }

        private void CalculateBestMatchingCellAndSegment(Time time, out Cell cell, out Segment segment)
        {
            throw new NotImplementedException();
        }

        private SegmentUpdate GetSegemntActiveSynapses(Segment segment, Cell cell, bool newSynapses = false)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Instance

        public Column(TemporalPooler temporalPooler, Parameters parameters, Point2D location)
        {
            _segmentUpdateList = new List<SegmentUpdate>();
            _temporalPooler = temporalPooler;
            _parameters = parameters;
            _location = location;
        }

        #endregion
    }
}
