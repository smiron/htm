﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Temporal
{
    public class Segment
    {
        public bool IsSequenceSegment { get; set; }

        public bool GetIsSegmentActive(ActiveMode mode, Time time)
        {
            throw new NotImplementedException();
        }
    }
}
