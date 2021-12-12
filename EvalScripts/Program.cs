﻿using System;
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
            CREATE_CONDITIONAL_STATEMENTS_COMBINED_WITH_OTHER_STATEMENT,
            CREATE_FOR_LOOP,

        }

        private static void InitilizeIdentifers()
        {
            //
            // Registers
            //
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION { IdentifierName = "@rax", Value = 0x1 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION { IdentifierName = "@rdx", Value = 0x3 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION { IdentifierName = "@rbx", Value = 0x4 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION { IdentifierName = "@rsp", Value = 0x5 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION { IdentifierName = "@rbp", Value = 0x6 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION { IdentifierName = "@rsi", Value = 0x7 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION { IdentifierName = "@rdi", Value = 0x8 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION { IdentifierName = "@r8", Value = 0x9 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION { IdentifierName = "@r9", Value = 0xa });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION { IdentifierName = "@r10", Value = 0xb });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION { IdentifierName = "@r11", Value = 0xc });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION { IdentifierName = "@r12", Value = 0xd });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION { IdentifierName = "@r13", Value = 0xe });

            //
            // Pseudo-registers
            //
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION { IdentifierName = "$proc", Value = 0x0 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION { IdentifierName = "$thread", Value = 0x0 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION { IdentifierName = "$teb", Value = 0x0 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION { IdentifierName = "$ip", Value = 0x0 });
            StatementGenerator.Identifiers.Add(new StatementGenerator.IDENTIFIER_DEFINITION { IdentifierName = "$buffer", Value = 0x0 });

        }

        private static bool Generate(ACTION_TYPE Type, StreamWriter TestCaseWithErrorFile, StreamWriter TestCaseWithoutErrorFile, int Limit, UInt64 Counter)
        {
            string Result = string.Empty;
            string Sentence = string.Empty;
            string Script = string.Empty;
            bool EvalResult = false;


            StatementGenerator.ResetDepth();

            if (Type == ACTION_TYPE.CREATE_EXPRESSIONS)
            {
                Sentence = StatementGenerator.EXPRESSION();

                //
                // Avoid big statements!
                //
                if (!(Sentence.Length <= Limit))
                {
                    return false;
                }

                Script = "x = " + Sentence + "; test_statement(x);";
                EvalResult = HighLevelScriptGen.EvaluateExpression(Sentence, ref Result);
            }
            else if (Type == ACTION_TYPE.CREATE_CONDITIONAL_STATEMENTS)
            {
                Sentence = StatementGenerator.IF_STATEMENT(false);

                if (!(Sentence.Length <= Limit))
                {
                    return false;
                }

                Script = Sentence;
                EvalResult = HighLevelScriptGen.EvaluateConditionalStatement(Sentence, ref Result);

            }
            else if (Type == ACTION_TYPE.CREATE_CONDITIONAL_STATEMENTS_COMBINED_WITH_OTHER_STATEMENT)
            {
                Sentence = StatementGenerator.IF_STATEMENT(true);

                if (!(Sentence.Length <= Limit))
                {
                    return false;
                }

                Script = Sentence;
                EvalResult = HighLevelScriptGen.EvaluateConditionalStatement(Sentence, ref Result);

            }
            else if (Type == ACTION_TYPE.CREATE_FOR_LOOP)
            {
                Sentence = StatementGenerator.FOR_STATEMENT();

                if (!(Sentence.Length <= Limit))
                {
                    return false;
                }

                Script = Sentence;
                EvalResult = HighLevelScriptGen.EvaluateForLoops(Sentence, ref Result);

            }

            Console.WriteLine(Counter + '\n' + Script + '\n' + Result + '\n');

            if (EvalResult)
            {
                TestCaseWithoutErrorFile.WriteLine(Counter.ToString());
                TestCaseWithoutErrorFile.WriteLine(Script);
                TestCaseWithoutErrorFile.WriteLine(Result);
                TestCaseWithoutErrorFile.WriteLine("$end$");
                TestCaseWithoutErrorFile.Flush();
                return true;
            }
            else
            {
                TestCaseWithErrorFile.WriteLine(Counter.ToString());
                TestCaseWithErrorFile.WriteLine(Script);
                TestCaseWithErrorFile.WriteLine(Result);
                TestCaseWithErrorFile.WriteLine("$end$");
                TestCaseWithErrorFile.Flush();
                return false;

            }

        }

        static void Main(string[] args)
        {
            UInt64 Counter = 0;
            int CounterOfCorrectCases = 0;

            //
            // First of all we should initialize identifiers and their values
            //
            
            InitilizeIdentifers();

            StreamWriter TestCaseWithErrorFile = new StreamWriter("test-cases-wrong.txt");
            StreamWriter TestCaseWithoutErrorFile = new StreamWriter("test-cases-correct.txt");

            //
            // This the count of correct values
            //
            while (true)
            {
                Counter++;

              // Generate(ACTION_TYPE.CREATE_EXPRESSIONS, TestCaseWithErrorFile, TestCaseWithoutErrorFile, 150, Counter);
              // Generate(ACTION_TYPE.CREATE_CONDITIONAL_STATEMENTS_COMBINED_WITH_OTHER_STATEMENT, TestCaseWithErrorFile, TestCaseWithoutErrorFile, 10000, Counter);

              bool Correctness =  Generate(ACTION_TYPE.CREATE_FOR_LOOP, TestCaseWithErrorFile, TestCaseWithoutErrorFile, 1500, Counter);

                if (Correctness)
                {
                    CounterOfCorrectCases++;

                    if (CounterOfCorrectCases >= 10)
                    {
                        break;
                    }
                }

            }

            TestCaseWithErrorFile.Close();
            TestCaseWithoutErrorFile.Close();

        }
    }
}
