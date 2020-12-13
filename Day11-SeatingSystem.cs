using System;
using System.IO;

namespace advent_of_code_2020
{
  class Seating
  {
    private static char[,] seats;
    private static int width;
    private static int height;
    private static int activeRule = 0;

    public static void LoadFile(string path)
    {
      string[] lines = File.ReadAllLines(path);

      width = lines[0].Length;
      height = lines.Length;

      seats = new char[width, height];

      for (int y = 0; y < height; y++)
        for (int x = 0; x < width; x++)
          seats[x, y] = lines[y][x];
    }

    public static int SeatsOccupiedAfterRuleSettles(int rule)
    {
      activeRule = (rule == 1) ? 1 : 2;

      int result = 0;

      int loopCount = 0;
      while (ApplyRule() && loopCount < 999) { loopCount++; }
      //PrintSeats();

      for (int y = 0; y < height; y++)
        for (int x = 0; x < width; x++)
          if (seats[x, y] == '#')
            result++;

      return result;
    }

    private static bool ApplyRule()
    {
      if (activeRule == 1)
        return ApplyRule1();

      return ApplyRule2();
    }

    private static bool ApplyRule1()
    {
      bool stateChange = false;

      char[,] newState = new char[width, height];

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          if ((seats[x, y] == 'L') && (OccupiedNeighbours(x, y) == 0))
          {
            newState[x, y] = '#';
            stateChange = true;
          }
          else if ((seats[x, y] == '#') && (OccupiedNeighbours(x, y) >= 4))
          {
            newState[x, y] = 'L';
            stateChange = true;
          }
          else
          {
            newState[x, y] = seats[x, y];
          }
        }
      }
      seats = newState;

      return stateChange;
    }

    private static bool ApplyRule2()
    {
      bool stateChange = false;

      char[,] newState = new char[width, height];

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          if ((seats[x, y] == 'L') && (OccupiedVisibleNeighbours(x, y) == 0))
          {
            newState[x, y] = '#';
            stateChange = true;
          }
          else if ((seats[x, y] == '#') && (OccupiedVisibleNeighbours(x, y) >= 5))
          {
            newState[x, y] = 'L';
            stateChange = true;
          }
          else
          {
            newState[x, y] = seats[x, y];
          }
        }
      }
      seats = newState;

      return stateChange;
    }

    private static bool IsOutOfBounds(int x, int y)
    {
      if (x < 0 || y < 0 || x >= width || y >= height)
        return true;

      return false;
    }

    private static bool IsOccupied(int x, int y)
    {
      if (IsOutOfBounds(x, y))
        return false;

      return (seats[x, y] == '#');
    }

    private static int OccupiedNeighbours(int x, int y)
    {
      int result = 0;

      for (int dy = - 1; dy <= 1; dy++)
        for (int dx = -1; dx <=  1; dx++)
        {
          if (dx == 0 && dy == 0)
            continue;
          if (IsOccupied(x + dx, y + dy))
            result++;
        }

      return result;
    }

    private static int OccupiedVisibleNeighbours(int x, int y)
    {
      int result = 0;

      for (int dy = - 1; dy <= 1; dy++)
        for (int dx = -1; dx <=  1; dx++)
        {
          if (dx == 0 && dy == 0)
            continue;

          int currentX = x;
          int currentY = y;
          bool seatSeen = false;
          while (!seatSeen)
          {
            currentX += dx;
            currentY += dy;
            if (IsOutOfBounds(currentX, currentY))
              break;
            
            if (seats[currentX,currentY] != '.')
              seatSeen = true;
          }

          if (seatSeen && seats[currentX,currentY] == '#')
            result++;
        }

      return result;
    }

    private static void PrintSeats()
    {
      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          Console.Write(seats[x, y]);
        }
        Console.WriteLine();
      }
      Console.WriteLine();
    }

  }
}
