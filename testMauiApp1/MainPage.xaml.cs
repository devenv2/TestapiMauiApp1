
using System.Xml;

namespace testMauiApp1;

public partial class MainPage : ContentPage
{
    private string _baseUrl;
    private string _apiKey;

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        for (int i = 0; i < int.Parse(repetition.Text); i++)
        {
            try
            {
                int num1 = GetRandomNumber();
                int num2 = GetRandomNumber();
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"https://api.e-innovation.net/?number1={num1}&number2={num2}&api_key=secret_key");

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Failed to fetch result from the API");
                    }

                    var content = await response.Content.ReadAsStringAsync();
                    var xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(content);

                    var resultNode = xmlDoc.SelectSingleNode("result");
                    l1.Text = $"{num1} + {num2} = {int.Parse(resultNode.InnerText)}";
                }
            }
            catch (Exception ex)
            {
                l1.Text = ex.Message;
                
            }
           
        }
    }

    public int GetRandomNumber()
    {
        Random random = new Random();
        return random.Next(0, 100);
    }

}

