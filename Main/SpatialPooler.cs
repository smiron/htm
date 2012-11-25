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

        public List<Column> Columns
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

        /// <summary>
        /// Average connected receptive field size of the columns.
        /// </summary>
        /// <returns></returns>
        public double InhibitionRadius
        {
            get;
            private set;
        }

        #region Parameters

        /// <summary>
        /// If the permanence value for a synapse is greater than this value, it is said to be connected.
        /// Represents the "connectedPerm" variable in the algorithm description
        /// </summary>
        public double MinPermanence
        {
            get;
            private set;
        }

        public double PermanenceInc
        {
            get;
            private set;
        }

        public double PermanenceDec
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

        public int ColumnActivityHistorySize
        {
            get;
            private set;
        }

        #endregion

        #endregion

        #region Methods

        public SpatialPoolerOutput Process(SpatialPoolerInput input)
        {
            CurrentInput = input;

            Columns.ForEach(column => column.Process());
            Columns.ForEach(column => column.PostProcess());

            InhibitionRadius = GetAverageReceptiveFieldSize();

            return null;
        }

        /// <summary>
        /// The radius of the average connected receptive field size of all the columns. 
        /// The connected receptive field size of a column includes only the connected synapses 
        /// (those with permanence values >= connectedPerm). 
        /// This is used to determine the extent of lateral inhibition between columns.
        /// </summary>
        /// <returns></returns>
        private double GetAverageReceptiveFieldSize()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Instance

        public SpatialPooler(float minPermanence, int minOverlap, int desiredLocalActivity,
            double permanenceInc, double permanenceDec, int columnActivityHistorySize)
        {
            MinPermanence = minPermanence;
            MinOverlap = minOverlap;
            DesiredLocalActivity = desiredLocalActivity;
            PermanenceInc = permanenceInc;
            PermanenceDec = permanenceDec;
            ColumnActivityHistorySize = columnActivityHistorySize;
        }

        #endregion
    }
}
