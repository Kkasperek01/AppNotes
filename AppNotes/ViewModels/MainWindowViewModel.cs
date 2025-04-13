using Avalonia.Controls.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.IO;

namespace test.ViewModels;
public partial class MainWindowViewModel : ObservableObject
{
    private const string FilePath = "notes.txt";

    [ObservableProperty]
    private string notatka = "";

    public MainWindowViewModel()
    {
        // Wczytaj zawartość pliku przy starcie
        if (File.Exists(FilePath))
        {
            Notatka = File.ReadAllText(FilePath);
        }
    }

    [RelayCommand]
    public void ZapiszCommand()
    {
        Console.WriteLine("Dodałe tekst do pliku");
        using (StreamWriter outputFile = new StreamWriter(FilePath))
        {
            outputFile.WriteLine(notatka);
        }
    }
}

