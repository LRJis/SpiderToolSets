using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Spider.Local;
using Spider.NetClient;

namespace Spider.Spider
{
    public abstract class PlainSpider:ISpider
    {
        public string Url { get; set; }
        public HtmlParser Parser;
        public IDocument Document;
        public ILogger Logger { get; set; }
        
        public virtual async void Init(string url, params string[] args)
        {
            Url = url;
            Parser = new HtmlParser();
            string content = await NetClient.NetClient.GetHtmlAsync(url);
            Document = Parser.ParseDocument(content);
        }
        
        public virtual async Task Work()
        {
            await Task.Delay(0);
        }

        public virtual void Dispose()
        {
            Document.Dispose();
        }

        public IEnumerable<IElement> SelectElementByCss(string css)
        {
            foreach (IElement element in Document.QuerySelectorAll(css))
            {
                yield return element;
            }
        }

        public IEnumerable<IElement> SelectElementByCss(string parentSelector, string childSelector)
        {
            foreach (IElement element in Document.QuerySelectorAll(parentSelector))
            {
                foreach (IElement element1 in element.QuerySelectorAll(childSelector))
                {
                    yield return element1;
                }
            }
        }

        public IEnumerable<string> SelectAttributes(string css, string attr)
        {
            foreach (IElement element in SelectElementByCss(css))
            {
                if (element.GetAttribute(attr) != null)
                    yield return element.GetAttribute(attr);
            }
        }
        
        public IEnumerable<string> SelectAttributes(string parentSelector,string childSelector, string attr)
        {
            foreach (IElement element in SelectElementByCss(parentSelector, childSelector))
            {
                if (element.GetAttribute(attr) != null)
                    yield return element.GetAttribute(attr);
            }
        }
    }
}