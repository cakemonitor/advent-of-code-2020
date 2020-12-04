using System.IO;

namespace advent_of_code_2020
{
  class TobogganRoute
  {
    static string[] map;

    public static void LoadFile(string path)
    {
      map = File.ReadAllLines(path);
    }

    public static int CountTreesOnRoute(int across, int down)
    {
      int result = 0;
      int x = 0;
      int y = 0;

      do
      {
        if (map[y][x] == '#')
          result++;

        x = (x + across) % map[0].Length;
        y = y + down;
      } while (y < map.Length);

      return result;
    }
  }
}
