using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using PpeManager.Api.Infrastructure.Services;

namespace PpeManager.UnitTests.Application.Services
{
    public class ConsultApprovalCertificateNumberServiceTest
    {
        private readonly Mock<HttpClient> _client;

        public ConsultApprovalCertificateNumberServiceTest()
        {
            _client = new Mock<HttpClient>();   
        }

        [Fact]
        public void Consult_validity_with_valid_number()
        {
            var _service = new ConsultApprovalCertificateNumberService(_client.Object);
            var validity =  _service.ConsultValidity("31469");
            Assert.IsType<DateTime>(validity);
        }

        [Fact]
        public void Consult_validity_with_invalid_number()
        {
            var _service = new ConsultApprovalCertificateNumberService(_client.Object);
            Assert.Throws<Exception>(() => _service.ConsultValidity("000000"));
        }

        [Fact]
        public void Consult_validity_with_nonexistent_number()
        {
            var _service = new ConsultApprovalCertificateNumberService(_client.Object);
            Assert.Throws<Exception>(() => _service.ConsultValidity("00000"));
        }
    }
}
