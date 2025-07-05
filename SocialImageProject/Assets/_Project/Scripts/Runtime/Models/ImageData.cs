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
        public DateTime uploadDate;

        public ImageData()
        {
            uploadDate = DateTime.Now;
        }
    }
}