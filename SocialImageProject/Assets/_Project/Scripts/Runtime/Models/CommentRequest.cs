using System;

namespace Models
{
    [Serializable]
    public class CommentRequest
    {
        public string imageId;
        public string playerId;
        public string comment;
    }
}