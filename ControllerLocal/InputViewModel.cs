using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerLocal
{
    public class InputViewModel : ViewModelBase
    {
        #region Fields

        private bool m_isChecked;

        #endregion

        #region Properties

        public int Row
        {
            get;
            private set;
        }

        public int Column
        {
            get;
            private set;
        }

        public bool IsChecked
        {
            get
            {
                return m_isChecked;
            }
            set
            {
                ChangeAndNotify(ref m_isChecked, value, () => IsChecked);
            }
        }

        #endregion
    }
}
