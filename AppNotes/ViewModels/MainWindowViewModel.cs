using Avalonia.Controls.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.IO;

namespace test.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    
    private const string NotesFolder = "Notes";

    [ObservableProperty]
    private DateTime? selectedDate = DateTime.Today;

    [ObservableProperty]
    private string notatka = string.Empty;

    public MainWindowViewModel()
    {
        
        Directory.CreateDirectory(NotesFolder);
        LoadNoteForDate(SelectedDate);
    }

    
    partial void OnSelectedDateChanged(DateTime? value)
    {
        LoadNoteForDate(value);
    }

    [RelayCommand]
    public void Zapisz()
    {
        if (SelectedDate == null) return;
        var path = GetFilePath(SelectedDate.Value);
        File.WriteAllText(path, Notatka ?? string.Empty);
    }

    private void LoadNoteForDate(DateTime? date)
    {
        if (date == null)
        {
            Notatka = string.Empty;
            return;
        }

        var path = GetFilePath(date.Value);
        if (File.Exists(path))
            Notatka = File.ReadAllText(path);
        else
            Notatka = string.Empty;
    }

    private string GetFilePath(DateTime date)
    {
        var fileName = date.ToString("yyyy-MM-dd") + ".txt";
        return System.IO.Path.Combine(NotesFolder, fileName);

    }
}
