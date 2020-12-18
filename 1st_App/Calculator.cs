using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1st_App
{
    public class Calculator
    {
        public static double Calculate(string central)
        {
            central = central.Replace("-", "+-");

            if (!central.Contains("("))
            {
                return SimpleOperators(central);
            }

            int i = central.IndexOf("(");
            int i2 = central.Substring(i + 1).IndexOf("(") + i + 1;
            int j2 = central.IndexOf(")");
            if (j2 < i2)
            {
                string oper2 = central[j2 + 1].ToString();
                string midcentral = central.Substring(i + 1, j2 - i - 1);  // (__)+()
                string endcentral = central.Substring(j2 + 2); // ()+__(___)_
                double midvalue = Calculate(midcentral);
                double endvalue = Calculate(endcentral);

                if (i != 0) // Example 1+(1+2)+(2+2)
                {
                    string oper1 = central[i - 1].ToString();
                    string startcentral = central.Substring(0, i - 1);

                    if (startcentral.Contains('+') && oper1 != "+")
                    {
                        string partofstart = startcentral.Substring(startcentral.IndexOf('+') + 1);
                        midvalue = Separate(oper1, partofstart, midvalue);
                        oper1 = "+";
                        startcentral = startcentral.Substring(0, startcentral.IndexOf('+'));
                    }

                    double startvalue = SimpleOperators(startcentral);
                    return SimpleOperators(startvalue + oper1 + midvalue + oper2 + endvalue);
                }
                else  // Example (1+2)+(2+2)
                {
                    return SimpleOperators(midvalue + oper2 + endvalue);
                }
            }
            else
            {
                int j = central.LastIndexOf(")");

                string midcentral = central.Substring(i + 1, j - i - 1); // (__)
                double midvalue = Calculate(midcentral);

                if (i != 0)
                {
                    if (j != central.Length - 1) // Example 1+(1+2)+2
                    {
                        string oper1 = central[i - 1].ToString();
                        string oper2 = central[j + 1].ToString();
                        string startcentral = central.Substring(0, i - 1);
                        string endcentral = central.Substring(j + 2);
                        if (startcentral.Contains('+') && oper1 != "+")
                        {
                            string partofstart = startcentral.Substring(startcentral.IndexOf('+') + 1);
                            midvalue = Separate(oper1, partofstart, midvalue);
                            oper1 = "+";
                            startcentral = startcentral.Substring(0, startcentral.IndexOf('+'));
                        }

                        if (endcentral.Contains('+') && oper2 != "+")
                        {
                            string partofend = endcentral.Substring(0, endcentral.IndexOf('+'));
                            midvalue = Separate(oper2, partofend, midvalue);
                            oper2 = "+";
                            endcentral = endcentral.Substring(endcentral.IndexOf('+') + 1);
                        }

                        double startvalue = SimpleOperators(startcentral);
                        double endvalue = SimpleOperators(endcentral);
                        return SimpleOperators(startvalue + oper1 + midvalue + oper2 + endvalue);
                    }
                    else // Example 1+(1+2)
                    {
                        string oper1 = central[i - 1].ToString();
                        string startcentral = central.Substring(0, i - 1);
                        if (startcentral.Contains('+') && oper1 != "+")
                        {
                            string partofstart = startcentral.Substring(startcentral.IndexOf('+') + 1);
                            midvalue = Separate(oper1, partofstart, midvalue);
                            oper1 = "+";
                            startcentral = startcentral.Substring(0, startcentral.IndexOf('+'));
                        }
                        double startvalue = SimpleOperators(startcentral);
                        return SimpleOperators(startvalue + oper1 + midvalue);
                    }
                }
                else if (j != central.Length - 1) // Example (1+2)+2
                {
                    string oper2 = central[j + 1].ToString();
                    string endcentral = central.Substring(j + 2);
                    if (endcentral.Contains('+') && oper2 != "+")
                    {
                        string partofend = endcentral.Substring(0, endcentral.IndexOf('+'));
                        midvalue = Separate(oper2, partofend, midvalue);
                        oper2 = "+";
                        endcentral = endcentral.Substring(endcentral.IndexOf('+') + 1);
                    }
                    double endvalue = SimpleOperators(endcentral);
                    return SimpleOperators(midvalue + oper2 + endvalue);
                }
                else // Example (1+2)
                {
                    return SimpleOperators(midvalue.ToString());
                }
            }
        }
        private static double SimpleOperators(string startcentral)
        {
            double resultvalue = 0;
            int Lengthstart = startcentral.Length;
            int start = 0;
            int end = startcentral.IndexOf('+');
            string subcentral = "";

            if (startcentral.Contains('+'))
            {
                for (int i = 0; i < Lengthstart; i++)
                {
                    subcentral = startcentral.Substring(start, end);
                    if (!subcentral.Contains('*') && !subcentral.Contains('/') && !startcentral.Contains('^'))
                    {
                        resultvalue += Convert.ToDouble(subcentral);
                        start = 0;
                        if (end == startcentral.Length)
                            break;
                        startcentral = startcentral.Substring(end + 1);
                        end = startcentral.Contains('+') ? startcentral.IndexOf('+') : startcentral.Length;
                        continue;
                    }
                    resultvalue += ComplexOperators(subcentral);

                    start = 0;
                    if (end == startcentral.Length)
                        break;
                    startcentral = startcentral.Substring(end + 1);
                    end = startcentral.Contains('+') ? startcentral.IndexOf('+') : startcentral.Length;

                }
            }
            else if (startcentral.Contains('/') || startcentral.Contains('*') || startcentral.Contains('^'))
            {
                resultvalue = ComplexOperators(startcentral);
            }
            else
            {
                resultvalue = Convert.ToDouble(startcentral);
            }
            return resultvalue;
        }

        private static double ComplexOperators(string Subcentral)
        {
            double resultvalue1 = 0;
            string num1 = "";
            string num2 = "";
            char op = ' ';
            for (int j = 0; j < Subcentral.Length; j++)
            {
                if (Subcentral[j] == '*')
                {
                    if (op == '*')
                    {
                        num2 = (Convert.ToDouble(num2) * Convert.ToDouble(num1)).ToString();
                        num1 = "";
                    }
                    else if (op == '/')
                    {
                        num2 = (Convert.ToDouble(num2) / Convert.ToDouble(num1)).ToString();
                        num1 = "";
                    }
                    else if (op == '^')
                    {
                        num2 = (Math.Pow(Convert.ToDouble(num2), Convert.ToDouble(num1))).ToString();
                        num1 = "";
                    }
                    else
                    {
                        num2 = num1;
                        num1 = "";
                    }
                    op = '*';
                }
                else if (Subcentral[j] == '/')
                {
                    if (op == '*')
                    {
                        num2 = (Convert.ToDouble(num2) * Convert.ToDouble(num1)).ToString();
                        num1 = "";
                    }
                    else if (op == '/')
                    {
                        num2 = (Convert.ToDouble(num2) / Convert.ToDouble(num1)).ToString();
                        num1 = "";
                    }
                    else if (op == '^')
                    {
                        num2 = (Math.Pow(Convert.ToDouble(num2), Convert.ToDouble(num1))).ToString();
                        num1 = "";
                    }
                    else
                    {
                        num2 = num1;
                        num1 = "";
                    }
                    op = '/';
                }
                else if (Subcentral[j] == '^')
                {
                    if (op == '*')
                    {
                        num2 = (Convert.ToDouble(num2) * Convert.ToDouble(num1)).ToString();
                        num1 = "";
                    }
                    else if (op == '/')
                    {
                        num2 = (Convert.ToDouble(num2) / Convert.ToDouble(num1)).ToString();
                        num1 = "";
                    }
                    else if (op == '^')
                    {
                        num2 = (Math.Pow(Convert.ToDouble(num2), Convert.ToDouble(num1))).ToString();
                        num1 = "";
                    }
                    else
                    {
                        num2 = num1;
                        num1 = "";
                    }
                    op = '^';
                }
                else
                {
                    num1 += Subcentral[j];
                }
            }

            if (op == '*')
            {
                resultvalue1 += Convert.ToDouble(num2) * Convert.ToDouble(num1);
            }
            else if (op == '/')
            {
                resultvalue1 += Convert.ToDouble(num2) / Convert.ToDouble(num1);
            }
            else if (op == '^')
            {
                resultvalue1 = Math.Pow(Convert.ToDouble(num2), Convert.ToDouble(num1));
            }

            return resultvalue1;
        }
        private static double Separate(string oper, string partstr, double midval) // 2+2*2*2*(1+2)
        {
            double partNum = 0;
            if (partstr.Contains('*') || partstr.Contains('/') || partstr.Contains('^'))
                partNum = ComplexOperators(partstr);
            else
                partNum = Convert.ToDouble(partstr);

            if (oper == "*")
                midval *= partNum;
            else if (oper == "/")
                midval /= partNum;
            else if (oper == "^")
                midval = Math.Pow(midval, partNum);

            return midval;
        }

        public static double OperatorClickCalc(string text) // when click operator its calculate last all function or from open braces
        {
            string textcopy = text;
            int OpInd = text.LastIndexOf('(');
            for (; ; )
            {
                if (!textcopy.Substring(OpInd + 1).Contains(')'))
                    break;
                textcopy = textcopy.Remove(textcopy.Substring(OpInd + 1).IndexOf(')') + OpInd + 1, 1);
                OpInd = textcopy.Substring(0, OpInd).LastIndexOf('(');
            }
            text = text.Substring(OpInd + 1);
            double result = Calculate(text);
            return result;
        }
        public static double CloseBracCalc(string text)
        {
            int count = 1;
            int OpIndex = 0;
            for (int i = text.Length - 1; i > 0; i--)
            {
                if (text[i - 1] == ')')
                    count++;
                else if (text[i - 1] == '(')
                    count--;

                if (count == 0)
                {
                    OpIndex = i - 1;
                    break;
                }
            }
            double result = Calculate(text.Substring(OpIndex));
            return result;
        }
    }
}
