using System;
using System.Collections.Generic;
using System.IO;

namespace advent_of_code_2020
{
  class LuggageProcessing
  {
    private static Dictionary<string, List<(string Colour, int Amount)>> bags =
      new Dictionary<string, List<(string Colour, int Amount)>>();

    public static void LoadFile(string path)
    {
      StreamReader inputFile = new StreamReader(path);
      string line;

      while ((line = inputFile.ReadLine()) != null)
      {

        string[] splitLine = line.Split(new string[] { " bags contain ", ", " }, StringSplitOptions.None);
        string bagColour = splitLine[0];

        if (!bags.ContainsKey(bagColour))
          bags.Add(bagColour, new List<(string Colour, int Amount)>());

        for (int i = 1; i < splitLine.Length; i++)
        {
          int firstSpacePosition = splitLine[i].IndexOf(' ');
          int amount;
          if (Int32.TryParse(splitLine[i].Substring(0, firstSpacePosition), out amount))
          {
            int bagPosition = splitLine[i].IndexOf(" bag");
            int colourTextLength = bagPosition - firstSpacePosition - 1;
            string colour = splitLine[i].Substring(firstSpacePosition + 1, colourTextLength);

            bags[bagColour].Add((colour, amount));
          }
        }
      }
    }

    public static int BagsTheContainShinyGoldBags()
    {
      int result = 0;

      foreach (var bag in bags)
      {
        if (BagContainsShinyGoldBag(bag.Key))
          result++;
      }

      return result;
    }

    private static bool BagContainsShinyGoldBag(string bagColour)
    {
      if (!bags.ContainsKey(bagColour))
        return false;

      foreach (var bag in bags[bagColour])
      {
        if (bag.Colour == "shiny gold")
          return true;
        else if (BagContainsShinyGoldBag(bag.Colour))
          return true;
      }
      return false;
    }

    public static int BagsInsideOf(string bagColour)
    {
      int result = 0;

      foreach (var bag in bags[bagColour])
      {
        result += bag.Amount + bag.Amount * BagsInsideOf(bag.Colour);
      }

      return result;
    }
  }
}
