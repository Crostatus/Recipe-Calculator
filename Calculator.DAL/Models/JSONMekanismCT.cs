using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MODEL.Models
{
    public class Result
    {
        public string item { get; set; }
        public string tag { get; set; }
    }

    /// <summary>
    /// JSON Mekanism Crafting Table Repice
    /// </summary>
    public class JSONMekanismCTR
    {
        public string type { get; set; }
        public Dictionary<string, Result> key { get; set; }
        public List<string> pattern { get; set; }
        public Result result { get; set; }
    }
}
