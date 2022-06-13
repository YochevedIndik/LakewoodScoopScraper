using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LakewoodScoopScraper.Scraping
{
    public static class LakewoodScoopScrape
    {
        public static List<Article> Scrape()
        {
            var html = GetLakewoodscoopHtml();
            return ParseLakewoodScoopHtml(html);
        }

        private static List<Article> ParseLakewoodScoopHtml(string html)
        {
            var parser = new HtmlParser();
            var document = parser.ParseDocument(html);
            var resultDivs = document.QuerySelectorAll(".post");
            var items = new List<Article>();
            foreach (var div in resultDivs)
            {
                var item = new Article();
                var titleSpan = div.QuerySelector("h2");
                if (titleSpan == null)
                {
                    continue;
                }
                if (titleSpan != null)
                {
                    item.Title = titleSpan.TextContent;
                }



                var imageTag = div.QuerySelector(".aligncenter.size-large");
                if (imageTag != null)
                {
                    item.ImageUrl = imageTag.Attributes["src"].Value;
                }


                var linkTag = div.QuerySelector("a");
                if (linkTag != null)
                {
                    item.Link = $"{linkTag.Attributes["href"].Value}";
                }
                var textSpan = div.QuerySelector("p");
                if(textSpan != null)
                {
                    item.Text = textSpan.TextContent;
                }
                var commentAmountSpan = div.QuerySelector(".backtotop");
                {
                    if (commentAmountSpan != null)
                    {
                        item.AmountOfComments = commentAmountSpan.TextContent;
                    }
                }

                items.Add(item);
            }

            return items;
        }

        private static string GetLakewoodscoopHtml()
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            };
            using var client = new HttpClient(handler);
            var url = "https://www.thelakewoodscoop.com/";
            var html = client.GetStringAsync(url).Result;
            return html;
        }
    }
}
