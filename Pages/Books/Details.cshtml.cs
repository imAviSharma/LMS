using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lib.Pages.Books{

  public class DetailsModel : PageModel{
    public BooksInfo booksInfo = new BooksInfo();
      public string role;

    public void OnGet(){
      string id = Request.Query["id"];
      try{
        role = HttpContext.Session.GetString("role");
        string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=library;Integrated Security=True";
        using( SqlConnection conn = new SqlConnection(connectionString)){
          conn.Open();
          string sql = "SELECT * FROM books WHERE BookID = @id";
          using(SqlCommand command = new SqlCommand(sql, conn)){
            command.Parameters.AddWithValue("@id", id);
            using(SqlDataReader reader = command.ExecuteReader()){
              while(reader.Read()){
                booksInfo.id = "" + reader.GetInt32(0);
                booksInfo.title = reader.GetString(1);
                booksInfo.author = reader.GetString(2);
                booksInfo.publisher = reader.GetString(3);
                booksInfo.categories = reader.GetString(4);
                booksInfo.prices = reader.GetInt32(5);
                booksInfo.assignedTo = reader.GetString(6);
                Console.WriteLine(reader.GetString(6));
              }
            }
          }
        }

      }catch(Exception ex){
        Console.WriteLine("Error: " + ex.Message);
      }
    }
  }

  public class BooksInfo{
    public string id { get; set; }
    public string title { get; set; }
    public string author { get; set; }
    public string publisher { get; set; }
    public string categories { get; set; }
    public int prices { get; set; }
    public string assignedTo { get; set; }
    public string returnBy { get; set; }
  }

}