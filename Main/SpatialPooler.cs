using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class SpatialPooler
    {
        #region Fields

        private SpatialPoolerInput m_currentInput;

        #endregion

        #region Properties

        public IEnumerable<Column> Columns
        {
            get;
            private set;
        }

        public SpatialPoolerInput CurrentInput
        {
            get
            {
                return m_currentInput;
            }
            private set
            {
                m_currentInput = value;
            }
        }

        public double MinPermanence
        {
            get;
            private set;
        }

        public int MinOverlap
        {
            get;
            private set;
        }

        public int DesiredLocalActivity
        {
            get;
            private set;
        }

        /// <summary>
        /// Average connected receptive field size of the columns.
        /// </summary>
        /// <returns></returns>
        public double InhibitionRadius
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public SpatialPoolerOutput Process(SpatialPoolerInput input)
        {
            CurrentInput = input;

            Step1Overlap();

            return null;
        }

        private void Step1Overlap()
        {
            foreach (var column in Columns)
            {
            }
        }

        #endregion

        #region Instance

        public SpatialPooler(float minPermanence, int minOverlap, int desiredLocalActivity)
        {
            MinPermanence = minPermanence;
            MinOverlap = minOverlap;
            DesiredLocalActivity = desiredLocalActivity;
        }

        #endregion
    }
}
