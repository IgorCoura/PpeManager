using HtmlAgilityPack;
using PpeManager.Domain.ValueTypes;

namespace PpeManager.Api.Infrastructure.Services
{
    public class ConsultApprovalCertificateNumberService : IConsultApprovalCertificateNumberService
    {
        private readonly HttpClient _client;
        public ConsultApprovalCertificateNumberService(HttpClient client)
        {
            _client = client;
            client.Timeout = TimeSpan.FromSeconds(10);
        }

        public DateTime ConsultValidity(ApprovalCertificate number)
        {

            if (!number.contract.IsValid)
            {
                var message = "";
                var messagens = number.contract.Notifications.Select(n => n.Message).ToList();
                foreach (var m in messagens)
                {
                    message += m + "\n";
                }
                throw new Exception(message);
            }

            try
            {
                var response = _client.GetAsync("https://consultaca.com/" + number.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    var dataObject = response.Content.ReadAsStringAsync().Result;
                    var html = new HtmlDocument();
                    html.LoadHtml(dataObject);
                    var doc = html.DocumentNode;
                    var element = doc.Descendants(0).Where(n => n.HasClass("validade_ca"));
                    var text = element.Single().InnerText;
                    if (DateTime.TryParse(text, out DateTime date))
                    {
                        return date;
                    }

                    throw new Exception("The validity is invalid");


                }
                else
                {
                    throw new Exception("Bad Request: It was not possible to check the validity of the number entered");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


        }
    }
}
