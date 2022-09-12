using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Lib.Pages.Books;

namespace Lib.Pages.Books{
  public class AddBookModel : PageModel{
    public BooksInfo booksInfo = new BooksInfo();
    public string successMessage = "";
    public string errorMessage = "";

    public void OnGet(){}

    public void OnPost(){
      string title = Request.Form["title"];
      string author = Request.Form["author"];
      string publisher = Request.Form["publisher"];
      string categories = Request.Form["categories"];
      int prices = Convert.ToInt32(Request.Form["prices"]);
      Console.WriteLine(title + " " + author + " " + publisher + " " + categories + " " + prices);
      if (title.Length == 0 || author.Length == 0 || publisher.Length == 0 || categories.Length == 0){
        errorMessage = "Please fill all the fields";
        return;
      }
      try{
        string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=library;Integrated Security=True";
        using (SqlConnection conn = new SqlConnection(connectionString)){
          conn.Open();
          string query = "INSERT INTO Books (title, author, publisher, categories, prices, assignendTo) VALUES (@title, @author, @publisher, @categories, @prices, 'NULL')";
          using (SqlCommand cmd = new SqlCommand(query, conn)){
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@author", author);
            cmd.Parameters.AddWithValue("@publisher", publisher);
            cmd.Parameters.AddWithValue("@categories", categories);
            cmd.Parameters.AddWithValue("@prices", prices);
            cmd.ExecuteNonQuery();
            successMessage = "Book added successfully";
          }
        }
      }catch(Exception ex){
        errorMessage = ex.Message;
      }
    }
  }
}