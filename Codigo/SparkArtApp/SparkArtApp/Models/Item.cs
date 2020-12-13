using System;

namespace SparkArtApp.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public Item()
        {
            this.Id = Guid.NewGuid();
        }
    }
}