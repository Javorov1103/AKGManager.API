using AutoService.API.Data;
using AutoService.API.Features.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace AutoService.API
{
    public class AppContext
    {
        private static object lockExceptionLogging = new object();
        public static User User
        {
            get
            {
                if (Thread.CurrentPrincipal != null && Thread.CurrentPrincipal.Identity is User)
                {
                    return (User)Thread.CurrentPrincipal.Identity;
                }

                return null;
            }
        }

        public static void HandleException(Exception ex)
        {
            HandleException(ex, 0, null);
        }

        public static void HandleException(Exception ex, int ErrorCode, string CustomMessage)
        {
            lock (lockExceptionLogging)
            {
                var traceErrors = new TraceSource("ErrorsLogTrace");

                var errorMessage = Environment.NewLine + Environment.NewLine + ex.Message + (string.IsNullOrEmpty(CustomMessage) ? string.Empty : " - " + CustomMessage) + "\n" + ex.StackTrace;

                traceErrors.TraceEvent(TraceEventType.Error, ErrorCode, errorMessage);
                traceErrors.Close();
            }
        }

        public void SetUser(HttpRequest request)
        {
            //string encryptedToken = null;

            //StringValues test;

            //bool result = request.Headers.TryGetValue("Authorization", out test);

           
        }
    }
}
