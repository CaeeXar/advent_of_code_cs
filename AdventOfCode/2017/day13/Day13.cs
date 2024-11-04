using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace AOC2017.Day13
{
    internal class Day13
    {        private struct Sensor
        {
            public Sensor(int depth, int range) : this()
            {
                this.Depth = depth;
                this.Range = range;
            }

            public int Depth { get; set; }
            public int Range { get; set; }
        }

        string path = @"2017\day13\Input.txt";

        public string ReadFile()
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return null;
            }
        }

        public int Part1()
        {
            List<Sensor> sensors = GetSensors(ReadFile());
            return GetSeverity(sensors);
        }

        public int Part2()
        {
            List<Sensor> sensors = GetSensors(ReadFile());
            return FindDelay(sensors);
        }

        private int FindDelay(List<Sensor> sensors)
        {
            int delay = 1;
            while(sensors.Any(s => Caught(s.Depth, s.Range, delay)))
            {
                delay++;
            }

            return delay;
        }

        private int GetSeverity(List<Sensor> sensors)
        {
            return sensors.Aggregate(
                0, 
                (severity, sensor) => 
                    severity + (Caught(sensor.Depth, sensor.Range) ? sensor.Depth * sensor.Range : 0)
            );
        }
        private bool Caught(int depth, int range, int delay = 0)
        {
            return (depth + delay) % (2 * range - 2) == 0;
        }

        private List<Sensor> GetSensors(string input)
        {
            List<Sensor> sensors = new List<Sensor>();
            foreach (string line in input.Split("\n"))
            {
                int[] data = line.Split(":").Select(d => int.Parse(d.Trim())).ToArray();
                sensors.Add(new Sensor(data[0], data[1]));
            }

            return sensors;
        }
    }
}
