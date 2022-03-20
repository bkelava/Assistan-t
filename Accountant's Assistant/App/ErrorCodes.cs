using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accountant_s_Assistant.App
{
    class ErrorCodes
    {
        private ErrorCodes()
        {
            //empty
        }

        public static int NoError
        {
            get
            {
                return 0;
            }
            
            private set
            {
                //empty
            }
        }

        public static int Error
        {
            get
            {
                return 1;
            }
            private set
            {
                //empty
            }
        }
    }
}
