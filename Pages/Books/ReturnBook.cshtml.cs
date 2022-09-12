using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Lib.Pages.Books;

namespace Lib.Pages.Books{
    public class ReturnBooks{
        public string id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string categories { get; set; }
        public string assignedTo { get; set; }
        public string updatedDate { get; set; }
    }
    public class ReturnBookModel : PageModel{
        public bool displayBool = true;
        public string errorMessage = "";
        public List<ReturnBooks> returnBooks = new List<ReturnBooks>();
        public string successMessage = "";
        public string userName = "";
        public void OnGet(){
            userName = HttpContext.Session.GetString("userName");
            try{
                    string connectionString = "Server=tcp:libcomakeit.database.windows.net,1433;Initial Catalog=libcmit;Persist Security Info=False;User ID=avi;Password=Earth@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                    using (SqlConnection conn = new SqlConnection(connectionString)){
                        conn.Open();
                        
                        string sql = "SELECT * FROM books where assignendTo = '"+userName +"'";
                        using(SqlCommand command = new SqlCommand(sql, conn)){
                            using(SqlDataReader reader = command.ExecuteReader()){
                                if(reader.HasRows){
                                    while(reader.Read()){
                                        ReturnBooks book = new ReturnBooks();

                                        book.id = "" + reader.GetInt32(0);
                                        book.title = reader.GetString(1);
                                        book.author = reader.GetString(2);
                                        book.categories = reader.GetString(4);
                                        book.assignedTo = reader.GetString(6);
                                        book.updatedDate = reader.GetDateTime(8).ToString();

                                        returnBooks.Add(book);
                                    }
                                }else{
                                    displayBool = false;
                                    errorMessage = "No books found for user " + userName;
                                }
                            }
                        }
                    }
                }
            catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }
  }
}