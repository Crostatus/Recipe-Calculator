using Calculator.DAL.Default_data;
using Calculator.DAL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Calculator.BL
{
    public class JSONLoader
    {
        private JsonSerializer serializer;
        private List<JSONMekanismCTR> recipes;

        public JSONLoader()
        {
            serializer = new JsonSerializer();
            recipes = new List<JSONMekanismCTR>();
        }

        /// <summary>
        /// WIP: For now, can only handle crafting table's recipes
        /// </summary>
        /// <returns>
        /// A list of parsed JSON objects, containing crafting recipes 
        /// </returns>
        public List<JSONMekanismCTR> LoadRecipes()
        {
            Debug.WriteLine("JSONLoader.LoadRecipes() - Start");

            recipes = new List<JSONMekanismCTR>();
            JSONMekanismCTR craftingTableRecipe = null;
            StreamReader fileStream;

            int errorAmt = 0;
            foreach (string file in Directory.EnumerateFiles(PATH.RECIPES, "*.json", SearchOption.AllDirectories))
            {
                using (fileStream = File.OpenText(file))
                {
                    try
                    {
                        Debug.WriteLine("JSONLoader.LoadRecipes() - Opening " + file);
                        craftingTableRecipe = (JSONMekanismCTR)serializer.Deserialize(fileStream, typeof(JSONMekanismCTR));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("JSONLoader.LoadRecipes() - Unable to open " + file + ": " + ex.Message);
                        errorAmt++;
                    }
                    if(craftingTableRecipe.type == TAG.CRAFTING_TABLE)  
                    {
                        recipes.Add(craftingTableRecipe);
                    }
                }
            }
            Debug.WriteLine("JSONLoader.LoadRecipes() - Loaded " + recipes.Count + " recipes succesfully");
            Debug.WriteLine("JSONLoader.LoadRecipes() - Failed to load " + errorAmt + " recipes");
            
            Debug.WriteLine("JSONLoader.LoadRecipes() - End");
            return recipes;
        }


        




    }
}
