using Fundo.Applications.WebApi.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Fundo.Services.Tests.Integration
{

    public class LoanManagementControllerTests : IClassFixture<WebApplicationFactory<Applications.WebApi.Controllers.LoanManagementController>>
    {
        private readonly HttpClient _client;

        public LoanManagementControllerTests(WebApplicationFactory<Fundo.Applications.WebApi.Controllers.LoanManagementController> factory)
        {
            var baseUri = new Uri("http://localhost:5050");
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                BaseAddress = baseUri
            });
        }

        [Fact]
        public async Task GetBalances_ShouldReturnExpectedResult()
        {
            //Act
            var response = await _client.GetAsync("loans/1");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            //var loan = JsonSerializer.Deserialize<List<LoanListedDto>>(stringResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            var loan = JsonSerializer.Deserialize<LoanListedDto>(stringResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            //Assert
            Assert.NotNull(loan);
            Assert.Equal("El Chavo del Ocho", loan.CustomerName);
            Assert.Equal(3500, loan.CurrentBalance);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateLoan_ShouldReturnExpectedResult()
        {
            //Arrange
            var obj = new LoanDto
            {
                CustomerId = 3,
                Amount = 350
            };

            JsonContent content = JsonContent.Create(obj);
            //Act
            var response = await _client.PostAsync("loans", content);
            response.EnsureSuccessStatusCode();
            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var id = JsonSerializer.Deserialize<int>(stringResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            id.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task MakePayment_ShouldReturnExpectedResult()
        {
            //Arrange
            var obj = new PaymentDto
            {
                LoanId = 2,
                PaymentAmount = 100
            };

            JsonContent content = JsonContent.Create(obj);
            //Act
            var response = await _client.PostAsync("loans/2/payment", content);
            response.EnsureSuccessStatusCode();
            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var id = JsonSerializer.Deserialize<int>(stringResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            id.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetAllLoans_ShouldReturnExpectedResult()
        {
            //Act
            var response = await _client.GetAsync("loans");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var loans = JsonSerializer.Deserialize<List<LoanListedDto>>(stringResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            
            //Assert
            Assert.NotNull(loans);
            loans.Count.Should().BeGreaterThan(0);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
