using System;
namespace day14
{
    public class Element
    {
        public char Value;
        public Element Next;
        public Element Previous;

        public Element(char v)
        {
            Value = v;
            Previous = null;
            Next = null;
        }

        public Element NewElementAfter(char v)
        {
            Element newel = new Element(v);
            newel.Next = this.Next;
            newel.Previous = this;
            this.Next = newel;

            return newel;
        }

        public string PairValue()
        {
            string str = "";

            if (Next != null)
            {
                str = $"{Value}{Next.Value}";
            }

            return str;
        }
    }
}
