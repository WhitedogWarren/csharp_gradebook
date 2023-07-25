using System.Collections.Generic;
using System;
using System.IO;

namespace GradeBook
{
    public class FileMemoryBook : BookBase
    {
        public FileMemoryBook(string author, string name, List<double> grades) : base(author, name)
        {
            using (var writer = File.AppendText($"{this.Name}.txt"))
            {
                grades.ForEach(grade => writer.WriteLine(grades));
            }
        }

        

        public override List<double> GetGrades()
        {
            var stringList = File.ReadAllLines($"{this.Name}.txt");
            var grades = new List<double>();
            foreach ( var grade in stringList )
            {
                grades.Add( Convert.ToDouble(grade) );
            }
            return grades;
            //$"{Name.txt}"
        }

        public override void AddGrade(double grade)
        {
            if (grade <= 100 && grade >= 0)
            {
                Logger.Success($"Adding {grade} to book {this.Name}.");
                using (var writer = File.AppendText($"{Name}.txt"))
                {
                    writer.WriteLine(grade);
                }
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else
            {
                throw new ArgumentException($"{grade} is outsite valid range ( between 0,0 and 100,0 )");
            }
        }

        public override event GradeAddedDelegate GradeAdded;

        public double Highest()
        {
            var max = double.MinValue;
            foreach (var grade in this.GetGrades())
            {
                if (grade > max)
                {
                    max = grade;
                }
            }
            return max;
        }

        public double Lowest()
        {
            var min = double.MaxValue;
            foreach (var grade in this.GetGrades())
            {
                if (grade < min)
                {
                    min = grade;
                }
            }
            return min;
        }

        public double Avg()
        {
            var sum = 0.0;
            var grades = this.GetGrades();
            foreach (var grade in grades) { sum += grade; };
            return sum / grades.Count;
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            result.Highest = this.Highest();
            result.Lowest = this.Lowest();
            result.Avg = this.Avg();
            return result;
        }
    }
}