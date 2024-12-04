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

namespace H11Oef3Inschrijvingsgeld
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string[] degrees = { "Programmeren", "Netwerkbeheer", "Internet of Things", "Digitale Vormgever", "Drone opleiding"};
        double[] costs = {920.80, 920.80, 520.80, 750.80, 520.80};

        double discountNoWork;
        double discountDegree;

        string name;
        string selectedDegree;
        double selectedCost;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string degree in degrees)
            {
                ComboBoxItem comboItem = new ComboBoxItem();
                comboItem.Content = degree;
                educationComboBox.Items.Add(comboItem);
            }
        }

        private void noWorkCheckBox_CheckedOrUnchecked(object sender, RoutedEventArgs e)
        {
            if (noWorkCheckBox.IsChecked == true)
            {
                discountNoWork = 0.5;
            }
            else if (noWorkCheckBox.IsChecked == false)
            { 
                discountNoWork = 1;
            }
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton selectedRadioButton = sender as RadioButton;

            if (selectedRadioButton == secondaryRadioButton)
            {                
                discountDegree = 0.7;                
            }
            else if (selectedRadioButton == graduateRadioButton)
            {
                discountDegree = 0.8;
            }
            else if (selectedRadioButton == bachelorRadioButton)
            {
                discountDegree = 1.0;
            }
            else if (selectedRadioButton == masterRadioButton)
            {
                discountDegree = 1.1;
            }
        }

        ComboBoxItem selectedItem;

        private void educationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = educationComboBox.SelectedItem as ComboBoxItem;

            if (selectedItem != null)
            {
                selectedDegree = selectedItem.Content.ToString();

                selectedCost = 0;

                for (int i = 0; i <= degrees.Length -1; i++)
                {
                    if (degrees[i] == selectedDegree)
                    { 
                        selectedCost = costs[i];
                    }
                }  
            }
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            name = nameTextBox.Text;

            if (string.IsNullOrWhiteSpace(name) || selectedItem == null || discountDegree == 0)
            {
                MessageBox.Show("U moet een naam ingeven, een opleiding en behaald diploma kiezen voor de berekening!","Error",MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                double totalCost = selectedCost * discountDegree * discountNoWork;

                resultTextBox.Text = $"Inschrijvingsbedrag voor {name} \n\rOpleiding: {selectedDegree} \nBasisbedrag: € {selectedCost} \nTe betalen: € {Math.Round(totalCost,2)}";
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            nameTextBox.Clear();
            resultTextBox.Clear();
            nameTextBox.Focus();
        }
    }
}