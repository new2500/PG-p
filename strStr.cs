strStr

#region Cheating O(m*n)

public int strStr(string source, string target)
{
     return source.IndexOf(target);   //If invalid, return -1
}

#endregion


#region Brute Force

//Say n is source length, m is target's length, total time: O((n-m)*n) => O(N^2)

public int strStr(string source, string target)
{    
     if(string.IsNullOrEmpty(target)   
          return 0;                         //Invalid cases:
     if(target.Length > source.Length)
          return -1;


     for(int i = 0; i <= source.Length - target.Length; i++)
     {
          for(int j = 0; j < target.Length && source[i + j] == target[j]; j++)
          {
               if(j == target.Length - 1)   //All element in target appear in source
                    return i;
          }
     }
     return -1;
}

#endregion



#region KMP
//First we create the pattern table for target string, the value of table is base on the common string of suffix and prefix.

       public int StrStr(string source, string target)
        {
            if (source == null || target == null ||
            target.Length > source.Length)
                return -1;

            int[] parr = KMP(target);

            int i = 0, j = 0;
            while (i < source.Length && j < target.Length)
            {
                if (source[i] == target[j])
                {
                    i++; j++;
                }
                else if (j > 0)
                { j = parr[j - 1]; }
                else
                { i++; }
            }

            return j == target.Length ? i - j : -1;
        }

        private int[] KMP(string pattern)
        {
            if (string.IsNullOrEmpty(pattern)) return null;

            int[] res = new int[pattern.Length];

            if (pattern.Length == 1)
                return res;
            int i = 1;
            int j = 0;

            while (i < pattern.Length)
            {
                if (pattern[i] == pattern[j])
                {
                    res[i] = j + 1;
                    i++;
                    j++;
                }
                else if (j > 0)
                    j = res[j - 1];
                else
                    i++;
            }
            return res;
        }

#endregion