using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.DAL.Default_data
{
    public enum Crafting
    {
        RecipeSlot = 9  // CARE: needs to be a perfect square
    }

    public abstract class PATH
    {
        public static string RECIPES = "..\\recipes\\";
    }

    public abstract class TAG
    {
        public static string CRAFTING_TABLE = "minecraft:crafting_shaped";
    }



}
