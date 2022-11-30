using Calculator.DAL.Default_data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.DAL.Models
{
    public class CraftingTableRecipe //: IRecipe
    {
        public string result { get; set; }
        public string tag { get; set; }
        public string type { get; set; }


        private CraftingTableRecipe[] craftingTable;

        public CraftingTableRecipe(string result, string tag)
        {
            this.result = result;
            this.tag = tag;

            craftingTable = new CraftingTableRecipe[(int)Crafting.RecipeSlot];
        }



    }
}
