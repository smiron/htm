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
        private ColumnReceptiveField m_columnReceptiveField;
        private Parameters m_parameters;
        
        private double m_activeDutyCycle;
        private double m_stagingActiveDutyCycle;
        private double m_boost;

        /// <summary>
        /// A sliding average representing how often column c has had significant overlap (i.e. greater than minOverlap) 
        /// with its inputs (e.g. over the last 1000 iterations).
        /// </summary>
        private double m_overlapDutyCycle;

        #endregion

        #region Properties

        public int ColumnX
        {
            get;
            private set;
        }

        public int ColumnY
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        private IEnumerable<Synapse> GetConnectedSynapses()
        {
            return m_columnReceptiveField.SynapseList.Where(synapse => synapse.Permanence >= m_parameters.MinPermanence);
        }

        private int GetMinLocalActivity()
        {
            return kthScore(GetNeighbors(), m_parameters.DesiredLocalActivity);
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
            double minX = Math.Max(ColumnX - m_spatialPooler.InhibitionRadius, 0);
            double maxX = Math.Min(ColumnX + m_spatialPooler.InhibitionRadius, m_spatialPooler.Input.Values.ColumnCount);

            double minY = Math.Max(ColumnY - m_spatialPooler.InhibitionRadius, 0);
            double maxY = Math.Min(ColumnY + m_spatialPooler.InhibitionRadius, m_spatialPooler.Input.Values.RowCount);

            return m_spatialPooler.Columns.
                Where(column => column != this
                                && column.ColumnX >= minX
                                && column.ColumnX < maxX
                                && column.ColumnY >= minY
                                && column.ColumnY < maxY);
        }

        /// <summary>
        /// Implements Spatial Pooler Phase 1: Overlap
        /// </summary>
        /// <returns>The actual overlap value</returns>
        private int GetOverlap()
        {
            var overlap = GetConnectedSynapses().Sum(synapse => synapse.CurrentValue ? 1 : 0);

            return overlap < m_parameters.MinOverlap
                   ? 0
                   : (int)(overlap * m_boost);
        }

        /// <summary>
        /// Implements Spatial Pooler Phase 2: Inhibition
        /// </summary>
        /// <returns></returns>
        private bool GetActive()
        {
            var overlap = GetOverlap();

            return overlap > 0
                   && overlap >= GetMinLocalActivity();
        }

        public void Process()
        {
            foreach (var synapse in m_columnReceptiveField.SynapseList)
            {
                synapse.Process();
            }

            var minDutyCycle = 0.01 * GetMaxDutyCycle(GetNeighbors());

            m_stagingActiveDutyCycle = GetUpdatedActiveDutyCycle();

            m_boost = GetUpdatedBoost(m_stagingActiveDutyCycle, minDutyCycle);


            m_overlapDutyCycle = GetUpdatedOverlapDutyCycle();
            if (m_overlapDutyCycle < minDutyCycle)
            {
                IncreasePermanences(0.1 * m_parameters.MinPermanence);
            }
        }

        /// <summary>
        /// Increase the permanence value of every synapse in column c by a scale factor s.
        /// </summary>
        /// <param name="amount"></param>
        private void IncreasePermanences(double scale)
        {
            foreach (var synapse in m_columnReceptiveField.SynapseList)
            {
                synapse.IncreasePermanence(scale);
            }
        }

        /// <summary>
        /// Returns the boost value of a column. 
        /// The boost value is a scalar >= 1. If activeDutyCyle(c) is above minDutyCycle(c), the boost value is 1. 
        /// The boost increases linearly once the column's activeDutyCyle starts falling below its minDutyCycle.
        /// </summary>
        /// <returns></returns>
        private double GetUpdatedBoost(double activeDutyCycle, double minDutyCycle)
        {
            return activeDutyCycle > minDutyCycle
                   ? 1
                   : minDutyCycle / activeDutyCycle;
        }

        /// <summary>
        /// Computes a moving average of how often column c has overlap greater than minOverlap.
        /// </summary>
        /// <returns></returns>
        private double GetUpdatedOverlapDutyCycle()
        {
            return (m_overlapDutyCycle * (m_parameters.ColumnActivityHistorySize - 1) + (GetActive() ? 1 : 0)) / m_parameters.ColumnActivityHistorySize;
        }

        /// <summary>
        /// Call this function after all the columns have been procesed
        /// </summary>
        public void PostProcess()
        {
            // update ActiveDutyCicle according to the latest GetActive() value
            m_activeDutyCycle = m_stagingActiveDutyCycle;
        }

        private double GetUpdatedActiveDutyCycle()
        {
            return (m_activeDutyCycle * (m_parameters.ColumnActivityHistorySize - 1) + (GetActive() ? 1 : 0)) / m_parameters.ColumnActivityHistorySize;
        }

        /// <summary>
        /// Returns the maximum active duty cycle of the columns in the given list of columns.
        /// </summary>
        /// <param name="neighbors"></param>
        /// <returns></returns>
        private double GetMaxDutyCycle(IEnumerable<Column> neighbors)
        {
            return neighbors.Select(neighbor => neighbor.m_activeDutyCycle).Max();
        }

        #endregion

        #region Instance

        public Column(SpatialPooler spatialPooler, Parameters parameters, 
            ColumnReceptiveField columnReceptiveFieldPipe, int columnX, int columnY)
        {
            m_spatialPooler = spatialPooler;
            m_parameters = parameters;
            m_columnReceptiveField = columnReceptiveFieldPipe;

            ColumnX = columnX;
            ColumnY = columnY;
        }

        #endregion
    }
}
