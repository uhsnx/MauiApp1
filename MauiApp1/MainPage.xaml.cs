
namespace MauiApp1;

using System;
using System.IO;
using System.Text;
using System.Diagnostics;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnBtnClicked(object sender, EventArgs e)
    {
        count++;

        var result = GetInfo();
        CheckBtn.Text = $"UDID: {result}";

        SemanticScreenReader.Announce(CheckBtn.Text);
    }
    
    private string GetInfo()
    {
        string result = "";

        var command = $"{Path.Combine(Directory.GetCurrentDirectory(), "Contents/Resources/build.arm64/bin/idevice_id")}";
        
        try
        {
            Process process = new Process();
            ProcessStartInfo processStartInfo2 = process.StartInfo = new ProcessStartInfo
            {
                FileName = "bash",
                Arguments = $"-c \"{command}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WorkingDirectory = "/bin/",
            };
            
            Process process2 = process;
            
            process2.Start();
            
            while (!process2.StandardOutput.EndOfStream)
            {
                string text = ((TextReader)process2.StandardOutput).ReadToEnd();
                Console.WriteLine($"Text: {text}");
                
                if (!text.Contains("Unable"))
                {
                    result = text;
                }
            }

            while (!process2.StandardError.EndOfStream)
            {
                string etext = ((TextReader)process2.StandardError).ReadToEnd();
                Console.WriteLine($"Error: {etext}");
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        
        Console.WriteLine($"UDID: {result}");
        
        return result;
    }
    
}