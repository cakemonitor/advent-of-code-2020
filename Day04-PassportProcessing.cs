using System;
using System.IO;
using System.Collections.Generic;

namespace advent_of_code_2020
{
  class PassportData
  {
    private bool byr, iyr, eyr, hgt, hcl, ecl, pid, cid;
    private static List<PassportData> passports = new List<PassportData>();

    private PassportData()
    {
      byr = iyr = eyr = hgt = hcl = ecl = pid = cid = false;
    }

    public static void LoadFile(string path)
    {
      passports.Clear();
      string[] data = File.ReadAllLines(path);

      PassportData currentPassport = new PassportData();
      passports.Add(currentPassport);

      foreach (string s in data)
      {
        if (s == "")
        {
          currentPassport = new PassportData();
          passports.Add(currentPassport);
          continue;
        }

        string[] pairs = s.Split(' ');
        foreach (string pair in pairs)
        {
          string[] keyValue = pair.Split(':');
          switch (keyValue[0])
          {
            case "byr":
              currentPassport.byr = true;
              break;
            case "iyr":
              currentPassport.iyr = true;
              break;
            case "eyr":
              currentPassport.eyr = true;
              break;
            case "hgt":
              currentPassport.hgt = true;
              break;
            case "hcl":
              currentPassport.hcl = true;
              break;
            case "ecl":
              currentPassport.ecl = true;
              break;
            case "pid":
              currentPassport.pid = true;
              break;
            case "cid":
              currentPassport.cid = true;
              break;
            default:
              break;
          }
        }
      }
    }

    public static void LoadFileStrict(string path)
    {
      passports.Clear();
      string[] data = File.ReadAllLines(path);

      PassportData currentPassport = new PassportData();
      passports.Add(currentPassport);

      foreach (string s in data)
      {
        if (s == "")
        {
          currentPassport = new PassportData();
          passports.Add(currentPassport);
          continue;
        }

        string[] pairs = s.Split(' ');
        foreach (string pair in pairs)
        {
          string[] keyValue = pair.Split(':');
          int i;
          switch (keyValue[0])
          {
            case "byr":
              i = Int32.Parse(keyValue[1]);
              if (i >= 1920 && i <= 2002)
                currentPassport.byr = true;
              break;
            case "iyr":
              i = Int32.Parse(keyValue[1]);
              if (i >= 2010 && i <= 2020)
                currentPassport.iyr = true;
              break;
            case "eyr":
              i = Int32.Parse(keyValue[1]);
              if (i >= 2020 && i <= 2030)
                currentPassport.eyr = true;
              break;
            case "hgt":
              string unit = keyValue[1].Substring(keyValue[1].Length - 2);
              try
              {
                int height = Int32.Parse(keyValue[1].Substring(0, keyValue[1].Length - 2));
                switch (unit)
                {
                  case "cm":
                    if (height >= 150 && height <= 193)
                      currentPassport.hgt = true;
                    break;
                  case "in":
                    if (height >= 59 && height <= 76)
                      currentPassport.hgt = true;
                    break;
                }
              }
              catch { }
              break;
            case "hcl":
              if (keyValue[1].Length != 7)
                break;
              if (keyValue[1][0] != '#')
                break;
              int col;
              currentPassport.hcl = Int32.TryParse(
                keyValue[1].Substring(1, 6),
                System.Globalization.NumberStyles.HexNumber,
                System.Globalization.CultureInfo.CurrentCulture,
                out col);
              break;
            case "ecl":
              currentPassport.ecl = (
                keyValue[1] == "amb" ||
                keyValue[1] == "blu" ||
                keyValue[1] == "brn" ||
                keyValue[1] == "gry" ||
                keyValue[1] == "grn" ||
                keyValue[1] == "hzl" ||
                keyValue[1] == "oth"
              );
              break;
            case "pid":
              if (keyValue[1].Length != 9)
                break;
              currentPassport.pid = Int32.TryParse(keyValue[1], out i);
              break;
            case "cid":
              currentPassport.cid = true;
              break;
            default:
              break;
          }
        }
      }
    }

    public static int CountValid()
    {
      int result = 0;
      int validExceptCid = 0;

      foreach (PassportData p in passports)
      {
        if (p.byr && p.iyr && p.eyr && p.hgt && p.hcl && p.ecl && p.pid)
        {
          result++;
          if (!p.cid)
            validExceptCid++;
        }
      }
      Console.WriteLine("instance where cid is ignored on otherwise valid passports: " + validExceptCid.ToString());
      return result;
    }
  }
}
