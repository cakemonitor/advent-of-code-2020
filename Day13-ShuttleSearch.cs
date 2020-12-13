using System;
using System.Collections.Generic;
using System.IO;

namespace advent_of_code_2020
{
  class BusTimetable
  {
    private static List<int> busNumbers = new List<int>();
    private static int departureTime = -1;

    private struct BusInfo {
      public int busNumber;
      public int minute;
    }
    private static List<BusInfo> busInfoList = new List<BusInfo>();


    public static void LoadFile(string path)
    {
      StreamReader inputFile = new StreamReader(path);
      departureTime = Int32.Parse(inputFile.ReadLine());

      string[] inputText = inputFile.ReadLine().Split(',');
      int minute = 0;
      foreach (var text in inputText)
      {
        int busNumber;
        if (Int32.TryParse(text, out busNumber))
        {
          busNumbers.Add(busNumber);
          
          BusInfo busInfo;
          busInfo.minute = minute;
          busInfo.busNumber = busNumber;
          busInfoList.Add(busInfo);
        }
        minute++;
      }
    }

    public static int NextBusIdTimesMinutesWaiting()
    {
      int minimumWaitTime = int.MaxValue;
      int firstBusNumber = 0;

      foreach(int busNumber in busNumbers)
      {
        int timeSinceArrival = (departureTime % busNumber);
        int waitTime = (timeSinceArrival == 0) ? 0 : busNumber - timeSinceArrival;

        if (waitTime < minimumWaitTime)
        {
          minimumWaitTime = waitTime;
          firstBusNumber = busNumber;
        }
      }

      return minimumWaitTime * firstBusNumber;
    }

    public static long CompetitionResult()
    {
      long time = busInfoList[0].busNumber;

      for (int i = 1; i < busInfoList.Count; i++)
      {
        time = TimeIncorporatingBus(time, busInfoList[i]);
      }

      return time;
    }

    private static long TimeIncorporatingBus(long time, BusInfo busInfo)
    {
      Console.WriteLine("Incorporating bus: " + busInfo.busNumber.ToString());
      int multiplierA = 1;
      while ((time * multiplierA) < busInfo.busNumber)
      {
        multiplierA++;
      }

      int targetRemainder;
      int multiplierB = 1;
      do {
        targetRemainder = (multiplierB++ * busInfo.busNumber) - busInfo.minute;
      } while (targetRemainder < 0);

      while ((time * multiplierA) % busInfo.busNumber != targetRemainder)
      {
        multiplierA++;
      }

      return time * multiplierA;
    }
  }
}
