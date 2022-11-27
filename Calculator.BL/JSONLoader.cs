using Calculator.DAL.Default_data;
using Calculator.MODEL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.BL
{
    public class JSONLoader
    {
        public JSONLoader()
        {
            
        }

        public void LoadData()
        {
            List<JSONMekanismCTR> recipes = new List<JSONMekanismCTR>();
            JsonSerializer serializer = new JsonSerializer();
            JSONMekanismCTR craftingTableRecipe = null;

            // System.IO.DirectoryNotFoundException:
            // 'Impossibile trovare una parte del percorso
            // 'M:\Recipe-Calculator\Calculator.UT\bin\recipes'.'

            // ..\\Calculator.DAL\\recipes\\"

            //using (StreamReader file = File.OpenText(@"M:\Recipe-Calculator\Calculator.MODEL\recipes\crusher.json"))
            //foreach (string file in Directory.EnumerateFiles(PATH.RECIPES, "*.json", SearchOption.AllDirectories))
            int errorAmt = 0;
            foreach (string file in Directory.EnumerateFiles(PATH.RECIPES, "*.json", SearchOption.AllDirectories))
            {
                Console.WriteLine(file);
                using (StreamReader fileStream = File.OpenText(file))
                {
                    try
                    {
                        craftingTableRecipe = (JSONMekanismCTR)serializer.Deserialize(fileStream, typeof(JSONMekanismCTR));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        errorAmt++;
                    }
                    if(craftingTableRecipe.type == TAG.CRAFTING_TABLE)  // capire come avere un oggetto che li parsa tutti
                    {
                        recipes.Add(craftingTableRecipe);
                    }
                }
            }
        }


        




    }
}
