using System.Windows;

namespace Balkendiagramm
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel(); // Stellt sicher, dass die Datenbindung funktioniert
        }
    }
}