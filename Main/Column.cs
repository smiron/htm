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
        private List<Synapse> m_synapses;

        #endregion

        #region Methods

        private IEnumerable<Synapse> GetConnectedSynapses()
        {
            return m_synapses.Where(synapse => synapse.Permanence >= m_spatialPooler.Network.MinPermanence);
        }

        public int GetOverlap()
        {
            return 0;
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
