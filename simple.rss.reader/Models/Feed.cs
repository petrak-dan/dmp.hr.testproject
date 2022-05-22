using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace simple.rss.reader.Models
{
    public class Feed
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        [Required]
        [DisplayName("URL")]
        [Url]
        public string Url { get; set; } = "";

        [Required]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        public string Title { get; set; } = "";

        public string Link { get; set; } = "";
        
        public string Description { get; set; } = "";

        public DateTime LastUpdatedDateTime { get; set; }

        public DateTime PublishedDateTime { get; set; }

        public List<FeedItem> Items { get; set; } = new();

        public DateTime EarliestArticleDate
        {
            get
            {
                if (Items != null && Items.Count > 0)
                {
                    return Items.Select(x => x.PublishedDateTime).Min();
                }
                else
                {
                    return DateTime.Now;
                }
            }
        }

        public DateTime LatestArticleDate
        {
            get
            {
                if (Items != null && Items.Count > 0)
                {
                    return Items.Select(x => x.PublishedDateTime).Max();
                }
                else
                {
                    return DateTime.Now;
                }
            }
        }

        public void Reload()
        {
            Items.Clear();
            XmlDocument xml = new();
            xml.Load(Url);

            Title = GetNodeInnerText(ref xml, "rss/channel/title");
            Link = GetNodeInnerText(ref xml, "rss/channel/link");
            Description = GetNodeInnerText(ref xml, "rss/channel/description");
            
            string pubDate = GetNodeInnerText(ref xml, "rss/channel/pubDate");
            PublishedDateTime = DateTime.TryParse(pubDate, out DateTime feedDt) ? feedDt : DateTime.Now;

            XmlNodeList nodes = xml.SelectNodes("rss/channel/item")!;

            foreach (XmlNode node in nodes)
            {
                FeedItem feedItem = new()
                {
                    Title = GetNodeInnerText(node.SelectSingleNode("title")),
                    Link = GetNodeInnerText(node.SelectSingleNode("link")),
                    Description = GetNodeInnerText(node.SelectSingleNode("description"))
                };
                string itemDate = GetNodeInnerText(node.SelectSingleNode("pubDate"));
                feedItem.PublishedDateTime = DateTime.TryParse(itemDate, out DateTime itemDt) ? itemDt : DateTime.Now;
                Items.Add(feedItem);
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                Name = !string.IsNullOrWhiteSpace(Title) ? Title : Url;
            }
            LastUpdatedDateTime = DateTime.Now;
        }

        private static string GetNodeInnerText(ref XmlDocument xml, string xPath)
        {
            XmlNode? node = xml.SelectSingleNode(xPath);
            //return node != null ? node.InnerText : "";
            return GetNodeInnerText(node);
        }

        private static string GetNodeInnerText(XmlNode? node)
        {
            return node != null ? node.InnerText : "";
        }
    }

    public class FeedItem
    {
        [Key]
        public int Id { get; set; }

        public int FeedId { get; set; }

        public string? Title { get; set; }
        
        public string? Link { get; set; }

        public string? Description { get; set; }

        public DateTime PublishedDateTime { get; set; }
    }

    public class DateFilter
    {
        [Key]
        public int Id { get; set; }

        public DateTime From { get; set; } = DateTime.MinValue;

        public DateTime To { get; set; } = DateTime.Now;
    }

    public class DataModel
    {
        public List<Feed>? Feeds { get; set; }
        
        public List<FeedItem>? Items { get; set; }

        public List<DateFilter>? DateConfig { get; set; }

        public DateTime DateFrom { get; set; } = DateTime.MinValue;
        /* tmp test
        {
            get
            {
                return (DateConfig?.Count > 0) ? DateConfig.Last().From : DateTime.MinValue;
            }
            set
            {
                if (DateConfig?.Count > 0)
                {
                    DateConfig.Last().From = DateFrom;
                }
            }
        }*/

        public DateTime DateTo { get; set; } = DateTime.Now;
        /* tmp test
        {
            get
            {
                return (DateConfig?.Count > 0) ? DateConfig.Last().To : DateTime.Now;
            }
            set
            {
                if (DateConfig?.Count > 0)
                {
                    DateConfig.Last().To = DateTo;
                }
            }
        }*/

        public DateTime EarliestArticleDate
        {
            get
            {
                return (Feeds?.Count > 0) ? Feeds.Select(x => x.EarliestArticleDate).Min() : DateTime.Now;
            }
        }

        public DateTime LatestArticleDate
        {
            get
            {
                return (Feeds?.Count > 0) ? Feeds.Select(x => x.LatestArticleDate).Max() : DateTime.Now;
            }
        }
    }
}
