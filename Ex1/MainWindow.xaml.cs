using System.Diagnostics;
using System.Windows;

namespace Ex1;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ThreadInfoText.Text = $"Главный поток (UI): {Thread.CurrentThread.ManagedThreadId}";
    }

    private async void StartNotepadButton_Click(object sender, RoutedEventArgs e)
    {
        StatusText.Dispatcher.Invoke(() => StatusText.Text = "Запуск Notepad...");

        try
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "notepad.exe",
                UseShellExecute = true
            };

            var process = Process.Start(processStartInfo);
            
            await Task.Delay(3000);

            var realProcess = Process.GetProcessesByName("Notepad")[0];
            realProcess.Kill();
        }
        catch (Exception ex)
        {
            StatusText.Dispatcher.Invoke(() => StatusText.Text = $"Ошибка: {ex.Message}");
        }
    }
}