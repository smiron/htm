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

        #endregion

        #region Methods

        private IEnumerable<Synapse> GetConnectedSynapses()
        {
            return m_synapses.Where(synapse => synapse.Permanence >= m_spatialPooler.MinPermanence);
        }

        /// <summary>
        /// Implements Spatial Pooler Phase 1: Overlap
        /// </summary>
        /// <returns>The actual overlap value</returns>
        public int GetOverlap()
        {
            var overlap = GetConnectedSynapses().Sum(synapse => synapse.CurrentValue ? 1 : 0);

            return overlap < m_spatialPooler.MinOverlap
                ? 0
                : overlap * Boost;
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
