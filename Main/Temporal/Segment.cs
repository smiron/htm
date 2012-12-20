﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Temporal
{
    public class Segment
    {
        #region Properties

        public IEnumerable<Synapse> Synapses
        {
            get;
            private set;
        }

        public bool IsSequenceSegment
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// segmentActive(s, t, state)
        ///     This routine returns true if the number of connected synapses on segment s that are active due 
        ///     to the given state at time t is greater than activationThreshold. 
        ///     The parameter state can be activeState, or learnState.
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool GetIsSegmentActive(ActiveMode mode, Time time)
        {
            return GetIsSegmentActiveScore(mode, time) >= 0;
        }

        public int GetIsSegmentActiveScore(ActiveMode mode, Time time)
        {
            return Synapses.Count(synapse => synapse.IsConnected && synapse.GetIsActive(mode, time))
                - Network.Instance.Parameters.ActivationThreshold;
        }

        #endregion
    }
}
