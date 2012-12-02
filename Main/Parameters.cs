using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class Parameters
    {
        #region Properties

        /// <summary>
        /// If the permanence value for a synapse is greater than this value, it is said to be connected.
        /// Represents the "connectedPerm" variable in the algorithm description
        /// </summary>
        public double MinPermanence
        {
            get;
            set;
        }

        public double PermanenceInc
        {
            get;
            set;
        }

        public double PermanenceDec
        {
            get;
            set;
        }

        public int MinOverlap
        {
            get;
            set;
        }

        public int DesiredLocalActivity
        {
            get;
            set;
        }

        public int ColumnActivityHistorySize
        {
            get;
            set;
        }

        #endregion

        #region Instance

        public Parameters(float minPermanence, int minOverlap, int desiredLocalActivity,
            double permanenceInc, double permanenceDec, int columnActivityHistorySize)
        {
            MinPermanence = minPermanence;
            PermanenceInc = permanenceInc;
            PermanenceDec = permanenceDec;
            MinOverlap = minOverlap;
            DesiredLocalActivity = desiredLocalActivity;
            ColumnActivityHistorySize = columnActivityHistorySize;
        }

        #endregion
    }
}
