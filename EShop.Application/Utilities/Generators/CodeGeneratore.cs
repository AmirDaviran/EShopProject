using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Utilities.Generators
{
    public class CodeGeneratore
    {
        public static string GenerateRandomCode()
        {
            return new Random().Next(10000, 99999).ToString();
        }
    }
}
