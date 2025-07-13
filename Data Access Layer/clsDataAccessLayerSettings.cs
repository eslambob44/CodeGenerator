using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    static internal class clsDataAccessLayerSettings
    {
        static public event Action<Exception> onErrorOccur;
        
        static clsDataAccessLayerSettings()
        {
            onErrorOccur += clsLogger.Log;
            clsLogger.SourceName = "Code Generator";
        }

        public static void DealingWithOnErrorOccurEvent(Exception e)
        {
            onErrorOccur?.Invoke(e);
        }

    }
}
