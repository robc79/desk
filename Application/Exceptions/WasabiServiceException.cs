namespace Desk.Application.Exceptions;

[Serializable]
public class WasabiServiceException : Exception
{
    public int ExpectedStatusCode { get; protected set; }

        public int ActualStatusCode { get; protected set; }

        protected WasabiServiceException() { }
        
        public WasabiServiceException(string message, int expectedStatusCode, int actualStatusCode) : base(message) { }
        
        public WasabiServiceException(string message, int expectedStatusCode, int actualStatusCode, Exception inner) : base(message, inner) { }
}