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

        private static void InitilizeIdentifers()
        {
            //
            // Registers
            //
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION {IdentifierName = "@rax", Value = 0x1 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION {IdentifierName = "@rdx", Value = 0x3 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION {IdentifierName = "@rbx", Value = 0x4 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION {IdentifierName = "@rsp", Value = 0x5 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION {IdentifierName = "@rbp", Value = 0x6 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION {IdentifierName = "@rsi", Value = 0x7 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION {IdentifierName = "@rdi", Value = 0x8 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION {IdentifierName = "@r8", Value = 0x9 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION {IdentifierName = "@r9", Value = 0xa });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION {IdentifierName = "@r10", Value = 0xb });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION {IdentifierName = "@r11", Value = 0xc });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION {IdentifierName = "@r12", Value = 0xd });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION {IdentifierName = "@r13", Value = 0xe });

            //
            // Pseudo-registers
            //
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION { IdentifierName = "$proc", Value = 0x0 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION { IdentifierName = "$thread", Value = 0x0 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION { IdentifierName = "$teb", Value = 0x0 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION { IdentifierName = "$ip", Value = 0x0 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION { IdentifierName = "$buffer", Value = 0x0 });

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

                    if (!(Sentence.Length <= 1000))
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
            //
            // First of all we should initialize identifiers and their values
            //
            InitilizeIdentifers();

             Generate(ACTION_TYPE.CREATE_EXPRESSIONS);
            //Generate(ACTION_TYPE.CREATE_CONDITIONAL_STATEMENTS);
        }
    }
}
