using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BLL.Validation
{
    [Serializable]
    public class CatalogException : Exception
    {
        public CatalogException()
        {

        }
        public CatalogException(string message) : base(message)
        {

        }
        public CatalogException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
        public CatalogException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
