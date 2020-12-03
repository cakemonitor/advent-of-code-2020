using System;
using System.IO;
using System.Collections.Generic;

namespace advent_of_code_2020
{
  class ExpenseReport
  {
    static List<int> values = new List<int>();

    public static void LoadFile(string path)
    {
      string[] lines = File.ReadAllLines(path);

      foreach (var line in lines)
      {
        values.Add(Int32.Parse(line));
      }
    }

    public static int Answer()
    {
      for (int i = 0; i < values.Count; i++)
      {
        for (int j = i + 1; j < values.Count; j++)
        {
          if (values[i] + values[j] == 2020)
            return values[i] * values[j];
        }
      }
      return -1;
    }

    public static int Answer2()
    {
      for (int i = 0; i < values.Count; i++)
      {
        for (int j = i + 1; j < values.Count; j++)
        {
          for (int k = j + 1; k < values.Count; k++)
          {
            if (values[i] + values[j] + values[k] == 2020)
              return values[i] * values[j] * values[k];
          }
        }
      }
      return -1;
    }
  }
}
