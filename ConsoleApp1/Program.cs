using System;
using System.Text;
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
        return Input[Index + offset];
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
    public bool TryReadNumber(out string number)
    {
        StringBuilder sb = new StringBuilder();

        while (!IsEnd)
            if (char.IsDigit(Peek()))
                sb.Append(Read());
            else
                break;

        number = sb.ToString();
        return number.Length > 0;
    }
    public bool TryReadComments(string start, string end, out string comment)
    {
        StringBuilder sb = new StringBuilder();

        if (TryRead(start, out string startStr))
        {
            //sb.Append(startStr);
            while(!TryRead(end, out string endStr))
                sb.Append(Read());
            //sb.Append(end);
        }


        comment = sb.ToString();
        return comment.Length > 0;

        //Func<char, bool> commentControl = c => c == '/';
        //Func<char, bool> asteriskControl = c => c == '*';
        //Func<char, bool> fn = commentControl;
        //Func<char, bool> fn1 = asteriskControl;

        //StringBuilder sb = new StringBuilder();
        //
        ////while (!IsEnd)
        ////{
        ////    if (fn(Peek()) && fn1(Peek(1)))
        ////        while (!(fn1(Peek()) && fn(Peek(1))))
        ////            sb.Append(Read());
        ////    else
        ////        break;
        ////}
        //comment = sb.ToString();
        //return comment.Length > 0;
    }
    public bool TryReadBooleans(out string boolean)
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
        //Func<char, bool> ilkKarakerKontrolu = c => c =='f'|| c == 't';
        //Func<char, bool> karakerKontrolu = c => char.IsLetter(c) ;
        //Func<string, bool> booleanControl = b => b=="true" || b=="false";
        //Func<char, bool> fn = ilkKarakerKontrolu;
        //Func<string, bool> fnBool = booleanControl;
        //StringBuilder sb = new StringBuilder();
        //
        //while (!IsEnd)
        //{
        //    if (fn(Peek()) && !fnBool(sb.ToString()))
        //        sb.Append(Read());
        //    else
        //        break;
        //    fn = karakerKontrolu;
        //}
        //
        //boolean = sb.ToString();
        //return boolean.Length>0;

    }
    public bool TryReadOperators(out string symbol)
    {
        
        StringBuilder sb = new StringBuilder();

        while (!IsEnd)
            if (char.IsSymbol(Peek()))
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
    public bool Try(string str, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < str.Length; i++)
            sb.Append(Peek(i));

        return string.Equals(str, sb.ToString(), comparison);
    }
    public bool TryRead(string str, out string r, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < str.Length; i++)
            sb.Append(Peek(i));

        r = sb.ToString();
        bool rr = string.Equals(str, sb.ToString(), comparison);
        
        if(rr)
            Index+=str.Length;

        return rr;
    }


}