﻿using System;
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
            String s = "ERROR: Invalid Information Exception!\n" + Message;
            if (InnerException != null)
                s = InnerException.ToString() + "\n" + s;
            return s;
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
            String s = "ERROR: Argument Not Found Exception!\n" + Message + "\n";
            if (InnerException != null)
                s = InnerException.ToString() + "\n" + s;
            return s;
        }
    }
}
