using System.Security.AccessControl;

namespace TBRapp2;

public class ListManager
{
    public ListManager()
    {
#if DEBUG
        TBR_Books.AddRange(new List<Book>
        {
            new Book
            {
                Title = "Test Book 1", Author = "Author A", YearPublished = 2001, Pages = 320, Country = "USA",
                Subject = "Love", Vibe = "Hopeful", Source = "BookTok"
            },
            new Book
            {
                Title = "Test Book 2", Author = "Author B", YearPublished = 2015, Pages = 450, Country = "UK",
                Subject = "History", Vibe = "Thoughtful", Source = "Review"
            },
            new Book
            {
                Title = "Test Book 3", Author = "Author C", YearPublished = 2020, Pages = 220, Country = "Canada",
                Subject = "Murder", Vibe = "Scary", Source = "Recommended"
            },
            new Book
            {
                Title = "The Cosmic Drift", Author = "Ada Holt", YearPublished = 1998, Pages = 310,
                Country = "USA", Subject = "Outer Space", Vibe = "Thoughtful", Source = "Cool Cover"
            },
            new Book
            {
                Title = "Beneath the Weeping Willow", Author = "John Fair", YearPublished = 2007, Pages = 275,
                Country = "Canada", Subject = "Love", Vibe = "Sad", Source = "Review"
            },
            new Book
            {
                Title = "March of the Machines", Author = "Lee Zhang", YearPublished = 2021, Pages = 410,
                Country = "China", Subject = "Life", Vibe = "Scary", Source = "BookTok"
            },
            new Book
            {
                Title = "Kingdoms & Ruin", Author = "Elena Drake", YearPublished = 2013, Pages = 500, Country = "UK",
                Subject = "Kings & Queens & Realms", Vibe = "Crazy", Source = "Love the Genre"
            },
            new Book
            {
                Title = "The Final Seed", Author = "Sami Rivera", YearPublished = 2018, Pages = 330, Country = "Spain",
                Subject = "Life", Vibe = "Hopeful", Source = "Recommended"
            },
            new Book
            {
                Title = "Murder at Morning", Author = "Clive Winters", YearPublished = 1995, Pages = 290,
                Country = "Australia", Subject = "Murder", Vibe = "Scary", Source = "Back Cover Description"
            },
            new Book
            {
                Title = "Solar Soup", Author = "Jess Moon", YearPublished = 2022, Pages = 215, Country = "USA",
                Subject = "Outer Space", Vibe = "Funny", Source = "Cool Title"
            },
            new Book
            {
                Title = "Letters to the Past", Author = "Mina Patel", YearPublished = 2004, Pages = 360,
                Country = "India", Subject = "History", Vibe = "Thoughtful", Source = "Review"
            },
            new Book
            {
                Title = "The Hope Engine", Author = "Devon Hale", YearPublished = 2017, Pages = 275,
                Country = "New Zealand", Subject = "Life", Vibe = "Hopeful", Source = "Love the Author"
            },
            new Book
            {
                Title = "Rogue Justice", Author = "Nina Delgado", YearPublished = 2010, Pages = 440, Country = "Mexico",
                Subject = "Murder", Vibe = "Crazy", Source = "Author Blurb"
            },
            new Book
            {
                Title = "Chasing Orbits", Author = "Derek Stone", YearPublished = 2023, Pages = 380, Country = "USA",
                Subject = "Outer Space", Vibe = "Hopeful", Source = "Bookstagram"
            },
            new Book
            {
                Title = "Love in the Ashes", Author = "Sasha Greene", YearPublished = 2002, Pages = 325,
                Country = "Ireland", Subject = "Love", Vibe = "Sad", Source = "Book Club Book"
            },
            new Book
            {
                Title = "Court of Fire", Author = "Tamara Bell", YearPublished = 2019, Pages = 495,
                Country = "Scotland", Subject = "Kings & Queens & Realms", Vibe = "Scary", Source = "Cool Cover"
            },
            new Book
            {
                Title = "The Dead Speak Loudest", Author = "Felix Moore", YearPublished = 2011, Pages = 370,
                Country = "USA", Subject = "Murder", Vibe = "Scary", Source = "Review"
            },
            new Book
            {
                Title = "Bright New Tomorrows", Author = "Lynn Frost", YearPublished = 2020, Pages = 300,
                Country = "Canada", Subject = "Life", Vibe = "Hopeful", Source = "Recommended"
            },
            new Book
            {
                Title = "Planetfall", Author = "Sam Ibarra", YearPublished = 2009, Pages = 415, Country = "USA",
                Subject = "Outer Space", Vibe = "Thoughtful", Source = "Love the Genre"
            },
            new Book
            {
                Title = "A Study in Crows", Author = "Harriet Grimm", YearPublished = 2006, Pages = 285,
                Country = "Germany", Subject = "Murder", Vibe = "Crazy", Source = "Back Cover Description"
            },
            new Book
            {
                Title = "Broken Atlas", Author = "Theo Wright", YearPublished = 2016, Pages = 450,
                Country = "South Africa", Subject = "History", Vibe = "Sad", Source = "Cool Title"
            },
            new Book
            {
                Title = "Laughing Into Space", Author = "Penny Wilde", YearPublished = 2024, Pages = 260,
                Country = "USA", Subject = "Outer Space", Vibe = "Funny", Source = "BookTok"
            },
            new Book
            {
                Title = "A Queen’s Burden", Author = "Rhea Dorne", YearPublished = 2012, Pages = 420,
                Country = "France", Subject = "Kings & Queens & Realms", Vibe = "Thoughtful", Source = "Review"
            }
        });
#endif
    }

    public List<Book> TBR_Books { get; set; } = new List<Book>();
    public List<Book> ReadBooks { get; set; } = new List<Book>();

    private Dictionary<string, int>
        subjectCounts =
            new Dictionary<string, int>(StringComparer
                .OrdinalIgnoreCase); //tracks user entries and accounts for similar entries (i.e. "Funny" & "funny") as one entity

    private Dictionary<string, int> vibeCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
    private Dictionary<string, int> discoverCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

    //initial suggestions; will gradually be replaced by user input

    private readonly List<string> defaultSubjects = new List<string>
        { "Love", "Life", "Outer Space", "Kings & Queens & Realms", "Murder" };

    private readonly List<string> defaultVibes = new List<string> { "Sad", "Scary", "Hopeful", "Funny", "Thoughtful" };

    private readonly List<string> defaultDiscovered = new List<string>
        { "Recommended", "BookTok", "Cool Cover", "Review", "Love the Author", "Book Club Book" };

    public void AddBook(Book book)
    {
        TBR_Books.Add(book);
        IncrementCount(subjectCounts, book.Subject);
        IncrementCount(vibeCounts, book.Vibe);
    }

    public Book CreateBook()
    {
        Console.WriteLine("\nEnter the book's Title: ");
        string title = Console.ReadLine();

        Console.WriteLine("Enter the book's Author (full name): ");
        string author = Console.ReadLine();

        Console.WriteLine("Enter the year it was Published: ");
        int year = ReadInt();

        Console.WriteLine("Enter the number of Pages: ");
        int pages = ReadInt();

        Console.WriteLine("Enter the Country of Origin: ");
        string country = Console.ReadLine();

        //method allows user to select an option from the default/their history or add a new one
        string subject = SelectOrAdd("Subject", subjectCounts, defaultSubjects);
        string vibe = SelectOrAdd("Vibe", vibeCounts, defaultVibes);
        string discovered = SelectOrAdd("How did you learn about this book?", discoverCounts, defaultDiscovered);

        return new Book
        {
            Title = title,
            Author = author,
            YearPublished = year,
            Pages = pages,
            Country = country,
            Subject = subject,
            Vibe = vibe,
            Source = discovered
        };
    }

    public void AddReadBook(Book book) //meant to take book from TBR_Pile to ReadPile, provide date, let the user change
        //vibe if they feel differently after reading the book, and provide a recommendation for other readers
    {
        do
        {
            if (!string.IsNullOrWhiteSpace(book.Title))
            {
                book.Title = book.Title;
            }
            else 
            {
                Console.WriteLine("Title cannot be empty, try again.");
            }
        } while (book.Title == null);

        book.IsRead = true;
        book.DateFinished = DateOnly.FromDateTime(DateTime.Now);
        ReadBooks.Add(book);

        if (TBR_Books.Contains(book))
        {
            TBR_Books.Remove(book);
        }

        Console.WriteLine("\nDo you want to update the vibe? yes/no");
        var vibeUpdate = Console.ReadLine();
        if (vibeUpdate.ToLower() == "yes" || vibeUpdate.ToLower() == "y")
        {
            Console.WriteLine("\nEnter the book's Vibe: ");
            book.Vibe = Console.ReadLine();
        }

        Console.WriteLine("Did you like this book enough to recommend it? yes/no");
        var recommend = Console.ReadLine();
        if (recommend.ToLower() == "yes" || recommend.ToLower() == "y")
        {
            Console.WriteLine("\nName an author or book that you would use as a recommendation for this book: ");
            var comparable = Console.ReadLine();
            book.Comparable = $"Would recommend this book if you like {comparable}";
        }
        else
        {
            book.Comparable = "I would not recommend this book";
        }
    }

    //this method checks to see if a user addition already exists and either increases the count or adds the input
    private void IncrementCount(Dictionary<string, int> dict, string key)
    {
        if (dict.ContainsKey(key))
            dict[key]++;
        else
            dict[key] = 1;
    }

    private string SelectOrAdd(string label, Dictionary<string, int> counts, List<string> defaults)
    {
        Console.WriteLine($"\nChoose a {label} or type your own: ");

        var suggestions = GetTopSuggestions(counts, defaults, 5);
        for (int i = 0; i < suggestions.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {suggestions[i]}");
        }

        Console.Write("Enter number or your own entry: ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int choice) && choice > 1 && choice < suggestions.Count)
        {
            return suggestions[choice - 1];
        }

        return input.Trim();
    }

    private List<string> GetTopSuggestions(Dictionary<string, int> counts, List<string> defaults, int max)
    {
        var top = counts
            .OrderByDescending(kvp => kvp.Value)
            .Select(kvp => kvp.Key)
            .ToList();

        foreach (var item in defaults)
        {
            if (!top.Contains(item, StringComparer.OrdinalIgnoreCase))
                top.Add(item);
        }

        return top.Take(max).ToList();
    }

    private static int ReadInt()
    {
        int value;
        while (!int.TryParse(Console.ReadLine(), out value) || value < 0)
        {
            Console.WriteLine("Please enter a valid positive number:");
        }

        return value;
    }

    private static string SelectFromList(List<string> options)
    {
        for (int i = 0; i < options.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }

        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > options.Count)
        {
            Console.WriteLine("Please select a valid option:");
        }

        return options[choice - 1];
    }
}