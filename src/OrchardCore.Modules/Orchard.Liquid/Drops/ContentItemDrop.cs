using System;
using DotLiquid;
using Orchard.ContentManagement;
using Newtonsoft.Json.Linq;

namespace Orchard.Liquid.Drops
{
    public class ContentItemDrop : Drop
    {
        private ContentItem _contentItem;

        public ContentItemDrop(ContentItem contentItem)
        {
            _contentItem = contentItem;
        }

        public int Id => _contentItem.Id;
        public string ContentItemId => _contentItem.ContentItemId;
        public int Number => _contentItem.Number;
        public string Owner => _contentItem.Owner;
        public string Author => _contentItem.Author;
        public bool Published => _contentItem.Published;
        public bool Latest => _contentItem.Latest;
        public string ContentType => _contentItem.ContentType;
        public DateTime? CreatedUtc => _contentItem.CreatedUtc;
        public DateTime? ModifiedUtc => _contentItem.ModifiedUtc;
        public DateTime? PublishedUtc => _contentItem.PublishedUtc;
        public JTokenDrop Content => new JTokenDrop((JToken)_contentItem.Content);

    }
}
