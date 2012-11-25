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
        private double m_permanence;

        #endregion

        #region Properties

        /// <summary>
        /// The X coordonate of the input
        /// </summary>
        public int SynapseX
        {
            get;
            private set;
        }

        /// <summary>
        /// The Y coordonate of the input
        /// </summary>
        public int SynapseY
        {
            get;
            private set;
        }

        public double Permanence
        {
            get
            {
                return m_permanence;
            }
            private set
            {
                lock (this)
                {
                    if (value < 0)
                    {
                        m_permanence = 0;
                    }
                    else if (value > 1)
                    {
                        m_permanence = 1;
                    }
                    else
                    {
                        m_permanence = value;
                    }
                }
            }
        }

        public bool CurrentValue
        {
            get
            {
                return m_spatialPooler.CurrentInput.Values[m_x, m_y];
            }
        }

        #endregion

        #region Methods

        public void Process()
        {
            if (CurrentValue)
            {
                Permanence += m_spatialPooler.PermanenceInc;
            }
            else
            {
                Permanence -= m_spatialPooler.PermanenceDec;
            }
        }

        /// <summary>
        /// Increase the permanence value of the synapse by a scale factor s.
        /// </summary>
        /// <param name="scale"></param>
        public void IncreasePermanence(double scale)
        {
            Permanence *= scale;
        }

        #endregion

        #region Instance

        public Synapse(SpatialPooler spatialPooler, int x, int y, double permanence)
        {
            m_spatialPooler = spatialPooler;
            m_x = x;
            m_y = y;
            Permanence = permanence;
        }

        #endregion
    }
}
