using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class VinmartPlusController : Controller
    {
        // GET: VinmartPlus
        public ActionResult Index(string storecode, string templatenumber, string serialnumber, string documentno)
        {
            String connectionStr = ConfigurationManager.ConnectionStrings["connect2"].ConnectionString;
            var sql = string.Format("select contentfile from SumInvoiceDetail where storecode=@storecode and documentno=@documentno and templatenumber =@templatenumber and serialnumber =@serialnumber");
            using (var con = new SqlConnection(connectionStr))
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@storecode", storecode);
                cmd.Parameters.AddWithValue("@documentno", documentno);
                cmd.Parameters.AddWithValue("@templatenumber", templatenumber);
                cmd.Parameters.AddWithValue("@serialnumber", serialnumber);
                con.Open();
                Response.AppendHeader("Content-Disposition", "inline; filename=NGOCDM.pdf");
                return File((byte[])cmd.ExecuteScalar(), "application/pdf");
            }
        }
    }
}