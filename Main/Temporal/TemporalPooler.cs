using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.Misc;

namespace Main.Temporal
{
    public class TemporalPooler
    {
        #region Fields

        private TemporalPoolerInputPipe m_temporalPoolerInputPipe;
        private Parameters m_parameters;

        #endregion

        #region Methods

        public void Process()
        {
        }

        #endregion

        #region Instance

        public TemporalPooler(TemporalPoolerInputPipe temporalPoolerInputPipe, Parameters parameters)
        {
            #region Argument Check

            if (temporalPoolerInputPipe == null)
            {
                throw new ArgumentNullException("temporalPoolerInputPipe");
            }

            #endregion

            m_temporalPoolerInputPipe = temporalPoolerInputPipe;
            m_parameters = parameters;
        }

        #endregion
    }
}
