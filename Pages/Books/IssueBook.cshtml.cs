using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Lib.Pages.Books;

namespace Lib.Pages.Books{

  public class IssueBookModel : PageModel{
    public BooksInfo booksInfo = new BooksInfo();
    public string successMessage = "";
    public string errorMessage = "";
    public void OnGet(){

    }

    public void OnPost(){
      string id = Request.Form["id"];
      string assignedTo = HttpContext.Session.GetString("userName");
      try{
        string connectionString = "Server=tcp:libcomakeit.database.windows.net,1433;Initial Catalog=libcmit;Persist Security Info=False;User ID=avi;Password=Earth@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        using (SqlConnection conn = new SqlConnection(connectionString)){
          conn.Open();
          string query = "SELECT assignendTo FROM Books WHERE BookID = '"+id+"'";
          using (SqlCommand cmd = new SqlCommand(query, conn)){
            using(SqlDataReader reader = cmd.ExecuteReader()){
              while(reader.Read()){
                string content = reader.GetString(0);
                if(content != "NULL"){
                  throw new Exception("Book is assigned to " + reader.GetString(0));
                }
              }
            }
          }
          string sql = "UPDATE Books SET assignendTo = @assignedTo WHERE bookID = @id";
          using(SqlCommand command = new SqlCommand(sql, conn)){
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@assignedTo", assignedTo);
            command.ExecuteNonQuery();
            successMessage = "Book issued successfully";
          }
          conn.Close(); 
        }
      }catch(Exception ex){
        errorMessage = ex.Message;
      }
    }

  }

}