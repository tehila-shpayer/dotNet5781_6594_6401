using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    class InvalidInformationException<T> : Exception
    {
        public T Argument;
        public InvalidInformationException(T arg) : base() => Argument = arg;
        public InvalidInformationException(T arg, string message) : base(message) => Argument = arg;
        public InvalidInformationException(T arg, string message, Exception innerException) : base(message, innerException) => Argument = arg;
        public override string ToString() => base.ToString() + $", bad argument content: {Argument}";
    }
    class ArgumentNotFoundException<T> : Exception
    {
        public T Argument;
        public ArgumentNotFoundException(T arg) : base() => Argument = arg;
        public ArgumentNotFoundException(T arg, string message) : base(message) => Argument = arg;
        public ArgumentNotFoundException(T arg, string message, Exception innerException) : base(message, innerException) => Argument = arg;
        public override string ToString() => base.ToString() + $", argument not found: {Argument}";
    }
}
