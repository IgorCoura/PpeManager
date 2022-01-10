using HtmlAgilityPack;
using PpeManager.Domain.ValueTypes;
using System.Globalization;

namespace PpeManager.Api.Infrastructure.Services
{
    public class ConsultApprovalCertificateNumberService : IConsultApprovalCertificateNumberService
    {
        private readonly HttpClient _client;
        public ConsultApprovalCertificateNumberService(HttpClient client)
        {
            _client = client;
        }

        public DateOnly ConsultValidity(ApprovalCertificate number)
        {

            if (!number.contract.IsValid)
            {
                var message = "";
                var messagens = number.contract.Notifications.Select(n => n.Message).ToList();
                foreach (var m in messagens)
                {
                    message += m + "\n";
                }
                throw new ConsultApprovalCertificateNumberException(message);
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
                    if (DateOnly.TryParse(text, new CultureInfo("pt-BR"), DateTimeStyles.None, out DateOnly date))
                    {
                        return date;
                    }

                    
                    throw new ConsultApprovalCertificateNumberException("The validity is invalid");


                }
                else
                {
                    throw new ConsultApprovalCertificateNumberException("Bad Request: It was not possible to check the validity of the number entered");
                }
            }
            catch (Exception ex)
            {
                throw new ConsultApprovalCertificateNumberException(ex.Message, ex);
            }


        }
    }
}
