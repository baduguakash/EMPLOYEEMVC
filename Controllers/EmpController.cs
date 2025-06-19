using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using EMPLOYEEMVC.Models;

namespace EMPLOYEEMVC.Controllers
{
   
    public class EmpController : Controller
    {
        string connection = @"Data Source=AKASH\SQLEXPRESS;Initial Catalog=Employes;Integrated Security=True;";
        [HttpGet]
        // GET: Emp
        public ActionResult Index()
        {
            DataTable dt =  new DataTable();
            using (SqlConnection con = new SqlConnection(connection))
            {
             con.Open();
             string query = "select * from Emp";
             SqlDataAdapter sda = new SqlDataAdapter(query,con);
             sda.Fill(dt);
            }
                return View(dt);
        }

          
        // GET: Emp/Create
        public ActionResult Create()
        {
            
            return View(new Database());
        }

        // POST: Emp/Create
        [HttpPost]
        public ActionResult Create(Database Database)
        {
          
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                string query = "insert into Emp values(@EmpName,@Email,@Salary)";
                SqlCommand cmd = new SqlCommand(query,con);
                cmd.Parameters.AddWithValue("@EmpName", Database.EmpName);
                cmd.Parameters.AddWithValue("@Email", Database.Email);
                cmd.Parameters.AddWithValue("@Salary", Database.Salary);
                cmd.ExecuteNonQuery();


            }
            return RedirectToAction("Index");
            
        }

        // GET: Emp/Edit/5
        public ActionResult Edit(int id)
        {
            Database Model = new Database();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                string query = "select * from Emp where EmpId = @EmpId";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                sda.SelectCommand.Parameters.AddWithValue("@EmpId",id);
                sda.Fill(dt);
                if (dt.Rows.Count==1)
                {
                    Model.EmpId = Convert.ToInt32(dt.Rows[0][0].ToString());
                    Model.EmpName = dt.Rows[0][1].ToString();
                    Model.Email = dt.Rows[0][2].ToString();
                    Model.Salary = Convert.ToInt32(dt.Rows[0][3].ToString());
                    return View(Model);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

         
        }

        // POST: Emp/Edit/5
        [HttpPost]
        public ActionResult Edit(Database Database)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                string query = "update Emp set EmpName=@EmpName,Email=@Email,Salary=@Salary where EmpId=@EmpId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@EmpId", Database.EmpId);
                cmd.Parameters.AddWithValue("@EmpName", Database.EmpName);
                cmd.Parameters.AddWithValue("@Email", Database.Email);
                cmd.Parameters.AddWithValue("@Salary", Database.Salary);
                cmd.ExecuteNonQuery();


            }
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Emp/Delete/5
        public ActionResult Delete(int id)
        {
            Database Model = new Database();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                string query = "select * from Emp where EmpId = @EmpId";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                sda.SelectCommand.Parameters.AddWithValue("@EmpId", id);
                sda.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    Model.EmpId = Convert.ToInt32(dt.Rows[0][0].ToString());
                    Model.EmpName = dt.Rows[0][1].ToString();
                    Model.Email = dt.Rows[0][2].ToString();
                    Model.Salary = Convert.ToInt32(dt.Rows[0][3].ToString());
                    return View(Model);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }    }

        // POST: Emp/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();
                string query = "Delete from Emp where EmpId=@EmpId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@EmpId",id);
                cmd.ExecuteNonQuery();             

            }
            {
                return RedirectToAction("Index");
            }
        }
    }
}
