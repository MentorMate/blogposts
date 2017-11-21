using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace Tests.Acceptance
{
    internal static class WebBrowser
    {
        private static readonly string _baseUrl = "http://localhost:62838";

        public static IWebDriver Current
        {
            get
            {
                if (!ScenarioContext.Current.ContainsKey("browser"))
                {
                    ScenarioContext.Current["browser"] = new ChromeDriver();
                }

                return ScenarioContext.Current["browser"] as IWebDriver;
            }
        }

        public static string GetUrl(string pageName) =>
            _baseUrl + "/" + (pageName == "home" ? string.Empty : pageName);

        public static IWebElement FindByAria(string label, string role)
        {
            var selector = $"[aria-label=\"{label}\"][role={role}]";
            return WebBrowser.Current?.FindElement(By.CssSelector(selector));
        }

        public static string Url
        {
            get => WebBrowser.Current.Url;
            set => WebBrowser.Current.Navigate().GoToUrl(value);
        }
    }
}