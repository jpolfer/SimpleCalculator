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
                // Evaluate statement
                object commandResult = CsEval.Eval(this, txtThingToCalculate.Text);
                lblResult.Text = commandResult.ToString();
            }
        }
    }
}