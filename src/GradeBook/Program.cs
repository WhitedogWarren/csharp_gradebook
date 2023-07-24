using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.Title = "GradeBook";

            string nameValue;
            nameValue = args.Length == 0 ? "you" : args[0];
            Logger.Write($"hello, {nameValue} !");

            if (args.Length == 0 )
            {
                var isGoodName = false;
                do
                {
                    Logger.Write("What's your name ?");
                    var nameInput = Console.ReadLine();
                    if(nameInput == "")
                    {
                        Logger.Error("Your name can't be empty");
                    }
                    else
                    {
                        isGoodName = true;
                        nameValue = nameInput;
                    }
                } while(!isGoodName);
            }
            Console.Title = $"{nameValue}'s GradeBook";

            var bookName = $"{nameValue}'s book";
            Logger.Write($"Your current book name is '{bookName}'");
            Logger.Write("Would you like to change it ?");
            string changeBookName;
            do
            {
                Logger.Write("y/n ?");
                changeBookName = Console.ReadLine();
                if(!changeBookName.Equals("y") && !changeBookName.Equals("Y") && !changeBookName.Equals("n") & !changeBookName.Equals("N"))
                {
                    Logger.Error("Invalid answer !");
                }
            } while (!changeBookName.Equals("y") && !changeBookName.Equals("Y") && !changeBookName.Equals("n") && !changeBookName.Equals("N"));

            if(changeBookName.Equals("y") || changeBookName.Equals("Y"))
            {
                
                var isGoodName = false;
                do
                {
                    Logger.Write("Enter your book name :");
                    var bookNameInput = Console.ReadLine();
                    if(bookNameInput == "")
                    {
                        Logger.Error("Book name can't be empty !");
                    }
                    else
                    {
                        bookName = bookNameInput;
                        isGoodName = true;
                    }
                } while (!isGoodName);
            }

            var book = new InnerMemoryBook(nameValue, bookName, new List<double>());
            book.GradeAdded += OnGradeAdded;
            Logger.Success($"Book {book.Name} created");

            EnterGrades( book );

            Logger.Success($"Statistics for {book.Name} of {book.author}:");
            Logger.Write($"Le plus haut score est de {book.GetStatistics().Highest} points");
            Logger.Write($"Le plus petit score est de {book.GetStatistics().Lowest} points");
            Logger.Write( $"Le score moyen est de {book.GetStatistics().Avg:N2} points");
        }

        private static void EnterGrades(BookBase book)
        {
            Logger.Write("Enter a grade ( write 'q' to end ) :");
            var isOver = false;
            do
            {
                var input = Console.ReadLine();
                if (input == "q")
                {
                    isOver = true;
                }
                else
                {
                    try
                    {
                        book.AddGrade(double.Parse(input));
                    }
                    catch (FormatException)
                    {
                        Logger.Error("invalid format, grade must be numeric");
                    }
                    catch (ArgumentException error)
                    {
                        Logger.Error(error.Message);
                    }
                    finally
                    {
                        if (!isOver)
                        {
                            Logger.Write("next grade ? ( or 'q' to end ) :");
                        }
                    }
                }
            } while (!isOver);
            Console.Clear();
            var listOutput = "";
            for (var i = 0; i < book.GetGrades().Count; i++)
            {
                listOutput += book.GetGrades()[i].ToString().Replace(',', '.');
                if (i != book.GetGrades().Count - 1)
                {
                    listOutput += ", ";
                }
            }
            Logger.Write($"Grades : {listOutput}");
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            Logger.Write("A grade was added");
            
        }
    }
}