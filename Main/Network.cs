using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class Network
    {
        #region Fields

        private SpatialPooler m_spatialPooler;
        private TemporalPooler m_temporalPooler;

        #endregion

        #region Methods

        /// <summary>
        /// Function used to make one iteration
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public object Process(SpatialPoolerInput input)
        {
            return m_temporalPooler.Process(m_spatialPooler.Process(input));
        }

        #endregion

        #region Instance

        public Network(float minPermanence, int minOverlap, int desiredLocalActivity)
        {
            m_spatialPooler = new SpatialPooler(minPermanence, minOverlap, desiredLocalActivity);
        }

        #endregion
    }
}
