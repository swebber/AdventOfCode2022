using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    public class Processor
    {
        private readonly string _fileName = @"C:\Users\WebberS\source\repos\AdventOfCode2022\Day10\Day10\data.txt";

        public int X { get; set; } = 1;

        private int _cycle = 0;
        private int _signalStrength = 0;

        private void AddCycle()
        {
            _cycle++;
            if (_cycle is 20 or 60 or 100 or 140 or 180 or 220)
            {
                int signalStrength = _cycle * X;
                _signalStrength += signalStrength;
                Console.WriteLine($"Cycle: {_cycle}, Signal Strength: {signalStrength}, Total Signal Strength: {_signalStrength}");
            }
        }

        public void Run()
        {
            foreach (string line in File.ReadLines(_fileName))
            {
                var command = line.Split(' ');

                switch (command)
                {
                    case ["noop"]:
                        Noop();
                        break;
                    case ["addx", var value]:
                        AddX(value);
                        break;
                }
            }
        }

        private void AddX(string value)
        {
            AddCycle();
            AddCycle();
            X += int.Parse(value);
        }

        private void Noop()
        {
            AddCycle();
        }
    }
}
