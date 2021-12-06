using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace EvalScripts
{
    class Program
    {

        static void Main(string[] args)
        {
            StatementGenerator.generate();
        }
    }
}
