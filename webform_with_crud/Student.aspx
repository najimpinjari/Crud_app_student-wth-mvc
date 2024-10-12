<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Student.aspx.cs" Inherits="webform_with_crud.Student" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Management</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            <h2>Student Management</h2>
            
            <label>Name:</label>
            <asp:TextBox ID="textname" runat="server"></asp:TextBox>
            <br /><br />
            
            <label>Gender:</label>
            <asp:DropDownList ID="ddlgender" runat="server">
                <asp:ListItem Text="Select Gender" Value=""></asp:ListItem>
                <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
            </asp:DropDownList>
            <br /><br />
            
            <label>Fees:</label>
            <asp:TextBox ID="textfees" runat="server"></asp:TextBox>
            <br /><br />
            
            <asp:Button ID="btnadd" runat="server" Text="Add Student" OnClick="BtnAdd_click" />
            <br /><br />
            
            <asp:GridView ID="gvstudent" runat="server" AutoGenerateColumns="false" 
                OnRowCommand="gvStudents_RowCommand" style="margin: auto; width: 60%;">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Gender" HeaderText="Gender" />
                    <asp:BoundField DataField="Fees" HeaderText="Fees" />
                    <asp:ButtonField ButtonType="Button" CommandName="Edit" Text="Edit" />
                    <asp:ButtonField ButtonType="Button" CommandName="Delete" Text="Delete" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
