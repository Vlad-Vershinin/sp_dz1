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
                FileName = "Notepad.exe",
                UseShellExecute = false
            };

            var process = Process.Start(processStartInfo);

            await Task.Delay(3000);

            var realProcess = Process.GetProcessesByName("Notepad")[0];

            realProcess.Kill();

            await StatusText.Dispatcher.InvokeAsync(() => StatusText.Text = "Процес завершён");
        }
        catch (Exception ex)
        {
            await StatusText.Dispatcher.InvokeAsync(() => StatusText.Text = $"Ошибка: {ex.Message}");
        }
    }

    private void UpdateUI(object sender, RoutedEventArgs e)
    {
        var thread = new Thread(() =>
        {
            Dispatcher.Invoke(() => StatusText.Text = "Данная строка обновлена из фонового потока");
        });

        thread.Start();
    }
}