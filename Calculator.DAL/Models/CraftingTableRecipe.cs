using Calculator.DAL.Default_data;
using Calculator.MODEL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.DAL.Models
{
    public class CraftingTableRecipe //: IRecipe
    {
        public BlockID result { get; set; }

        public CraftingTableRecipe[] recipe { get; set; }

        public CraftingTableRecipe()
        {
        }

        public CraftingTableRecipe(BlockID res)
        {
            this.result = res;
            result = null;          // result        == null => Raw item
            recipe = null;   // craftingTable == null => Raw item
        }

        public CraftingTableRecipe(BlockID res, CraftingTableRecipe[] ct)
        {
            this.result = res;
            recipe = ct;   
        }



    }
}
