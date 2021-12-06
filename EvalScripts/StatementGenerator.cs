using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvalScripts
{
    class StatementGenerator
    {
        static Random random = new Random();
        static int depth = 0;
        const int MAX_DEPTH = 20;

        private static string S()
        {

            var r = random.Next(2);
            string res = string.Empty;

            depth += 1;

            if (depth >= MAX_DEPTH)
            {
                r = 1;
            }

            if (r == 0)
            {
                res = STATEMENT() + " " + S();
                depth -= 1;
                return res;
            }
            else if (r == 1)
            {
                res = "";
                depth -= 1;
                return res;
            }

            return string.Empty;
        }

        private static string STATEMENT()
        {
            var r = random.Next(8);
            string res = string.Empty;

            depth += 1;

            if (r == 0)
            {
                res = IF_STATEMENT();
                depth -= 1;
                return res;
            }
            else if (r == 1)
            {
                res = WHILE_STATEMENT();
                depth -= 1;
                return res;
            }
            else if (r == 2)
            {
                res = DO_WHILE_STATEMENT();
                depth -= 1;
                return res;
            }
            else if (r == 3)
            {
                res = FOR_STATEMENT();
                depth -= 1;
                return res;
            }
            else if (r == 4)
            {
                res = ASSIGN_STATEMENT() + ";";
                depth -= 1;
                return res;
            }
            else if (r == 5)
            {
                res = CALL_FUNC_STATEMENT() + ";";
                depth -= 1;
                return res;
            }
            else if (r == 6)
            {
                res = "break ;";
                depth -= 1;
                return res;
            }
            else if (r == 7)
            {
                res = "continue ;";
                depth -= 1;
                return res;
            }

            return string.Empty;
        }

        private static string CALL_FUNC_STATEMENT()
        {
            string res = string.Empty;
            return res;
        }

        private static string ASSIGN_STATEMENT()
        {
            string res = string.Empty;
            depth += 1;
            res = L_VALUE() + " = " + EXPRESSION() + " " + NULL();
            depth -= 1;
            return res;
        }

        private static string IF_STATEMENT()
        {
            string res = string.Empty;
            depth += 1;
            res = "if (" + BOOLEAN_EXPRESSION() + ")  {" + S() + "}" + ELSIF_STATEMENT() + ELSE_STATEMENT() + END_OF_IF();
            depth -= 1;
            return res;
        }

        private static string ELSIF_STATEMENT()
        {
            var r = random.Next(2);
            string res = string.Empty;


            depth += 1;

            if (depth >= MAX_DEPTH)
            {
                r = 1;
            }

            if (r == 0)
            {
                res = "elsif (" + BOOLEAN_EXPRESSION() + ") {" + S() + "}" + ELSIF_STATEMENT();
                depth -= 1;
                return res;
            }
            else if (r == 1)
            {
                res = ELSIF_STATEMENTP();
                depth -= 1;
                return res;
            }

            return string.Empty;
        }

        private static string ELSIF_STATEMENTP()
        {
            string res = string.Empty;
            return res;
        }

        private static string ELSE_STATEMENT()
        {
            var r = random.Next(2);
            string res = string.Empty;

            depth += 1;
            if (depth >= MAX_DEPTH)
            {
                r = 1;
            }

            if (r == 0)
            {
                res = "else {" + S() + "}";
                depth -= 1;
                return res;
            }
            else if (r == 1)
            {
                res = "";
                depth -= 1;
                return res;
            }

            return string.Empty;

        }

        private static string END_OF_IF()
        {
            string res = string.Empty;
            return res;
        }

        private static string WHILE_STATEMENT()
        {
            string res = string.Empty;
            depth += 1;
            res = "while  (" + BOOLEAN_EXPRESSION() + ")  { " + S() + " }";
            depth -= 1;
            return res;
        }

        private static string DO_WHILE_STATEMENT()
        {
            string res = string.Empty;
            depth += 1;
            res = "do  {" + S() + "} while ( " + BOOLEAN_EXPRESSION() + ") ;";
            depth -= 1;
            return res;
        }

        private static string FOR_STATEMENT()
        {
            string res = string.Empty;
            depth += 1;
            res = "for (" + SIMPLE_ASSIGNMENT() + "; " + BOOLEAN_EXPRESSION() + ";" + INC_DEC() + ") { " + S() + "}";
            depth -= 1;
            return res;
        }

        private static string SIMPLE_ASSIGNMENT()
        {
            var r = random.Next(2);
            string res = string.Empty;

            depth += 1;
            if (depth >= MAX_DEPTH)
            {
                r = 1;
            }

            if (r == 0)
            {
                res = L_VALUE() + "= " + EXPRESSION() + SIMPLE_ASSIGNMENTP();
                depth -= 1;
                return res;
            }
            else if (r == 1)
            {
                res = "";
                depth -= 1;
                return res;
            }

            return string.Empty;

        }

        private static string SIMPLE_ASSIGNMENTP()
        {
            string res = string.Empty;
            return res;
        }

        private static string INC_DEC()
        {
            string res = string.Empty;
            depth += 1;
            res = L_VALUE() + INC_DECP();
            depth -= 1;
            return res;
        }

        private static string INC_DECP()
        {
            string res = string.Empty;
            depth += 1;
            res = "--" + DECP();
            depth -= 1;
            return res;
        }

        private static string INCP()
        {
            string res = string.Empty;
            return res;
        }

        private static string DECP()
        {
            string res = string.Empty;
            return res;
        }

        private static string BOOLEAN_EXPRESSION()
        {
            string res = string.Empty;
            return res;
        }

        private static string EXPRESSION()
        {
            string res = string.Empty;
            depth += 1;
            res = E1() + E0P();
            depth -= 1;
            return res;
        }

        private static string E0P()
        {
            var r = random.Next(2);
            string res = string.Empty;

            depth += 1;
            if (depth >= MAX_DEPTH)
            {
                r = 1;
            }

            if (r == 0)
            {
                res = " | " + E1() + E0P();
                depth -= 1;
                return res;

            }
            else if (r == 1)
            {
                res = "";
                depth -= 1;
                return res;
            }

            return string.Empty;

        }

        private static string E1()
        {
            string res = string.Empty;

            depth += 1;
            res = E2() + E1P();
            depth -= 1;
            return res;
        }

        private static string E1P()
        {
            var r = random.Next(2);
            string res = string.Empty;

            depth += 1;
            if (depth >= MAX_DEPTH)
            {
                r = 1;
            }

            if (r == 0)
            {
                res = " ^ " + E2() + E1P();
                depth -= 1;
                return res;
            }
            else if (r == 1)
            {
                res = "";
                depth -= 1;
                return res;
            }

            return string.Empty;

        }

        private static string E2()
        {
            string res = string.Empty;

            depth += 1;
            res = E3() + E2P();
            depth -= 1;
            return res;
        }

        private static string E2P()
        {
            string res = string.Empty;

            var r = random.Next(2);

            depth += 1;
            if (depth >= MAX_DEPTH)
            {
                r = 1;
            }

            if (r == 0)
            {
                res = " & " + E3() + E2P();
                depth -= 1;
                return res;
            }
            else if (r == 1)
            {
                res = "";
                depth -= 1;
                return res;
            }

            return string.Empty;

        }

        private static string E3()
        {
            string res = string.Empty;

            depth += 1;
            res = E4() + E3P();
            depth -= 1;
            return res;
        }

        private static string E3P()
        {
            string res = string.Empty;

            var r = random.Next(2);

            depth += 1;
            if (depth >= MAX_DEPTH)
            {
                r = 1;
            }

            if (r == 0)
            {
                res = " >> " + E4() + E3P();
                depth -= 1;
                return res;
            }
            else if (r == 1)
            {
                res = "";
                depth -= 1;
                return res;
            }

            return string.Empty;

        }

        private static string E4()
        {
            string res = string.Empty;

            depth += 1;
            res = E5() + E4P();
            depth -= 1;
            return res;
        }

        private static string E4P()
        {
            string res = string.Empty;

            var r = random.Next(2);

            depth += 1;
            if (depth >= MAX_DEPTH)
            {
                r = 1;
            }

            if (r == 0)
            {
                res = " << " + E5() + E4P();
                depth -= 1;
                return res;
            }

            else if (r == 1)
            {
                res = "";
                depth -= 1;
                return res;
            }

            return string.Empty;

        }

        private static string E5()
        {
            string res = string.Empty;

            depth += 1;
            res = E6() + E5P();
            depth -= 1;
            return res;
        }

        private static string E5P()
        {
            string res = string.Empty;

            var r = random.Next(2);

            depth += 1;
            if (depth >= MAX_DEPTH)
            {
                r = 1;
            }

            if (r == 0)
            {
                res = " + " + E6() + E5P();
                depth -= 1;
                return res;
            }
            else if (r == 1)
            {
                res = "";
                depth -= 1;
                return res;
            }

            return string.Empty;

        }

        private static string E6()
        {
            string res = string.Empty;

            depth += 1;
            res = E7() + E6P();
            depth -= 1;
            return res;
        }

        private static string E6P()
        {
            string res = string.Empty;

            var r = random.Next(2);

            depth += 1;
            if (depth >= MAX_DEPTH)
            {
                r = 1;
            }

            if (r == 0)
            {
                res = " - " + E7() + E6P();
                depth -= 1;
                return res;
            }
            else if (r == 1)
            {
                res = "";
                depth -= 1;
                return res;
            }

            return string.Empty;

        }

        private static string E7()
        {
            string res = string.Empty;

            depth += 1;
            res = E8() + E7P();
            depth -= 1;
            return res;
        }

        private static string E7P()
        {
            string res = string.Empty;

            var r = random.Next(2);

            depth += 1;
            if (depth >= MAX_DEPTH)
            {
                r = 1;
            }

            if (r == 0)
            {
                res = " * " + E8() + E7P();
                depth -= 1;
                return res;
            }
            else if (r == 1)
            {
                res = "";
                depth -= 1;
                return res;
            }

            return string.Empty;

        }

        private static string E8()
        {
            string res = string.Empty;

            depth += 1;
            res = E9() + E8P();
            depth -= 1;
            return res;
        }

        private static string E8P()
        {
            string res = string.Empty;

            var r = random.Next(2);

            depth += 1;
            if (depth >= MAX_DEPTH)
            {
                r = 1;
            }

            if (r == 0)
            {
                res = " / " + E9() + E8P();
                depth -= 1;
                return res;
            }
            else if (r == 1)
            {
                res = "";
                depth -= 1;
                return res;
            }

            return string.Empty;

        }

        private static string E9()
        {
            string res = string.Empty;

            depth += 1;
            res = E10() + E9P();
            depth -= 1;
            return res;
        }

        private static string E9P()
        {
            string res = string.Empty;

            var r = random.Next(2);

            depth += 1;
            if (depth >= MAX_DEPTH)
            {
                r = 1;
            }

            if (r == 0)
            {
                res = " % " + E10() + E9P();
                depth -= 1;
                return res;
            }
            else if (r == 1)
            {
                res = "";
                depth -= 1;
                return res;
            }

            return string.Empty;

        }

        private static string E10()
        {
            string res = string.Empty;

            depth += 1;
            res = E12();
            depth -= 1;
            return res;
        }

        private static string E13()
        {
            string res = string.Empty;

            res = "";
            return res;
        }
        private static string L_VALUE()
        {
            string res = string.Empty;

            var r = random.Next(2);

            if (r == 0)
            {
                res = "x";  // id
                return res;
            }
            else if (r == 1)
            {
                res = "@rax"; // register
                return res;
            }

            return string.Empty;
        }

        private static string NULL()
        {
            string res = string.Empty;
            return res;
        }

        private static string E12()
        {
            string res = string.Empty;

            var r = random.Next(0, 8);

            depth += 1;

            if (depth >= MAX_DEPTH)
            {
                r = 1;
            }


            if (r == 0)
            {
                res = "(" + EXPRESSION() + ")";
                depth -= 1;
                return res;
            }
            else if (r == 1)
            {
                var ri = random.Next(0, 20);
                res = "0x" + ri.ToString("X");  // hex
                depth -= 1;
                return res;
            }
            else if (r == 2)
            {
                var ri = random.Next(0, 20);
                res = "0n" + ri.ToString(); // decimal
                depth -= 1;
                return res;
            }
            else if (r == 3)
            {
                var ri = random.Next(0, 20);
                //
                // C# don't support prefix for octals
                //
                // res = "0t" + Convert.ToString(ri, 8); // octal
                res = Convert.ToString(ri, 8); // octal
                depth -= 1;
                return res;
            }
            else if (r == 4)
            {
                var ri = random.Next(0, 20);
                res = "0y" + Convert.ToString(ri, 2);  // binary
                depth -= 1;
                return res;
            }
            else if (r == 5)
            {
                res = "-" + E12() + E13();
                depth -= 1;
                return res;
            }
            else if (r == 6)
            {
                res = "+" + E12() + E13();
                depth -= 1;
                return res;
            }
            else if (r == 7)
            {
                res = "~" + E12() + E13();
                depth -= 1;
                return res;
            }
            else if (r == 8)
            {
                res = "@rax"; // register
                depth -= 1;
                return res;
            }
            else if (r == 9)
            {
                res = "x"; // id
                depth -= 1;
                return res;
            }
            else if (r == 10)
            {
                res = "pid"; // sample pseudo register, can be added more options later
                depth -= 1;
                return res;
            }

            return string.Empty;
        }

        private static bool evaluate(string s, ref string result)
        {
            string s2 = s.Replace("0n", "").Replace("0y", "0b").Replace("/", "//");

            var tmp = Eval.EvalStatementAsync(s2);

            if (tmp.Result.Item1 == true)
            {
                result = tmp.Result.Item2.ToString("X");
                result = result.Replace("0x", "");
                return true;
            }
            else
            {
                result = "$error$";
                return false;
            }

        }

        public static void generate()
        {
            string result = string.Empty;

            StreamWriter fw = new StreamWriter("test-cases-wrong.txt");
            StreamWriter fc = new StreamWriter("test-cases-correct.txt");

            int counter = 1;

            int initCounter = counter;
            while (counter - initCounter < 10000)
            {
                depth = 0;
                string sentence = EXPRESSION();
                if (sentence.Length <= 150)
                {
                    string res = "x = " + sentence + "; test_statement(x);";
                    bool evalres = evaluate(sentence, ref result);

                    if (evalres)
                    {
                        fc.WriteLine(counter.ToString());
                        fc.WriteLine(res);
                        fc.WriteLine(result);
                        fc.WriteLine("$end$");
                        fc.Flush();
                    }
                    else
                    {
                        fw.WriteLine(counter.ToString());
                        fw.WriteLine(res);
                        fw.WriteLine(result);
                        fw.WriteLine("$end$");
                    }


                    Console.WriteLine(counter);
                    Console.WriteLine(res);
                    Console.WriteLine(result);
                    Console.WriteLine();
                    counter += 1;
                }
            }
            fw.Close();
            fc.Close();
        }


    }
}
