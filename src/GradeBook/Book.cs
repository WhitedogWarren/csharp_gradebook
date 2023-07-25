using System;
using System.Collections.Generic;

namespace GradeBook
{
    public interface IBook
    {
        string Name { get; }
        string Author { get; }
        List<double> GetGrades();
        void AddGrade(double grade);
        Statistics GetStatistics();
        event GradeAddedDelegate GradeAdded;
    }
    
    public delegate void GradeAddedDelegate(object sender, GradeAddedEventArgs args);

    public abstract class BookBase : NamedObject, IBook
    {
        public BookBase(string author, string name) : base(name)
        {
            this.author = author;
        }
        private string author;
        public string Author
        {
            get
            {
                return author;
            }
        }
        public abstract void AddGrade(double grade);
        public abstract List<double> GetGrades();
        public abstract Statistics GetStatistics();
        public virtual event GradeAddedDelegate GradeAdded;
    }
}