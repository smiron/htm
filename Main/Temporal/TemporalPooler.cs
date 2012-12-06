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

        private TemporalPoolerInputPipe m_input;
        private Parameters m_parameters;

        #endregion

        #region Properties

        public TemporalPoolerInputPipe Input
        {
            get;
            private set;
        }

        public Column[,] Columns
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public void Process()
        {
            m_input.ActiveColumns.
                Select(activeColumnLocation => Columns[activeColumnLocation.X, activeColumnLocation.Y]).ToList().
                ForEach(column => column.Process());
        }

        #endregion

        #region Instance

        public TemporalPooler(TemporalPoolerInputPipe input, Parameters parameters)
        {
            #region Argument Check

            if (input == null)
            {
                throw new ArgumentNullException("temporalPoolerInputPipe");
            }

            #endregion

            m_input = input;
            m_parameters = parameters;
        }

        #endregion
    }
}
