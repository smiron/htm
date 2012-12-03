using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class TemporalPooler
    {
        #region Fields

        private TemporalPoolerInputPipe m_temporalPoolerInputPipe;

        #endregion

        #region Methods

        public void Process()
        {

        }

        #endregion

        #region Instance

        public TemporalPooler(TemporalPoolerInputPipe temporalPoolerInputPipe, int numberOfCellsPerColumn)
        {
            #region Argument Check

            if (temporalPoolerInputPipe == null)
            {
                throw new ArgumentNullException("temporalPoolerInputPipe");
            }

            #endregion

            m_temporalPoolerInputPipe = temporalPoolerInputPipe;
        }

        #endregion
    }
}
