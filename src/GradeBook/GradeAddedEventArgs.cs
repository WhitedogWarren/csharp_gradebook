using System;

namespace GradeBook
{
    public class GradeAddedEventArgs : EventArgs
    {
        public GradeAddedEventArgs(string book, double grade)
        {
            Book = book;
            Grade = grade;
        }

        public string Book { get; private set; }
        public double Grade { get; private set; }
    }
}