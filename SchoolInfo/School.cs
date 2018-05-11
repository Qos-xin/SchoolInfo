using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SchoolInfo
{
    public class School
    {
        public void GetMainContent()
        {
            HtmlAgilityPack.HtmlWeb htmlWeb = new HtmlAgilityPack.HtmlWeb();
            htmlWeb.OverrideEncoding = Encoding.GetEncoding("gb2312");
            HtmlDocument htmlDocument = htmlWeb.Load("https://www.daxuecn.com/info/jiangsu/");
            var root = htmlDocument.DocumentNode;
            var tableList = root.SelectNodes("//table[@class='v_box']//td/a");
            foreach (var item in tableList)
            {
                if (!string.IsNullOrWhiteSpace(item.InnerText))
                {
                    Console.Write(item.InnerText);
                    GetSchoolInfo(item.GetAttributeValue("href", ""));
                    Console.Write(Environment.NewLine);                    
                }
            }
        }
        public void GetSchoolInfo(string url)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                HtmlWeb htmlWeb = new HtmlWeb();
                htmlWeb.OverrideEncoding = Encoding.GetEncoding("gb2312");
                HtmlDocument htmlDocument = htmlWeb.Load(url);
                var root = htmlDocument.DocumentNode;
                var address = root.SelectSingleNode("//div[@class='right']//div[@class='r_box']/div");
                if (address.ChildNodes.Count > 2)
                    Console.Write("^" + address.ChildNodes[2].InnerText);
                else
                    Console.Write("^");
                if (address.ChildNodes.Count > 5)
                    Console.Write("^" + address.ChildNodes[5].InnerText);
                else
                    Console.Write("^");
                var info = root.SelectSingleNode("//div[@class='left']/div[@class='info cl']/div[9]");
                var text = info.InnerText;
                var match = Regex.Match(text, "\\d+[人余]{1}");
                if (match.Success)
                {
                    Console.Write("^" + match.Value);//3000人
                    Console.Write("^" + text.Substring(match.Index - 10, 20));//30000余人
                }
            }

        }
    }
}
