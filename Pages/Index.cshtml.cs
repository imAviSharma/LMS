using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


// TODO: ADMIN, BOOKS, 
// Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;
namespace Lib.Pages{
    public class IndexModel : PageModel
    {
        public List<BooksInfo> booksInfo = new List<BooksInfo>();
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(){
            try{
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=library;Integrated Security=True";
                // string connectionString = "server=localhost;Database=library; Trusted_Connection=True";
                using (SqlConnection conn = new SqlConnection(connectionString)){
                    conn.Open();
                    string sql = "SELECT * FROM books";
                    using(SqlCommand command = new SqlCommand(sql, conn)){
                        using(SqlDataReader reader = command.ExecuteReader()){
                            while(reader.Read()){
                                BooksInfo book = new BooksInfo();

                                book.id = "" + reader.GetInt32(0);
                                book.title = reader.GetString(1);
                                book.author = reader.GetString(2);
                                book.categories = reader.GetString(4);
                                book.assignedTo = reader.GetString(6).ToString();

                                booksInfo.Add(book);
                            }
                        }
                    }
                }
            }
            catch(Exception ex){
                _logger.LogError(ex, "Error");
                Console.WriteLine(ex.Message);
            }
        }

        public class BooksInfo{
            public string id { get; set; }
            public string title { get; set; }
            public string author { get; set; }
            public string categories { get; set; }
            public string assignedTo { get; set; }
        }
    }
}