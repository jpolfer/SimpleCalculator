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
        private void LoadHistoryList()
        {
            List<HistoryEntry> historyEntries = DatabaseHelper.GetHistoryEntriesFromDb();

            rptHistory.DataSource = historyEntries;
            rptHistory.DataBind();
        }

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

        protected void btnRunCommand_Click(object sender, EventArgs e)
        {
                if(!string.IsNullOrWhiteSpace(txtThingToCalculate.Text))
                {
                    try
                    {
                        // Evaluate statement
                        string result = EvaluateStatement(txtThingToCalculate.Text);

                        // Store statement results
                        if(!DatabaseHelper.StoreEvaluationToHistoryTable(txtThingToCalculate.Text, result))
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
            txtThingToCalculate.Text = "";
            lblResult.Text = "";
            DatabaseHelper.ClearHistoryTable();
            LoadHistoryList();
        }

    }
}