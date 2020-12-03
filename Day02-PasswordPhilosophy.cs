using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2020
{
  class PasswordInfo
  {
    int a;
    int b;
    char character;
    string password;

    static List<PasswordInfo> passwordList = new List<PasswordInfo>();

    public PasswordInfo(int a, int b, char character, string password)
    {
      this.a = a;
      this.b = b;
      this.character = character;
      this.password = password;
    }

    public static void LoadFile(string path)
    {
      string[] lines = File.ReadAllLines(path);

      foreach (var line in lines)
      {
        string[] tokens = line.Split('-', ' ', ':');
        PasswordInfo passwordInfo = new PasswordInfo(
          Int32.Parse(tokens[0]),
          Int32.Parse(tokens[1]),
          tokens[2][0],
          tokens[4]
          );
        passwordList.Add(passwordInfo);
      }
    }

    public static int ValidPasswords()
    {
      int result = 0;
      foreach(var passwordInfo in passwordList)
      {
        int occurrences = passwordInfo.password.Count(f => (f == passwordInfo.character));
        if (occurrences >= passwordInfo.a && occurrences <= passwordInfo.b)
          result++;
      }
      return result;
    }

    public static int ValidPasswords2()
    {
      int result = 0;
      foreach(var passwordInfo in passwordList)
      {
        // - 1 because inputs are not zero indexed
        bool aMatches = passwordInfo.password[passwordInfo.a - 1] == passwordInfo.character;
        bool bMatches = passwordInfo.password[passwordInfo.b - 1] == passwordInfo.character;
        if (aMatches ^ bMatches)  // xor
          result++;
      }
      return result;
    }
  }
}
