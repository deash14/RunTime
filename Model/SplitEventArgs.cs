using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SplitEventArgs
    {
        public List<Split> SplitList { get; private set; }

        public SplitEventArgs(List<Split> splitList)
        {
            SplitList = splitList;
        }
    }
}
