namespace WebsiteTinhThanFoundation.ViewModels
{
    public class DashboardView
    {
        public int BlogCount {get; set;}
        public int NumberVolunteer {get; set;}
        public int TotalViewBlog {get; set;}
        public int NumberContact {get; set;}
        public List<int> NumberContactMonthly {get; set;} = new();
        public List<BlogCommentView> CommentViews {get; set;}
    }
}