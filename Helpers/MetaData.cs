namespace TournamentManagementSystem.Helpers
{
    public class MetaData
    {
        public int TotalCount {  get; set; }//broj item-a
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount/(double)PageSize);
        //broj strana koje user dobija  
        public bool HasNext => CurrentPage < TotalPages;
        public bool HasPrevious => CurrentPage > 1;
    }
}
