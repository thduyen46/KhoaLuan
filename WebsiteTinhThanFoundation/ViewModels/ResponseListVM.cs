namespace WebsiteTinhThanFoundation.ViewModels
{
    public class ResponseListVM<T>
    {
        public List<T>? ObjectListData { get; set; }
        public T? ObjectData { get; set; }
        public int MaxPage { get; set; }
    }
}
