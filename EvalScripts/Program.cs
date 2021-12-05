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

        public static bool ScriptRun(string Script)
        {
            /*
            var code = "5 + num";
            var numValue = 3;

            var setParam = await CSharpScript.RunAsync("var num = " + numValue + ";");
            var runCode = await setParam.ContinueWithAsync(code);
            var x = runCode.ReturnValue;

            */

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
        static async Task Main(string[] args)
        {
            StatementGenerator.generate();
            return;

            bool result = ScriptRun(@"
            
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
