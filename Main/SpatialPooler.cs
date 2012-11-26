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

        private SpatialPoolerInputPipe m_currentInput;
        private List<Column> m_columnList;

        #endregion

        #region Properties

        public IEnumerable<Column> Columns
        {
            get;
            private set;
        }

        public SpatialPoolerInputPipe Input
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

        public void Process()
        {
            m_columnList.ForEach(column => column.Process());
            m_columnList.ForEach(column => column.PostProcess());

            InhibitionRadius = GetAverageReceptiveFieldSize();
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

        public SpatialPooler(SpatialPoolerInputPipe input,
            int columnCountWidth, int columnCountHeight,
            float minPermanence, int minOverlap, int desiredLocalActivity,
            double permanenceInc, double permanenceDec, int columnActivityHistorySize)
        {
            Input = input;

            MinPermanence = minPermanence;
            MinOverlap = minOverlap;
            DesiredLocalActivity = desiredLocalActivity;
            PermanenceInc = permanenceInc;
            PermanenceDec = permanenceDec;
            ColumnActivityHistorySize = columnActivityHistorySize;

            m_columnList = new List<Column>();

            for (int y = 0; y < columnCountHeight; y++)
            {
                for (int x = 0; x < columnCountWidth; x++)
                {
                    var synapses = new List<Synapse>();

                    // TODO: add synapses to list

                    m_columnList.Add(new Column(this, x, y, synapses.ToArray()));
                }
            }
        }

        #endregion
    }
}
