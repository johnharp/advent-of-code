using System;
using System.Collections;

namespace day19
{
    // Vector3 is a value type
    // this way, each assignment creates a copy of the value rather
    // than a reference to the value.
    //
    // var v2 = new Vector3(1, 0, 0);
    // var v3 = v2;
    // var v3.x = 100;
    // Assert.AreEqual(1, v2.x);
    public struct Vector3 : IComparable
    {
        public int x;
        public int y;
        public int z;

        public Vector3(int nx, int ny, int nz) =>
            (x, y, z) = (nx, ny, nz);

        public Vector3(string str)
        {
            // This is only really used when loading the puzzle
            // input so it isn't necessary to be efficient or
            // even really bullet-proof since we're guaranteed to
            // have well-formatted input.
            //
            // However, we may want to construct vectors from
            // strings in the test cases, so handle a few things
            // to make human typed vectors likely to be parsable.
            //
            // allow optional whitespace before/after
            str = str.Trim();
            // allow optional starting "(" and ending ")"
            str = str.TrimStart('(');
            str = str.TrimEnd(')');

            string[] parts = str.Split(",");
            int nx = int.Parse(parts[0].Trim());
            int ny = int.Parse(parts[1].Trim());
            int nz = int.Parse(parts[2].Trim());

            x = nx;
            y = ny;
            z = nz;
        }

        public Vector3 TimesScalar(int i)
        {
            return new Vector3(x * i, y * i, z * i);
        }

        public Vector3 CrossProduct(Vector3 v)
        {
            return new Vector3(
                y*v.z - z*v.y,
                z*v.x - x*v.z,
                x*v.y - y*v.x
            );
        }

        public Vector3 Add(Vector3 v)
        {
            return new Vector3(
                v.x + x,
                v.y + y,
                v.z + z);
        }

        public Vector3 Subtract(Vector3 v)
        {
            return new Vector3(
                x - v.x,
                y - v.y,
                z - v.z);
        }

        public override bool Equals(object obj)
        {
            // If the other object is null, can't be equal
            if (obj == null) return false;
            // If the two objects are of different types can't be equal
            if (!this.GetType().Equals(obj.GetType())) return false;


            Vector3 v = (Vector3)obj;

            return x == v.x &&
                   y == v.y &&
                   z == v.z;
        }

        //
        // https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-overriding-gethashcode/263416#263416
        //
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + x.GetHashCode();
            hash = hash * 23 + y.GetHashCode();
            hash = hash * 23 + z.GetHashCode();

            return hash;
        }

        public override string ToString()
        {
            return $"{x,5},{y,5},{z,5}";
        }

        // return 1 if "larger" than v
        // return 0 if equal
        // return -1 if "smaller" than v
        public int CompareSortOrder(Vector3 other)
        {
            if (x > other.x ||
                 (x == other.x && y > other.y) ||
                 (x == other.x && y == other.y && z > other.z))
            {
                return 1;
            }
            else if (x < other.x ||
                (x == other.x && y < other.y) ||
                (x == other.x && y == other.y && z < other.z))
            {
                return -1;
            }
            else
            {
                return 0;
            }


        }

        int IComparable.CompareTo(object obj)
        {
            if (obj == null) return 1;

            if (obj.GetType() != typeof(Vector3))
                throw new Exception("Object is not a Vector3");

            Vector3 other = (Vector3)obj;
            return CompareSortOrder(other);            
        }

        //private class VectorComparerHelper : IComparer
        //{
        //    int IComparer.Compare(object x, object y)
        //    {
        //        Vector3 v1 = (Vector3)x;
        //        Vector3 v2 = (Vector3)y;

        //        if (v1.x > v2.x ||
        //            (v1.x == v2.x && v1.y > v2.y) ||
        //            (v1.x == v2.x && v1.y == v2.y && v1.z > v2.z))
        //        {
        //            return 1;
        //        }
        //        else if (v1.x < v2.x ||
        //            (v1.x == v2.x && v1.y < v2.y) ||
        //            (v1.x == v2.x && v1.y == v2.y && v1.z < v2.z))
        //        {
        //            return -1;
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }

        //}

    }
}
