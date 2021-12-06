using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvalScripts
{
    class HighLevelScriptGen
    {
        private static string ScriptApplyCSharpChange(string Script)
        {
            string Temp = Script.Replace("0n", "").Replace("0y", "0b").Replace("/", "//");

            return Temp;
        }

        public static bool EvaluateExpression(string Script, ref string Result)
        {
            //
            // Change script based on c# (convert script engine scripts to
            // c# codes)
            //
            string CSharpBasedString = ScriptApplyCSharpChange(Script);

            //
            // Evaluate the statement
            //
            var EvalResult = Eval.EvalStatementAsync(CSharpBasedString);

            if (EvalResult.Result.Item1 == true)
            {
                Result = EvalResult.Result.Item2.ToString("X");
                Result = Result.Replace("0x", "");
                return true;
            }
            else
            {
                Result = "$error$";
                return false;
            }
        }

        public static bool EvaluateConditionalStatement(string Script, ref string Result)
        {
            //
            // Change script based on c# (convert script engine scripts to
            // c# codes)
            //
            string CSharpBasedString = ScriptApplyCSharpChange(Script);

            //
            // Evaluate the conditional statement
            //
            var EvalResult = Eval.EvalScriptRunConditionalStatementAsync(CSharpBasedString);

            if (EvalResult.Result.Item1 == true)
            {
                Result = EvalResult.Result.Item2.ToString("X");
                return true;
            }
            else
            {
                Result = "$error$";
                return false;
            }
        }

    }
}
