using AppNotes.Models;
using Avalonia.Controls.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using test.ViewModels;

namespace test.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{

    private const string NotesFolder = "Notes";

    [ObservableProperty]
    private DateTime? selectedDate = DateTime.Today;

    [ObservableProperty]
    private string notatka = string.Empty;

    [ObservableProperty]
    private List<Task> zadania = [new Task { Name = "test1", IsChecked = false }, new Task { Name = "test2", IsChecked = true }];

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

    [RelayCommand]
    public void Odchaczone()
    {
        if (SelectedDate == null) return;

        var path = GetFilePath(SelectedDate.Value);

        var json = JsonSerializer.Serialize(new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(path, json);
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
            Notatka = "Brak notatki na wybrany dzień";
    }

    private string GetFilePath(DateTime date)
    {
        var fileName = date.ToString("yyyy-MM-dd") + ".txt";
        return System.IO.Path.Combine(NotesFolder, fileName);

    }
}
