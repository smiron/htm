using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Misc
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

        /// <summary>
        /// Used in TemporalPooler.Column.GetBestMatchingSegment
        /// </summary>
        public double AbsoluteMinPermanence
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

        public double InitialPermanence
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

        public int ColumnCountWidth
        {
            get;
            private set;
        }

        public int ColumnCountHeight
        {
            get;
            private set;
        }

        public int NumberOfCellsPerColumn
        {
            get;
            private set;
        }

        public int ActivationThreshold
        {
            get;
            private set;
        }

        /// <summary>
        /// minThreshold used in TemporalPooler.Column.GetBestMatchingSegment
        /// </summary>
        public int MinActivationThreshold
        {
            get;
            set;
        }

        #endregion

        #region Instance

        public Parameters(float minPermanence, int minOverlap, int desiredLocalActivity,
            double permanenceInc, double permanenceDec, int columnActivityHistorySize,
            int columnCountWidth, int columnCountHeight, int numberOfCellsPerColumn,
            int activationThreshold, double initialPermanence, double absoluteMinPermanence,
            int minActivationThreshold)
        {
            MinPermanence = minPermanence;
            PermanenceInc = permanenceInc;
            PermanenceDec = permanenceDec;
            MinOverlap = minOverlap;
            DesiredLocalActivity = desiredLocalActivity;
            ColumnActivityHistorySize = columnActivityHistorySize;
            ColumnCountWidth = columnCountWidth;
            ColumnCountHeight = columnCountHeight;
            NumberOfCellsPerColumn = numberOfCellsPerColumn;
            ActivationThreshold = activationThreshold;
            InitialPermanence = initialPermanence;
            AbsoluteMinPermanence = absoluteMinPermanence;
            MinActivationThreshold = minActivationThreshold;
        }

        #endregion
    }
}
