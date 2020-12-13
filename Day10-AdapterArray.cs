using System;
using System.Collections.Generic;
using System.IO;

namespace advent_of_code_2020
{
  class JoltageAdapter
  {
    private static List<int> data = new List<int>();

    public static void LoadFile(string path)
    {
      StreamReader inputFile = new StreamReader(path);
      string line;

      while ((line = inputFile.ReadLine()) != null)
      {
        data.Add(Int32.Parse(line));
      }

      data.Add(0); // outlet
      data.Sort();
    }

    public static int JoltCalculation()
    {
      int ones = 0;
      int threes = 0;

      for (int i = 1; i < data.Count; i++)
      {
        int diff = data[i] - data[i - 1];
        if (diff == 1)
          ones++;
        if (diff == 3)
          threes++;
      }
      threes++; // last adapter to device

      return ones * threes;
    }

    public static long AdapterCombos()
    {
      return 0;
    }
  }
}
