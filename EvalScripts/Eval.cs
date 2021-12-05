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
        public static async Task<Tuple<bool,int>> EvalStatementAsync(string Statement)
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
        }
}
