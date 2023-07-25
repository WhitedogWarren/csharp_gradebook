using System.Collections.Generic;
using System;

namespace GradeBook
{
    public class FileMemoryBook : BookBase
    {
        public FileMemoryBook(string author, string name, List<double> grades) : base(author, name)
        {
            this.grades = grades;
        }

        private List<double> grades;

        public override List<double> GetGrades()
        {
            return this.grades;
        }

        public override void AddGrade(double grade)
        {
            if (grade <= 100 && grade >= 0)
            {
                Logger.Success($"Adding {grade} to book {Name}.");
                grades.Add(grade);
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
            foreach (var grade in this.grades)
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
            foreach (var grade in this.grades)
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
            foreach (var grade in this.grades) { sum += grade; };
            return sum / this.grades.Count;
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