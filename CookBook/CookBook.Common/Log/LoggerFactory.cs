namespace CookBook.Common.Log
{
    public class LoggerFactory
    {
        public ILogger GetLogger()
        {
            return new Logger();
        }
    }
}
