using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvalScripts
{
    class StatementGenerator
    {

        static Random random = new Random();
        static int depth = 0;
        const int MAX_DEPTH = 10;
        public const int SizeOfStructOfIdentifiers = 50;
        public struct IDENTIFIER_DEFINITION
        {

            public string IdentifierName;
            public int Value;
        }

        public static List<IDENTIFIER_DEFINITION> Identifiers = new List<IDENTIFIER_DEFINITION>();

        public static void ResetDepth()
        {
            depth = 0;
        }

        public static string GET_CHECK_STATEMENT()
        {
            var token = random.Next(10000);
            string res = string.Empty;

            res = " test_statement(" + "0x" + token.ToString("X") + "); ";
            return res;
        }

        public static string S()
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

        public static string STATEMENT()
        {
            var r = random.Next(8);
            string res = string.Empty;

            depth += 1;

            if (r == 0)
            {
                res = IF_STATEMENT(false);
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
                res = " break;";
                depth -= 1;
                return res;
            }
            else if (r == 7)
            {
                res = " continue;";
                depth -= 1;
                return res;
            }

            return string.Empty;
        }

        public static string CALL_FUNC_STATEMENT()
        {
            string res = string.Empty;
            return res;
        }

        public static string ASSIGN_STATEMENT()
        {
            string res = string.Empty;
            depth += 1;
            res = L_VALUE() + " = " + EXPRESSION() + " " + NULL();
            depth -= 1;
            return res;
        }

        public static string IF_STATEMENT(bool AddStatement)
        {
            string res = string.Empty;
            depth += 1;

            if (AddStatement)
            {
                res = " if (" + BOOLEAN_EXPRESSION() + ")  {" + GET_CHECK_STATEMENT() + S() + "}" + ELSIF_STATEMENT(AddStatement) + ELSE_STATEMENT(AddStatement) + END_OF_IF();
            }
            else
            {
                res = " if (" + BOOLEAN_EXPRESSION() + ")  {" + GET_CHECK_STATEMENT() + "}" + ELSIF_STATEMENT(AddStatement) + ELSE_STATEMENT(AddStatement) + END_OF_IF();
            }

            depth -= 1;
            return res;
        }

        public static string ELSIF_STATEMENT(bool AddStatement)
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
                if (AddStatement)
                {
                    res = " elsif (" + BOOLEAN_EXPRESSION() + ") {" + GET_CHECK_STATEMENT() + S() + "}" + ELSIF_STATEMENT(AddStatement);
                }
                else
                {
                    res = " elsif (" + BOOLEAN_EXPRESSION() + ") {" + GET_CHECK_STATEMENT() + "}" + ELSIF_STATEMENT(AddStatement);
                }
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

        public static string ELSIF_STATEMENTP()
        {
            string res = string.Empty;
            return res;
        }

        public static string ELSE_STATEMENT(bool AddStatement)
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
                if (AddStatement)
                {
                    res = " else {" + GET_CHECK_STATEMENT() + S() + "}";
                }
                else
                {
                    res = " else {" + GET_CHECK_STATEMENT() + "}";
                }

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

        public static string END_OF_IF()
        {
            string res = string.Empty;
            return res;
        }

        public static string WHILE_STATEMENT()
        {
            string res = string.Empty;
            depth += 1;
            res = " while  (" + BOOLEAN_EXPRESSION() + ")  { " + S() + " }";
            depth -= 1;
            return res;
        }

        public static string DO_WHILE_STATEMENT()
        {
            string res = string.Empty;
            depth += 1;
            res = " do  {" + S() + "} while ( " + BOOLEAN_EXPRESSION() + ") ;";
            depth -= 1;
            return res;
        }

        public static string FOR_STATEMENT()
        {
            string res = string.Empty;
            depth += 1;
            res = " for (" + SIMPLE_ASSIGNMENT() + "; " + BOOLEAN_EXPRESSION() + ";" + INC_DEC() + ") { " + S() + "}";
            depth -= 1;
            return res;
        }

        public static string SIMPLE_ASSIGNMENT()
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

        public static string SIMPLE_ASSIGNMENTP()
        {
            string res = string.Empty;
            return res;
        }

        public static string INC_DEC()
        {
            string res = string.Empty;
            depth += 1;
            res = L_VALUE() + INC_DECP();
            depth -= 1;
            return res;
        }

        public static string INC_DECP()
        {
            string res = string.Empty;
            depth += 1;
            res = "--" + DECP();
            depth -= 1;
            return res;
        }

        public static string INCP()
        {
            string res = string.Empty;
            return res;
        }

        public static string DECP()
        {
            string res = string.Empty;
            return res;
        }

        public static string BOOLEAN_EXPRESSION()
        {
            string res = string.Empty;
            string expr = string.Empty;
            string[] Operators = { " ", " == ", " <= ", " >= ", " <> ", " >< ", " ! ", " != ", " = ", " > ", " < ", "((", "(", ")", "))" };
            var r = random.Next(0, Operators.Length);
            var r2 = random.Next(3);

            depth += 1;


            expr = EXPRESSION();



            if (r2 == 0)
            {
                //
                // Two sides are equal
                //
                res = expr + Operators[r] + expr + SIMPLE_ASSIGNMENTP();
                depth -= 1;
                return res;
            }
            else
            {

                //
                // Two sides are not equal 
                //
                string expr2 = EXPRESSION(true);

                if (expr2.Length >= 150)
                {
                    expr2 = EXPRESSION(true);
                }

                res = expr + Operators[r] + expr2 + SIMPLE_ASSIGNMENTP();
                depth -= 1;
                return res;
            }
        }

        public static string EXPRESSION()
        {
            string res = string.Empty;
            depth += 1;
            res = E1() + E0P();
            depth -= 1;
            return res;
        }

        public static string EXPRESSION(bool ForceToBeValid)
        {
            string Result = string.Empty;

            if (!ForceToBeValid)
            {
                return EXPRESSION();
            }
            else
            {

                while (true)
                {
                    string Expr = EXPRESSION();

                    if (HighLevelScriptGen.EvaluateExpression(Expr, ref Result))
                    {
                        return Expr;
                    }
                }

            }

            return string.Empty;

        }

        public static string E0P()
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

        public static string E1()
        {
            string res = string.Empty;

            depth += 1;
            res = E2() + E1P();
            depth -= 1;
            return res;
        }

        public static string E1P()
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

        public static string E2()
        {
            string res = string.Empty;

            depth += 1;
            res = E3() + E2P();
            depth -= 1;
            return res;
        }

        public static string E2P()
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

        public static string E3()
        {
            string res = string.Empty;

            depth += 1;
            res = E4() + E3P();
            depth -= 1;
            return res;
        }

        public static string E3P()
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

        public static string E4()
        {
            string res = string.Empty;

            depth += 1;
            res = E5() + E4P();
            depth -= 1;
            return res;
        }

        public static string E4P()
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

        public static string E5()
        {
            string res = string.Empty;

            depth += 1;
            res = E6() + E5P();
            depth -= 1;
            return res;
        }

        public static string E5P()
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

        public static string E6()
        {
            string res = string.Empty;

            depth += 1;
            res = E7() + E6P();
            depth -= 1;
            return res;
        }

        public static string E6P()
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

        public static string E7()
        {
            string res = string.Empty;

            depth += 1;
            res = E8() + E7P();
            depth -= 1;
            return res;
        }

        public static string E7P()
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

        public static string E8()
        {
            string res = string.Empty;

            depth += 1;
            res = E9() + E8P();
            depth -= 1;
            return res;
        }

        public static string E8P()
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

        public static string E9()
        {
            string res = string.Empty;

            depth += 1;
            res = E10() + E9P();
            depth -= 1;
            return res;
        }

        public static string E9P()
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

        public static string E10()
        {
            string res = string.Empty;

            depth += 1;
            res = E12();
            depth -= 1;
            return res;
        }

        public static string E13()
        {
            string res = string.Empty;

            res = "";
            return res;
        }
        public static string L_VALUE()
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

        public static string NULL()
        {
            string res = string.Empty;
            return res;
        }
 


        public static string E12()
        {
            string res = string.Empty;

            var r = random.Next(0, 8 + Identifiers.Count);

            depth += 1;

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
            else 
            {
                var ri = random.Next(0, Identifiers.Count);

                res = Identifiers[ri].IdentifierName; // register, pesudo-registers, ids whatever
                depth -= 1;
                return res;
            }
           

            return string.Empty;
        }

    }
}
