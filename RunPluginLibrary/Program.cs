using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PluginLibrary.Extensions;

namespace PluginLibrary
{
    public class Program
    {
        public int size;
        public string types;

       static void Main(string[] args)
        {
            Program p = new Program();
            p.size = 100;
            p.types = "test";
            Console.WriteLine("Real object size={{0}}, types={{1}}", p.size,p.types);
            Program b= (Program)p.Clone();
            b.size = 1;
            b.types = "tset";
            Console.WriteLine(" = Changing object = set size=1 and types=\"tset\"");
            Console.WriteLine("Cloned object size={{0}}, types={{1}}", b.size, b.types);

            #region testing of clone primitives and array 1 Task
            var str = "str fght".Clone();
            int sdff = 1;
            var arr2 = sdff.Clone();
            string[] arrr= new string[]{"12","23","34","56"};
            var arrCopy = arrr.Clone();
            #endregion

            #region 5 Task
            var result = ExtensionsMettods.GenerateRandom(10,100,0);
           foreach(var val in result.Result){
               Console.WriteLine("Elem val={0}", val);
            } 
            Console.ReadLine();
            #endregion
        }
    }
}
