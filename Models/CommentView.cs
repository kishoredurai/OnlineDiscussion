using OnlineDiscussion.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace OnlineDiscussion.Models
{
    public class CommentView
    {
        [Key]
        public int CommentId { get; set; }
        public string CommentText { get; set; }

        public DateTime posteddate { get; set; }

        public TopicViewModel topic { get; set; }

        public OnlineDiscussionUser user { get; set; }
    }
}
