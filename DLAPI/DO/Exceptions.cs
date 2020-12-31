using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
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
}
