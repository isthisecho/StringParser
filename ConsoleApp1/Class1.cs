﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;

namespace ConsoleApp1
{
    class Example
    { 
        public static void Main()
        {
            System.Globalization.CultureInfo invariant = CultureInfo.InvariantCulture;
            System.Globalization.CultureInfo en = CultureInfo.GetCultureInfo("en-us");
            System.Globalization.CultureInfo tr = CultureInfo.GetCultureInfo("tr-tr");

            double d2 = double.Parse("1,250.315", invariant);
            double d1 = double.Parse("1.250,315", tr);

            string e1= d1.ToString(en);
            string e2= d1.ToString(tr);

            double pi = 3.14;

            X xx = new X("aabb");

            bool b1 = xx.Try("aa");
            bool b2 = xx.Try("bb");

            bool b0 = xx.TryRead("cc", out string xx0);
            bool b3 = xx.TryRead("aa", out string xx1);
            bool b4 = xx.TryRead("bb", out string xx2);



            //X x = new X("1 + /*comment*/ 2");
            //X  x = new X("    a   true     false   +       b  /*cmmn*/    +     1    +     2     +     xx    +     123    +     a15   ");

            string code = @"1 + /*comment*/ 2 + 3 ;//başka comment
a + b";

            X x = new X(code);
         

          while(!x.IsEnd)
          {
              x.SkipWhileWhiteSpace();
          
              if (x.TryReadBooleans(out string boolean))
                  Console.WriteLine($"{boolean} : Boolean");
              else if (x.TryReadIdentifier(out string id))
                  Console.WriteLine($"{id} : Identifier");
              else if (x.TryReadNumber(out string n))
                  Console.WriteLine($"{n} : Number");
              else if (x.TryReadOperators(out string symbol))
                  Console.WriteLine($"{symbol} : Symbol");
              else if (x.TryReadComments("/*", "*/", out string comment)
                    || x.TryReadComments("//", "\r\n", out comment))
                  Console.WriteLine($"{comment} : Comment");
              else
                  Console.WriteLine(x.Read());    
          }
        }

    }
   
}