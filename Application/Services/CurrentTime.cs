using Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CurrentTime : ICurrentTime
    {
        public DateTime GetCurrentTime()
        {
          return   DateTime.UtcNow;
        }
    }
}
