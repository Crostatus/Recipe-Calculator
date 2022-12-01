using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MODEL.Models
{
    public class BlockID
    {
        public string modSource { get; set; }
        public string blockName { get; set; }

        public BlockID(string modSource, string blockName)
        {
            this.modSource = modSource;
            this.blockName = blockName;
        }

        public BlockID()
        {

        }
    }
}
