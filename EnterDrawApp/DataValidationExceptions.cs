using System;

namespace EnterDrawApp
{
    public class NameNotValidException : Exception
    {
        public NameNotValidException() : base()
        {
            
        }

        public NameNotValidException(string message) : base(message)
        {
            
        }

        public NameNotValidException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }

    public class EmailNotValidException : Exception
    {
        public EmailNotValidException() : base()
        {

        }

        public EmailNotValidException(string message) : base(message)
        {

        }

        public EmailNotValidException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }

    public class PhoneNrNotValidException : Exception
    {
        public PhoneNrNotValidException() : base()
        {

        }

        public PhoneNrNotValidException(string message) : base(message)
        {

        }

        public PhoneNrNotValidException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }

    public class SerialNrNotValidException : Exception
    {
        public SerialNrNotValidException() : base()
        {

        }

        public SerialNrNotValidException(string message) : base(message)
        {

        }

        public SerialNrNotValidException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }

}