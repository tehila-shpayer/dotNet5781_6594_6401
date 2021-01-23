using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// מחלקת חריגות לקלט בלתי תקין
    /// </summary>
    [Serializable]
    public class InvalidInformationException : Exception
    {
        public InvalidInformationException() : base() { }
        public InvalidInformationException(string message) : base(message) { }
        public InvalidInformationException(string message, Exception innerException) : base(message, innerException) { }
        public override string ToString()
        {
            return base.ToString() + "\nInvalid Information Exception!\n" + Message;
        }
    }
    /// <summary>
    /// מחלקת חריגות לאי מציאת ארגומנט כלשהו
    /// </summary>
    [Serializable]
    public class ArgumentNotFoundException : Exception
    {
        public ArgumentNotFoundException() : base() { }
        public ArgumentNotFoundException(string message) : base(message) { }
        public ArgumentNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        public override string ToString()
        {
            return base.ToString() + "\nArgument Not Found Exception!\n" + Message;
        }
    }
    /// <summary>
    /// XML מחלקת חריגות לכשל בהעלאה או יצירת קבצי 
    /// </summary>
    public class XMLFileLoadCreateException : Exception
    {
        public string xmlFilePath;
        public XMLFileLoadCreateException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
    }
}
