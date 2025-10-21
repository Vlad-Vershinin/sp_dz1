using System.Diagnostics;
using System.Text;
using System.Windows;

namespace Ex2;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnButtonClick(object sender, RoutedEventArgs e)
    {
        using (Process process = new Process())
        {
            var info = process.StartInfo;

            info.FileName = "cmd.exe";
            info.Arguments = "/c dir";
            info.UseShellExecute = false;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            info.CreateNoWindow = true;
            info.WorkingDirectory = Environment.CurrentDirectory;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            info.StandardOutputEncoding = Encoding.GetEncoding(866);
            info.StandardErrorEncoding = Encoding.GetEncoding(866);

            try
            {
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                
                process.WaitForExit();

                if (!string.IsNullOrEmpty(error))
                {
                    Dispatcher.Invoke(() => Output.Text = "Error:\n" + error);
                }
                else
                {
                    Dispatcher.Invoke(() => Output.Text = "Result:\n" + output);
                }
            }
            catch (Exception ex)
            {
                Output.Text = ex.Message;
            }
        }
    }
}