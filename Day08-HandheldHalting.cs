using System;
using System.Collections.Generic;
using System.IO;

namespace advent_of_code_2020
{
  class GameConsole
  {
    private static List<(string opcode, int value)> program = new List<(string, int)>();

    private static int accumulator = 0;
    private static int programCounter = 0;


    public static void LoadFile(string path)
    {
      StreamReader inputFile = new StreamReader(path);
      string line;

      while ((line = inputFile.ReadLine()) != null)
      {
        string[] tokens = line.Split(' ');

        (string opcode, int value) instruction;
        instruction.opcode = tokens[0];
        instruction.value = Int32.Parse(tokens[1]);

        program.Add(instruction);
      }
    }

    public static bool RunProgram()
    {
      accumulator = 0;
      programCounter = 0;

      List<int> instructionsRun = new List<int>();

      while (programCounter < program.Count)
      {
        // infinite loop check
        if (instructionsRun.Contains(programCounter))
          break;
        instructionsRun.Add(programCounter);

        switch (program[programCounter].opcode)
        {
          case "acc":
            accumulator += program[programCounter].value;
            programCounter++;
            break;
          case "jmp":
            programCounter += program[programCounter].value;
            break;
          case "nop":
          default:
            programCounter++;
            break;
        }
      }

      // exactly one line after the zero-indexed program
      if (programCounter == program.Count)
        return true;

      return false;
    }

    public static int GetAccumulatorValue()
    {
      return accumulator;
    }

    public static void FixCorruptJmpOrNop()
    {
      List<int> jmps = new List<int>();
      List<int> nops = new List<int>();

      for (int i = 0; i < program.Count; i++)
      {
        if (program[i].opcode == "jmp")
          jmps.Add(i);
        if (program[i].opcode == "nop")
          nops.Add(i);
      }

      foreach (int i in jmps)
      {
        program[i] = ("nop", program[i].value);
        if (RunProgram())
        {
          Console.WriteLine("Fixed corrupt 'nop' at line: " + (i + 1).ToString());
          return;
        }
        program[i] = ("jmp", program[i].value);
      }

      foreach (int i in nops)
      {
        program[i] = ("jmp", program[i].value);
        if (RunProgram())
        {
          Console.WriteLine("Fixed corrupt 'jmp' at line: " + (i + 1).ToString());
          return;
        }
        program[i] = ("nop", program[i].value);
      }
    }
  }
}
