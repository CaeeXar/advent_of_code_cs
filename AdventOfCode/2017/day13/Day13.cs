using System.Collections.Generic;

namespace AOC2017.Day13
{
    internal class Day13
    {
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
            int max = sensors.MaxBy(d => d.Depth)!.Depth;
            int player = -1,
                severity = 0;
            for (int i = 0; i <= max; i++)
            {
                // move player
                player++;

                // check caugth
                Sensor? sensor = sensors.FirstOrDefault(d => d.Depth == player);
                if (sensor != null && sensor.Position == 0)
                {
                    severity += (sensor.Depth * sensor.Range);
                }

                // move sensors
                MoveAllSensors(sensors);
            }

            return severity;
        }

        public int Part2()
        {
            return 0;
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

        private void MoveAllSensors(List<Sensor> sensors)
        {
            foreach (Sensor sensor in sensors)
            {
                sensor.Move();
            }
        }
    }
}
