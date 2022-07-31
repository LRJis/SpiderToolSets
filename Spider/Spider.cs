using System;
using System.Collections;
using System.Collections.Generic;
using OpenQA.Selenium.Edge;
using ToolSets;

namespace Spider
{
    public delegate void SpiderHandler(ISpider spider, EventArgs args);
    public interface ISpider
    {
        /// <summary>
        /// the event occured when the spider has vital change.
        /// </summary>
        event SpiderHandler SpiderEvent;
        
        /// <summary>
        /// the url of the spider.
        /// </summary>
        string Url { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">The start url.</param>
        /// <param name="options"></param>
        void Init(string url, params string[] options);

        
        /// <summary>
        /// the work it was scheduled to do.
        /// </summary>
        void Work();
        
        /// <summary>
        /// finish the work.
        /// </summary>
        void Finish();
    }
}