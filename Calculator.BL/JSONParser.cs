using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.DAL.Default_data;
using Calculator.DAL.Models;
using Calculator.MODEL.Models;

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
                        recipeToAdd = new CraftingTableRecipe
                        {
                            result = ParseTagAndItem(recipe.result.tag, recipe.result.item),
                            recipe = ResolveCraftingTablePattern(recipe.pattern, recipe.key)
                        };
                    }
                    else
                    {
                        Debug.WriteLine("JSONParser.GetAndParseRecipes() - Skipping not yet supported recipe");
                        continue;
                    }
                    recipes.Add(recipeToAdd);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("JSONParser.GetAndParseRecipes() - Skipping broken recipe: " + ex.Message);
                    continue;
                }
            }

            Debug.WriteLine("JSONParser.GetAndParseRecipes() - End - Parsed " + recipes.Count + " recipes");
            return recipes; // recipe type needs to be more general and hold all different recipes, not only crafting 4 Mekanism
        }

        private CraftingTableRecipe[] ResolveCraftingTablePattern(List<string> pattern, Dictionary<string, Result> key)
        {
            Debug.WriteLine("JSONParser.ResolveCraftingTablePattern() - Start");
            CraftingTableRecipe[] result = new CraftingTableRecipe[(int)Crafting.RecipeSlot];
            
            int index = 0;
            foreach (string row in pattern)
            {
                foreach (char cell in row)
                {
                    if(index >= result.Length)
                    {
                        throw new InvalidOperationException("JSONParser.ResolveCraftingTablePattern() - Index out of bound: Index: " + index + " Max: " + result.Length);
                    }
                    else if(cell == ' ')
                    {
                        result[index] = null;
                    }
                    else
                    {
                        if (key.TryGetValue(cell.ToString(), out Result JSONKeyContent))
                        {
                            result[index] = new CraftingTableRecipe(ParseTagAndItem(JSONKeyContent.tag, JSONKeyContent.item));
                        }
                        else
                        {
                            Debug.WriteLine("JSONParser.ResolveCraftingTablePattern() - Very sus: unable to map pattern key in the 'keys' Dictionary");
                            result[index] = null;
                        }
                    }
                    index++;
                }
            }
            Debug.WriteLine("JSONParser.ResolveCraftingTablePattern() - End");
            return result;
        }

        private BlockID ParseTagAndItem(string tag, string item)
        {
            bool bTag = !string.IsNullOrEmpty(tag);
            bool bItem = !string.IsNullOrEmpty(item);

            if(bTag && bItem)
            {
                throw new InvalidOperationException("JSONParser.ParseTagAndItem() - BlocksID cannot have both tag and item! tag: " + tag + " item: " + item);
            }
            
            string stringToParse;
            if(bTag)
            {
                stringToParse = tag;
            }
            else
            {
                stringToParse = item; 
            }

            string[] vals = stringToParse.Split(':');   // Safe enough for now
            return new BlockID
            {
                modSource = vals[0],
                blockName = vals[1]
            };
        }

    }
}
