using System;
namespace day18
{
    public class Calculator
    {
        public Calculator()
        {
        }

        public Element Add(Element a, Element b)
        {
            Element sum = new Element();
            sum.IsPair = true;

            sum.Left = a;
            sum.Right = b;

            return sum;
        }

        // returns true if able to explode an element
        // false, if none explode
        public bool Explode(Element root)
        {
            bool didExplode = false;
            Element e = FindFirstPairToExplode(root);

            if (e != null)
            {
                didExplode = true;
                AddToFirstNumberLeftOf(e, e.Left.Value);
                AddToFirstNumberRightOf(e, e.Right.Value);
                var zero = new Element();
                zero.IsScalar = true;
                zero.IsPair = false;
                zero.Value = 0;
                zero.Parent = e.Parent;

                if (e.Parent.Left == e)
                {
                    e.Parent.Left = zero;
                }

                if (e.Parent.Right == e)
                {
                    e.Parent.Right = zero;
                }
            }

            return didExplode;
        }

        public bool Split(Element root)
        {
            bool didSplit = false;

            Element e = FindFirstScalarToSplit(root);
            if (e != null)
            {
                didSplit = true;

                int left = e.Value / 2;
                int right = (e.Value+1) / 2;

                var newpair = Element.CreateElementFromString($"[{left},{right}]");
                newpair.Parent = e;

                if (e.Parent.Left == e)
                {
                    e.Parent.Left = newpair;
                }

                if (e.Parent.Right == e)
                {
                    e.Parent.Right = newpair;
                }
            }
            return didSplit;
        }

        public void AddToFirstNumberLeftOf(Element pair, int value)
        {
            // move up the tree until we reach a pair
            // who's parent we are linked to via their right
            // branch

            // if we have no parent we won't be able to find
            // something to the left of us in the tree
            if (pair.Parent == null) return;

            Element p = pair;
            Element foundparent = null;
            while (p.Parent != null)
            {
                if (p.Parent.Right == p)
                {
                    // found a parent where we came in via the right
                    // branch
                    foundparent = p.Parent;
                    break;
                }
                else
                {
                    p = p.Parent;
                }
            }

            // if the left branch of this tree is a scalar
            // that's the one to modify
            // if not, traverse down it's right side

            // now traverse down the left hand side from this
            // parent, always looking to the left for a scalar.
            if (foundparent != null)
            {
                p = foundparent.Left;

                while (!p.IsScalar)
                {
                    p = p.Right;
                }
                p.Value += value;
            }

        }

        public void AddToFirstNumberRightOf(Element pair, int value)
        {
            // move up the tree until we reach a pair
            // who's parent we are linked to via their left
            // branch

            // if we have no parent we won't be able to find
            // something to the right of us in the tree
            if (pair.Parent == null) return;

            Element p = pair;
            Element foundparent = null;
            while (p.Parent != null)
            {
                if (p.Parent.Left == p)
                {
                    // found a parent where we came in via the left
                    // branch
                    foundparent = p.Parent;
                    break;
                }
                else
                {
                    p = p.Parent;
                }
            }

            // if the right branch of this tree is a scalar
            // that's the one to modify
            // if not, traverse down it's left side
            
            // now traverse down the right hand side from this
            // parent, always looking to the left for a scalar.
            if (foundparent != null)
            {
                p = foundparent.Right;

                while (!p.IsScalar)
                {
                    p = p.Left;
                }
                p.Value += value;
            }
        }

        public Element FindFirstPairToExplode(Element s)
        {
            if (s.IsScalar) return null;

            if (s.IsPair)
            {
                if (s.NestedLevel < 4)
                {
                    Element a = FindFirstPairToExplode(s.Left);
                    if (a != null) return a;

                    Element b = FindFirstPairToExplode(s.Right);
                    if (b != null) return b;
                }

                // for a pair to explode it must be at level
                // 4 nesting and contains two scalars
                if (s.NestedLevel == 4 &&
                    s.Left.IsScalar &&
                    s.Right.IsScalar)
                {
                    return s;
                }
            }

            // if not a pair, if lower than level 4, or otherwise
            // can't find a candidate
            return null;
        }

        public Element FindFirstScalarToSplit(Element s)
        {
            if (s.IsScalar && s.Value >= 10) return s;

            if (s.IsPair)
            {
                Element a = FindFirstScalarToSplit(s.Left);
                if (a != null) return a;

                Element b = FindFirstScalarToSplit(s.Right);
                if (b != null) return b;
            }
            return null;
        }

        public void Reduce(Element e)
        {
            Calculator calc = new Calculator();
            bool didSomething = true;

            while (didSomething)
            {
                bool explode = false;
                bool split = false;

                explode = calc.Explode(e);
                if (!explode)
                {
                    split = calc.Split(e);
                }

                didSomething = explode || split;
            }
        }
    }
}
