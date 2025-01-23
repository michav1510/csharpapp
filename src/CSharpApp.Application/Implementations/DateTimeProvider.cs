using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpApp.Application.Implementations
{
    public class DateTimeProvider :IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
