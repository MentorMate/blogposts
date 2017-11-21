using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Tests.Acceptance
{
    [Binding]
    public class StepDefinitions
    {
        [Given(@"I am at the (.+) page")]
        public void GivenAtPage(string page) =>
            WebBrowser.Url = WebBrowser.GetUrl(page);

        [When(@"I fill the (.+) (.+) with value '(.+)'")]
        public void WhenFillWithValue(string label, string role, string value) =>
            WebBrowser.FindByAria(label, role)
                      .SendKeys(value);

        [When(@"I click the (.+) (.+)")]
        public void WhenPress(string label, string role) =>
            WebBrowser.FindByAria(label, role)
                      .Click();

        [Then(@"a text '(.+)' should appear in the (.+) (.+)")]
        public void ThenAnErrorShouldAppearShowing(string text, string label, string role) =>
            Assert.AreEqual(
                text,
                WebBrowser.FindByAria(label, role).Text);

        [Then("I should be at the (.+) page")]
        public void ThenShouldBeAtTheXPage(string expectedPage) =>
            Assert.AreEqual(
                WebBrowser.GetUrl(expectedPage),
                WebBrowser.Url);

        [AfterScenario]
        public void Destroy() =>
            WebBrowser.Current
                      .Quit();
    }
}