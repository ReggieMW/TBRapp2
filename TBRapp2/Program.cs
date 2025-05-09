using System.ComponentModel;

namespace TBRapp2;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        ListManager manager = new ListManager();
        bool running = true;
        Book readBook = null;

        while (running)
        {
            Console.WriteLine("Your TBR Pile");
            Console.WriteLine("1. Show Current TBR Pile");
            Console.WriteLine("2. Sort Pile");
            Console.WriteLine("3. Filter Pile");
            Console.WriteLine("4. Add a Book to the TBR Pile");
            Console.WriteLine("5. Mark a book as finished!");
            Console.WriteLine("6. See Read Books pile");
            Console.WriteLine("7. Exit");
            Console.WriteLine("Please select an option: ");

            var menuSelection = Console.ReadLine();

            switch (menuSelection)
            {
                case "1":
                    if (manager.TBR_Books.Count == 0)
                    {
                        Console.WriteLine("There are no books in your book pile yet.");
                    }
                    else
                    {
                        foreach (var book in manager.TBR_Books)
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
                            var authorAZ = manager.TBR_Books.OrderBy(b => GetLastName(b.Author)).ToList();
                            foreach (var book in authorAZ)
                            {
                                Console.WriteLine($"{book.Title} by {book.Author} ({book.Pages} pages)");   
                            }
                            break;
                        case "2":
                            var authorZA = manager.TBR_Books.OrderByDescending(b => GetLastName(b.Author)).ToList();
                            foreach (var book in authorZA)
                            {
                                Console.WriteLine($"{book.Title} by {book.Author} ({book.Pages} pages)");
                            }
                            break;
                        case "3":
                            var titleAZ = manager.TBR_Books.OrderBy(b => b.Title).ToList();
                            foreach (var book in titleAZ)
                            {
                                Console.WriteLine($"{book.Title} by {book.Author} ({book.Pages} pages)");
                            }
                            break;
                        case "4":
                            var titleZA = manager.TBR_Books.OrderByDescending(b => b.Title).ToList();
                            foreach (var book in titleZA)
                            {
                                Console.WriteLine($"{book.Title} by {book.Author} ({book.Pages} pages)");
                            }
                            break;
                        case "5":
                            var pagesAscending = manager.TBR_Books.OrderBy(b => (b.Pages));
                            foreach (var book in pagesAscending)
                            {
                                Console.WriteLine($"{book.Title} by {book.Author} ({book.Pages} pages)");
                            }
                            break;
                        case "6":
                            var pagesDescending = manager.TBR_Books.OrderByDescending(b => (b.Pages));
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
                    Console.WriteLine("\nEnter the book's Title: ");
                    string readTitle = Console.ReadLine();
                    readBook = manager.TBR_Books.FirstOrDefault(b => b.Title.Equals(readTitle, StringComparison.OrdinalIgnoreCase));
                    if (manager.TBR_Books.Contains(readBook))
                    {
                        if (readBook != null)
                        {
                            manager.AddReadBook(readBook);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nThis book is not in your TBR pile. Would you still like to add it to your read pile? yes/no");
                        {
                            var stillAdd = Console.ReadLine();
                            if (stillAdd.ToLower() == "yes" || stillAdd.ToLower() == "y")
                            {
                                readBook = manager.CreateBook();
                                manager.AddReadBook(readBook);
                            }
                            else
                            {
                                Console.WriteLine("No problem, back to the menu");
                                break; 
                            }
                        }
                    }
                    break;
                case "6":
                    if (manager.ReadBooks.Count == 0)
                    {
                        Console.WriteLine("There are no books in your book pile yet.");
                    }
                    else
                    {
                        foreach (var book in manager.ReadBooks)
                        {
                            Console.WriteLine($"- {book.Title} by {book.Author} {book.DateFinished}. {book.Vibe}. {book.Comparable}.");
                        }
                    } break;
                case "7":
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

            var options = manager.TBR_Books
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
                var filteredBooks = manager.TBR_Books
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