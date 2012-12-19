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

        public Segment GetActiveSegment(ActiveMode mode, Time time)
        {
            //return Segments.FirstOrDefault(segment => segment.GetIsSegmentActive(mode));
            throw new NotImplementedException();
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
