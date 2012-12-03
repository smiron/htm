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
        private Parameters m_parameters;
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
            return m_columnList.Select(column => column.GetReceptiveFieldSize()).Average();
        }

        #endregion

        #region Instance

        public SpatialPooler(SpatialPoolerInputPipe input, Parameters parameters)
        {
            Input = input;
            m_parameters = parameters;

            m_columnList = new List<Column>();

            for (int y = 0; y < parameters.ColumnCountHeight; y++)
            {
                for (int x = 0; x < parameters.ColumnCountWidth; x++)
                {
                    // TODO: add coordonates maps for X and Y

                    m_columnList.Add(new Column(this, m_parameters,
                        new ColumnReceptiveField(input, parameters, null, null), new Point2D(x, y)));
                }
            }
        }

        #endregion
    }
}
