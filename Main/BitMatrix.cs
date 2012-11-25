﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class BitMatrix
    {
        #region Fields

        private List<BitArray> _rows;

        #endregion

        #region Properties

        public bool this[int x, int y]
        {
            get
            {
                CheckParameters(x, y);

                return _rows[y][x];
            }
            set
            {
                CheckParameters(x, y);

                _rows[y][x] = value;
            }
        }

        #endregion

        #region Methods

        private void CheckParameters(int x, int y)
        {
            if (y < 0 || y >= _rows.Count)
            {
                throw new ArgumentOutOfRangeException("y");
            }
            if (x < 0 || x >= _rows.First().Count)
            {
                throw new ArgumentOutOfRangeException("x");
            }
        }

        #endregion

        #region Instance

        public BitMatrix(int numRows, int numColumns)
        {
            _rows = new List<BitArray>();
            for (int i = 0; i < numRows; i++)
            {
                _rows.Add(new BitArray(numColumns));
            }
        }

        #endregion
    }
}