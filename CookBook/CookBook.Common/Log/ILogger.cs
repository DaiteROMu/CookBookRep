using System;

namespace CookBook.Common.Log
{
    public interface ILogger
    {        
        void ErrorMessage(Exception ex);               
    }
}
