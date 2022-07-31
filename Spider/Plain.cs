using System;
using System.Collections.Generic;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Spider;
using ToolSets;

namespace Spider
{
    /// <summary>
    ///  The spider can only make some simple task.If you want to run js, use DriverSpider.
    /// </summary>
    public abstract class PlainSpider:ISpider
    {
        public string Content { get; set; }
        public string Url { get; set; }
        public IDocument Document { get; set; }
        
        public event SpiderHandler SpiderEvent;
        public virtual void Init(string url, params string[] options)
        {
            Url = url;
            string content = NetWorkTool.GetHtml(url);
            Content = content ?? throw new Exception("Invalid URL!");
            HtmlParser parser = new HtmlParser();
            Document = parser.ParseDocument(content);
        }

        public IEnumerable<IElement> CssPickElements(string css)
        {
            foreach (IElement element in Document.QuerySelectorAll(css))
            {
                yield return element;
            }
        }

        public virtual void Work()
        {
            
        }

        public virtual void Finish()
        {
            
        }
    }
}