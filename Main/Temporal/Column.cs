using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.Misc;

namespace Main.Temporal
{
    public class Column
    {
        #region Properties

        public TemporalPooler TemporalPooler
        {
            get;
            private set;
        }

        public Parameters Parameters
        {
            get;
            private set;
        }

        public int Location
        {
            get;
            private set;
        }

        public IEnumerable<Cell> Cells
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public void Process()
        {
            bool buPredicted = false;
            bool icChosen = false;

            foreach (var cell in Cells)
            {

            }
        }

        #endregion

        #region Instance

        public Column(TemporalPooler temporalPooler, Parameters parameters, Point2D location)
        {
        }

        #endregion
    }
}
