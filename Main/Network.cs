﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class Network
    {
        #region Fields

        private SpatialPooler m_spatialPooler;
        private TemporalPooler m_temporalPooler;

        #endregion

        #region Properties

        public SpatialPoolerInputPipe Input
        {
            get;
            private set;
        }

        public Parameters Parameters
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Function used to make one iteration
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void Process()
        {
            m_spatialPooler.Process();

            m_temporalPooler.Process();
        }

        #endregion

        #region Instance

        public Network(SpatialPoolerInputPipe input, int columnCountWidth, int columnCountHeight,
            float minPermanence, int minOverlap, int desiredLocalActivity,
            double permanenceInc, double permanenceDec, int columnActivityHistorySize)
        {
            Input = input;

            Parameters = new Parameters
                (minPermanence, minOverlap, desiredLocalActivity,
                permanenceInc, permanenceDec, columnActivityHistorySize,
                columnCountWidth, columnCountHeight);

            m_spatialPooler = new SpatialPooler(input, Parameters);
        }

        #endregion
    }
}
