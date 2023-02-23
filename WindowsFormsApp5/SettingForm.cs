using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class SettingForm : Form
    {
        public static List<string> FinalCities = new List<string>();
        public static string FinalRefreshTime = "";
        public SettingForm()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string cities = string.Empty;
            foreach (var item in checkedListBoxCities.CheckedItems)
            {
                FinalCities.Add(item.ToString());
                cities += item.ToString() + ",";
            }
            cities = cities.TrimEnd(',');
            Console.WriteLine(cities);
            FinalRefreshTime = comboBoxTime.SelectedItem.ToString();

            
            this.Close();
            WeatherData form1 = new WeatherData();
            form1.Show();
        }
        private void SettingForm_Load_1(object sender, EventArgs e)
        {
            checkedListBoxCities.Items.Add(MyStrings.City5);
            checkedListBoxCities.Items.Add(MyStrings.City4);
            checkedListBoxCities.Items.Add(MyStrings.Ctiy1);
            checkedListBoxCities.Items.Add(MyStrings.City3);
            checkedListBoxCities.Items.Add(MyStrings.City2);
            checkedListBoxCities.CheckOnClick = true;

            comboBoxTime.Items.Add(MyStrings.time1);
            comboBoxTime.Items.Add(MyStrings.time2);
            comboBoxTime.Items.Add(MyStrings.time3);
        }
    }
}
