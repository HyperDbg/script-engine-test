using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.IO;

namespace EvalScripts
{
    class Program
    {
        enum ACTION_TYPE
        {
            CREATE_EXPRESSIONS,
            CREATE_CONDITIONAL_STATEMENTS,

        }
        private static void Generate(ACTION_TYPE Type)
        {
            string Result = string.Empty;
            string Sentence = string.Empty;
            string Script = string.Empty;
            bool EvalResult = false;


            StreamWriter TestCaseWithErrorFile = new StreamWriter("test-cases-wrong.txt");
            StreamWriter TestCaseWithoutErrorFile = new StreamWriter("test-cases-correct.txt");

            int Counter = 1;

            int InitCounter = Counter;
            while (Counter - InitCounter < 10000)
            {
                StatementGenerator.ResetDepth();

                if (Type == ACTION_TYPE.CREATE_EXPRESSIONS)
                {
                    Sentence = StatementGenerator.EXPRESSION();

                    //
                    // Avoid big statements!
                    //
                    if (!(Sentence.Length <= 150))
                    {
                        continue;
                    }

                    Script = "x = " + Sentence + "; test_statement(x);";
                    EvalResult = HighLevelScriptGen.EvaluateExpression(Sentence, ref Result);
                }
                else if (Type == ACTION_TYPE.CREATE_CONDITIONAL_STATEMENTS)
                {
                    Sentence = StatementGenerator.IF_STATEMENT();

                    if (!(Sentence.Length <= 400))
                    {
                        continue;
                    }

                    Script = Sentence;
                    EvalResult = HighLevelScriptGen.EvaluateConditionalStatement(Sentence, ref Result);

                }

                if (EvalResult)
                {
                    TestCaseWithoutErrorFile.WriteLine(Counter.ToString());
                    TestCaseWithoutErrorFile.WriteLine(Script);
                    TestCaseWithoutErrorFile.WriteLine(Result);
                    TestCaseWithoutErrorFile.WriteLine("$end$");
                    TestCaseWithoutErrorFile.Flush();
                }
                else
                {
                    TestCaseWithErrorFile.WriteLine(Counter.ToString());
                    TestCaseWithErrorFile.WriteLine(Script);
                    TestCaseWithErrorFile.WriteLine(Result);
                    TestCaseWithErrorFile.WriteLine("$end$");
                }


                Console.WriteLine(Counter);
                Console.WriteLine(Script);
                Console.WriteLine(Result);
                Console.WriteLine();
                Counter += 1;

            }

            TestCaseWithErrorFile.Close();
            TestCaseWithoutErrorFile.Close();
        }

        static void Main(string[] args)
        {
            // Generate(ACTION_TYPE.CREATE_EXPRESSIONS);
            Generate(ACTION_TYPE.CREATE_CONDITIONAL_STATEMENTS);
        }
    }
}
