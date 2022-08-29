using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringParse
{
    class ShuntingYard
    {
        //public string Input { get; set; }

        //public ShuntingYard(string input)
        //{
        //    Input = input;
        //}
        Stack<char> stack = new Stack<char>() { };
        Queue<char> queue = new Queue<char>();
        Dictionary<char,int> OpsPrecedence = new Dictionary<char,int>()
        {
            {'-',1},
            {'+',1},
            {'*',2},
            {'/',2},
            {'^',3},
        };


        public void Add(char c)
        {
            if (stack.Count == 0)
                stack.Push(c);
            

            else if (OpsPrecedence.TryGetValue(c, out int val1) && OpsPrecedence.TryGetValue(stack.Peek(), out int val2))
            {
                if (val1 <= val2)
                    stack.Push(c);
                queue.Enqueue(stack.Pop());
            }
            else if (char.IsDigit(c))
                queue.Enqueue(c);                            
        }
        public string  Print()
        {
            StringBuilder sb = new StringBuilder();
            while (queue.Count <= 0)
            {
                sb.Append(queue.Dequeue()); 
            }
            return sb.ToString();

         
        }

    




                











    }


}
