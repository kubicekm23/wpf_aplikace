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

    private int KdeCarka1 = -1;
    private int KdeCarka2 = -1;
    
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

        if (PiseSeCisloStr == "0")
        {
            PiseSeCisloStr = $"{number}";
            PiseSeCislo = number;
        }
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

        if (KdeCarka1 != -1)
        {
            var cislo1str = Convert.ToString(cislo1);
            cislo1str = cislo1str.Insert(KdeCarka1, ",");
            cislo1 = Convert.ToDouble(cislo1str);
        }

        if (KdeCarka2 != -1)
        {
            var cislo2str = Convert.ToString(cislo2);
            cislo2str = cislo2str.Insert(KdeCarka2, ",");
            cislo2 = Convert.ToDouble(cislo2str);
        }
        
        Console.WriteLine(cislo1);
        
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
            case "2\u221aX":
                calculatedNumber += Math.Sqrt(cislo1);
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
    private void ZmacknutoDruhaOdmocnina(object sender, RoutedEventArgs e) { AppendOperator("2\u221aX"); Calculate(sender, e); }
    private void ZmacknutoReversePlusMinus(object sender, RoutedEventArgs e) { ReversePolarity(); }

    private void ZmacknutaDesetinnaCarka(object sender, RoutedEventArgs e)
    {
        PiseSeCisloStr += ",";

        if (operace != "")
        {
            KdeCarka2 = PiseSeCisloStr.Length - 2;
        }
        else
        {
            KdeCarka1 = PiseSeCisloStr.Length - 2;
        }
        
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

    private void ClearNumber(object sender, RoutedEventArgs e)
    {
        if (operace != "") 
        { 
            cislo2 = 0; 
            PiseSeCislo = 0; 
            PiseSeCisloStr = "0"; 
            TextVypoctu.Text = "0"; 
        }
        else
        {
            cislo1 = 0;
            PiseSeCislo = 0;
            PiseSeCisloStr = "0";
            TextVypoctu.Text = "0";
        }
    }

    private void RemoveLast(object sender, RoutedEventArgs e)
    {
        if (PiseSeCisloStr.Length > 1) // If there's more than one character
        {
            PiseSeCisloStr = PiseSeCisloStr.Remove(PiseSeCisloStr.Length - 1);
            PiseSeCislo = Convert.ToDouble(PiseSeCisloStr);
        }
        else // If there's only one character
        {
            PiseSeCisloStr = "0";
            PiseSeCislo = 0;
        }

        TextVypoctu.Text = PiseSeCisloStr;
    }

    private void ReversePolarity()
    {
        if (cislo2 != 0)
        {
            cislo2 *= -1;
        }
        else if (cislo1 != 0)
        {
            cislo1 *= -1;
        }
        
        PiseSeCislo *= -1;
        PiseSeCisloStr = Convert.ToString(PiseSeCislo);
        
        TextVypoctu.Text = PiseSeCisloStr;
    }
}
        
