using Calculator.DAL.Models;
using Calculator.MODEL;
using Calculator.MODEL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.BL
{
    public class RecipesContainer
    {
        private JSONParser parser;
        private List<CraftingTableRecipe> recipes;


        public RecipesContainer()
        {
            parser = new JSONParser();
        }

        public void LoadData()
        {
            Debug.Write("RecipesContainer.LoadData() - Start");
            recipes = new List<CraftingTableRecipe>();
            recipes = parser.GetAndParseRecipes();
            Debug.Write("RecipesContainer.LoadData() - End");
        }

        public List<RecipeCost> GetBlockCost(BlockID blockToFind)
        {
            CraftingTableRecipe recipe = recipes.First(x => x.result.blockName == blockToFind.blockName);

            return recipe != null ? recipe.RecipeCost(recipe, recipes) : null;
        }


    }
}
