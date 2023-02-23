using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class WeatherData : Form
    {
        //WeatherInfo weatherInfo;
        private int panelIndex = 0;
        private int totalCities = 0;
        private HttpClient httpClient = new HttpClient();
        private string apiKey = MyStrings.Api_key;

        public WeatherData()
        {
            InitializeComponent();
            totalCities = SettingForm.FinalCities.Count;
            //weatherInfo = new WeatherInfo();
            UpdateLabels();
            timer1.Interval = Int32.Parse(SettingForm.FinalRefreshTime) * 1000; 
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            panelIndex++;
            
            if (panelIndex >= totalCities)
            {
                panelIndex = 0;
            }
            Console.WriteLine(MyStrings.Tick + panelIndex);
            UpdateLabels();
            
        }
        private async Task<string> GetWeatherData(string city)
        {
            try
            {
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"An error occurred while getting the weather data for {city}: {ex.Message}");
                return null;
            }
        }
        private async void UpdateLabels()
        {
            
            try
            {
                Console.WriteLine(panelIndex.ToString());
                var weatherData = await GetWeatherData(SettingForm.FinalCities.ElementAt(panelIndex));
                dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject(weatherData);
                double temperature = json.main.temp;
                double humidity = json.main.humidity;
                double pressure = json.main.pressure;
                label1.Text = SettingForm.FinalCities.ElementAt(panelIndex);
                lblHumidity.Text = $"Humidity: {humidity}%";
                lblAtmosphere.Text = $"Atmosphere: {pressure} Pa";
                lblTemperature.Text = $"Temperature: {temperature}°C";
                
               
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Left = WindowsFormsApp5.Properties.Settings.Default.Left;
            this.Width = WindowsFormsApp5.Properties.Settings.Default.Width;
            this.Height = WindowsFormsApp5.Properties.Settings.Default.Height;
            this.Top = WindowsFormsApp5.Properties.Settings.Default.Top;

        }
        private void WeatherData_FormClosed(object sender, FormClosedEventArgs e)
        {
            WindowsFormsApp5.Properties.Settings.Default.Left = this.Left;
            WindowsFormsApp5.Properties.Settings.Default.Width = this.Width;
            WindowsFormsApp5.Properties.Settings.Default.Height = this.Height;
            WindowsFormsApp5.Properties.Settings.Default.Top = this.Top;
            WindowsFormsApp5.Properties.Settings.Default.Save();
        }
    }
}



      

