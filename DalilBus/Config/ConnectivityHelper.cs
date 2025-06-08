using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalilBus.Config
{
    public  class ConnectivityHelper
    {
        public static bool IsConnectedToInternet()
        {
            // Check network access
            return Connectivity.NetworkAccess == NetworkAccess.Internet;
        }
    }
}
