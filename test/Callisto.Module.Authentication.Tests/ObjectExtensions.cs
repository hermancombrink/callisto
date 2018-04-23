using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Callisto.Module.Authentication.Tests
{
    public static class ObjectExtensions
    {
        public static StringContent ToContent(this string Json)
        {
            return new StringContent(Json, Encoding.UTF8, "application/json");
        }
    }
}
