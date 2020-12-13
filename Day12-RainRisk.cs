using System;
using System.Collections.Generic;
using System.IO;

namespace advent_of_code_2020
{
  class FerryControl
  {
    private struct NavigationInstruction
    {
      public char direction;
      public int value;
    }
    private static List<NavigationInstruction> navInstructions = new List<NavigationInstruction>();

    private static int ferryX;
    private static int ferryY;
    private static char ferryDirection;

    private static int waypointX;
    private static int waypointY;

    public static void LoadFile(string path)
    {
      StreamReader inputFile = new StreamReader(path);
      string line;

      while ((line = inputFile.ReadLine()) != null)
      {
        NavigationInstruction nav;
        nav.direction = line[0];
        nav.value = Int32.Parse(line.Substring(1));
        navInstructions.Add(nav);
      }
    }

    public static void FollowNavInstructions()
    {
      ferryX = 0;
      ferryY = 0;
      ferryDirection = 'E';

      foreach (var instruction in navInstructions)
      {
        FollowInstruction(instruction);
      }
    }

    public static void FollowWaypointInstructions()
    {
      ferryX = 0;
      ferryY = 0;
      waypointX = 10;
      waypointY = 1;

      foreach (var instruction in navInstructions)
      {
        FollowWaypointInstruction(instruction);
      }
    }

    public static int ManhattenDistanceFromOrigin()
    {
      return Math.Abs(ferryX) + Math.Abs(ferryY);
    }

    private static void FollowInstruction(NavigationInstruction instruction)
    {
      char direction = instruction.direction;
      if (direction == 'F')
        direction = ferryDirection;

      switch (direction)
      {
        case 'N':
          ferryY += instruction.value;
          break;
        case 'E':
          ferryX += instruction.value;
          break;
        case 'S':
          ferryY -= instruction.value;
          break;
        case 'W':
          ferryX -= instruction.value;
          break;
        case 'L':
        case 'R':
          Turn(instruction);
          break;
      }
    }

    private static void FollowWaypointInstruction(NavigationInstruction instruction)
    {
      switch (instruction.direction)
      {
        case 'N':
          waypointY += instruction.value;
          break;
        case 'E':
          waypointX += instruction.value;
          break;
        case 'S':
          waypointY -= instruction.value;
          break;
        case 'W':
          waypointX -= instruction.value;
          break;
        case 'L':
        case 'R':
          TurnWaypoint(instruction);
          break;
        case 'F':
          for (int i = 0; i < instruction.value; i++)
          {
            ferryX += waypointX;
            ferryY += waypointY;
          }
          break;
      }
    }

    private static void Turn(NavigationInstruction instruction)
    {
      int rotation;
      if (instruction.direction == 'R')
        rotation = instruction.value;
      else
        rotation = 360 - instruction.value;

      while (rotation > 0)
      {
        TurnRight();
        rotation -= 90;
      }
    }

    private static void TurnRight()
    {
      switch (ferryDirection)
      {
        case 'N':
          ferryDirection = 'E';
          break;
        case 'E':
          ferryDirection = 'S';
          break;
        case 'S':
          ferryDirection = 'W';
          break;
        case 'W':
          ferryDirection = 'N';
          break;
      }
    }

    private static void TurnWaypoint(NavigationInstruction instruction)
    {
      int rotation;
      if (instruction.direction == 'R')
        rotation = instruction.value;
      else
        rotation = 360 - instruction.value;

      while (rotation > 0)
      {
        int temp = waypointX;
        waypointX = waypointY;
        waypointY = -temp;

        rotation -= 90;
      }
    }
  }
}
