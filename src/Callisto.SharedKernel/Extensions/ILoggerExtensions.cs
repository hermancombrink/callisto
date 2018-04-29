using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Callisto.SharedKernel.Extensions
{
   public static class ILoggerExtensions
    {
        public static void LogAndThrowError<T>(this ILogger logger, string message) where T : Exception
        {
            logger.LogError(message);
            var ex = (T)Activator.CreateInstance(typeof(T), message);
            throw ex;
        }
    }
}
