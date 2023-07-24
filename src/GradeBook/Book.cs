using System;
using System.Collections.Generic;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public abstract class BookBase : NamedObject
    {
        public BookBase(string name) : base(name) { }
        public abstract void AddGrade(double grade);
        public abstract List<double> GetGrades();
    }

    public class InnerMemoryBook : BookBase
    {
        public InnerMemoryBook(string author, string name, List<double> grades) : base(name)
        {
            this.grades = grades;
            this.author = author;
        }
        
        public override void AddGrade(double grade)
        {
            if(grade <= 100 && grade >= 0)
            {
                Logger.Success($"Adding {grade} to book {Name}.");
                grades.Add(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else
            {
                throw new ArgumentException($"{grade} is outsite valid range ( between 0,0 and 100,0 )");
            }
        }

        public event GradeAddedDelegate GradeAdded;

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
                if(grade < min)
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

        public Statistics GetStatistics()
        {
            var result = new Statistics();
            result.Highest = this.Highest();
            result.Lowest = this.Lowest();
            result.Avg = this.Avg();
            return result;
        }

        public override List<double> GetGrades()
        {
            return this.grades;
        }

        private List<double> grades;
        
        public readonly string author;
    }
}