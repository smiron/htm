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
            switch (TemporalPooler.Mode)
            {
                case Mode.Inference:
                    break;
                case Mode.InferenceAndLearning:
                    {
                        ProcessInferenceAndLearning();
                        break;
                    }
            }
        }

        private void ProcessInference()
        {
            #region phase 1

            bool buPredicted = false;
            foreach (var cell in Cells)
            {
                if (cell.GetPredictiveState(Time.Prev))
                {
                    var s = cell.GetActiveSegment(ActiveMode.ActiveState, Time.Prev);
                    if (s.IsSequenceSegment)
                    {
                        buPredicted = true;
                        cell.SetIsActiveState(Time.Now, true);
                    }
                }
            }

            // if no cell was predicted to be active .. activate all cell in the column
            if (buPredicted == false)
            {
                foreach (var cell in Cells)
                {
                    cell.SetIsActiveState(Time.Now, true);
                }
            }

            #endregion

            #region phase 2

            foreach (var cell in Cells)
            {
                foreach (var segment in cell.Segments)
                {
                    if (segment.GetIsSegmentActive(ActiveMode.ActiveState, Time.Now))
                    {
                        cell.SetPredictiveState(Time.Now, true);
                    }
                }
            }

            #endregion
        }

        private void ProcessInferenceAndLearning()
        {
            #region Phase 1

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
                        cell.SetIsActiveState(Time.Now, true);
                        if (s.GetIsSegmentActive(ActiveMode.LearnState, Time.Prev))
                        {
                            icChosen = true;
                            cell.SetIsLearningState(Time.Now, true);
                        }
                    }
                }
            }

            // if no cell was predicted to be active .. activate all cell in the column
            if (buPredicted == false)
            {
                foreach (var cell in Cells)
                {
                    cell.SetIsActiveState(Time.Now, true);
                }
            }

            if (icChosen == false)
            {
                Cell cell = null;
                Segment segment = null;

                CalculateBestMatchingCellAndSegment(Time.Prev, out cell, out segment);
                cell.SetIsLearningState(Time.Now, true);

                var sUpdate = GetSegmentActiveSynapses(segment, cell, Time.Prev, true);
                sUpdate.SequenceSegment = true;
                _segmentUpdateList.Add(sUpdate);
            }

            #endregion

            #region Phase 2

            foreach (var cell in Cells)
            {
                foreach (var segment in cell.Segments)
                {
                    if (segment.GetIsSegmentActive(ActiveMode.ActiveState, Time.Now))
                    {
                        cell.SetPredictiveState(Time.Now, true);

                        var activeUpdate = GetSegmentActiveSynapses(segment, cell, Time.Now, false);
                        _segmentUpdateList.Add(activeUpdate);

                        var predSegment = GetBestMatchingSegment(cell, Time.Prev);
                        var predUpdate = GetSegmentActiveSynapses(predSegment, cell, Time.Prev, true);

                        _segmentUpdateList.Add(predUpdate);
                    }
                }
            }

            #endregion

            #region Phase 3

            foreach (var cell in Cells)
            {
                var cellSegmentUpdates = _segmentUpdateList.Where(segmentUpdate => segmentUpdate.Cell == cell).ToArray();

                if (cell.GetIsLearningState(Time.Now))
                {
                    AdaptSegments(cellSegmentUpdates, true);
                    _segmentUpdateList.RemoveAll(segmentUpdate => cellSegmentUpdates.Contains(segmentUpdate));
                }
                else if (cell.GetPredictiveState(Time.Now) == false
                        && cell.GetPredictiveState(Time.Prev) == true)
                {
                    AdaptSegments(cellSegmentUpdates, false);
                    _segmentUpdateList.RemoveAll(segmentUpdate => cellSegmentUpdates.Contains(segmentUpdate));
                }
            }

            #endregion
        }

        private void AdaptSegments(IEnumerable<SegmentUpdate> cellSegmentUpdates, bool positiveReinforcement)
        {
            throw new NotImplementedException();
        }

        private Segment GetBestMatchingSegment(Cell cell, Time time)
        {
            throw new NotImplementedException();
        }

        private void CalculateBestMatchingCellAndSegment(Time time, out Cell cell, out Segment segment)
        {
            throw new NotImplementedException();
        }

        private SegmentUpdate GetSegmentActiveSynapses(Segment segment, Cell cell, Time time, bool newSynapses = false)
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
