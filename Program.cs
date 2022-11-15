using System;

namespace Task
{
    class Program
    {
        static void exercise_3_1()
        {
            string error = "ошибка 3_1";
            int countGrades = 5;
            //int countGroups = 3;
            if(!File.Exists(InputOutput.Path + @"\" + InputOutput.NameInput))
            {
                Console.WriteLine($"нет файла {InputOutput.Path + @"\" + InputOutput.NameInput}");
                return;
            }
            StreamReader reader = new StreamReader(InputOutput.Path + @"\" + InputOutput.NameInput);
            List<string> S = new List<string>();
            while (true)
            {
                string to = reader.ReadLine();
                S.Add(to);
                if (to == null|| to == InputOutput.EndString) break;
                //Console.WriteLine(to);
            }
            reader.Close();
            int I = 0, SI = S.Count;
            List<Group> groups = new();
            Dictionary<string, int> map = new();
            var Q = new PriorityQueue<Group, double>(new Comparer(-1));
            do
            {
                Console.WriteLine($"введите имя студента");
                string name;
                if (I < SI)
                { 
                    name = S[I++];
                }
                else
                {
                    break;
                }
                Console.WriteLine(name);

                Console.WriteLine($"имя группы");
                string nameGroup;
                if (I < SI)
                {
                    nameGroup = S[I++];
                }
                else
                {
                    break;
                }
                Console.WriteLine(nameGroup);

                Console.WriteLine($"{countGrades} оценок");
                List<double> l;
                if (I < SI)
                {
                    l = StringToList(S[I++], out bool ch);
                    if (!ch || l.Count != countGrades)
                    {
                        return;
                    }
                }
                else
                {
                    break;
                }
                Console.WriteLine(ListToString(l));

                Student student = new Student(l, name);
                if (map.ContainsKey(nameGroup))
                {
                    int nom = map[nameGroup];
                    groups[nom].students.Add(student);
                }
                else
                {
                    Group group = new Group(nameGroup);
                    group.students.Add(student);
                    groups.Add(group);
                    map[nameGroup] = groups.Count() - 1;
                }

            } while (true);

            foreach (var group in groups)
            {
                Q.Enqueue(group, group.MidGrade());
            }
            List<string> St = new List<string>();
            St.Add("название групы /средний бал груп");
            while (Q.TryDequeue(out Group group, out double q))
            {
                St.Add(group.name + " " + q.ToString());
            }
            StreamWriter writer = new StreamWriter(InputOutput.Path + @"\" + InputOutput.NameOutput);
            foreach (string s in St)
            {
                Console.WriteLine(s);
                writer.WriteLine(s);
            }
            writer.Close();
        }
        static void WriteTask_3_4(PriorityQueue<Skier, double> Q)
        {
            List<string> S = new List<string>();
            S.Add("имя / результат");
            while (Q.TryDequeue(out Skier skier, out double q))
            {
                S.Add(skier.name + " " + q.ToString());
            }

            StreamWriter writer = new StreamWriter(InputOutput.Path + @"\" + InputOutput.NameOutput);
            foreach (string s in S)
            {
                Console.WriteLine(s);
                writer.WriteLine(s);
            }
            writer.Close();
        }
        static void exercise_3_4()
        {
            if (!File.Exists(InputOutput.Path + @"\" + InputOutput.NameInput))
            {
                Console.WriteLine($"нету файла {InputOutput.Path + @"\" + InputOutput.NameInput}");
                return;
            }
            StreamReader reader = new StreamReader(InputOutput.Path + @"\" + InputOutput.NameInput);
            List<string> St = new List<string>();
            while (true)
            {
                string to = reader.ReadLine();
                if (to == null || to == InputOutput.EndString) break;
                St.Add(to);
                //Console.WriteLine(to);
            }
            reader.Close();
            int I = 0, SI = St.Count;
            string error = "ошибка 3_4";
            List<Skier> skiers = new List<Skier>();
            var Q = new PriorityQueue<Skier, double>(new Comparer(-1));
            var Q1 = new PriorityQueue<Skier, double>(new Comparer(-1));
            var Q2 = new PriorityQueue<Skier, double>(new Comparer(-1));
            for (int i = 0; i == i; ++i)
            {
                string s;
                List<double> l = new List<double>();
                Console.WriteLine("введите имя");
                if (I < SI)
                {
                    s = St[I++];
                }
                else
                {
                    break;
                }
                Console.WriteLine(s);

                Console.WriteLine("ведите первый и второй результат");
                if (I < SI)
                {
                    l = StringToList(St[I++], out bool ch);
                    if (!ch || l.Count != 2)
                    {
                        return;
                    }
                }
                else
                {
                    break;
                }
                Console.WriteLine(ListToString(l));

                Skier skier = new Skier(l[0], l[1], s);
                Q.Enqueue(skier, skier.sum);
                Q1.Enqueue(skier, skier.rez1);
                Q2.Enqueue(skier, skier.rez2);
            }

            WriteTask_3_4(Q1);
            Console.WriteLine();
            WriteTask_3_4(Q2);
            Console.WriteLine();
            WriteTask_3_4(Q);


        }
        static void Main(string[] args)
        {
            {
                string path = Directory.GetCurrentDirectory();
                var s = Directory.GetParent(path);
                path = s.FullName;
                s = Directory.GetParent(path);
                path = s.FullName;
                s = Directory.GetParent(path);
                path = s.FullName;
                Console.WriteLine(path);
                InputOutput.Path = path;
            }

            InputOutput.NameInput = "Input1.txt";
            InputOutput.NameOutput = "Output1.txt";
            #region exercise_3_1;
            exercise_3_1();
            #endregion

            InputOutput.NameInput = "Input4.txt";
            InputOutput.NameOutput = "Output4.txt";
            #region exercise_3_4;
            exercise_3_4();
            #endregion
        }

        static string ListToString(List<double> L)
        {
            string s = "";
            if(L!=null)
            foreach (var v in L)
            {
                if (v == null) continue;
                s += v.ToString();
                s += " ";
            }
            return s;
        }
        static string ArrayToString(double[] L)
        {
            string s = "";
            foreach (double v in L)
            {
                s += v.ToString();
                s += " ";
            }
            return s;
        }
        static string[] ArrayToString(double[][] L, int n = 0)
        {
            string[] S = new string[n];
            int sh = 0;
            foreach (double[] v in L)
            {
                S[sh] = ArrayToString(v);
                sh++;
            }
            return S;
        }
        static string[] ArrayToString(double[,] L)
        {
            int n = L.GetLength(0), m = L.GetLength(1);
            string[] S = new string[n];
            for (int i = 0; i < n; i++)
            {
                string s = "";
                for (int j = 0; j < m; j++)
                {
                    s += L[i, j].ToString();
                    s += " ";
                }
                S[i] = s;
            }
            return S;
        }
        static int Compare((double, int) x, (double, int) y)
        {
            if (x.Item1 < y.Item1)
            {
                return 1;
            }

            if (x.Item1 > y.Item1)
            {
                return -1;
            }

            return 0;
        }
        static List<double>? StringToList(string s, out bool check)
        {
            
            check = true;
            if (s == null) return null;
            List<double> list = new List<double>();
            string[] S = s.Split(" ");
            foreach(string to in S)
            {
                if (to == "") continue;
                double add;
                if(!double.TryParse(to, out add))
                {
                    check = false;
                    continue;
                }
                list.Add(add);
            }
            return list;
        }
    }

    static class InputOutput
    {
        public const string EndString = "";
        public static string NameInput = "Input1.txt";
        public static string NameOutput = "Output1.txt";
        public static string Path = @"d:\";
       /* static public string? ReadStream(string path)
        {
            if (!File.Exists(Path + @"\" + NameInput))
            {
                //File.Create(Path + @"\" + NameInput);
                Console.WriteLine("нет файла " + Path + @"\" + NameInput);
                return "";
            }
            string Input = path + @"\" + NameInput;
            StreamReader Reader = new StreamReader(Input);
            string? ans = Reader.ReadLine();
            Reader.
            Reader.Close();
            Console.WriteLine($"ввод: {ans}");
            return ans;
        }*/
        /*static public void WriteStream(string path, string str)
        {
            if (!File.Exists(Path + @"\" + NameOutput))
            {
                File.Create(Path + @"\" + NameOutput);
            }
            string Output = path + @"\" + NameOutput;
            StreamWriter Writer = new StreamWriter(Output);
            Writer.WriteLine(str);
            Writer.Close();
            Console.WriteLine($"ввывод: {str}");
        }*/
        static public void Write(int ans)
        {
            Console.WriteLine("ans : " + ans.ToString());
        }
        static public void Write(double ans)
        {
            Console.WriteLine("ans : " + ans.ToString());
        }
        static public bool Read(out string s)
        {
            s = Console.ReadLine();
            if (s == EndString)
            {
                return false;
            }
            return true;
        }
        static public bool Read(out double x)
        {
            string s;
            s = Console.ReadLine();

            if (!double.TryParse(s, out x))
            {
                return false;
            }
            return true;
        }
        static public bool Read(out int x)
        {
            string s;
            s = Console.ReadLine();

            if (!int.TryParse(s, out x))
            {
                return false;
            }
            return true;
        }
        static public bool Read(out int x, out bool fl)
        {
            fl = false;
            string s;
            s = Console.ReadLine();
            if (s == EndString) fl = true;
            if (!int.TryParse(s, out x))
            {
                return fl;
            }
            return true;
        }
        static public bool Read(out double x, out bool fl)
        {
            fl = false;
            string s;
            s = Console.ReadLine();
            if (s == EndString) fl = true;
            if (!double.TryParse(s, out x))
            {
                return fl;
            }
            return true;
        }

        static public bool CheckRead(out double x, string Erorr = "ошибка", string? ans = null)
        {
            bool fl;
            if (!Read(out x, out fl))
            {
                Console.WriteLine(Erorr);
                return false;
            }

            if (fl)
            {
                if (ans != null)
                {
                    Console.WriteLine(ans);
                }
                return false;
            }
            return true;
        }
        static public bool CheckRead(out int x, string Erorr = "ошибка", string? ans = null)
        {
            bool fl;
            if (!Read(out x, out fl))
            {
                Console.WriteLine(Erorr);
                return false;
            }

            if (fl)
            {
                if (ans != null)
                {
                    Console.WriteLine(ans);
                }
                return false;
            }
            return true;
        }
        static public bool CheckSplitRead(out List<double> L, out bool Check, string Erorr = "ошибка", int? kol = null, string? ans = null)
        {
            Check = false;
            List<double> l = new List<double>();
            L = l;
            string? s = Console.ReadLine();
            if (s == EndString)
            {
                if (ans != null)
                {
                    Console.WriteLine(ans);
                }
                if (ans == null)
                {
                    Check = true;
                }
                return false;
            }
            if (s == null)
            {
                Console.WriteLine(Erorr);
                return false;
            }
            string[] S = s.Split(" ");
            foreach (string st in S)
            {
                double x;
                if (st == "") continue;
                if (!double.TryParse(st, out x))
                {
                    Console.WriteLine(Erorr);
                    return false;
                }
                L.Add(x);
            }
            if (kol != null && L.Count() != kol)
            {
                Console.WriteLine("не верное количество элементов в строке");
                return false;
            }
            return true;
        }

        /*static public bool CheckSplitReadStream(out List<double> L, out bool Check, string Erorr = "ошибка", int? kol = null, string path = null)
        {
            if(path == null)
            {
                path = Path;
            }
            Check = false;
            List<double> l = new List<double>();
            L = l;
            string? s = ReadStream(path);
            if (s == EndString)
            {
                Check = true;
                return false;
            }
            if (s == null)
            {
                Console.WriteLine(Erorr);
                return false;
            }
            string[] S = s.Split(" ");
            foreach (string st in S)
            {
                double x;
                if (st == "") continue;
                if (!double.TryParse(st, out x))
                {
                    Console.WriteLine(Erorr);
                    return false;
                }
                L.Add(x);
            }
            if (kol != null && L.Count() != kol)
            {
                Console.WriteLine("не верное количество элементов в строке");
                return false;
            }
            return true;
        }*/
    
    }
    class Comparer : IComparer<double>
    {
        public int A = 1;
        public Comparer(int a)
        {
            A = a;
        }
        public int Compare(double p1, double p2)
        {
            if (p1 < p2)
            {
                return -1 * A;
            }
            if (p1 > p2)
            {
                return A;
            }

            return 0;
        }
    }
    class Person
    {
        public string name;
    }
    class Student : Person
    {
        public List<double> grades = new List<double>();
        public double midGrade = 0;
        //public string name;
        public Student(double[] x, string name = "неизвестно")
        {
            int dl = x.Length;
            double sum = 0;
            for (int i = 0; i < dl; i++)
            {
                grades.Add(x[i]);
                sum += x[i];
            }
            if (dl != 0)
            {
                midGrade = sum / dl;
            }
            this.name = name;
        }
        public Student(List<double> x, string name = "неизвестно")
        {
            int dl = x.Count;
            double sum = 0;
            for (int i = 0; i < dl; i++)
            {
                grades.Add(x[i]);
                sum += x[i];
            }
            if (dl != 0)
            {
                midGrade = sum / dl;
            }
            this.name = name;
        }
    }
    class Skier : Person
    {
        //public string name;
        public double rez1, rez2, sum;
        public Skier(double rez1, double rez2, string name = "неизвестно")
        {
            this.name = name;
            this.rez1 = rez1;
            this.rez2 = rez2;
            sum = rez1 + rez2;
        }
    }
    class Group
    {
        public string name = "неизвестно";
        public List<Student> students = new List<Student>();
        public Group(string name = "неизвестно")
        {
            this.name = name;
        }
        public double MidGrade()
        {
            double sum = 0, ans = 0;
            foreach (Student student in students)
            {
                sum += student.midGrade;
            }

            if (students.Count != 0)
            {
                ans = sum / students.Count;
            }
            return ans;
        }

    }



}