using System;

namespace advent_of_code_2020
{
  class Program
  {
    static void Main(string[] args)
    {
      Day06();
    }

    static void Day06()
    {
      DeclarationForm.LoadFile("day6input.txt");
      Console.WriteLine("Part 1 result: " + DeclarationForm.SumOfUniqueAnswersByGroup().ToString());
      Console.WriteLine("Part 2 result: " + DeclarationForm.SumOfCommonAnswersByGroup().ToString());
    }

    static void Day05()
    {
      // BoardingPass.LoadFile("day5input.txt");
      // Console.WriteLine("Part 1 result: " + BoardingPass.HighestSeatID().ToString());
      // Console.WriteLine("Part 2 result: " + BoardingPass.MissingID().ToString());
      BoardingPassB.LoadFile("day5input.txt");
      BoardingPassB.PrintHightestAndMissingSeatID();
    }

    static void Day04()
    {
      PassportData.LoadFile("day4input.txt");
      Console.WriteLine("Part 1 result: " + PassportData.CountValid());

      PassportData.LoadFileStrict("day4input.txt");
      Console.WriteLine("Part 2 result: " + PassportData.CountValid());
    }

    static void Day03()
    {
      TobogganRoute.LoadFile("day3input.txt");

      Console.WriteLine("Part 1 result: " + TobogganRoute.CountTreesOnRoute(3, 1).ToString());

      int result = TobogganRoute.CountTreesOnRoute(1, 1);
      result *= TobogganRoute.CountTreesOnRoute(3, 1);
      result *= TobogganRoute.CountTreesOnRoute(5, 1);
      result *= TobogganRoute.CountTreesOnRoute(7, 1);
      result *= TobogganRoute.CountTreesOnRoute(1, 2);
      Console.WriteLine("Part 2 result: " + result.ToString());
    }


    static void Day02()
    {
      PasswordInfo.LoadFile("day2input.txt");

      Console.WriteLine("Part 1 result: " + PasswordInfo.ValidPasswords().ToString());
      Console.WriteLine("Part 2 result: " + PasswordInfo.ValidPasswords2().ToString());
    }

    static void Day01()
    {
      ExpenseReport.LoadFile("day1input.txt");

      Console.WriteLine("Part 1 result: " + ExpenseReport.Answer().ToString());
      Console.WriteLine("Part 2 result: " + ExpenseReport.Answer2().ToString());
    }
  }
}
