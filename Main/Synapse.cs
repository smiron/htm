using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class Synapse
    {
        #region Fields

        private SpatialPooler m_spatialPooler;
        private int m_x;
        private int m_y;

        #endregion

        #region Properties

        public double Permanence
        {
            get;
            set;
        }

        public bool CurrentValue
        {
            get
            {
                return m_spatialPooler.CurrentInput.Values[m_x, m_y];
            }
        }

        #endregion

        #region Instance

        public Synapse(SpatialPooler spatialPooler, int x, int y)
        {
            m_spatialPooler = spatialPooler;
            m_x = x;
            m_y = y;
        }

        #endregion
    }
}
