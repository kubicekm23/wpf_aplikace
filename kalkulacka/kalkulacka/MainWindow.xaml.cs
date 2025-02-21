using System.Net.Mime;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace kalkulacka;


public partial class MainWindow : Window
{
    private string vysledek = "";

    private double cislo1;
    private double cislo2;
    private string operace = "";

    private double PiseSeCislo = 0;
    private string PiseSeCisloStr = "0";
    
    public MainWindow()
    {
        InitializeComponent();
        MaxWidth = 250; MaxHeight = 300;
        MinWidth = 250; MinHeight = 300;
    }
    
    private void AppendNumber(int number)
    {
        string cislo = PiseSeCislo.ToString();
        cislo += number; 
        
        if (PiseSeCisloStr == "0") { PiseSeCisloStr = $"{number}"; PiseSeCislo = number; }
        else
        {
            PiseSeCisloStr += number;
            PiseSeCislo = Convert.ToDouble(cislo);
        }
        
        TextVypoctu.Text = PiseSeCisloStr;
    }

    private void AppendOperator(string operatorString)
    {
        operace = operatorString;
        cislo1 = PiseSeCislo;
        PiseSeCislo = 0;
        
        PiseSeCisloStr = "0";
        TextVypoctu.Text = "0";
    }

    private void Calculate(object sender, RoutedEventArgs e)
    {
        double calculatedNumber = 0;
        
        cislo2 = PiseSeCislo;
        //PiseSeCislo = 0;

        switch (operace)
        {
            case "+":
                calculatedNumber += cislo1 + cislo2;
                break;
            case "-":
                calculatedNumber += cislo1 - cislo2;
                break;
            case "*":
                calculatedNumber += cislo1 * cislo2;
                break;
            case "/":
                calculatedNumber += cislo1 / cislo2;
                break;
            case "x^-1":
                calculatedNumber += Math.Pow(cislo1, -1);
                break;
            case "^2":
                calculatedNumber += Math.Pow(cislo1, 2);
                break;
        }
        
        vysledek = calculatedNumber.ToString();
        cislo1 = calculatedNumber; cislo2 = 0;
        PiseSeCislo = cislo1;
        TextVypoctu.Text = vysledek;
    }

    private void ZmacknutoJedna(object sender, RoutedEventArgs e) { AppendNumber(1); }
    private void ZmacknutoDva(object sender, RoutedEventArgs e) { AppendNumber(2); }
    private void ZmacknutoTri(object sender, RoutedEventArgs e) { AppendNumber(3); }
    private void ZmacknutoCtyry(object sender, RoutedEventArgs e) { AppendNumber(4); }
    private void ZmacknutoPet(object sender, RoutedEventArgs e) { AppendNumber(5); }
    private void ZmacknutoSest(object sender, RoutedEventArgs e) { AppendNumber(6); }
    private void ZmacknutoSedm(object sender, RoutedEventArgs e) { AppendNumber(7); }
    private void ZmacknutoOsm(object sender, RoutedEventArgs e) { AppendNumber(8); }
    private void ZmacknutoDevet(object sender, RoutedEventArgs e) { AppendNumber(9); }
    private void ZmacknutoNula(object sender, RoutedEventArgs e) { AppendNumber(0); }

    private void ZmacknutoLomitko(object sender, RoutedEventArgs e) { AppendOperator("/"); }
    private void ZmacknutoKrat(object sender, RoutedEventArgs e) { AppendOperator("*"); }
    private void ZmacknutoMinus(object sender, RoutedEventArgs e) { AppendOperator("-"); }
    private void ZmacknutoPlus(object sender, RoutedEventArgs e) { AppendOperator("+"); }
    private void ZmacknutoMinusPrvni(object sender, RoutedEventArgs e) { AppendOperator("x^-1"); Calculate(sender, e); }
    private void ZmacknutoNaDruhou(object sender, RoutedEventArgs e) { AppendOperator("^2"); Calculate(sender, e); }

    private void ZmacknutaDesetinnaCarka(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void ClearEverything(object sender, RoutedEventArgs e)
    {
        vysledek = "";

        cislo1 = 0; cislo2 = 0;
        operace = "";

        PiseSeCislo = 0;
        PiseSeCisloStr = "";

        TextVypoctu.Text = "";
    }

    private void RemoveLast(object sender, RoutedEventArgs e)
    {
        if (cislo2 != 0)
        {
            var prevodString = Convert.ToString(cislo2);
            prevodString = prevodString.Remove(prevodString.Length - 1, 1);
            cislo2 = Convert.ToDouble(prevodString);
        }
        else
        {
            if (operace != "")
            {
                operace = "";
            }

            else if (cislo1 != 0)
            {
                var prevodString = Convert.ToString(cislo1);
                prevodString = prevodString.Remove(prevodString.Length - 1, 1);
                cislo2 = Convert.ToDouble(prevodString);
            }
        }
    }
}