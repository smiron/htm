using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class Column
    {
        #region Fields

        private SpatialPooler m_spatialPooler;
        private IEnumerable<Synapse> m_synapses;

        #endregion

        #region Properties

        public int Boost
        {
            get;
            private set;
        }

        public int X
        {
            get;
            private set;
        }

        public int Y
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        private IEnumerable<Synapse> GetConnectedSynapses()
        {
            return m_synapses.Where(synapse => synapse.Permanence >= m_spatialPooler.MinPermanence);
        }

        private int GetMinLocalActivity()
        {
            return kthScore(GetNeighbors(), m_spatialPooler.DesiredLocalActivity);
        }

        /// <summary>
        /// Given the list of columns, return the k'th highest overlap value.
        /// TODO: improve code speed by eliminating the extra GetOverlap() call
        /// </summary>
        /// <param name="neighbors">The column neighbors</param>
        /// <param name="desiredLocalActivity">A parameter controlling the number of columns that will be winners after the inhibition step.</param>
        /// <returns></returns>
        private int kthScore(IEnumerable<Column> neighbors, int desiredLocalActivity)
        {
            var neighborsOverlap = neighbors.Select(neighbor => neighbor.GetOverlap()).
                OrderByDescending(overlap => overlap).ToArray();

            return neighborsOverlap.Length < desiredLocalActivity
                ? neighborsOverlap.Last()
                : neighborsOverlap.Skip(desiredLocalActivity - 1).First();
        }

        /// <summary>
        /// A list of all the columns that are within inhibitionRadius of column.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Column> GetNeighbors()
        {
            double minX = Math.Max(X - m_spatialPooler.InhibitionRadius, 0);
            double maxX = Math.Min(X + m_spatialPooler.InhibitionRadius, m_spatialPooler.CurrentInput.Values.ColumnCount);

            double minY = Math.Max(Y - m_spatialPooler.InhibitionRadius, 0);
            double maxY = Math.Min(Y + m_spatialPooler.InhibitionRadius, m_spatialPooler.CurrentInput.Values.RowCount);

            return m_spatialPooler.Columns.
                Where(column => column != this
                                && column.X >= minX
                                && column.X < maxX
                                && column.Y >= minY
                                && column.Y < maxY);
        }

        /// <summary>
        /// Implements Spatial Pooler Phase 1: Overlap
        /// </summary>
        /// <returns>The actual overlap value</returns>
        private int GetOverlap()
        {
            var overlap = GetConnectedSynapses().Sum(synapse => synapse.CurrentValue ? 1 : 0);

            return overlap < m_spatialPooler.MinOverlap
                ? 0
                : overlap * Boost;
        }

        /// <summary>
        /// Implements Spatial Pooler Phase 2: Inhibition
        /// </summary>
        /// <returns></returns>
        public bool GetActive()
        {
            var overlap = GetOverlap();

            return overlap > 0
                   && overlap >= GetMinLocalActivity();
        }

        #endregion

        #region Instance

        public Column(SpatialPooler spatialPooler)
        {
            m_spatialPooler = spatialPooler;
        }

        #endregion
    }
}
