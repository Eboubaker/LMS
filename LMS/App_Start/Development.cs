using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataTables.AspNet.Mvc5;
using LMS.Models;
using LMS.Models;
using System.Data.Entity;
namespace LMS.App_Start
{
    public class Development
    {
        public static void RunTest()
        {
            Environment.Exit(0);
        }
        public static void DoWork()
        {
            dostuff();
            Environment.Exit(0);
        }
        private static void dostuff()
        {
            var context = new LibraryManagmentContext();
            var inventories = context.Inventories.Where(m => m.Size < 8);
            var r = new Random();
            foreach(var inventory in inventories)
            {
                inventory.Size = r.Next(8, 17);
            }
            context.SaveChanges();
        }
        //private static void dostuff()
        //{
        //    var lastNames = new List<string>(new string[] { "Abadi", "Abdallah", "Ahmad", "Ali", "Amin", "Asghar", "Ayad", "Aziz", "Badawi", "Baghdadi", "Bakir", "Bashar", "Bilal", "Burhan", "Darwish", "Dawoud", "Ebeid", "Fadel", "Faez", "Faheem", "Faizan", "Farhat", "Farouq", "Farsi", "Fasih", "Fasil", "Fayed", "Gaddafi", "Ghazali", "Ghazawwi", "Ghulam", "Habib", "Hadi", "Hadid", "Hafeez", "Hakim", "Hamdi", "Hariri", "Hashim", "Hasnawi", "Hatem", "Hijazi", "Hussein", "Ibrahim", "Iqbal", "Irfan", "Isa", "Ismat", "Issawi", "Jabal", "Jabir", "Jalal", "Jameel", "Jawahir", "Jaziri", "Kader", "Karim", "Kashif", "Kassab", "Kazem", "Khalid", "Laghmani", "Maalouf", "Maamoun", "Mohammed", "Mahmud", "Marwan", "Mufti", "Mughrabi", "Mustafa", "Nabih", "Nader", "Nagi", "Nahdi", "Najdi", "Najm", "Najjar", "Noor", "Osman", "Qadir", "Qasim", "Qureshi", "Rafiq", "Rahim", "Rajab", "Ramzan", "Ramzi", "Rashid", "Reza", "Sader", "Sajjad", "Shariq", "Saqqaf", "Sultan", "Taleb", "Tawfiq", "Wahed", "Yasin", "Yusuf", "Zaman" });
        //    var FirstNames = new List<string>(new string[] { "Aaban", "Aadil", "Aahil", "Aamir", "Aaqib", "Aaqil", "Aariz", "Aashir", "Aasim", "Abbas", "Abdul", "Abdullah", "Abdulraheem", "Adam", "Aden", "Adyan", "Ahmed", "Ales", "Ali", "Amaan", "Amari", "Amir", "Antwan", "Asif", "Aslam", "Atif", "Awadi", "Azees", "Bashir", "Caleb", "Chirag", "Dawood", "Dilshad", "Ebrahim", "Ehsan", "Faheem", "Fareed", "Farooq", "Gafar", "Ghani", "Ghufran", "Haafiz", "Haamid", "Haaroon", "Haider", "Hamza", "Hani", "Haroon", "Hasan", "Hussain", "Ibrahim", "Ijaz", "Imam", "Imran", "Imtiaz", "Intekhab", "Iqbal", "Ismael", "Jamal", "Jawad", "Kaden", "Kadin", "Kale", "Kamal", "Karim", "Kemo", "Khaled", "Khalil", "Madni", "Mahamat", "Makhi", "Malik", "Messiah", "Mohammad", "Moiz", "Naif", "Nash", "Nasser", "Omar", "Rashad", "Rizwan", "Saad", "Saif", "Samir", "Shahid", "Shams", "Shamz", "Taha", "Talal", "Tariq", "Tetsuo", "Wasi", "Yahir", "Yaser", "Youssof", "Yusuf", "Zaire", "Zakaria", "Zakir", "Zishan", "Aadila", "Aaeesha", "Aafia", "Aafreen", "Aaliyah", "Aameena", "Aamira", "Aanisah", "Aasma", "Abrar", "Aidah", "Aisha", "Alaa", "AlAnoud", "Aleah", "Aleeza", "Alesha", "Alina", "Alma", "Alsama", "Amani", "Amelia", "Amina", "Amira", "Anjum", "Ariana", "Asgari", "Azeema", "Aziza", "Balqees", "Banu", "Batool", "Bayan", "Beatrice", "Benazir", "Bushra", "Callie", "Chanda", "Chandini", "Daania", "Dilshad", "Eliza", "Farha", "Farida", "Faten", "Fatima", "Fauzia", "Fazeela", "Firdaus", "Firoza", "Gazala", "Ghada", "Ghayda", "Gudia", "Haafiza", "Haamida", "Habeeba", "Hadeel", "Hana", "Hasina", "Heena", "Imani", "Jaliyah", "Jazmin", "Jenna", "Kaleigh", "Kaley", "Kaliyah", "Kefaya", "Keyla", "Laila", "Lamia", "Layla", "Leila", "Lily", "Lulu", "Lydia", "Lyla", "Mariam", "Maritza", "Mina", "Nada", "Nadia", "Naima", "Nouf", "Noura", "Ola", "Raihana", "Rana", "Reem", "Reenad", "Sabrina", "Salma", "Samira", "Sanaa", "Sarah", "Wadha", "Yasmine", "Yesenia", "Zain" });
        //    FirstNames = FirstNames.OrderBy(m => Guid.NewGuid()).ToList();
        //    lastNames = lastNames.OrderBy(m => Guid.NewGuid()).ToList() ;
        //    var r = new Random();
        //    var context = new LibraryManagmentContext();
        //    foreach (var _ in Enumerable.Range(0, 200))
        //    {
        //        string code = "1818" + r.Next(1, 4).ToString() + r.Next(0, 8).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString() + r.Next(0, 9).ToString();
        //        var customer = new Customer()
        //        {
        //            Name = lastNames[r.Next(lastNames.Count)] + " " + FirstNames[r.Next(FirstNames.Count)],
        //            CardId = code,
        //            Birthdate = RandomDate()
        //        };
        //        context.Customers.Add(customer);
                
        //    }
        //    context.SaveChanges();
        //}

        //private static void dostuff()
        //{
        //    var context = new ApplicationDbContext();
        //    int row = 4,column = 5;
        //    int shelf = 0;
        //    Inventory inventory = new Inventory { Size = 0};
        //    int size = 0;
        //    var r = new Random((int)DateTime.Now.Ticks);
        //    foreach(var copy in context.BookCopies.ToList())
        //    {
        //        if(column == 5)
        //        {
        //            if(row == 4)
        //            {
        //                shelf++;
        //                row = 1;
        //            }
        //            else
        //            {
        //                row++;
        //            }
        //            column = 1;
        //        }
        //        if(inventory.Size == size)
        //        {
        //            int s = r.Next(r.Next(2, 5), r.Next(6, 10));
        //            size = 0;
        //            inventory = new Inventory()
        //            {
        //                Shelf = shelf,
        //                Column = column,
        //                Row = row,
        //                Size = s
        //            };
        //            context.Inventories.Add(inventory);
        //            context.SaveChanges();
        //            column++;
        //        }
        //        copy.Inventory = inventory;
        //        size++;
        //    }
        //    context.SaveChanges();
        //}

        //private static void dostuff()
        //{
        //    var context = new ApplicationDbContext();
        //    var codes = new HashSet<string>();
        //    foreach (var c in context.Classes.ToList())
        //        codes.Add(c.CodeName.TrimEnd('A', 'F'));
        //    foreach (var c in codes)
        //        context.Classes.Add(new Class() { CodeName = c, Name = "Unspecified" });
        //    context.SaveChanges();
        //    foreach (var book in context.Books.Include(c => c.Class).ToList())
        //    {
        //        if (book.Class.CodeName.EndsWith("A"))
        //            book.LanguageId = 1;
        //        else if (book.Class.CodeName.EndsWith("F"))
        //            book.LanguageId = 2;
        //        var v = book.Class.CodeName.TrimEnd('A', 'F');
        //        var v2 = context.Classes.SingleOrDefault(c => c.CodeName == v);
        //        book.Class = v2;
        //    }
        //    context.SaveChanges();
        //}

        //private static void dostuff()
        //{
        //    var context = new ApplicationDbContext();
        //    var books = context.Books.ToList();
        //    var classes = context.Classes.ToList();
        //    foreach (var book in books)
        //    {
        //        var @class = classes.Single(c => c.CodeName == book.Classs.ToUpper());
        //        book.Class = @class;
        //    }
        //    context.SaveChanges();
        //}

        //private static void seedClasses()
        //{
        //    var context = new ApplicationDbContext();
        //    var books = context.Books.ToList();
        //    var classes = new List<string>();
        //    foreach (var book in books)
        //    {
        //        var i = book.Classs.ToUpper();
        //        if (!classes.Contains(i))
        //            classes.Add(i);
        //    }
        //    foreach (var @class in classes){
        //        context.Classes.Add(new Class() {
        //            CodeName = @class,
        //            Name = "Unspecified"
        //        });
        //    }
        //    context.SaveChanges();
        //}

        //private static void reconfig()
        //{
        //    var context = new ApplicationDbContext();
        //    context.Books.RemoveRange(context.Books.ToList());
        //    context.BookCopies.RemoveRange(context.BookCopies.ToList());
        //    var oldbooks = context.OldBooks.ToList();
        //    System.Diagnostics.Debug.WriteLine(oldbooks.Count);
        //    var baseBooksIds = new Dictionary<string, int>();
        //    var count = 0;
        //    Book baseBook = null;
        //    foreach (var oldbook in oldbooks)
        //    {
        //        System.Diagnostics.Debug.WriteLine(count++);
        //        var bookCode = oldbook.Class + oldbook.ClassCode;
        //        if (!baseBooksIds.ContainsKey(bookCode))
        //        {
        //            baseBook = new Book()
        //            {
        //                Classs = oldbook.Class,
        //                ClassCode = oldbook.ClassCode,
        //                Authors = oldbook.Authors,
        //                DateAdded = oldbook.DateAdded,
        //                Isbn = oldbook.Isbn,
        //                Price = oldbook.Price,
        //                Publisher = oldbook.Publisher,
        //                ReleaseYear = oldbook.ReleaseYear,
        //                Source = oldbook.Source,
        //                Title = oldbook.Title
        //            };
        //            context.Books.Add(baseBook);
        //            context.SaveChanges();
        //            baseBooksIds.Add(bookCode, baseBook.Id);
        //        }
        //        if (bookCode != baseBook.Classs + baseBook.ClassCode)
        //        {
        //            context.SaveChanges();
        //            var id = baseBooksIds[bookCode];
        //            baseBook = context.Books.SingleOrDefault(c => c.Id == id);
        //        }
        //        context.BookCopies.Add(new BookCopy()
        //        {
        //            BaseBook = baseBook,
        //            InventoryColumn = oldbook.InventoryColumn,
        //            InventoryRow = oldbook.InventoryRow,
        //            Rented = false
        //        });
        //        baseBook.NumberInStock++;
        //        baseBook.NumberAvailable++;
        //    }
        //    context.SaveChanges();
        //}

        //private static void seed()
        //{
        //    var context = new ApplicationDbContext();
        //    context.Books.RemoveRange(context.Books.ToList());
        //    var inserted = 0;
        //    var errors = 0;
        //    var dublicates = 0;
        //    using(var log = new StreamWriter(WebLib.Utils.MapPath(@"~\log.txt")))
        //    using (var reader = new StreamReader(WebLib.Utils.MapPath(@"~\Seeds\library-books-2.csv")))
        //    {
        //        int lineNumber = 0;
        //        bool error = false;
        //        var line = "";
        //        var regex = new Regex("\"[^\"]+\"");
        //        var regex2 = new Regex(@"[\s]");
        //        var values = new List<string>();
        //        var basebooks = new HashSet<string>();
        //        var book = new BaseBook();
        //        var ids = new HashSet<string>(30000);
        //        reader.ReadLine();
        //        while (!reader.EndOfStream)
        //        {
        //            if (error)
        //            {
        //                error = false;
        //                errors++;
        //                log.WriteLine(String.Format("Line {0} | {1}", lineNumber, line));
        //            }
        //            System.Diagnostics.Debug.WriteLine(inserted);
        //            lineNumber++;
        //            line = reader.ReadLine().Trim();
        //            foreach (Match match in regex.Matches(line))
        //            {
        //                line = line.Replace(match.Value, match.Value.Replace(",", "|:|").Replace("\"", ""));
        //            }
        //            if (line.Length == 0)
        //                continue;
        //            values = line.Split(',').ToList();
        //            if (values.Count < 3)
        //                continue;
        //            values = values.Select(e => e.Replace("|:|", ",").Trim()).ToList();
        //            while(regex2.Match(values[1]).Success)
        //            {
        //                var index = regex2.Match(values[1]).Index;
        //                if (values[1].Length != index-1 && Char.IsDigit(values[1][index+1]) && Char.IsLetter(values[1][index - 1]))
        //                {
        //                    values[1] = values[1].Substring(0, index) + "/" + values[1].Substring(index + 1);
        //                    break;
        //                }
        //                values[1] = values[1].Remove(index, 1);

        //            }
        //            values[1] = values[1].Replace(" ", "");
        //            if (values[1].Count(i => i == '/') == 1 && Char.IsDigit(values[1][values[1].IndexOf('/') - 1]))
        //            {
        //                var index = new Regex(@"[\d]").Match(values[1]).Index;
        //                values[1] = values[1].Substring(0, index) + "/" + values[1].Substring(index);
        //            }
        //            if (ids.Contains(values[1]))
        //            {
        //                dublicates++;
        //                continue;
        //            }
        //            else
        //                ids.Add(values[1]);

        //            if (values[1].Count(i => i == '/') < 2 || values[2].Length < 1)
        //            {
        //                error = true;
        //                continue;
        //            }
        //            basebooks.Add(values[1].Substring(0, values[1].IndexOf('/', values[1].IndexOf('/') + 1)));
        //            try
        //            {
        //                book = new BaseBook()
        //                {
        //                    Title = values[2],
        //                    Class = values[1].Split('/')[0],
        //                    ClassCode = Convert.ToInt32(values[1].Split('/')[1]),
        //                    Copy = Convert.ToInt32(values[1].Split('/')[2]),
        //                    DateAdded = DateTime.Now
        //                };
        //                try
        //                {
        //                    book.InventoryColumn = Convert.ToInt32(values[0].Split('/')[0]);
        //                    book.InventoryRow = Convert.ToInt32(values[0].Split('/')[1]);
        //                }
        //                catch
        //                {
        //                    try
        //                    {
        //                        book.InventoryColumn = Convert.ToInt32(values[0]);
        //                        book.InventoryRow = -1;
        //                    }
        //                    catch
        //                    {
        //                        book.InventoryColumn = -1;
        //                        book.InventoryRow = -1;
        //                    }
        //                }
        //                if (values.Count > 3 && values[3].Length != 0)
        //                    book.Authors = values[3];
        //                if (values.Count > 4 && values[4].Length != 0)
        //                    book.Publisher = values[4];
        //                if (values.Count > 5 && values[5].Length == 4)
        //                    book.ReleaseYear = (short)Convert.ToInt32(values[5]);
        //                if (values.Count > 6 && values[6].Length > 4)
        //                    book.Isbn = values[6].Replace("-", "").Replace(",", "").Replace(" ", "").Trim();
        //                if (values.Count > 7 && values[7].Length > 1)
        //                    book.Price = values[7];
        //                if (values.Count > 8 && values[8].Length > 1)
        //                    book.Source = values[8];
        //            }
        //            catch
        //            {
        //                error = true;
        //                continue;
        //            }
        //            context.Books.Add(book);
        //            inserted++;

        //        }
        //        context.SaveChanges();
        //        log.WriteLine("Inserted {0} Unique {3} Dublicates {1} Errors {2}", inserted, dublicates, errors, basebooks.Count);
        //    }
        //}
        // "id": 1,
        //"book_id": 2767052,
        //"best_book_id": 2767052,
        //"work_id": 2792775,
        //"books_count": 272,
        //"isbn": "439023483",
        //"isbn13": 9780439023480,
        //"authors": "Suzanne Collins",
        //"original_publication_year": 2008,
        //"original_title": "The Hunger Games",
        //"title": "The Hunger Games (The Hunger Games, #1)",
        //"language_code": "eng",
        //"average_rating": 4.34,
        //"ratings_count": 4780653,
        //"work_ratings_count": 4942365,
        //"work_text_reviews_count": 155254,
        //"ratings_1": 66715,
        //"ratings_2": 127936,
        //"ratings_3": 560092,
        //"ratings_4": 1481305,
        //"ratings_5": 2706317,
        //"image_url": "https://images.gr-assets.com/books/1447303603m/2767052.jpg",
        //"small_image_url": "https://images.gr-assets.com/books/1447303603s/2767052.jpg"
    }
}