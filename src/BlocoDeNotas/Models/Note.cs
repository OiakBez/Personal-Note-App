namespace Personal_Note_App.Models;
public class Note
{
    public int Id {get; set;}

    public string Title {get; set;} = string.Empty;
    public string ContentRtf {get; set;} = string.Empty;
    public DateTime CreatedAt {get; set;} = DateTime.Now;

    public DateTime UpdatedAt {get; set;} = DateTime.Now;
    public string Category {get; set;} = string.Empty;
    public bool IsFavourite {get; set;}
}