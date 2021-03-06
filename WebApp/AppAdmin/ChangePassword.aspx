﻿<%@ Page Title="Bike Tour - Change Your Password" Culture="de-DE" UICulture="de-DE" Language="C#" MasterPageFile="~/SiteMaster/AdminMaster.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="_js/jquery-1.7.1.js" type="text/javascript"></script>
    <script src="_js/jquery.corner.js" type="text/javascript"></script>
    <script src="_js/customJs.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container">
    <h5>
    <asp:Label ID="lblHead" runat="server" meta:ResourceKey="lblHead">
   </asp:Label></h5>
<div class="AdminContWrap">
<div>
        <asp:Panel ID="pnlchangepassward" runat="server" CssClass="frmBox">
            <table cellspacing="8">
                <tr>
                    <td style="width: 175px">
                        <asp:Label ID="lblCurrentPassword" runat="server" meta:ResourceKey="lblCurrentPassword" CssClass="GlbLbl"></asp:Label>
                    </td>
                    <td style="width: 5px">
                        <asp:Label ID="Label5" runat="server" Text=":" CssClass="GlbLbl"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCurrentPassword" runat="server" Width="183px" CssClass="Glbtxt"
                            TextMode="Password" ontextchanged="txtCurrentPassword_TextChanged"></asp:TextBox>
                        <span class="error">*</span>
                        <div class="ClearBoth">
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCurrentPassword"
                            Display="Dynamic" CssClass="error" ValidationGroup="Save"
                            meta:ResourceKey="RequiredFieldValidator1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 175px">
                        <asp:Label ID="lblNewPassword" runat="server" meta:ResourceKey="lblNewPassword" CssClass="GlbLbl"></asp:Label>
                    </td>
                    <td style="width: 5px">
                        <asp:Label ID="Label2" runat="server" Text=":" CssClass="GlbLbl"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNewPswd" runat="server" Width="183px" CssClass="Glbtxt" TextMode="Password"></asp:TextBox>
                        <span class="error">*</span>
                        <div class="ClearBoth">
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewPswd"
                            meta:ResourceKey="RequiredFieldValidator2" Display="Dynamic"  CssClass="error"  ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 175px">
                        <asp:Label ID="lblConfirmPassword" runat="server" meta:ResourceKey="lblConfirmPassword" CssClass="GlbLbl"></asp:Label>
                    </td>
                    <td style="width: 5px">
                        <asp:Label ID="Label4" runat="server" Text=":" CssClass="GlbLbl"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtConfirm" runat="server" Width="183px" CssClass="Glbtxt" TextMode="Password"></asp:TextBox>
                        <span class="error">*</span>
                        <div class="ClearBoth">
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtConfirm"
                            Display="Dynamic"  CssClass="error"  
                            ValidationGroup="Save" meta:ResourceKey="RequiredFieldValidator3"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                            ControlToCompare="txtNewPswd" ControlToValidate="txtConfirm" CssClass="error"
                            Display="Dynamic"  ValidationGroup="Save" meta:ResourceKey="CompareValidator1"></asp:CompareValidator>

                            <asp:CompareValidator ID="CompareValidator2" runat="server"
                            ControlToCompare="txtCurrentPassword" ControlToValidate="txtNewPswd" CssClass="error" Operator="NotEqual" Type="String"
                            Display="Dynamic"  ValidationGroup="Save" meta:ResourceKey="CompareValidator2"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="Up_BtnUpdate" runat="server">
                                      <ContentTemplate> 
                                           <asp:Button ID="BtnUpdate" runat="server" meta:ResourceKey="BtnUpdate" CssClass="glbtn left" 
                                               onclick="BtnUpdate_Click"  ValidationGroup="Save" />
                                        </ContentTemplate>
                                        <Triggers >
                                            <asp:PostBackTrigger ControlID="BtnUpdate" />
                                        </Triggers>
                                     </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="Up_btnCancel" runat="server">
                                   <ContentTemplate> 
                                    <asp:Button ID="btnCancel" runat="server" meta:ResourceKey="btnCancel" CssClass="glbtn left" 
                                        CausesValidation="false" onclick="btnCancel_Click"/>
                                        </ContentTemplate> 
                                        <Triggers >
                                        <asp:PostBackTrigger ControlID="btnCancel" />
                                        </Triggers>
                                      </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:Label ID="lblmsg" runat="server" CssClass="error"  Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div>
                <div class="Clear">
                </div>
            </div>
        </asp:Panel>
    </div>
</div>
        </div>
</asp:Content>

