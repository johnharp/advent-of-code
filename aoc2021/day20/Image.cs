using System;
using System.Collections;

namespace day20
{
    public class Image
    {
        public int Width = 0;
        public int Height = 0;
        public BitArray Bits;
        public bool BorderValue = false;

        public Image(int w, int h)
        {
            Width = w;
            Height = h;
            Bits = new BitArray(w * h, false);
        }

        public Image Step(BitArray algorithm)
        {
            Image i2 = GrowImage(2, algorithm);

            Console.Out.WriteLine($"New image grows to {i2.Width} x {i2.Height} = {i2.Width * i2.Height}  ({i2.Bits.Length})");
            for (int y = 0; y < i2.Height; y++)
            {
                for (int x = 0; x < i2.Width; x++)
                {
                    int ox = x - 1;
                    int oy = y - 1;

                    int v =
                        (Get(ox-1, oy-1) ? 256 : 0) +
                        (Get(ox, oy-1) ? 128 : 0) +
                        (Get(ox+1, oy-1) ? 64 : 0) +
                        (Get(ox-1, oy) ? 32 : 0) +
                        (Get(ox, oy) ? 16 : 0) +
                        (Get(ox + 1, oy) ? 8 : 0) +
                        (Get(ox - 1, oy + 1) ? 4 : 0) +
                        (Get(ox, oy + 1) ? 2 : 0) +
                        (Get(ox + 1, oy + 1) ? 1 : 0);

                    bool setval = algorithm[v];
                    i2.Set(x, y, setval);

                }
            }

            return i2;
        }

        public Image GrowImage(int n, BitArray algo) {
            Image newimage =  new Image(Width + n, Height + n);

            // images outside of the known image are the border pixels
            // they will all be of a uniform value.
            // using the prior image's border value, compute the index
            // into the algorightm to apply
            int value =
                BorderValue ? 511 : 0;
            bool newBorderValue = algo[value];

            newimage.BorderValue = newBorderValue;
            return newimage;
        }

        public bool Get(int x, int y)
        {
            if (x < 0 || x >= Width ||
                y < 0 || y >= Height) return BorderValue;

            int indx = (y * Width) + x;
            return Bits[indx];
        }

        public void Set(int x, int y, bool v)
        {
            Bits[(y * Width) + x] = v;
        }

        public char GetChar(int x , int y)
        {
            return Get(x, y) == true ? '#' : '.';
        }

        public  int CountLitPixels()
        {
            int count = 0;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    bool v = Get(x, y);
                    if (v) count++; 
                }
            }
            return count;
        }

        public void Dump()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.Write(GetChar(x, y));
                }
                Console.WriteLine();
            }

        }

    }
}
