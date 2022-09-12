using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lib.Pages.Users {
    public class Login : PageModel{
        public string successMessage ="" ;
        public string errorMessage = "";
        public UserInfo userInfo = new UserInfo();
        public void OnGet(){

        }
        public void OnPost(){
            userInfo.userName = Request.Form["userName"];
            userInfo.password = Request.Form["password"];
            try{
                string connectionString = "Server=tcp:libcomakeit.database.windows.net,1433;Initial Catalog=libcmit;Persist Security Info=False;User ID=avi;Password=Earth@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection conn = new SqlConnection(connectionString)){
                    conn.Open();
                    string query = "select password, roles from users where userName = '"+userInfo.userName+"'";
                    using (SqlCommand cmd = new SqlCommand(query, conn)){
                        using(SqlDataReader reader = cmd.ExecuteReader()){
                            if(reader.HasRows){
                                while(reader.Read()){
                                    string password = reader.GetString(0);
                                    string role = reader.GetString(1);
                                    if(password.Equals(userInfo.password)){
                                        errorMessage = "";
                                        if (string.IsNullOrEmpty(HttpContext.Session.GetString("userName"))){
                                            successMessage = "Login Successful";
                                            HttpContext.Session.SetString("userName", userInfo.userName);
                                            HttpContext.Session.SetString("password",userInfo.password );
                                            HttpContext.Session.SetString("role",role);
                                            Response.Redirect("/");
                                        }else{
                                            errorMessage = "User " + userInfo.userName + " already login" ;
                                        }
                                    }else{
                                        errorMessage = "Incorrect Password";
                                    }
                                }
                            }else{
                                errorMessage = "Incorrect Password";
                            }
                        }
                    }
                }
            }catch(Exception ex){
                errorMessage = ex.Message;
            }
        }
    }
    public class UserInfo{
        public string userName { get; set; }
        public string password { get; set; }
    }
}