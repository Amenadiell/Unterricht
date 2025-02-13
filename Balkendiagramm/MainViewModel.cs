using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Balkendiagramm
{
    public class MainViewModel
    {
        // Modell für die Balken-Daten
        public class DiagramLevelItem
        {
            public string Name { get; set; } = string.Empty; // Standardwert gesetzt, um Null-Fehler zu vermeiden
            public double Value { get; set; }
        }

        // Liste mit allen Originaldaten
        public ObservableCollection<DiagramLevelItem> AllItems { get; set; }

        // Gefilterte Liste, die in der UI angezeigt wird
        public ObservableCollection<DiagramLevelItem> FilteredItems { get; set; }

        // Werte für die Y-Achsenbeschriftung
        public ObservableCollection<int> YAxisLabels { get; set; }

        // Konstruktor
        public MainViewModel()
        {
            // Initialisierung der Listen
            AllItems = new ObservableCollection<DiagramLevelItem>();
            FilteredItems = new ObservableCollection<DiagramLevelItem>();
            YAxisLabels = new ObservableCollection<int>();

            // Beispiel-Daten hinzufügen
            AllItems.Add(new DiagramLevelItem { Name = "1 Uhr", Value = 20 });
            AllItems.Add(new DiagramLevelItem { Name = "2 Uhr", Value = 35 });
            AllItems.Add(new DiagramLevelItem { Name = "3 Uhr", Value = 40 });
            AllItems.Add(new DiagramLevelItem { Name = "4 Uhr", Value = 45 });
            AllItems.Add(new DiagramLevelItem { Name = "5 Uhr", Value = 30 });
            AllItems.Add(new DiagramLevelItem { Name = "6 Uhr", Value = 40 });
            AllItems.Add(new DiagramLevelItem { Name = "7 Uhr", Value = 55 });
            AllItems.Add(new DiagramLevelItem { Name = "8 Uhr", Value = 45 });
            AllItems.Add(new DiagramLevelItem { Name = "9 Uhr", Value = 40 });
            AllItems.Add(new DiagramLevelItem { Name = "10 Uhr", Value = 50 });
            AllItems.Add(new DiagramLevelItem { Name = "11 Uhr", Value = 55 });
            AllItems.Add(new DiagramLevelItem { Name = "12 Uhr", Value = 65 });
            AllItems.Add(new DiagramLevelItem { Name = "13 Uhr", Value = 55 });
            AllItems.Add(new DiagramLevelItem { Name = "14 Uhr", Value = 70 });
            AllItems.Add(new DiagramLevelItem { Name = "15 Uhr", Value = 50 });
            AllItems.Add(new DiagramLevelItem { Name = "16 Uhr", Value = 60 });
            AllItems.Add(new DiagramLevelItem { Name = "17 Uhr", Value = 35 });
            AllItems.Add(new DiagramLevelItem { Name = "18 Uhr", Value = 30 });
            AllItems.Add(new DiagramLevelItem { Name = "19 Uhr", Value = 20 });
            AllItems.Add(new DiagramLevelItem { Name = "20 Uhr", Value = 35 });
            AllItems.Add(new DiagramLevelItem { Name = "21 Uhr", Value = 25 });
            AllItems.Add(new DiagramLevelItem { Name = "22 Uhr", Value = 35 });
            AllItems.Add(new DiagramLevelItem { Name = "23 Uhr", Value = 30 });
            AllItems.Add(new DiagramLevelItem { Name = "24 Uhr", Value = 25 });

            // Y-Achsen-Werte generieren
            for (int i = 0; i <= 100; i += 10)
            {
                YAxisLabels.Insert(0, i); // Höhere Werte oben
            }

            // Initialer Filter
            FilterData(item => item.Value > 10);
        }

        // Methode zur Filterung der Daten
        public void FilterData(Func<DiagramLevelItem, bool> filter)
        {
            if (FilteredItems == null)
                FilteredItems = new ObservableCollection<DiagramLevelItem>();

            FilteredItems.Clear(); // Vorherige Daten löschen

            foreach (var item in AllItems.Where(filter))
            {
                FilteredItems.Add(item);
            }
        }
    }
}