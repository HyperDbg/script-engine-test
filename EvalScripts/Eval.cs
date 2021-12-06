using Microsoft.CodeAnalysis.CSharp.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvalScripts
{
    class Eval
    {
        public static async Task<Tuple<bool, int>> EvalStatementAsync(string Statement)
        {
            // Statement = "5 + 5";

            try
            {
                var setParam = await CSharpScript.RunAsync("var num = " + Statement + ";");
                var runCode = await setParam.ContinueWithAsync("num");
                var x = runCode.ReturnValue;

                //
                // Script run without error
                //
                return new Tuple<bool, int>(true, (int)x);
            }
            catch (Exception)
            {

                //
                // Script has error
                //
                return new Tuple<bool, int>(false, 0);
            }

        }

        public static Tuple<bool, int> EvalScriptRun(string Script)
        {
            /*
            bool result = EvalScriptRun(@"
            
            for (int i = 0; i < 100; i++) 
            {
                System.Console.WriteLine(i);
            }

            ");s
            */

            try
            {
                CSharpScript.EvaluateAsync(Script);

                //
                // Script run without error
                //
                return new Tuple<bool, int>(true, 0);
            }
            catch (Exception)
            {

                //
                // Script has error
                //
                return new Tuple<bool, int>(false, 0);
            }
        }
    }
}
