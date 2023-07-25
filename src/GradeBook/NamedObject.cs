namespace GradeBook
{
    public class NamedObject
    {
        public NamedObject(string name)
        {
            this.name = name;
        }
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("name");
                }
                name = value;
            }
        }
    }
}