using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.DAL.Models
{
    public class JSONRecipe
    {
        public string type { get; set; }
    }



    public class Result
    {
        public string item { get; set; }
        public string tag { get; set; }
    }

    public class ChemicalInput
    {
        public int amount { get; set; }
        public string tag { get; set; }
        public string  item { get; set; }
    }

    public class ItemInput
    {
        public int amount { get; set; }
        public Result ingredient { get; set; }
    }

    /// <summary>
    /// JSON Mekanism Crafting Table Repice
    /// </summary>
    public class JSONMekanismCTR : JSONRecipe
    {
        public Dictionary<string, Result> key { get; set; }
        public List<string> pattern { get; set; }
        public Result result { get; set; }
    }

    public class JSONMekanismMetallurgicInfusing : JSONRecipe
    {
        public ChemicalInput chemicalInput;

        public ItemInput itemInput;
        public List<string> pattern { get; set; }
        public Result output { get; set; }
    }
}
