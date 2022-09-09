using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lib.Pages.Users {
    public class SignUp : PageModel{
        public string successMessage ="" ;
        public string errorMessage = "";
        public UserInfo userInfo = new UserInfo();
        public void OnGet(){

        }
        public void OnPost(){
            userInfo.userName = Request.Form["userName"];
            userInfo.password = Request.Form["password"];
            try{
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=library;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString)){
                conn.Open();
                string query = "INSERT INTO users (userName, password, roles) VALUES (@userName, @password, @role)";
                using (SqlCommand cmd = new SqlCommand(query, conn)){
                    cmd.Parameters.AddWithValue("@userName", userInfo.userName);
                    cmd.Parameters.AddWithValue("@password", userInfo.password);
                    cmd.Parameters.AddWithValue("@role", "student");
                    cmd.ExecuteNonQuery();
                    successMessage = "User Created Successfully";
                    Response.Redirect("/Users/Login");
                }
            }
            }catch(Exception ex){
                errorMessage = ex.Message;
            }
        }
    }
}