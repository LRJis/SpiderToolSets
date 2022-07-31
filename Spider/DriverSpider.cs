using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using Spider;


namespace Spider
{
    public abstract class DriverSpider:ISpider
    {
        public EdgeDriver Driver { get; set; }
        
        public event SpiderHandler SpiderEvent;
        public string Url { get; set; }
        public void Init(string url, params string[] options)
        {
            Url = url;
            EdgeOptions edgeOptions = new EdgeOptions();
            foreach (string option in options)
            {
                edgeOptions.AddArgument(option);
            }

            Driver = new EdgeDriver(edgeOptions);
            Driver.Navigate().GoToUrl(url);
            
        }

        

        public virtual void Work()
        {
            
        }

        public virtual void Finish()
        {
            Driver.Close();
            Driver.Dispose();
        }

        public IEnumerable<IWebElement> CssPickElements(string css)
        {
            By by = By.CssSelector(css);
            foreach (IWebElement element in Driver.FindElements(by))
            {
                yield return element;
            }
        }
    }
}