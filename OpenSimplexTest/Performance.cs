using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSimplexTest
{
    struct Performance
    {
        public string Name { get; set; }
        public long memory;
        public long elapsedInMS { get; set; }

        public long Memory
        {
            get {
                return memory / 1024;
            }
            set { memory = value; }
        }

    }
}
