using CardValidation.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Reqnroll;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace CardValidation.IntegrationTests.Steps
{
    [Binding]
    public class CardValidationStepDefinitions
    {
        private CreditCard creditcard = null!;
        private HttpResponseMessage response = null!;
        private string responseBody = string.Empty;
        private string endpointUrl = "/CardValidation/card/credit/validate";

        [Given("user has a card with details {string}, {string}, {string}, {string}")]
        public void GivenUserHasACardWithDetails(string owner, string cardNum, string date, string cvv)
        {
            creditcard = new CreditCard
            {
                Owner = owner,
                Number = cardNum,
                Date = date,
                Cvv = cvv
            };
        }

        [When("we call the card validation endpoint")]
        public async Task WhenWeCallTheCardValidationEndpoint()
        {
            response = await Hooks.Hooks.HttpClient.PostAsJsonAsync(endpointUrl, creditcard);
            responseBody = await response.Content.ReadAsStringAsync();
        }

        [Then("the response status is {int}")]
        public void ThenTheResponseStatusIs(int statusCode)
        {
            Assert.Equal(statusCode, (int)response.StatusCode);
        }     

        [Then("the response body contains {string}")]
        public void ThenTheResponseBodyContains(string message)
        {
            responseBody.Should().Contain(message);
        }

        [Then("the response body has error {string} for field {string}")]
        public void ThenTheResponseBodyHasErrorForField(string errorMessage, string field)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var errors = JsonSerializer.Deserialize<Dictionary<string, string[]>>(responseBody, options);

            errors.Should().ContainKey(field);
            errors[field].Should().Contain(errorMessage);
        }



    }
}
