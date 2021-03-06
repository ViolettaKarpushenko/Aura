using System;

namespace Aura.Web.Common
{
    [Serializable]
    public class PresentationException : Exception
    {
        public PresentationException()
            : base()
        {
        }

        public PresentationException(string message)
            : base(message)
        {
        }

        public PresentationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}