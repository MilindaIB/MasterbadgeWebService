using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
namespace MasterbadgeWebSeriveApp
{

public class user{

    public int id { get; set; }
    public string Reg_title { get; set; }
    public string Fname { get; set; }
    public string Mname { get; set; }
    public string Lname { get; set; }
    public string Organization { get; set; }
    public int Reg_type { get; set; }
    public string Reg_possition { get; set; }
    public string email { get; set; }
    public string Mobile { get; set; }

}
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 
    [System.Web.Script.Services.ScriptService]
    public class Datasource : System.Web.Services.WebService
    {
        [WebMethod]
       [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string Getid(string email)
        {   List<user> u = new List<user>();
            string strcon="Server=tcp:uqqvmjp5q2.database.windows.net,1433;Database=MasterbadgeDev;User ID=abuateek@uqqvmjp5q2;Password=P@$$w0rd911;Trusted_Connection=False;Encrypt=True;Connection Timeout=30";
            using (SqlConnection sql = new SqlConnection(strcon))
            {

                sql.Open();
                DataTable dt = new DataTable();
                SqlCommand scmd = new SqlCommand("select * from masterbadge.registration where reg_email=@rid", sql);
                scmd.Parameters.AddWithValue("rid", email);
                dt.Load(scmd.ExecuteReader());
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        u.Add(new user
                        {
                            id = Convert.ToInt32(item["R_id"].ToString()),
                            Reg_title = item["Reg_title"].ToString(),
                            Fname = item["Reg_f_name"].ToString(),
                            Mname = item["reg_m_name"].ToString(),
                            Lname = item["reg_l_name"].ToString(),
                            Reg_type = 1,
                            email = item["reg_email"].ToString(),
                            Reg_possition = item["reg_position"].ToString(),
                            Organization = item["Reg_work_name"].ToString(),
                            Mobile = item["reg_mobile"].ToString()
                        });   
                      
                    }
                        JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
                        var TheJson = TheSerializer.Serialize(u);
                        return TheJson;
                }
                else 
                {

                    return "notfound" ;
                
                }
              
  
            }
        
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public user[] Getidxml()
        {



             user[]emps= new user[] {  
            new user()  
            {  
                id = 1, email = "em@em.com", Fname = "nahas" 
            },  
            new user()  
            {  
               id = 2, email = "em@em.com", Fname = "nahas1"
            }  
            ,
            new user()  
            {  
               id = 3, email = "em@em.com", Fname = "nahas2"
            } 
        };

             return emps;
        }


        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}