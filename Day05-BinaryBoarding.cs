using System;
using System.IO;
using System.Collections.Generic;

namespace advent_of_code_2020
{
  class BoardingPass
  {
    static string[] seatSpecifiers;

    public static void LoadFile(string path)
    {
      seatSpecifiers = File.ReadAllLines(path);
    }

    public static int HighestSeatID()
    {
      int result = 0;

      foreach (string seatSpecifier in seatSpecifiers)
      {
        int row = GetRow(seatSpecifier);
        int col = GetColumn(seatSpecifier);
        int seatID = SeatID(row, col);
        
        if (seatID > result)
          result = seatID;
      }

      return result;
    }

    public static int MissingID()
    {
      bool[] allocatedSeats = new bool[1024];

      foreach (string seatSpecifier in seatSpecifiers)
      {
        int row = GetRow(seatSpecifier);
        int col = GetColumn(seatSpecifier);
        int seatID = SeatID(row, col);

        allocatedSeats[seatID] = true;
      }

      for (int i = 1; i < 1024; i++)
      {
        if (allocatedSeats[i-1] == true &&
            allocatedSeats[i] == false)
          return i;
      }

      return -1;
    }

    private static int GetRow(string seatSpecifier)
    {
      int min = 0;
      int max = 127;
      string columnData = seatSpecifier.Substring(0,7);
      foreach (char partition in columnData)
      {
        int mid = min + (max - min) / 2;
        if (partition == 'F')
          max = mid;
        else
          min = mid + 1;
      }
      return min;
    }

    private static int GetColumn(string seatSpecifier)
    {
      int min = 0;
      int max = 7;
      string columnData = seatSpecifier.Substring(7);
      foreach (char partition in columnData)
      {
        int mid = min + (max - min) / 2;
        if (partition == 'L')
          max = mid;
        else
          min = mid + 1;
      }
      return min;
    }

    private static int SeatID(int row, int col)
    {
      return (row * 8) + col;
    }
  }
}
