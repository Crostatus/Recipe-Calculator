using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.DAL.Default_data;
using Calculator.DAL.Models;

namespace Calculator.BL
{
    internal class JSONParser
    {
        private JSONLoader loader;
        private List<CraftingTableRecipe> recipes;

        public JSONParser()
        {
            loader = new JSONLoader();
        }

        public List<CraftingTableRecipe> GetAndParseRecipes()
        {
            Debug.WriteLine("JSONParser.GetAndParseRecipes() - Start");

            recipes = new List<CraftingTableRecipe>();
            CraftingTableRecipe recipeToAdd;

            foreach (JSONMekanismCTR recipe in loader.LoadRecipes())
            {
                try
                {
                    if (recipe != null && recipe.type == TAG.CRAFTING_TABLE)
                    {
                        Debug.WriteLine("JSONParser.GetAndParseRecipes() - Skipping not yet supported recipe");
                        RisolveCraftingTablePattern(recipe.pattern, recipe.key);
                    }
                    else
                    {
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("JSONParser.GetAndParseRecipes() - Skipping broken recipe: " + ex.Message);
                    continue;
                }
                //recipes.Add(recipeToAdd);
            }
            Debug.WriteLine("JSONParser.GetAndParseRecipes() - Parsed " + recipes.Count + " recipes");
            Debug.WriteLine("JSONParser.GetAndParseRecipes() - End");
            return recipes; // recipe type needs to be more general and hold all different recipes, not only crafting
        }

        private CraftingTableRecipe[] RisolveCraftingTablePattern(List<string> pattern, Dictionary<string, Result> key)
        {
            CraftingTableRecipe[] result = new CraftingTableRecipe[(int)Crafting.RecipeSlot];

            int rowLenght = (int) Math.Sqrt((int)Crafting.RecipeSlot);
            bool foundInKey;
            Result JSONKeyContent;
            int colIndex = 0;
            int rowIndex = 0;
            foreach (string row in pattern)
            {
                foreach (char cell in row)
                {
                    if(cell == ' ')
                    {
                        result[colIndex % result.Length] = null;
                    }
                    else
                    {
                        foundInKey = key.TryGetValue(cell.ToString(), out JSONKeyContent);
                        if (foundInKey)
                        {
                            //result[colIndex % result.Length] = DA PARSARE
                            //
                        }
                        else
                        {
                            result[colIndex % result.Length] = null;
                        }
                    }
                    colIndex = (rowIndex * rowLenght) + (colIndex + 1);
                }
                rowIndex++;
                colIndex = 0;
            }


            return result;
        }

    }
}
