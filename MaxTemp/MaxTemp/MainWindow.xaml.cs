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
public class SensorDataProcessor
{
    public Dictionary<string, List<SensorReading>> ReadSensorData(string filePath)
    {
        Dictionary<string, List<SensorReading>> sensorData = new Dictionary<string, List<SensorReading>>()
        {
            { "S1", new List<SensorReading>() },
            { "S2", new List<SensorReading>() },
            { "S3", new List<SensorReading>() },
            { "SB", new List<SensorReading>() },
            { "SD", new List<SensorReading>() }
        };

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
                        string sensorType = values[0];

                        if (sensorData.ContainsKey(sensorType)) 
                        {
                            sensorData[sensorType].Add(new SensorReading
                            {
                                Sensor = sensorType,
                                Timestamp = timestamp,
                                Temperature = temperature
                            });
                        }
                        else
                        {
                            Console.WriteLine($"Unknown sensor type: {sensorType}. Skipping row.");
                        }
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
            Console.WriteLine($"Error reading file: {ex.Message}");
        }

        return sensorData;
    }

    public Dictionary<string, SensorReading> GetHighestTemperatureReadings(Dictionary<string, List<SensorReading>> sensorData)
    {
        Dictionary<string, SensorReading> highestReadings = new Dictionary<string, SensorReading>();

        foreach (var sensor in sensorData)
        {
            if (sensor.Value.Count > 0)
            {
                var maxReading = sensor.Value.OrderByDescending(r => r.Temperature).First(); // Get the highest temperature reading
                highestReadings[sensor.Key] = maxReading;
            }
        }

        return highestReadings;
    }
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
        SensorDataProcessor processor = new SensorDataProcessor();
        var sensorReadings = processor.ReadSensorData("sensor_data.csv");

        var highestReadings = processor.GetHighestTemperatureReadings(sensorReadings);

        List<SensorReading> s1Readings = sensorReadings["S1"];
        List<SensorReading> s2Readings = sensorReadings["S2"];
        List<SensorReading> s3Readings = sensorReadings["S3"];
        List<SensorReading> sbReadings = sensorReadings["SB"];
        List<SensorReading> sdReadings = sensorReadings["SD"];

        string message = "Highest Temperature:\n"
        foreach(var sensor in sensorReadings)
        {
            message += $"{sensor.Key}: {sensor.Value.Temperature}°C at {sensor.Value.Timestamp}\n";
        }

        MessageBox.Show(message, "Sensor Data Analysis", MessageBoxButton.OK, MessageBoxImage.Information);

       //MessageBox.Show("Gleich kachelt das Programm...");
        //kommentieren Sie die Exception aus.
        //throw new Exception("peng");
        }
    }
}
