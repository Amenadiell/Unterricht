using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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

namespace MaxTemp
{

public class SensorReading
{
    public string Sensor { get; set; }
    public DateTime Timestamp { get; set; }
    public double Temperature { get; set; }
    
}

    public List<SensorReading> ReadSensorData(string filePath)
    {
        List<SensorReading> readings = new List<SensorReading>();

        try
        {
            using (var reader = new StreamReader(filePath))
            {
                
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (values.Length != 3)
                        continue;
                    

                    if (DateTime.TryParse(values[1], out DateTime timestamp) &&
                        double.TryParse(values[2], NumberStyles.Any, CultureInfo.InvariantCulture, out double temperature))
                    {
                        readings.Add(new SensorReading
                        {
                            Sensor = values[0],
                            Timestamp = timestamp,
                            Temperature = temperature
                        });
                    }
                    else
                    {
                        Console.WriteLine($"Skipped invalid row: {line}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
¥≤a                 Console.WriteLine($"Error reading file: {ex.Message}");
        }

        return readings;
    }
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Diese Routine (EventHandler des Buttons Auswerten) liest die Werte
        /// zeilenweise aus der Datei temps.csv aus, merkt sich den höchsten Wert
        /// und gibt diesen auf der Oberfläche aus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        

        private void BtnAuswerten_Click(object sender, RoutedEventArgs e)
        {
            var data = ReadSensorData("temps.csv");
            foreach(var Element in data)
        {
            
        }



        MessageBox.Show("Gleich kachelt das Programm...");
        //kommentieren Sie die Exception aus.
        throw new Exception("peng");
        }
    }
}
