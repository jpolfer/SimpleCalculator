using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSE;
using System.Data.SQLite;

namespace SimpleCalculator
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                // Display all history from the history table
                // Only do this if not postback - we'll load history from the evaluate click event otherwise
                LoadHistoryList();
            }
        }

        private string EvaluateStatement(string statementToEvaluate)
        {
                object commandResult = CsEval.Eval(new object(), statementToEvaluate);
                return commandResult.ToString();
        }

        private bool StoreEvaluationToHistoryTable(string command, string result)
        {
            using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + DatabaseHelper.dbfilename + ";Version=3;"))
            {
                m_dbConnection.Open();
                string timestamp = DateTime.Now.ToString();
                string evaluationStatement = command + " = " + result;
                string sql = "insert into history (timestamp, command) values ('" + timestamp + "', '" + evaluationStatement + "')";
                using (SQLiteCommand sqlCommand = new SQLiteCommand(sql, m_dbConnection))
                {
                    int sqlCommandResult = sqlCommand.ExecuteNonQuery();

                    return sqlCommandResult == 1;
                }
            }
        }

        private List<HistoryEntry> GetHistoryEntriesFromDb()
        {
            List<HistoryEntry> historyEntries = new List<HistoryEntry>();
            using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + DatabaseHelper.dbfilename + ";Version=3;"))
            {
                string sql = "select timestamp, command from history";
                using (SQLiteCommand sqlCommand = new SQLiteCommand(sql, m_dbConnection))
                {
                    m_dbConnection.Open();
                    using (SQLiteDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HistoryEntry newEntry = new HistoryEntry()
                            {
                                Timestamp = reader["timestamp"].ToString(),
                                Command = reader["command"].ToString()
                            };
                            historyEntries.Add(newEntry);
                        }
                    }
                }
            }

            return historyEntries;
        }
        private void LoadHistoryList()
        {
            List<HistoryEntry> historyEntries = GetHistoryEntriesFromDb();

            rptHistory.DataSource = historyEntries;
            rptHistory.DataBind();
        }

        protected void btnRunCommand_Click(object sender, EventArgs e)
        {
                if(!string.IsNullOrWhiteSpace(txtThingToCalculate.Text))
                {
                    try
                    {
                        // Evaluate statement
                        string result = EvaluateStatement(txtThingToCalculate.Text);

                        // Store statement results
                        if(!StoreEvaluationToHistoryTable(txtThingToCalculate.Text, result))
                        {
                            throw new Exception();
                        }

                        // Display result
                        lblResult.Text = result;
                    }
                    catch (Exception ex)
                    {
                        lblResult.Text = "An Error has occurred during evaluation or history update.  Please try again!";
                    }
                }

            // Reload history list
            LoadHistoryList();
        }

        protected void btnClearHistory_Click(object sender, EventArgs e)
        {
            ClearHistoryTable();
            LoadHistoryList();
        }

        private void ClearHistoryTable()
        {
            using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + DatabaseHelper.dbfilename + ";Version=3;"))
            {
                m_dbConnection.Open();
                string sql = "delete from history";
                using (SQLiteCommand sqlCommand = new SQLiteCommand(sql, m_dbConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}