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
    private bool JustCalculatedNumber = false;

    private List<double> cisla = new List<double>();
    private List<string> operace = new List<string>();

    private double PiseSeCislo = 0;
    private string PiseSeCisloStr = "";
    
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
        
        PiseSeCisloStr += number;
        PiseSeCislo = Convert.ToDouble(cislo);
        
        TextVypoctu.Text = PiseSeCisloStr;
    }

    private void AppendOperator(string operatorString)
    {
        operace.Add(operatorString);
        cisla.Add(PiseSeCislo);
        PiseSeCislo = 0;
        
        PiseSeCisloStr += operatorString;
        TextVypoctu.Text = PiseSeCisloStr;
    }

    private void Calculate(object sender, RoutedEventArgs e)
    {
        double calculatedNumber = 0;
        
        cisla.Add(PiseSeCislo);
        PiseSeCislo = 0;

        switch (operace[0])
        {
            case "+":
                calculatedNumber += cisla[0] + cisla[1];
                break;
            case "-":
                calculatedNumber += cisla[0] - cisla[1];
                break;
            case "*":
                calculatedNumber += cisla[0] * cisla[1];
                break;
            case "/":
                calculatedNumber += cisla[0] / cisla[1];
                break;
        }
        
        for (int i = 1; i < operace.Count; i++)
        {
            switch (operace[i])
            {
                case "+":
                    calculatedNumber += cisla[i+1];
                    break;
                case "-":
                    calculatedNumber -= cisla[i+1];
                    break;
                case "*":
                    calculatedNumber *= cisla[i+1];
                    break;
                case "/":
                    calculatedNumber /= cisla[i+1];
                    break;
            }
        }
        
        vysledek = calculatedNumber.ToString();
        TextVypoctu.Text = vysledek;
        JustCalculatedNumber = true;
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
    private void ZmacknutoMinusPrvni(object sender, RoutedEventArgs e) { AppendOperator("x^1"); }
    private void ZmacknutoNaDruhou(object sender, RoutedEventArgs e) { AppendOperator("^2"); }

    private void ZmacknutaDesetinnaCarka(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void ClearEverything(object sender, RoutedEventArgs e)
    {
        vysledek = "";
        JustCalculatedNumber = false;

        cisla = new List<double>();
        operace = new List<string>();

        PiseSeCislo = 0;
        PiseSeCisloStr = "";

        TextVypoctu.Text = "";
    }

    private void RemoveLast(object sender, RoutedEventArgs e)
    {
        if (PiseSeCisloStr == "")
        {
            if (operace.Count > 0 && operace.Count == cisla.Count)
            {
                operace.RemoveAt(operace.Count - 1);
            }
        }
        else
        {
            if (operace.Count > 0 && operace.Count == cisla.Count + 1)
            {
                operace.RemoveAt(operace.Count - 1);
            }
            else if (PiseSeCisloStr.Length > 0)
            {
                PiseSeCisloStr = PiseSeCisloStr.Remove(PiseSeCisloStr.Length - 1);

                if (PiseSeCisloStr.Length == 0)
                {
                    PiseSeCislo = 0;
                }
                else
                {
                    PiseSeCislo = Convert.ToDouble(PiseSeCisloStr);
                }
            }
        }
        
        if (operace.Count > 0)
        {
            StringBuilder displayText = new StringBuilder();
            for (int i = 0; i < cisla.Count && i < operace.Count; i++)
            {
                displayText.Append(cisla[i]);
                displayText.Append(operace[i]);
            }
            displayText.Append(PiseSeCisloStr);
            TextVypoctu.Text = displayText.ToString();
        }
        else
        {
            TextVypoctu.Text = PiseSeCisloStr;
        }
    }
}