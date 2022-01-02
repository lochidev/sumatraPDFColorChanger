using System.Diagnostics;

namespace sumatraPDFColorChanger
{
    public partial class Form1 : Form
    {
        private readonly string _filePath;
        private string _currentConfig = "green-on-blue.txt";
        public Form1()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _filePath = $"{appData}\\SumatraPDF\\SumatraPDF-settings.txt";
            InitializeComponent();
            foreach (string? file in Directory.GetFiles("configs"))
            {
                Pallets.Items.Add(file[8..]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string currentSettings = File.ReadAllText(_filePath);
            int indexOfState = currentSettings.IndexOf("FileStates");
            string? configText = File.ReadAllText($"configs//{_currentConfig}");
            configText = configText.Remove(configText.IndexOf("FileStates"));
            configText += currentSettings[indexOfState..];
            File.WriteAllText(_filePath, configText);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            File.WriteAllText(_filePath, string.Empty);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentConfig = (string)Pallets.SelectedItem;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", $"{Directory.GetCurrentDirectory()}\\configs\\");
        }
    }
}
