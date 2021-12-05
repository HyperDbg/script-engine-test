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
        public static bool EvalScriptRun(string Script)
        {
            try
            {
                CSharpScript.EvaluateAsync(Script);

                //
                // Script run without error
                //
                return true;
            }
            catch (Exception)
            {

                //
                // Script has error
                //
                return false;
            }
        }
        static void Main(string[] args)
        {
            StatementGenerator.generate();

            return;
            bool result = EvalScriptRun(@"
            
            for (int i = 0; i < 100; i++) 
            {
                System.Console.WriteLine(i);
            }

            ");

            if (!result)
            {
                Console.WriteLine("Script has error");
            }
            else
            {
                Console.WriteLine("Script run without error");
            }

            Console.ReadLine();

        }
    }
}
