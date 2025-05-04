namespace TBRapp2;

class Program
{
    static void Main(string[] args)
    {
        ListManager manager = new ListManager();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Your TBR Pile");
            Console.WriteLine("1. Show Current Pile");
            Console.WriteLine("2. Sort Pile");
            Console.WriteLine("3. Filter Pile");
            Console.WriteLine("4. Add a Book to the Pile");
            Console.WriteLine("5. Exit");
            Console.WriteLine("Please select an option: ");

            var menuSelection = Console.ReadLine();

            switch (menuSelection)
            {
                case "1":
                    if (manager.Books.Count == 0)
                    {
                        Console.WriteLine("There are no books in your book pile yet.");
                    }
                    else
                    {
                        foreach (var book in manager.Books)
                        {
                            Console.WriteLine($"- {book.Title} by {book.Author} ({book.Pages} pages)");
                        }
                    } break;

                case "2":
                    Console.WriteLine("How would you like to sort your TBR pile?");
                    Console.WriteLine("1. Author (Last Name) A-Z:");
                    Console.WriteLine("2. Author (Last Name) Z-A:");
                    Console.WriteLine("3. Title A-Z:");
                    Console.WriteLine("4. Title Z-A:");
                    Console.WriteLine("5. Pages- Shortest-Longest:");
                    Console.WriteLine("6. Pages- Longest-Shortest:");
                    Console.WriteLine("7. Go Back");
                    
                    var sortSelection = Console.ReadLine();

                    switch (sortSelection)
                    {
                        case "1":
                            var authorAZ = manager.Books.OrderBy(b => GetLastName(b.Author)).ToList();
                            foreach (var book in authorAZ)
                            {
                                Console.WriteLine($"{book.Title} by {book.Author} ({book.Pages} pages)");   
                            }
                            break;
                        case "2":
                            var authorZA = manager.Books.OrderByDescending(b => GetLastName(b.Author)).ToList();
                            foreach (var book in authorZA)
                            {
                                Console.WriteLine($"{book.Title} by {book.Author} ({book.Pages} pages)");
                            }
                            break;
                        case "3":
                            var titleAZ = manager.Books.OrderBy(b => b.Title).ToList();
                            foreach (var book in titleAZ)
                            {
                                Console.WriteLine($"{book.Title} by {book.Author} ({book.Pages} pages)");
                            }
                            break;
                        case "4":
                            var titleZA = manager.Books.OrderByDescending(b => b.Title).ToList();
                            foreach (var book in titleZA)
                            {
                                Console.WriteLine($"{book.Title} by {book.Author} ({book.Pages} pages)");
                            }
                            break;
                        case "5":
                            var pagesAscending = manager.Books.OrderBy(b => (b.Pages));
                            foreach (var book in pagesAscending)
                            {
                                Console.WriteLine($"{book.Title} by {book.Author} ({book.Pages} pages)");
                            }
                            break;
                        case "6":
                            var pagesDescending = manager.Books.OrderByDescending(b => (b.Pages));
                            foreach (var book in pagesDescending)
                            {
                                Console.WriteLine($"{book.Title} by {book.Author} ({book.Pages} pages)");
                            }
                            break;
                        case "7":
                            break;
                        default:
                            break;
                    }
                    
                    break;
                case "3":
                    FilterBooks(manager);
                    break;
                case "4":
                    Book newBook = manager.CreateBook();
                    manager.AddBook(newBook);
                    Console.WriteLine($"\nAdded \"{newBook.Title}\" to your TBR Pile!");
                    break;
                case "5":
                    Console.WriteLine("Thank you for using the TBR List!");
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid selection, please try again!");
                    break;
                    
            }
        }

        static string GetLastName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) return "";
            var parts = fullName.Trim().Split(' ');
            return parts.Length > 1 ? parts[^1] : parts[0];
        }

        static void FilterBooks(ListManager manager)
        {
            Console.WriteLine("\nHow would you like to filter your pile?");
            Console.WriteLine("1. By Subject");
            Console.WriteLine("2. By Vibe");
            Console.WriteLine("3. By Country");
            Console.WriteLine("4. By How I learned about them");
            
            string filterSelection = Console.ReadLine();
            Func<Book, string> propertySelector = null;
            string filterLabel = "";

            switch (filterSelection)
            {
                case "1":
                    propertySelector = book => book.Subject;
                    filterLabel = "Subject";
                    break;
                case "2":
                    propertySelector = book => book.Vibe;
                    filterLabel = "Vibe";
                    break;
                case "3":
                    propertySelector = book => book.Country;
                    filterLabel = "Country";
                    break;
                case "4":
                    propertySelector = book => book.Source;
                    filterLabel = "Source";
                    break;
                default:
                    Console.WriteLine("Invalid selection, please select again.");
                    return;
            }

            var options = manager.Books
                .Select(propertySelector)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(x => x)
                .ToList();

            if (options.Count == 0)
            {
                Console.WriteLine($"There are no books in your pile with that {filterLabel}.");
                return;
            }
            
            Console.WriteLine($"\nYour current {filterLabel}s:");
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }
            
            Console.WriteLine($"Choose a number to see your {filterLabel} pile:");
            if (int.TryParse(Console.ReadLine(), out int selected) && selected >= 1 && selected <= options.Count)
            {
                string selectedOption = options[selected - 1];
                var filteredBooks = manager.Books
                    .Where(b => propertySelector(b).Equals(selectedOption, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                Console.WriteLine($"\n{selectedOption} pile:");
                foreach (var book in filteredBooks)
                {
                    Console.WriteLine($"{book.Title} by {book.Author} ({book.Pages} pages)");
                }
            }
            else
            {
                Console.WriteLine("Invalid selection, please select again.");
            }
        }
    }
}