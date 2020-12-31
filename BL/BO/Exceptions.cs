using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class BOInvalidInformationException : Exception
    {
        public BOInvalidInformationException() : base() { }
        public BOInvalidInformationException(string message) : base(message) { }
        public BOInvalidInformationException(string message, Exception innerException) : base(message, innerException) { }
        public override string ToString()
        {
            return base.ToString() + "\nERROR!\n" + Message + InnerException.ToString();
        }
    }
    [Serializable]
    public class BOArgumentNotFoundException : Exception
    {
        public BOArgumentNotFoundException() : base() { }
        public BOArgumentNotFoundException(string message) : base(message) { }
        public BOArgumentNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        public override string ToString()
        {
            return base.ToString() + "\nERROR!\n" + Message + InnerException.ToString();
        }
    }
}
