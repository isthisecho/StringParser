using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Globalization;

namespace StringParse
{
    class Example
    { 
        public static void Main()
        {
            string path = Environment.CurrentDirectory;

            string fileContent = System.IO.File.ReadAllText(System.IO.Path.Combine(path, "test.txt"));

            X x = new X(fileContent);
            ShuntingYard sy = new ShuntingYard();

            while (!x.IsEnd)
          {
              x.SkipWhileWhiteSpace();
          
              if (x.TryReadBooleans("true",out string boolean) || x.TryReadBooleans("false", out boolean))
                  Console.WriteLine($"{boolean} : Boolean");
              else if (x.TryReadIdentifier(out string id))
                  Console.WriteLine($"{id} : Identifier");
              else if (x.TryReadNumber(out string n))
                  Console.WriteLine($"{n} : Number");
              else if (x.TryReadString("\"",out string str))
                  Console.WriteLine($"{str} : String");
              else if (x.TryReadOperators(out string Operator))
                {
                    Console.WriteLine($"{Operator} : Operator");
                  //  sy.Add(Operator)
                }
                
              else if (x.TryReadComments("/*", "*/", out string comment)
                    || x.TryReadComments("//", "\r\n", out comment))

                  Console.WriteLine($"{comment} : Comment");
              else
                  Console.WriteLine(x.Read());            
          }
           
        
       

        }

    }
   
}
