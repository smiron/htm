using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Temporal
{
    public class Cell
    {
        #region Fields

        private bool[] m_predictiveState;
        private bool[] m_isLearningState;
        private bool[] m_isActiveState;

        #endregion

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
            return m_isActiveState[(int)time];
        }

        public void SetIsActiveState(Time time, bool value)
        {
            m_isActiveState[(int)time] = value;
        }

        public void SetIsLearningState(Time time, bool value)
        {
            m_isLearningState[(int)time] = value;
        }

        public bool GetIsLearningState(Time time)
        {
            return m_isLearningState[(int)time];
        }

        public bool GetPredictiveState(Time time)
        {
            return m_predictiveState[(int)time];
        }

        public void SetPredictiveState(Time time, bool value)
        {
            m_predictiveState[(int)time] = value;
        }

        /// <summary>
        /// getActiveSegment(c, i, t, state)
        ///     For the given column c cell i, return a segment index such that segmentActive(s,t, state) is true. 
        ///     If multiple segments are active, sequence segments are given preference. 
        ///     Otherwise, segments with most activity are given preference.
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public Segment GetActiveSegment(ActiveMode mode, Time time)
        {
            var orderedSegments = Segments.
                Where(segment => segment.GetIsSegmentActive(mode, time)).
                OrderBy(segment => segment.GetIsSegmentActiveScore(mode, time)).ToArray();

            var firstSequenceSegment = orderedSegments.FirstOrDefault(segment => segment.IsSequenceSegment);

            return firstSequenceSegment != null
                ? firstSequenceSegment
                : orderedSegments.FirstOrDefault();
        }

        #endregion

        #region Instance

        public Cell()
        {
            m_predictiveState = new bool[2];
            m_isLearningState = new bool[2];
            m_isActiveState = new bool[2];
        }

        #endregion
    }
}
