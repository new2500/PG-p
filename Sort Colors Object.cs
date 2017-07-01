Sort Colors Object
给一个Pixel[], 根据其中的一个attribute来sort:

#region Comparer
//Quick Sort using Comparer O(N*logN) time, O(logN) space

        public void SortPixel(Pixel[] pixels)
        {
            if (pixels == null || pixels.Length == 0)
                return;
            Array.Sort(pixels, new Pixel1stComparer());
        }

        private class Pixel1stComparer : IComparer<Pixel>
        {
            public int Compare(Pixel x, Pixel y)
            {
                int result = x.red.CompareTo(y.red);
                //If red equal, we keep stable order
                return result == 0 ? 1 : result;
            }
        }
#end



#region Using List<T> OrderBy
        public Pixel[] SortPixel(Pixel[] pixels)
        {
            if (pixels == null || pixels.Length == 0)
                return null;
            var res = pixels.ToList();
            res = res.OrderBy(x => (x.red)).ToList();
            return res.ToArray();
        }
#end