using Reqnroll;
using CardValidation.IntegrationTests.Helpers;

namespace CardValidation.IntegrationTests.Hooks
{
    [Binding]
    public class Hooks
    {

        private static WebAppFactory Factory = null!;
        private static HttpClient Client = null!;

        [BeforeTestRun]
        public static void BeforeTestRun() 
        { 
            Factory = new WebAppFactory();
            Client = Factory.CreateClient();
        }

        [AfterTestRun]
        public static void AfterTestRun() 
        {
            Client.Dispose();
            Factory.Dispose();
        }

        // Expose the HttpClient to step definitions
        public static HttpClient HttpClient => Client;
    }
}
