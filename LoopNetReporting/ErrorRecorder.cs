using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopNetReporting
{
    public static class DataRecorder
    {
        private static List<string> errorStrings = new List<string>();
        public static void RecordInfo(params string[] infoToRecord)
        {
            foreach (string theInfoToRecord in infoToRecord)
                errorStrings.Add(theInfoToRecord);
        }
        //
    }
}
