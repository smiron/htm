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
                    {
                        ProcessInference();
                        break;
                    }
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

        #region Helpers

        /// <summary>
        /// adaptSegments(segmentList, positiveReinforcement)
        ///     This function iterates through a list of segmentUpdate's and reinforces each segment. 
        ///     For each segmentUpdate element, the following changes are performed. 
        ///         If positiveReinforcement is true then synapses on the active list get their permanence counts 
        ///     incremented by permanenceInc. All other synapses get their permanence counts decremented 
        ///     by permanenceDec. 
        ///         If positiveReinforcement is false, then synapses on the active list get 
        ///     their permanence counts decremented by permanenceDec. 
        ///     
        ///         After this step, any synapses in segmentUpdate that do yet exist get added with a 
        ///     permanence count of initialPerm.
        /// </summary>
        /// <param name="segmentUpdates"></param>
        /// <param name="positiveReinforcement"></param>
        private void AdaptSegments(IEnumerable<SegmentUpdate> segmentUpdates, bool positiveReinforcement)
        {
            foreach (var segmentUpdate in segmentUpdates)
            {
                // perform synapse update
                if (positiveReinforcement)
                {
                    segmentUpdate.Segment.Synapses.ToList().
                        ForEach(synapse =>
                        {
                            if (segmentUpdate.ActiveSynapses.Contains(synapse))
                            {
                                synapse.Permanence += Network.Instance.Parameters.PermanenceInc;
                            }
                            else
                            {
                                synapse.Permanence -= Network.Instance.Parameters.PermanenceDec;
                            }
                        });
                }
                else
                {
                    segmentUpdate.ActiveSynapses.ToList().ForEach
                        (synapse => synapse.Permanence -=
                        Network.Instance.Parameters.PermanenceDec);
                }

                // add missing synapses with initial permanence
                var newSynapses = segmentUpdate.ActiveSynapses.Except(segmentUpdate.Segment.Synapses).ToList();

                if (newSynapses.Any())
	            {
                    newSynapses.ForEach(synapse => synapse.Permanence = Network.Instance.Parameters.InitialPermanence);

                    segmentUpdate.Segment.AddSynapses(newSynapses);
                }
            }
        }

        /// <summary>
        /// getBestMatchingSegment(c, i, t)
        ///      For the given column c cell i at time t, find the segment with the largest number of active synapses. 
        ///      This routine is aggressive in finding the best match. 
        ///      The permanence value of synapses is allowed to be below connectedPerm. 
        ///      The number of active synapses is allowed to be below activationThreshold, but must be above minThreshold. 
        ///      The routine returns the segment index. If no segments are found, then an index of -1 is returned.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private Segment GetBestMatchingSegment(Cell cell, Time time)
        {
            return cell.Segments
                .Select(segment => 
                    new 
                    {
                        Segment = segment,
                        Score = segment.GetIsSegmentActiveScore
                            (ActiveMode.ActiveState, time,
                            Network.Instance.Parameters.AbsoluteMinPermanence,
                            Network.Instance.Parameters.MinActivationThreshold)
                    })
                .Where(item => item.Score >= 0).OrderByDescending(item => item.Score)
                .Select(item => item.Segment).FirstOrDefault();
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
