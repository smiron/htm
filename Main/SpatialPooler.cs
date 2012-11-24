using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class SpatialPooler
    {
        #region Properties

        public IEnumerable<Column> Columns
        {
            get;
            private set;
        }

        public SpatialPoolerInput CurrentInput
        {
            get;
            private set;
        }

        public Network Network
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public SpatialPoolerOutput Process(SpatialPoolerInput input)
        {
            CurrentInput = input;

            Step1Overlap();

            return null;
        }

        private void Step1Overlap()
        {
            foreach (var column in Columns)
            {
            }
        }

        #endregion

        #region Instance

        public SpatialPooler(Network network)
        {
            Network = network;
        }

        #endregion
    }
}
