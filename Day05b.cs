using System;
using System.IO;

namespace advent_of_code_2020
{
  class BoardingPassB
  {
    static bool[] seatID = new bool[1024];

    public static void LoadFile(string path)
    {
      StreamReader inputFile = new StreamReader(path);
      string line;

      while ((line = inputFile.ReadLine()) != null)
      {
        string binaryLine = "";

        for (int i = 0; i < line.Length; i++)
        {
          if (line[i] == 'B' || line[i] == 'R')
            binaryLine += '1';
          else
            binaryLine += '0';
        }

        seatID[Convert.ToInt32(binaryLine, 2)] = true;
      }
    }

    public static void PrintHightestAndMissingSeatID()
    {
      int highest = -1;
      int missing = -1;

      for (int i = 1; i < seatID.Length; i++)
      {
        if (seatID[i] && i > highest)
          highest = i;

        if (!seatID[i] && seatID[i - 1] && missing == -1)
          missing = i;
      }

      Console.WriteLine("Highest: " + highest);
      Console.WriteLine("Missing: " + missing);
    }
  }
}
