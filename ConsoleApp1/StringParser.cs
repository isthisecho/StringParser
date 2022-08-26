using System;
using System.Text;
using System.Xml.Linq;

namespace StringParse
{
    class X
    {
        public string Input { get; }
        public int Index { get; set; }
        public int Length { get; }
        public bool IsEnd => Index >= Length;


        public X(string input)
        {
            Input = input;
            Index = 0;
            Length = input.Length;
        }

   
        public char Peek()
        {
            return Input[Index];
        }
        public char Peek(int offset)
        {
            return Input.ElementAtOrDefault(Index + offset);
        }
        public char Read()
        {
            if (IsEnd)
                return '\0';
            return Input[Index++];
        }
        public X SkipWhileWhiteSpace()
        {
            while (!IsEnd)
                if (char.IsWhiteSpace(Peek()))
                    Read();
                else
                    break;
            return this;
        }
        public string ReadWhileWhiteSpace()
        {
            StringBuilder sb = new StringBuilder();
            while (!IsEnd)
                if (char.IsWhiteSpace(Peek()))
                    sb.Append(Read());
                else
                    break;
            return sb.ToString();
        }
        public bool ReadWhileTrue(Func<char, bool> fn, out string str)
        {
            StringBuilder sb = new StringBuilder();

            while (!IsEnd)
                if (fn(Peek()))
                    sb.Append(Read());
                else
                    break;

            str = sb.ToString();
            return str.Length > 0;
        }
        public bool TryReadIdentifier(out string identifier)
        {
            Func<char, bool> ilkKarakerKontrolu = c => char.IsLetter(c) || c == '_';
            Func<char, bool> karakerKontrolu = c => char.IsLetterOrDigit(c) || c == '_';

            Func<char, bool> fn = ilkKarakerKontrolu;

            StringBuilder sb = new StringBuilder();

            while (!IsEnd)
            {
                if (fn(Peek()))
                    sb.Append(Read());
                else
                    break;
                fn = karakerKontrolu;
            }

            identifier = sb.ToString();
        
            return identifier.Length > 0;

        }
        public string DecodeFromUtf8(string utf8String)
        {
           
            byte[] utf8Bytes = new byte[utf8String.Length];
            for (int i = 0; i < utf8String.Length; ++i)
            {
            
                utf8Bytes[i] = (byte)utf8String[i];
            }

            return Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);
        }

        public bool TryReadNumber(out string number)
        {

            Func<char, bool> ilkKarakerKontrolu = c => char.IsDigit(c);
           // Func<char, bool> karakerKontrolu = c => char.IsPunctuation(c) || char.IsDigit(c);

            Func<char, bool> fn = ilkKarakerKontrolu;

            int currentIndex = Index;
            StringBuilder sb = new StringBuilder();

            while (!IsEnd)
            {
                if (fn(Peek()))
                    sb.Append(Read());
                else
                    break;
              //  fn = karakerKontrolu;
            }

            number = sb.ToString();
            return number.Length > 0;
        }

        public bool TryReadString(string quoteType,out string str)
        {
            StringBuilder sb = new StringBuilder();

            if (TryRead(quoteType, out string quoteSymbol))
            {
                sb.Append(quoteSymbol);
              
                while (!TryRead(quoteType, out string endStr))
                {
                    if (TryRead("\\", out string slash))
                    { 
                        sb.Append(slash);
                    }             
                    sb.Append(Read());

                }
                sb.Append(quoteSymbol);

            }


            str = sb.ToString();
            return str.Length > 0;
        }


        public bool TryReadComments(string start, string end, out string comment)
        {
            StringBuilder sb = new StringBuilder();

            if (TryRead(start, out string startStr))
            {
                while(!TryRead(end, out string endStr))
                    sb.Append(Read()); 
            }


            comment = sb.ToString();
            return comment.Length > 0;

         
        }
        public bool TryReadBooleans(string booleanCheck , out string? boolean)
        {
              int currentIndex = Index;
     

           if (TryReadIdentifier(out string id))
           {
               if (string.Equals(id, "true", StringComparison.OrdinalIgnoreCase))
               {
                   boolean = id;
                   return true;
               }
               else if (string.Equals(id, "false", StringComparison.OrdinalIgnoreCase))
               {
                   boolean = id;
                   return true;
               }
           }

           Index = currentIndex;
           boolean = null;
           return false;   
        }
        public bool TryReadOperators(out string symbol)
        {
            Func<char, bool> operatorKontrol = c =>  c == '+' || c == '-' || c == '*' || c == '/' || c == '^';
            StringBuilder sb = new StringBuilder();

            while (!IsEnd)
                if (operatorKontrol(Peek()))
                    sb.Append(Read());
                else
                    break;

            symbol = sb.ToString();
            return symbol.Length > 0;
        }


        
        public bool Try(char c)
        {
            return Peek() == c;
        }
        public bool Try(string str, out string r, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < str.Length; i++)
                sb.Append(Peek(i));

            r = sb.ToString();
            return string.Equals(str, r, comparison);
        }

        public bool Try(string str, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return Try(str, out _, comparison);
        }

        public bool TryRead(string str, out string r, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (Try(str, out r))
            {
                Index += str.Length;
                return true;
            }
            return false;
        }


    }

   

}


