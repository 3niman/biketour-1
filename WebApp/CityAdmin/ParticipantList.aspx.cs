﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ClassAdmin_ParticipantList : System.Web.UI.Page
{
    BCCityAdmin objCityAdmin = new BCCityAdmin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
            Response.Redirect(System.Web.Security.FormsAuthentication.LoginUrl.ToString() + "?ReturnUrl=" + Request.RawUrl.ToString());

        if (!IsPostBack)
        {
            if (Session["SchoolId"] != null && Session["ClassId"] != null)   //Application["SchoolId"] != null && Application["ClassId"] != null)
            {
                
                ddlSchool.SelectedValue = Session["SchoolId"].ToString();
                ddlSchool.DataTextField = "School";
                ddlSchool.DataValueField = "SchoolId";
                ddlSchool.DataBind();
                
                ddlClass.SelectedValue = Session["ClassId"].ToString();
                ddlClass.DataTextField = "Class";
                ddlClass.DataValueField = "classid";
                ddlClass.DataBind();
                ddlClass_SelectedIndexChanged(sender, e);
            }
        }

    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        int schoolId = Convert.ToInt32(ddlSchool.SelectedValue);
        int classId = Convert.ToInt32(ddlClass.SelectedValue);
        try
        {
            if (grd_ParticipantsList.Rows.Count > 0)
            {
               
                foreach (GridViewRow rw in grd_ParticipantsList.Rows)
                {
                    CheckBox chkConfirmed = rw.FindControl("chk_Confirmed") as CheckBox;
                    CheckBox chkActive = rw.FindControl("chk_Active") as CheckBox;
                    Label lblStudentId = rw.FindControl("lbl_StudentId") as Label;
                    int confirmed = Convert.ToInt32(chkConfirmed.Checked);
                    int active = Convert.ToInt32(chkConfirmed.Checked);
                    int res = SaveGridData(Convert.ToInt32(lblStudentId.Text), confirmed, active);
                }

                string popupScript = "alert('" + (string)GetLocalResourceObject("RecordsUpdated") + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }
            else
            {
                string popupScript = "alert('" + (string)GetLocalResourceObject("NoRecords") + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            }

            ddlClass.SelectedValue = classId.ToString();
            ddlSchool.SelectedValue = schoolId.ToString();
        }
        catch (Exception ex)
        {
        }
        //ddlSchool.DataBind();
        //ddlClass.DataBind();
        _BindGrid();
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow rw in grd_ParticipantsList.Rows)
            {
                CheckBox chkConfirmed = rw.FindControl("chk_Confirmed") as CheckBox;
                CheckBox chkActive = rw.FindControl("chk_Active") as CheckBox;

                chkActive.Checked = chkConfirmed.Checked;
                //if (!chkConfirmed.Checked)
                //{
                //    chkActive.Checked = false;
                //}
            }
        }
        catch { }
    }
    public int SaveGridData(int StudentId, int IsConfirmed, int IsActive)
    {
        int res = 0;
        try
        {
            string qry = "Update StudentMaster set IsStatusActive=" + IsActive + ", IsStatusConfirmed=" + IsConfirmed + " where StudentId=" + StudentId + " ";
            res = DataAccessLayer.ExecuteNonQuery(qry);
        }
        catch { }
        return res;
    }

    protected void grd_ParticipantsList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            foreach (GridViewRow rw in grd_ParticipantsList.Rows)
            {
                CheckBox chkConfirmed = rw.FindControl("chk_Confirmed") as CheckBox;
                CheckBox chkActive = rw.FindControl("chk_Active") as CheckBox;
                Label lblStudentId = rw.FindControl("lbl_StudentId") as Label;
                if (chkActive.Checked)
                {
                    chkConfirmed.Enabled = false;
                }
                if (!chkConfirmed.Checked)
                {
                    chkActive.Enabled = false;
                }
            }
        }
        catch { }
    }

    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClass.SelectedIndex != 0)
        {
            Session["SchoolId"] = ddlSchool.SelectedValue;
            Session["ClassId"] = ddlClass.SelectedValue;
            _BindGrid();
        }
    }
    protected void grd_ParticipantsList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grd_ParticipantsList.PageIndex = e.NewPageIndex;
            _BindGrid();
        }
        catch (Exception)
        { }
    }

    private void _BindGrid()
    {
        if (ddlClass.SelectedValue == "")
            objCityAdmin.ClassId = 0;
        else
            objCityAdmin.ClassId = Convert.ToInt32(ddlClass.SelectedValue);
        objCityAdmin.ClassAdminId = 0;
        DataTable dt = objCityAdmin.GetClassParticipantsList(objCityAdmin);
        if (dt == null || dt.Rows.Count == 0)
        {
            dt = _CreateEmptyTable();
        }
        DataView dv = dt.DefaultView;
        if (this.ViewState["SortExp"] == null)
        {
            this.ViewState["SortExp"] = "Password";
            this.ViewState["SortOrder"] = "ASC";
        }
        dv.Sort = this.ViewState["SortExp"] + " " + this.ViewState["SortOrder"];

        if (dv.Count > 0)
            btnDeleteAllStudents.Visible = true;
        else
            btnDeleteAllStudents.Visible = false;
        grd_ParticipantsList.DataSource = dv;
        grd_ParticipantsList.DataBind();
        grd_ParticipantsList.PageIndex = 0;
    }

    private DataTable _CreateEmptyTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("StudentName");
        dt.Columns.Add("Password");
        //dt.Columns.Add("PolicyName");

        DataRow lRow = dt.NewRow();
        dt.AcceptChanges();
        return dt;
    }

    protected void grd_ParticipantsList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToLower().Equals("sort"))
        {
            if (this.ViewState["SortExp"] == null)
            {
                this.ViewState["SortExp"] = e.CommandArgument.ToString();
                this.ViewState["SortOrder"] = "ASC";
            }
            else
            {
                if (this.ViewState["SortExp"].ToString() == e.CommandArgument.ToString())
                {
                    if (this.ViewState["SortOrder"].ToString() == "ASC")
                        this.ViewState["SortOrder"] = "DESC";
                    else
                        this.ViewState["SortOrder"] = "ASC";
                }
                else
                {
                    this.ViewState["SortOrder"] = "ASC";
                    this.ViewState["SortExp"] = e.CommandArgument.ToString();
                }
            }

            _BindGrid();
        }
    }
    protected void grd_ParticipantsList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (this.ViewState["SortExp"] != null)
            {
                Image ImgSort = new Image();
                if (this.ViewState["SortOrder"].ToString() == "ASC")
                    ImgSort.ImageUrl = "~/_images/ArrowDown.png";
                else
                    ImgSort.ImageUrl = "~/_images/ArrowUp.png";

                switch (this.ViewState["SortExp"].ToString().ToLower())
                {
                    case "Password":
                        PlaceHolder placeholderSubDeptName = (PlaceHolder)e.Row.FindControl("placeholderPassword");
                        placeholderSubDeptName.Controls.Add(ImgSort);
                        break;
                    case "STUDENTNAME":
                        PlaceHolder placeholderSTUDENTNAME = (PlaceHolder)e.Row.FindControl("placeholderSTUDENTNAME");
                        placeholderSTUDENTNAME.Controls.Add(ImgSort);
                        break;
                }
            }
        }
    }
    protected void grd_ParticipantsList_Sorting(object sender, GridViewSortEventArgs e)
    {
        string s = e.SortExpression.ToString();
        string s2 = e.SortDirection.ToString();
    }

    protected void btnEditStudDetails_Click(object sender, EventArgs e)
    {
        try
        {
            int StudentID = Convert.ToInt32(((Button)sender).CommandArgument.ToString());
            Response.Redirect("StudentDetails.aspx?StudentId=" + StudentID.ToString());
        }
        catch (Exception ex)
        {
            //Helper.errorLog(ex, Server.MapPath(@"~/ImpTemp/Log.txt"));
        }
    }

    protected void btnDeleteStudent_Click(object sender, EventArgs e)
    {
        try
        {
            int StudentID = Convert.ToInt32(hdnDeleteId.Value);//Convert.ToInt32(((Button)sender).CommandArgument.ToString());
            objCityAdmin.DeleteStudent(StudentID, 0, Convert.ToInt32(Session["UserId"]));
            string msg = (string)GetLocalResourceObject("StudentDeleted");
            string popupScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), popupScript, true);
            _BindGrid();
        }
        catch (Exception ex)
        {
            //Helper.errorLog(ex, Server.MapPath(@"~/ImpTemp/Log.txt"));
        }
    }
    protected void btnDeleteAllStudents_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlClass.SelectedValue != "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ConfirmAll();", true);
            }
        }
        catch (Exception ex)
        {
            //Helper.errorLog(ex, Server.MapPath(@"~/ImpTemp/Log.txt"));
        }

    }

    protected void btnDeleteAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlClass.SelectedValue != "0")
            {
                int ClassId = Convert.ToInt32(ddlClass.SelectedValue);
                objCityAdmin.DeleteStudent(0, ClassId, Convert.ToInt32(Session["UserId"]));
                
                string msg = (string)GetLocalResourceObject("StudentDeleted");
                string popupScript = "alert('" + msg + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), popupScript, true);

                _BindGrid();
            }
        }
        catch (Exception ex)
        {
            //Helper.errorLog(ex, Server.MapPath(@"~/ImpTemp/Log.txt"));
        }
    }
}