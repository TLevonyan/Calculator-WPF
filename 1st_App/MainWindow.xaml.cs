using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _1st_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string allresult = "";
        string result = "";
        int bracksCount = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumBtn_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay2.Text.Contains('='))
            {
                allresult = "";
                result = "";
                txtDisplay.Text = "0";
                txtDisplay2.Text = "0";
            }

            if (txtDisplay2.Text[txtDisplay2.Text.Length - 1] == ')')
                return;

            Button button = sender as Button;
            result += button.Tag;
            txtDisplay.Text = result;
        }

        //-------------------------------
        // +- 1/x x2 sqrt Pi .

        private void PointBtn_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay2.Text[txtDisplay2.Text.Length - 1] == ')')
                return;

            if (result.Contains('.'))
                return;

            if (result == "")
                result = txtDisplay.Text;

            result += '.';
            txtDisplay.Text = result;

        }

        private void BtnPi_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay2.Text[txtDisplay2.Text.Length - 1] == ')')
                return;

            result = Math.PI.ToString();
            txtDisplay.Text = result;
        }

        private void BtnPosNeg_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay2.Text[txtDisplay2.Text.Length - 1] == ')')
                return;

            if (result == "")
                result = txtDisplay.Text;

            if (result[0] == '-')
                result = result.Substring(1, result.Length - 1);
            else
                result = "-" + result;

            txtDisplay.Text = result;
        }

        private void BtnRotate_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay2.Text[txtDisplay2.Text.Length - 1] == ')')
                return;

            if (result == "")
                result = txtDisplay.Text;

            result = (1 / Convert.ToDouble(result)).ToString();
            txtDisplay.Text = result;
        }

        private void BtnSqrt_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay2.Text[txtDisplay2.Text.Length - 1] == ')')
                return;

            if (result == "")
                result = txtDisplay.Text;

            result = (Math.Sqrt(Convert.ToDouble(result))).ToString();
            txtDisplay.Text = result;
        }

        private void BtnSquare_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay2.Text[txtDisplay2.Text.Length - 1] == ')')
                return;

            if (result == "")
                result = txtDisplay.Text;

            result = (Math.Pow(Convert.ToDouble(result), 2)).ToString();
            txtDisplay.Text = result;
        } 

        // ------------------------------------
        //    Brackets  ( )

        private void BtnOpenBracket_Click(object sender, RoutedEventArgs e)
        {
            if (bracksCount >= 10)
                return;

            allresult += '(';
            bracksCount++;
            txtDisplay2.Text = allresult;
        }

        private void BtnCloseBracket_Click(object sender, RoutedEventArgs e)
        {
            if (bracksCount <= 0)
                return;

            if (result == "")
                result = txtDisplay.Text;


            allresult += result + ")";
            bracksCount--;
            result = "";
            txtDisplay.Text = Calculator.CloseBracCalc(allresult).ToString();
            txtDisplay2.Text = allresult;
        }

        //-------------------------------
        //  Operators(+ - * / ^)    

        private void OperatorsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay2.Text.Contains('='))
            {
                allresult = "";
            }

            if (txtDisplay2.Text[txtDisplay2.Text.Length - 1] != ')')
            {
                if (result == "")
                    allresult += txtDisplay.Text;
                else
                    allresult += result;
            }

            if (bracksCount == 0)
                txtDisplay.Text = Calculator.Calculate(allresult).ToString();
            else
                txtDisplay.Text = Calculator.OperatorClickCalc(allresult).ToString();


            Button button = sender as Button;
            allresult += button.Tag;
            result = "";
            txtDisplay2.Text = allresult;
        }

        //-------------------------------
        // Clear buutons
        private void BtnClearEntry_Click_1(object sender, RoutedEventArgs e)
        {
            result = "";
            txtDisplay.Text = "0";
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            result = "";
            allresult = "";
            txtDisplay.Text = "0";
            txtDisplay2.Text = "0";
        }

        private void BtnBackspace_Click(object sender, RoutedEventArgs e)
        {
            if(result.Length > 0)
                result = result.Substring(0, result.Length - 1);

            if (result.Length > 0)
                txtDisplay.Text = result;
            else
                txtDisplay.Text = "0";
        }
        //-------------------------------
        private void BtnEquals_Click(object sender, RoutedEventArgs e)
        {
            allresult += result;
            if (bracksCount > 0)
            {
                for (int i = 0; i < bracksCount; i++)
                {
                    allresult += ")";
                }
            }
            txtDisplay2.Text = allresult + "=";
            try
            {
                result = Calculator.Calculate(allresult).ToString();
                txtDisplay.Text = result;
            }
            catch
            {
                MessageBox.Show("Your formula is invalid. Please try again");
                txtDisplay.Text = "0";
                txtDisplay2.Text = "0";
                allresult = "";
                result = "";
            }
        }

        // Key properties

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D6 && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                btnSquareY.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            else if (e.Key == Key.D9 && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                btnOpenBracket.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            else if (e.Key == Key.D0 && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                btnCloseBracket.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            else if (e.Key == Key.D0 || e.Key == Key.NumPad0)
                btn0.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            else if (e.Key == Key.D1 || e.Key == Key.NumPad1)
                btn1.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            else if (e.Key == Key.D2 || e.Key == Key.NumPad2)
                btn2.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            else if (e.Key == Key.D3 || e.Key == Key.NumPad3)
                btn3.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            else if (e.Key == Key.D4 || e.Key == Key.NumPad4)
                btn4.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            else if (e.Key == Key.D5 || e.Key == Key.NumPad5)
                btn5.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            else if (e.Key == Key.D6 || e.Key == Key.NumPad6)
                btn6.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            else if (e.Key == Key.D7 || e.Key == Key.NumPad7)
                btn7.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            else if (e.Key == Key.D8 || e.Key == Key.NumPad8)
                btn8.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            else if (e.Key == Key.D9 || e.Key == Key.NumPad9)
                btn9.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            else if (e.Key == Key.Add)
                btnPlus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            else if (e.Key == Key.Multiply)
                btnTimes.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            else if (e.Key == Key.Subtract)
                btnMinus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            else if (e.Key == Key.Divide)
                btnDivide.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            else if (e.Key == Key.OemPeriod || e.Key == Key.Decimal)
                btnPoint.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            else if (e.Key == Key.Enter)
                btnEquals.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            else if (e.Key == Key.Back)
                btnBackspace.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
    }
}
