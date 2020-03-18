using SM.Common;
using System;

namespace SM.Infrastructure.Services
{
    public class ServerDateTime: IDateTime
    {
        public DateTime Now => DateTime.Now;

        public int CurrentYear => DateTime.Now.Year;
    }
}
