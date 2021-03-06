﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SimpleCalculator._Default" %>

<asp:Content ID="HeaderStyleContent" ContentPlaceHolderID="HeaderStyles" runat="server">
    <link rel="stylesheet" href="Content/index.css" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Welcome to Simple Calculator!</h2>
    <asp:UpdatePanel ID="upHistoryPanel" runat="server">
        <ContentTemplate>
            <div id="calculator_command">
                <asp:TextBox runat="server" ID="txtThingToCalculate" CssClass="col-xd-6"></asp:TextBox>
                <asp:Button runat="server" ID="btnRunCommand" Text="=" CssClass="col-xd-2 execute-button" OnClick="btnRunCommand_Click" />
                <asp:Label runat="server" ID="lblResult" CssClass="col-xd-4" />
            </div>
            <hr />
            <div id="calculator_history">
                <h4>History</h4>
                <asp:Repeater id="rptHistory" runat="server">
                    <HeaderTemplate>
                        <table border="1">
                            <tr>
                                    <th class="col-xd-4 history-header">Timestamp</th>
                                    <th class="col-xd-4 history-header">Command</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                            <tr>
                                <td class="col-xd-4"><%#Eval("Timestamp")%></td>
                                <td class="col-xd-4"><%#Eval("Command")%></td>
                            </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Button ID="btnClearHistory" runat="server" Text="Clear History" CssClass="col-xd-4" OnClick="btnClearHistory_Click" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" src="Scripts/default.js"></script>
</asp:Content>
