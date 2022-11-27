using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.DAL.Models.JSON_Items
{
    public class Result
    {
        public string item { get; set; }
        public string tag { get; set; }
    }

    /// <summary>
    /// Mekanism Crafting Table Recipe JSON
    /// </summary>
    public class MekanismCTRecipeJSON   
    {
        public string type { get; set; }
        public Dictionary<string, Result> key { get; set; }
        public List<string> pattern { get; set; }
        public Result result { get; set; }
    }
}
