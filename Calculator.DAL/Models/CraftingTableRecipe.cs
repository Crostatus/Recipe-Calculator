using Calculator.DAL.Default_data;
using Calculator.MODEL;
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
            recipe = null;          // craftingTable == null => Raw item
        }

        public CraftingTableRecipe(BlockID res, CraftingTableRecipe[] ct)
        {
            this.result = res;
            recipe = ct;   
        }

        public List<RecipeCost> RecipeCost(CraftingTableRecipe recipeToEvaluate)
        {
            if (this.recipe == null || !this.recipe.Any(x => x != null))        // Might be "raw" block
            {   
                List<RecipeCost> totalCost = null;
                AddRecipeCost(totalCost, recipeToEvaluate.result);
                return totalCost;
            }

            var distinctBlocks = this.recipe.Where(x => x != null).GroupBy(x => x.result.blockName);

            List<List<RecipeCost>> localCosts = new List<List<RecipeCost>>();
            foreach (var block in distinctBlocks)
            {
                localCosts.Add(MultiplyRecipeCost(RecipeCost(block.First()), block.Count()));
            }

            return SimplifyRecipeCosts(localCosts);
        }

        public List<RecipeCost> RecipeCost(CraftingTableRecipe recipeToEvaluate, List<CraftingTableRecipe> allRecipes)
        {
            if (recipeToEvaluate.recipe == null || recipeToEvaluate.recipe.Where(x => x == null).Count() == 9)        // Might be "raw" block
            {
                List<CraftingTableRecipe> recipesLoadedAfter = allRecipes.FindAll(x => x.result.blockName == recipeToEvaluate.result.blockName);
                List<RecipeCost> totalCost = new List<RecipeCost>();

                if(recipesLoadedAfter == null || recipesLoadedAfter.Count < 2)
                {
                    AddRecipeCost(totalCost, recipeToEvaluate.result);
                    return totalCost;
                }
                CraftingTableRecipe final = recipesLoadedAfter.First();
                foreach (var recipe in recipesLoadedAfter)
                {
                    if(recipe.recipe != null && recipe.recipe.Where(x => x == null).Count() == 9)
                    {
                        if (recipe.result.modSource == "mekanism")
                        {
                            final = recipe;
                            break;
                        }
                    }
                }

                totalCost = RecipeCost(final, allRecipes);
                AddRecipeCost(totalCost, final.result);
                return totalCost;
            }

            var distinctBlocks = this.recipe.Where(x => x != null).GroupBy(x => x.result.blockName);

            List<List<RecipeCost>> localCosts = new List<List<RecipeCost>>();
            foreach (var block in distinctBlocks)
            {
                localCosts.Add(MultiplyRecipeCost(RecipeCost(block.First(), allRecipes), block.Count()));
            }

            return SimplifyRecipeCosts(localCosts);
        }

        private void AddRecipeCost(List<RecipeCost> totalCost, BlockID blockToAdd, int times = 1)
        {
            if(totalCost == null)
            {
                totalCost = new List<RecipeCost>();
            }
            RecipeCost cost;
            if ( (cost = totalCost.Find(x => x.blockID.blockName == blockToAdd.blockName)) == null)
            {
                totalCost.Add(
                    new RecipeCost
                    {
                        blockID = blockToAdd,
                        cost = times
                    });
            }
            else
            {
                cost.cost++;
            }
        }

        private List<RecipeCost> MultiplyRecipeCost(List<RecipeCost> totalCost, int factor)
        {
            if (totalCost == null)
            {
                return null;
            }
            foreach(RecipeCost cost in totalCost)
            {
                cost.cost *= factor;
            }
            return totalCost;
        }

        private List<RecipeCost> SimplifyRecipeCosts(List<List<RecipeCost>> recipesToSimplify)
        {
            List<RecipeCost> simplifiedCosts = new List<RecipeCost>();

            foreach(var costList in recipesToSimplify)
            {
                foreach(var cost in costList)
                {
                    AddRecipeCost(simplifiedCosts, cost.blockID, cost.cost);
                }
            }
            return simplifiedCosts;
        }


    }
}
