using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Temporal
{
    public class Synapse
    {
        #region Fields

        private bool[,] m_isActive;

        #endregion

        #region Properties

        public double Permanence
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public bool IsConnected(double? minPermanence = null)
        {
            return Network.Instance.Parameters.MinPermanence
                <= (minPermanence ?? Network.Instance.Parameters.MinPermanence);
        }

        public void SetIsActive(ActiveMode mode, Time time, bool value)
        {
            m_isActive[(int)mode, (int)time] = value;
        }

        public bool GetIsActive(ActiveMode mode, Time time)
        {
            return m_isActive[(int)mode, (int)time];
        }

        #endregion

        #region Instance

        public Synapse()
        {
            m_isActive = new bool[2, 2];
        }

        #endregion
    }
}
