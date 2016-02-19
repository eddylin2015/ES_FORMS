using System;
using System.Collections.Generic;
using System.Text;

namespace ES_FORMS.Publib
{
    public class Tools{
        private static ESLog _log = null;
        public static ESLog LogInst{
            set {
                if (_log == null)
                {
                    _log = value;
                }
            }
            get { return _log; }
        }
        public static void log(String msg)
        {
            if ( _log == null)
            {
                _log = new ESLog();
            }
            _log.log(msg);
        }
    }
    public class ESLog
    {
        public virtual void log(String message)
        {

        }
    }
    class G_ERROR
    {
        /// <summary>
        /// �Τ���~
        /// </summary>
        public static readonly string LoginUser = "�Τ���~";
        /// <summary>
        /// �K�X���~
        /// </summary>
        public static readonly string LoginPassword = "�K�X���~";
    }
}
