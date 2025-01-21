using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpApp.Core.Interfaces
{
    public interface ICredsStorage
    {
        public string GetEmail();

        public string GetPassword();
    }
}
