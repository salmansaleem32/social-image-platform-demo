using System;
using System.Collections.Generic;

namespace Models
{
    [Serializable]
    public class ImageData
    {
        public string imageId;
        public string imageUrl;
        public string title;
        public int voteCount;
        public List<string> comments;
        public DateTime uploadDate;

        public ImageData()
        {
            comments = new List<string>();
            uploadDate = DateTime.Now;
        }
    }
}