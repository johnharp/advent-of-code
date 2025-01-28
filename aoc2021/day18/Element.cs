using System;
using System.Text;

namespace day18
{
    public class Element
    {
        public bool IsPair = false;
        public bool IsScalar = false;

        public Element Left
        {
            get
            {
                if (IsScalar) throw new Exception("Cannot get LeftElement for a Scalar");
                return _Left;
            }
            set
            {
                if (IsScalar) throw new Exception("Cannot set LeftElement for a Scalar");

                if (value != null) value.Parent = this;
                _Left = value;
            }
        }
        public Element Right
        {
            get
            {
                if (IsScalar) throw new Exception("Cannot get RightElement for a Scalar");
                return _Right;
            }
            set
            {
                if (IsScalar) throw new Exception("Cannot set RightElement for a Scalar");
                if (value != null) value.Parent = this;
                _Right = value;
            }
        }
        private Element _Left = null;
        private Element _Right = null;

        public int Value
        {
            get
            {
                if (IsPair) throw new Exception("Cannot retrieve a Value for a Pair");
                return _value;
            }
            set
            {
                if (IsPair) throw new Exception("Cannot set a Value for a Pair");
                _value = value;
            }
        }
        private int _value;

        public Element Parent
        {
            get { return _Parent; }
            set
            {
                _Parent = value;
            }
        }
        private Element _Parent = null;

        public int NestedLevel
        {
            get
            {
                int level = 0;
                Element p = Parent;
                while (p != null)
                {
                    p = p.Parent;
                    level++;
                }
                return level;
            }
        }

        public Element()
        {

        }

        public int Magnitude()
        {
            if (IsScalar)
            {
                return Value;
            }
            else if (IsPair)
            {
                return (3 * Left.Magnitude()) +
                    (2 * Right.Magnitude());
            }
            else
            {
                return -1;
            }
        }

        public override string ToString()
        {
            if (IsScalar) return Value.ToString();
            else return $"[{Left.ToString()},{Right.ToString()}]";
        }

        public static Element CreateElementFromString(string txt)
        {
            Element el = new Element();

            if (txt[0] == '[')
            {
                el.IsPair = true;
                el.ParsePairText(txt);
            }
            else if (Char.IsDigit(txt[0]) || txt[0] == '-')
            {
                el.IsScalar = true;
                el.ParseScalarText(txt);
            }
            else
            {
                throw new Exception($"Invalid element text: {txt}");
            }

            return el;
        }

        private void ParsePairText(string txt)
        {
            if (txt[0] != '[' ||
                txt[txt.Length - 1] != ']')
            {
                throw new Exception($"Invalid pair element text: {txt}");
            }

            txt = txt.Substring(1, txt.Length - 2);
            int splitindex = -1;
            int braces = 0;
            for (int i = 0; i < txt.Length; i++)
            {
                char c = txt[i];
                if (c == '[') braces++;
                else if (c == ']') braces--;
                else if (c == ',' && braces == 0)
                {
                    splitindex = i;
                    break;
                }
            }

            if (splitindex == -1)
                throw new Exception($"Invalid pair text, can't find split index: {txt}");

            string lefttxt = txt.Substring(0, splitindex);
            string righttext = txt.Substring(splitindex + 1);

            Left = Element.CreateElementFromString(lefttxt);

            Right = Element.CreateElementFromString(righttext);
        }

        private void ParseScalarText(string txt)
        {
            Value = int.Parse(txt);
            
        }
    }
}
