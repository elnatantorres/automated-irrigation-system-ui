using AutomatedIrrigationSystem.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AutomatedIrrigationSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView1.DataSource = new List<IrrigationSystem>()
            {
                new IrrigationSystem()
                {
                    Controller = "Arduino UNO",
                    Id = 1,
                    Irrigator = "IRR-1",
                    Plantation = "Alpha",
                    Sensor = "SENS-HUM-1",
                    SolenoidValve = "SOLE-1"
                }
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dataGridView2.DataSource = GetExecution().results;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int irrigationSystemId = Convert.ToInt32(textBox1.Text);

            CreateIrrigationExecution(irrigationSystemId);

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView2.DataSource = GetExecution().results;
        }

        private GetIrrigationExecution GetExecution()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = null;

                using (var requestMessage = new HttpRequestMessage())
                {
                    requestMessage.Headers.Add("Accept", "application/json");
                    requestMessage.Headers.Add("Username", "Elnatan");
                    requestMessage.Headers.Add("Password", "123456");

                    requestMessage.RequestUri = new Uri("http://localhost:5000/irrigation-execution");

                    httpResponseMessage = httpClient
                        .SendAsync(requestMessage).Result;
                }

                var responseBody = JsonConvert
                    .DeserializeObject<GetIrrigationExecution>(httpResponseMessage.Content.ReadAsStringAsync().Result);

                return responseBody;
            }
        }

        private void CreateIrrigationExecution(int irrigationSystemId)
        {

            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = null;

                using (var requestMessage = new HttpRequestMessage())
                {
                    requestMessage.Headers.Add("Accept", "application/json");
                    requestMessage.Headers.Add("Username", "Elnatan");
                    requestMessage.Headers.Add("Password", "123456");

                    requestMessage.Method = HttpMethod.Post;
                    requestMessage.RequestUri = new Uri("http://localhost:5000/irrigation-execution");

                    var json = JsonConvert.SerializeObject(new CreateIrrigationExecution() { IrrigationSystemId = irrigationSystemId});
                    HttpContent httpContent = new StringContent(json);
                    httpContent.Headers.ContentType.MediaType = "application/json";

                    requestMessage.Content = httpContent;
                    httpResponseMessage = httpClient
                        .SendAsync(requestMessage).Result;

                    if(httpResponseMessage.IsSuccessStatusCode)
                        MessageBox.Show("Irrigação realizada com sucesso");
                }
            }
        }
    }
}


