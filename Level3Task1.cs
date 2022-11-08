using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net.Mail;
using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using System.Windows.Markup;

class HelloWorld
{
    static Group[] Sorti(Group[] A)
    {
        int left = 0;
        int right = A.Length - 1;

        while (left < right)
        {
            for (int i = left; i < right; i++)
            {
                if (A[i].GroupSredmark < A[i + 1].GroupSredmark)
                {
                    Group x = A[i];
                    A[i] = A[i + 1];
                    A[i + 1] = x;
                }

            }
            right--;

            for (int i = right; i > left; i--)
            {
                if (A[i - 1].GroupSredmark < A[i].GroupSredmark)
                {
                    Group x = A[i];
                    A[i] = A[i - 1];
                    A[i - 1] = x;
                }
            }
            left++;
        }
        return A;
    }
    class Student
    {
        public string Surname;
        public double[] Marks;
        public double sredmark;
        public Student(string Surname, double[] Marks, int m)
        {
            this.Surname = Surname;
            this.Marks = Marks;
            double sum = 0;
            for (int i = 0; i < m; i++)
            {
                sum = sum + Marks[i];
            }
            this.sredmark = sum / m;
        }
    }
    class Group
    {
        public string GroupName;
        public int KollvoStudents;
        public double GroupSredmark;
        public Student[] Students;
        public Group(string GroupName, int KollvoStudents, Student[] students)
        {
            this.GroupName = GroupName;
            this.KollvoStudents = KollvoStudents;
            this.Students = students;
            double sum = 0;
            for (int i = 0; i < KollvoStudents; i++)
            {
                sum = sum + students[i].sredmark;
            }
            this.GroupSredmark = sum / KollvoStudents;
        }
    }
    static int Main()
    {
        StreamReader sr = new StreamReader("C:\\Users\\User\\Desktop\\Проги\\Projects\\crasivo\\input3_1.txt");
        int n;
        int.TryParse(sr.ReadLine(), out n);
        int m;
        int.TryParse(sr.ReadLine(), out m);
        if (n <= 0 || m <= 0) { Console.WriteLine("Incorrect data!!!"); return 0; }
        Group[] Gropus = new Group[n];
        for (int i = 0; i < n; i++) // Ââîäèò ãðóïïó
        {
            string GroupName = sr.ReadLine();
            int p;
            int.TryParse(sr.ReadLine(), out p);
            if (p <= 0) { Console.WriteLine("Incorrect data!!!"); return 0; }
            Student[] student = new Student[p];
            double[] sredmars = new double[p];
            for (int j = 0; j < p; j++) //Ââîäèò ñòóäåíòà
            {
                double[] marks = new double[m];
                string surname = sr.ReadLine();
                string[] line;
                while (true)
                {
                    line = sr.ReadLine().Split(" ");
                    if (line.Length != m) { Console.WriteLine("Re-enter the row!"); continue; }
                    else break;
                }
                for (int l = 0; l < m; l++)
                {
                    double x;
                    if (double.TryParse(line[l], out x) == false) { Console.WriteLine("Incorrect data!!!"); return 0; }
                    double.TryParse(line[l], out x);
                    marks[l] = x;
                }
                student[j] = new Student(surname, marks, m);
            }
            Gropus[i] = new Group(GroupName, p, student);
        }
        sr.Close();
        Sorti(Gropus);
        StreamWriter sw = new StreamWriter("C:\\Users\\User\\Desktop\\Проги\\Projects\\crasivo\\output3_1.txt");
        sw.WriteLine("The list of TOP: ");
        for (int i = 0; i < n; i++)
        {
            sw.WriteLine(Gropus[i].GroupName + " - " + Gropus[i].GroupSredmark);
        }
        sw.Close();
        return 0;
    }
}
