using System;
using System.Collections.Generic;
using System.IO;

namespace advent_of_code_2020
{
  class SeatScreen
  {
    private static List<long> data = new List<long>();

    public static void LoadFile(string path)
    {
      StreamReader inputFile = new StreamReader(path);
      string line;

      while ((line = inputFile.ReadLine()) != null)
      {
        data.Add(long.Parse(line));
      }
    }

    public static long FirstValueWithoutSum25Property()
    {
      for (int i = 25; i < data.Count; i++)
      {
        bool sumFound = false;
        for (int j = i - 25; j < i; j++)
        {
          for (int k = j + 1; k < i; k++)
          {
            if (data[j] + data[k] == data[i])
            {
              sumFound = true;
              j = k = i; // break both inner loops
            }
          }
        }
        if (!sumFound)
          return data[i];
      }

      return -1;
    }

    public static long XmasWeakness(long target)
    {
      for (int i = 0; i < data.Count; i++)
      {
        long sum = data[i];
        int rangeOffset = 0;

        while (sum < target)
        {
          rangeOffset++;
          sum += data[i + rangeOffset];
          if (sum == target)
          {
            return SumOfSmallestAndLargestInRange(i, rangeOffset);
          }
        }
      }

      return -1;
    }

    private static long SumOfSmallestAndLargestInRange(int start, int rangeOffset)
    {
      long smallest = long.MaxValue;
      long largest = long.MinValue;

      for (int i = start; i <= start + rangeOffset; i++)
      {
        if (data[i] < smallest)
          smallest = data[i];
        if (data[i] > largest)
          largest = data[i];
      }

      return smallest + largest;
    }
  }
}
