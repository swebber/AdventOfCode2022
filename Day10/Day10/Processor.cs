using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    public class Processor
    {
        private readonly string _fileName = @"C:\Users\WebberS\source\repos\AdventOfCode2022\Day10\Day10\data.txt";

        public int X { get; set; } = 1;

        private int _cycle = 0;
        private int _pixel = -1;
        private int _signalStrength = 0;

        private void AddCycle()
        {
            _cycle++;
            
            _pixel++;
            if (_pixel >= 40)
            {
                _pixel = 0;
                Console.WriteLine();
            }

            Console.Write(CurrentPixelValue());

            if (_cycle is 20 or 60 or 100 or 140 or 180 or 220)
            {
                int signalStrength = _cycle * X;
                _signalStrength += signalStrength;
            }
        }

        private char CurrentPixelValue()
        {
            return _pixel == X - 1 || _pixel == X || _pixel == X + 1 ? '#' : '.';
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
            
            Console.WriteLine($"\nCycle: {_cycle}, Total Signal Strength: {_signalStrength}");
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
