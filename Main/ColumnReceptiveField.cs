using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class ColumnReceptiveField
    {
        #region Fields

        private SpatialPoolerInputPipe m_spatialPoolerinputPipe;
        private Parameters m_parameters;
        private Random m_random;

        private int[] m_coordonatesXMap;
        private int[] m_coordonatesYMap;

        #endregion

        #region Properties

        public int Width
        {
            get
            {
                return m_coordonatesXMap.Length;
            }
        }

        public int Height
        {
            get
            {
                return m_coordonatesYMap.Length;
            }
        }

        #endregion

        #region Methods

        public Synapse[] GetSynapses()
        {
            var ret = new Synapse[Height * Width];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    ret[y * Width + x] = new Synapse(m_parameters, this, x, y, GetRandomPermanence());
                }
            }

            return ret;
        }

        public bool GetInputValue(Synapse synapse)
        {
            return m_spatialPoolerinputPipe.Values[m_coordonatesXMap[synapse.SynapseX], m_coordonatesYMap[synapse.SynapseY]];
        }

        private double GetRandomPermanence()
        {
            var ind = m_random.NextDouble();

            if (ind < 0.4)
            {
                return m_parameters.MinPermanence - m_parameters.PermanenceDec;
            }
            if (ind > 0.6)
            {
                return m_parameters.MinPermanence + m_parameters.PermanenceInc;
            }

            return m_parameters.MinPermanence;
        }

        #endregion

        #region Instance

        public ColumnReceptiveField(SpatialPoolerInputPipe spatialPoolerInputPipe,
            Parameters parameters, IEnumerable<int> coordonatesXMap, IEnumerable<int> coordonatesYMap)
        {
            m_random = new Random();

            m_spatialPoolerinputPipe = spatialPoolerInputPipe;
            m_parameters = parameters;

            m_coordonatesXMap = coordonatesXMap.ToArray();
            m_coordonatesYMap = coordonatesYMap.ToArray();

            //var synapses = new Synapse[Height, Width];
            //var synapseList = new Synapse[Height * Width];

            //for (int y = 0; y < Height; y++)
            //{
            //    for (int x = 0; x < Width; x++)
            //    {
            //        var synapse = new Synapse(m_parameters, this, x, y, GetRandomPermanence());
            //        synapses[y, x] = synapse;
            //        synapseList[y * Width + x] = synapse;
            //    }
            //}

            //Synapses = synapses;
        }

        #endregion
    }
}
