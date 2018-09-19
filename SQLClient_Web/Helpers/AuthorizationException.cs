using System;

namespace SQLClient_Web.Helpers
{
    public class AuthorizationException<T> : Exception
    {
        public T Type { get; set; }
        public AuthorizationException(T Type) : base()
        {
            this.Type = Type;
        }
    }
}
