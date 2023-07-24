using System;
using System.Collections.Generic;
using Xunit;

namespace GradeBook.Tests
{
    public class TypeTests
    {
        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            var book1 = GetBook("Book 1", new List<double>() { 3.2, 7.6, 5.4 });
            var book2 = GetBook("Book 2", new List<double>() { 2.8, 4.3, 9.2 });

            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
            Assert.NotSame(book1, book2);
        }

        [Fact]
        public void TwoVarsCanReferenceSameObject()
        {
            var book1 = GetBook("Book 1", new List<double>() { 3.2, 7.6, 5.4 });
            var book2 = book1;

            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));
        }

        [Fact]
        public void CanSetNameFromReference()
        {
            var book1 = GetBook("Book 1", new List<double>() { 3.2, 7.6, 5.4 });
            SetName(book1, "New name");

            Assert.Equal("New name", book1.Name);
        }

        [Fact]
        public void CSharpIsPassByValue()
        {
            var book1 = GetBook("Book 1", new List<double>());
            GetBookSetName(book1, "New Name");

            Assert.Equal("Book 1", book1.Name);
        }

        [Fact]
        public void CSharpCanPassByRef()
        {
            var book1 = GetBook("Book 1", new List<double>());
            GetBookSetName(out book1, "New Name");

            Assert.Equal("New Name", book1.Name);
        }

        [Fact]
        public void ValueTypeAlsoPassByValue()
        {
            var x = GetInt();
            Assert.Equal(3, x);
            SetInt(ref x);
            Assert.Equal(42, x);
        }

        [Fact]
        public void StringsBehaveLikeValueType()
        {
            string name = "Whitedog";
            string upper = MakeUpperCase(name);

            Assert.Equal("Whitedog", name);
            Assert.Equal("WHITEDOG", upper);
        }
        
        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log = ReturnMessage; // equals: log = new WriteLogDelegate(ReturnMessage);
            log += ReturnMessage;
            log += ReturnLowerMessage;
            var result = log("Hello!");
            Assert.Equal(3, delegateCount);
        }

        private int delegateCount = 0;

        public delegate string WriteLogDelegate(string logMessage);

        private string ReturnMessage(string message)
        {
            delegateCount++;
            return message;
        }

        private string ReturnLowerMessage(string message)
        {
            delegateCount++;
            return message.ToLower();
        }

        private InnerMemoryBook GetBook(string name, List<double> grades)
        {
            return new InnerMemoryBook("Whitedog", name, grades);
        }

        private void SetName(BookBase book1, string name)
        {
            book1.Name = name;
        }

        private void GetBookSetName(BookBase book, string name)
        {
            book = new InnerMemoryBook("Whitedog", name, new List<double>() );
        }

        private void GetBookSetName(out InnerMemoryBook book, string name)
        {
            book = new InnerMemoryBook("Whitedog", name, new List<double>());
        }

        private int GetInt()
        {
            return 3;
        }

        private void SetInt(ref int x)
        {
            x = 42;
        }

        private string MakeUpperCase(string parameter)
        {
            return parameter.ToUpper();
        }
    }
}