using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Data.SQLite;

namespace SimpleCalculator
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            if(!File.Exists(DatabaseHelper.dbfilename))
            {
                // Create db
                SQLiteConnection.CreateFile(DatabaseHelper.dbfilename);

                // Create history table
                using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + DatabaseHelper.dbfilename + ";Version=3;"))
                {
                    m_dbConnection.Open();
                    string createHistoryTableSql = "CREATE TABLE history (timestamp TEXT, command TEXT);";
                    using (SQLiteCommand createHistoryTable = new SQLiteCommand(createHistoryTableSql, m_dbConnection))
                    {
                        createHistoryTable.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}