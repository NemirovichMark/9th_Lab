using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Globalization;
using System.Net.Mail;
using System.Runtime.ExceptionServices;
using System.Windows.Markup;

class HelloWorld
{
    static Student[] Sorti(Student[] A)
    {
        int left = 0;
        int right = A.Length - 1;

        while (left < right)
        {
            for (int i = left; i < right; i++)
            {
                if (A[i].Marks < A[i + 1].Marks)
                {
                    Student x = A[i];
                    A[i] = A[i + 1];
                    A[i + 1] = x;
                }

            }
            right--;

            for (int i = right; i > left; i--)
            {
                if (A[i - 1].Marks < A[i].Marks)
                {
                    Student x = A[i];
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
        public double Marks;
        public Student(string Surname, double Marks)
        {
            this.Surname = Surname;
            this.Marks = Marks;
        }
    }
    class Group
    {
        public string GroupName;
        public int KollvoStudents;
        public Student[] Students;
        public Group(string GroupName, int KollvoStudents, Student[] students)
        {
            this.GroupName = GroupName;
            this.KollvoStudents = KollvoStudents;
            this.Students = students;
        }
    }
    static int Main()
    {
        StreamReader sr = new StreamReader("C:\\Users\\User\\Desktop\\Проги\\Projects\\crasivo\\input3_4.txt");
        int n;
        int z = 0;
        int.TryParse(sr.ReadLine(), out n);
        if (n <= 0) { Console.WriteLine("Incorrect data!!!"); return 0; }
        Group[] Gropus = new Group[n];
        for (int i = 0; i < n; i++) // Вводит группу
        {
            string GroupName = sr.ReadLine();
            int p;
            int.TryParse(sr.ReadLine(), out p);
            if (p <= 0) { Console.WriteLine("Incorrect data!!!"); return 0; }
            Student[] student = new Student[p];
            double[] sredmars = new double[p];
            for (int j = 0; j < p; j++) //Вводит студента
            {
                z++;
                string surname = sr.ReadLine();
                double x;
                string line;
                line = sr.ReadLine();
                if (double.TryParse(line, out x) == false) { Console.WriteLine("Incorrect data!!!"); return 0; }
                double.TryParse(line, out x);
                student[j] = new Student(surname, x);
            }
            Gropus[i] = new Group(GroupName, p, student);
        }
        for (int i = 0; i < n; i++)
        {
            Sorti(Gropus[i].Students);
        }
        Student[] obsh = new Student[z];
        int l = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < Gropus[i].KollvoStudents; j++)
            {
                obsh[l] = Gropus[i].Students[j];
                l++;
            }
        }
        Sorti(obsh);
        sr.Close();
        StreamWriter sw = new StreamWriter("C:\\Users\\User\\Desktop\\Проги\\Projects\\crasivo\\output3_4.txt");
        sw.WriteLine("The general summary list:");

        for (int i = 0; i < z; i++)
        {
            sw.WriteLine(obsh[i].Surname + " - " + obsh[i].Marks);
        }

        sw.WriteLine();
        sw.WriteLine("The group list:");

        for (int j = 0; j < n; j++)
        {

            sw.WriteLine("The list of TOP of Group <" + Gropus[j].GroupName + ">: ");
            for (int i = 0; i < Gropus[j].KollvoStudents; i++)
            {
                sw.WriteLine(Gropus[j].Students[i].Surname + " - " + Gropus[j].Students[i].Marks);
            }
        }
        sw.Close();
        return 0;
    }
}
