using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSE;

namespace SimpleCalculator
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Page.IsPostBack)
            {
                if(!string.IsNullOrWhiteSpace(txtThingToCalculate.Text))
                {
                    // Evaluate statement
                    try
                    {
                        string result = EvaluateStatement(txtThingToCalculate.Text);
                        lblResult.Text = result;
                    }
                    catch (Exception ex)
                    {
                        lblResult.Text = "An Error has occurred during evaluation.  Please try again!";
                    }
                }
            }
        }

        private string EvaluateStatement(string statementToEvaluate)
        {
                object commandResult = CsEval.Eval(new object(), statementToEvaluate);
                return commandResult.ToString();
        }
    }
}