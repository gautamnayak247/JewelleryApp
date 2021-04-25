namespace Jewellery.Domain.Entities
{
    using System;

    public class UnAuthorizedException: Exception
    {
        public UnAuthorizedException(string message, Exception ex = null) : base(message, ex) { }
    }
    public class DBException: Exception
    {
        public DBException(string message, Exception ex = null) : base(message, ex) { }
    }
 
}
