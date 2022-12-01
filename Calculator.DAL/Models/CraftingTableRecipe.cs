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
            result = null;          // result        == null => Raw item
            recipe = null;          // craftingTable == null => Raw item
        }

        public CraftingTableRecipe(BlockID res, CraftingTableRecipe[] ct)
        {
            this.result = res;
            recipe = ct;   
        }

        public List<RecipeCost> RecipeCost(CraftingTableRecipe recipeToEvaluate)
        {
            if (this.recipe == null || !this.recipe.Any(x => x != null))
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

        private void AddRecipeCost(List<RecipeCost> totalCost, BlockID blockToAdd)
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
                        cost = 1
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
                    AddRecipeCost(simplifiedCosts, cost.blockID);
                }
            }
            return simplifiedCosts;
        }


    }
}
