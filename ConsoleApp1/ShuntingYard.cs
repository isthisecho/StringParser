using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringParse
{
    class ShuntingYard
    {
        public string Input { get; set; }

        public ShuntingYard(string input)
        {
            Input = input;
        }
        Dictionary<char,int> OpsPrecedence = new Dictionary<char,int>()
        {
            {'-',1},
            {'+',1},
            {'*',2},
            {'/',2},
            {'^',3},
        };

        public string infixToPostfix()
        {
            StringBuilder sb = new StringBuilder();
            Stack <char> stack = new Stack<char>();
            Queue <char> queue = new Queue<char>();
            foreach (var item in Input)
            {
                if (OpsPrecedence.TryGetValue(item, out int _))
                    stack.Push(item);
                else if (char.IsDigit(item))
                    queue.Enqueue(item);
            }
            while(queue.Count > 0)
            {
                sb.Append(queue.Dequeue());
            }
            while (stack.Count > 0)
            {
                sb.Append(stack.Pop());
            }


          
            return sb.ToString();
        }






    }


}
