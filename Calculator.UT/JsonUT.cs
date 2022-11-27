using Calculator.BL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Calculator.UT
{
    [TestClass]
    public class UnitTestJSON
    {
        public class Result
        {
            public string item { get; set; }
            public string tag { get; set; }
        }

        public class Root
        {
            public string type { get; set; }
            public Dictionary<string, Result> key { get; set; }
            public List<string> pattern { get; set; }
            public Result result { get; set; }
        }


        [TestMethod]
        public void TestJSONLoading()
        {
            using (StreamReader file = File.OpenText(@"M:\Recipe-Calculator\Calculator.MODEL\recipes\crusher.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                Root movie2 = (Root) serializer.Deserialize(file, typeof(Root));
            }
        }

        [TestMethod]
        public void TestJSONLoader()
        {
            JSONLoader loader = new JSONLoader();

            loader.LoadData();


        }
    }
}
