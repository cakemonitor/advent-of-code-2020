using System.Collections.Generic;
using System.IO;

namespace advent_of_code_2020
{
  class DeclarationForm
  {
    static List<List<string>> answersByGroup = new List<List<string>>();

    public static void LoadFile(string path)
    {
      StreamReader inputFile = new StreamReader(path);
      string line;

      List<string> groupAnswers = new List<string>();

      while ((line = inputFile.ReadLine()) != null)
      {
        if (line == "")
        {
          answersByGroup.Add(groupAnswers);
          groupAnswers = new List<string>();
        }
        else
          groupAnswers.Add(line);
      }
      answersByGroup.Add(groupAnswers);
    }

    public static int SumOfUniqueAnswersByGroup()
    {
      int result = 0;

      foreach (var group in answersByGroup)
      {
        for (char c = 'a'; c <= 'z'; c++)
        {
          bool found = false;
          foreach (var answer in group)
          {
            if (answer.Contains(c))
              found = true;
          }
          if (found)
            result++;
        }
      }

      return result;
    }

    public static int SumOfCommonAnswersByGroup()
    {
      int result = 0;

      foreach (var group in answersByGroup)
      {
        for (char c = 'a'; c <= 'z'; c++)
        {
          bool found = true;
          foreach (var answer in group)
          {
            if (!answer.Contains(c))
              found = false;
          }
          if (found)
            result++;
        }
      }

      return result;
    }
  }
}
