using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace _8th_Lab

{
    class Persona
    {
        public string name;
    }

    class Athlete: Persona
    {
        public double res1, res2, sum;
        public string team;

        public Athlete(double res1, double res2, string name = "unk", string team = "russia")
        {
            this.name = name;
            this.res1 = res1;
            this.res2 = res2;
            sum = res1 + res2;
            this.team = team;
        }
    }

    class WomanRunner: Persona
    {
        public string teacher;
        public string group;
        public double res;

        public WomanRunner(double res, string name = "unk", string teacher = "unk", string group = "unk")
        {
            this.res = res;
            this.teacher = teacher;
            this.group = group;
            this.name = name;
        }
    }

    class Program
    {
        private static void add_line(FileStream fs, string line)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(line);
            fs.Write(info, 0, info.Length);
        }

        static string path_to_file(string relative_path)
        {
            string current_directory = AppDomain.CurrentDomain.BaseDirectory;
            string file = Path.Combine(current_directory, relative_path);

            return Path.GetFullPath(file);
        }

        static void Main(string[] args)
        {
            #region task 1
            Console.WriteLine("task 1");
            {
                string output_path = "/Users/alexzzmtsvv/Desktop/task1.txt";
                // I put file in bin/Debug/net6.0 otherwise it doesn't work
                string input_path = path_to_file("input_data_1.txt");

                if (File.Exists(output_path))
                {
                    File.Delete(output_path);
                }

                if (!File.Exists(input_path))
                {
                    Console.WriteLine("Incorrect input path");
                    return;
                }

                string[] lines = File.ReadAllLines(input_path);
                List<Athlete> athletes = new List<Athlete>();

                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                    string[] tmp = line.Split(";");
                    // name team res1 res2
                    Athlete athl = new Athlete(double.Parse(tmp[2]), double.Parse(tmp[3]), tmp[0], tmp[1]);
                    athletes.Add(athl);

                }


                IOrderedEnumerable<Athlete> sorted_ = athletes.OrderBy(g => -g.sum);

                using (FileStream fs = File.Create(output_path))
                {
                    foreach (Athlete ath in sorted_)
                    {
                        string line = $"{ath.name}\t{ath.team}\t{ath.sum}\n";
                        add_line(fs, line);
                    }
                }
            }
            #endregion

            #region task 2
            Console.WriteLine("task 2");
            {
                string output_path = "/Users/alexzzmtsvv/Desktop/task2.txt";
                string input_path = path_to_file("input_data_2.txt");

                if (File.Exists(output_path))
                {
                    File.Delete(output_path);
                }

                if (!File.Exists(input_path))
                {
                    Console.WriteLine("Incorrect input path");
                    return;
                }

                string[] lines = File.ReadAllLines(input_path);
                List<WomanRunner> runners = new List<WomanRunner>();
                int normativ_counter = 0;

                double threshold = double.Parse(lines[0].Split("-")[1]);

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] tmp = lines[i].Split(";");
                    // name teacher group res
                    double res = double.Parse(tmp[3]);
                    WomanRunner runner = new WomanRunner(res, tmp[0], tmp[1], tmp[2]);
                    if (res >= threshold)
                    {
                        normativ_counter++;
                    }
                    runners.Add(runner);
                }

                IOrderedEnumerable<WomanRunner> sorted_ = runners.OrderBy(g => -g.res);

                using (FileStream fs = File.Create(output_path))
                {
                    foreach (WomanRunner runner in sorted_)
                    {
                        string line = $"{runner.name}\t{runner.group}\t{runner.teacher}\t{runner.res}\n";
                        add_line(fs, line);
                    }
                    string end_line = $"выполнили норматив\t{normativ_counter}";
                    add_line(fs, "\n");
                    add_line(fs, end_line);
                }
            }
            #endregion

        }
    }
}