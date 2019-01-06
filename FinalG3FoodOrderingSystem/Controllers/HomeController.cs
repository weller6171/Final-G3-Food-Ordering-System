using FinalG3FoodOrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;


namespace FinalG3FoodOrderingSystem.Controllers
{
    public class HomeController : Controller
    {
        #region Index method

        /// <summary>
        /// GET: Home/Index method.
        /// </summary>
        /// <returns>Returns - index view page</returns> 
        public ActionResult Index()
        {
            // Info.
            return this.View();
        }

        #endregion

        #region Get data method.

        /// <summary>
        /// GET: /Home/GetData
        /// </summary>
        /// <returns>Return data</returns>
        public ActionResult GetData()
        {
            // Initialization.
            JsonResult result = new JsonResult();

            try
            {
                // Loading.
                List<SalesOrderDetail> data = this.LoadData();

                // Setting.
                var graphData = data.GroupBy(p => new
                {
                    p.FoodName,
                    p.Quantity,

                })
                                    .Select(g => new
                                    {
                                        g.Key.FoodName,
                                        g.Key.Quantity,

                                    }).OrderByDescending(q => q.Quantity).ToList();

                // Top 10
                graphData = graphData.Take(10).Select(p => p).ToList();

                // Loading drop down lists.
                result = this.Json(graphData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // Return info.
            return result;
        }

        #endregion

        #region Helpers

        #region Load Data

        /// <summary>
        /// Load data method.
        /// </summary>
        /// <returns>Returns - Data</returns>
        private List<SalesOrderDetail> LoadData()
        {
            // Initialization.
            List<SalesOrderDetail> lst = new List<SalesOrderDetail>();

            try
            {
                // Initialization.
                string line = string.Empty;
                string srcFilePath = "Content/files/SalesOrderDetail.txt";
                var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                var fullPath = Path.Combine(rootPath, srcFilePath);
                string filePath = new Uri(fullPath).LocalPath;
                StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read));

                // Read file.
                while ((line = sr.ReadLine()) != null)
                {
                    // Initialization.
                    SalesOrderDetail infoObj = new SalesOrderDetail();
                    string[] info = line.Split(',');

                    // Setting.
                    infoObj.Id = Convert.ToInt32(info[0].ToString());
                    infoObj.FoodId = Convert.ToInt32(info[1].ToString());
                    infoObj.Quantity = Convert.ToInt32(info[2].ToString());
                    infoObj.FoodName = info[3].ToString();
                    infoObj.Status = info[4].ToString();


                    // Adding.
                    lst.Add(infoObj);
                }

                // Closing.
                sr.Dispose();
                sr.Close();
            }
            catch (Exception ex)
            {
                // info.
                Console.Write(ex);
            }

            // info.
            return lst;
        }

        #endregion

        #endregion
    }
}