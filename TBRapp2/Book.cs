namespace TBRapp2;

public class Book
{
    //properties that can be sorted
    public string Title { get; set; }
    public string Author { get; set; }
    public int Pages { get; set; }
    public int YearPublished { get; set; }
    
    //properties that can be filtered
    public string Country { get; set; }
    public string Subject { get; set; }
    public string Vibe { get; set; }
    public string Source { get; set; }
    
    //new properties once a book has been read by the user
    public bool IsRead { get; set; }
    
    public DateOnly DateFinished { get; set; }

    public string Comparable { get; set; }
    public bool CanonBook { get; set; }
    
    
}