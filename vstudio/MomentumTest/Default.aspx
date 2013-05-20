<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="MomentumTest._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
 
          <p>
          
          
          <table width="800">
          <tr>
            <td colspan="2" align="center"><h2>
        Welcome to MomentuM&#39;s CRM System

    </h2></td>
          </tr>
          <tr>
            <td colspan="2">
            <b>Instant Dashboard Highlights</b><br />
            # of Customers with an odd number of characters in their first name : 
                <asp:Label ID="lblDBOdd" runat="server" Text="0" Font-Bold="True"></asp:Label>
                <br />
            # of Customers with an even number of characters in their last name : 
                <asp:Label ID="lblDBEven" runat="server" Text="0" Font-Bold="True"></asp:Label>
                <br />
            # of Families : 
                <asp:Label ID="lblDBFamily" runat="server" Text="0" Font-Bold="True"></asp:Label>
                <br />
            # of Contacts created today : 
                <asp:Label ID="lblDBToday" runat="server" Text="0" Font-Bold="True"></asp:Label>
            </td>
          </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
          <tr>
            <td valign="top">                
            <table border=1 CellPadding="3" width="300">
                <tr>
                    <td colspan="2" valign="top" bgcolor="#99CCFF"><b>Customer Search</b></td>
                </tr>
                <tr>
                    <td width="50%" align="right">First Name</td>
                    <td width="50%">
                        <asp:TextBox ID="txtSearchFirstName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="50%" align="right">Last Name</td>
                    <td width="50%">
                        <asp:TextBox ID="txtSearchLastName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="50%" align="right">Phone Number</td>
                    <td width="50%">
                        <asp:TextBox ID="txtSearchPhoneNumber" runat="server"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td width="50%" align="right">Email</td>
                    <td width="50%">
                        <asp:TextBox ID="txtSearchEmail" runat="server"></asp:TextBox>
                     </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnDoSearch" runat="server" onclick="btnDoSearch_Click" 
                            Text="Search for Customer" />
                    </td>
                </tr>
                </table>
                <br />
                <table border=1 CellPadding="3" width="300">
                <tr>
                    <td colspan="2" bgcolor="#99CCFF"><b>Create Customer</b></td>
                </tr>

                <tr>
                    <td width="50%" align="right">First Name</td>
                    <td width="50%">
                        <asp:TextBox ID="txtCreateFirstName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="50%" align="right">Last Name</td>
                    <td width="50%">
                        <asp:TextBox ID="txtCreateLastName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="50%" align="right">Phone Number</td>
                    <td width="50%">
                        <asp:TextBox ID="txtCreatePhoneNumber" runat="server"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td width="50%" align="right">Email</td>
                    <td width="50%">
                        <asp:TextBox ID="txtCreateEmail" runat="server"></asp:TextBox>
                     </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnCreate" runat="server" 
                            Text="Create Customer" onclick="btnDoCreate_Click" />
                    </td>
                </tr>
                </table></td>
            <td valign="top">          <asp:Repeater id=Repeater1 runat="server">   
          <HeaderTemplate>
             <table border=1 CellPadding="3" width="600">
             <thead>
                <tr>
                    <td colspan="5" bgcolor="#99CCFF"><b>Customer List</b></td>
                </tr>
                <tr  bgcolor="#CCFFFF">
                    <td>Full Name</td>
                    <td>Phone Number</td>
                    <td>Email</td>
                    <td>Last Contact</td>
                    <td>Action</td>
                </tr>
             </thead>
             <tbody>
          </HeaderTemplate>
 
          <ItemTemplate>
             <tr>
                <td> <%# ((MomentumTest.lib.Customer)Container.DataItem).firstName %> <%# ((MomentumTest.lib.Customer)Container.DataItem).lastName %></td>
                <td> <%# ((MomentumTest.lib.Customer)Container.DataItem).phoneNumber %></td>
                <td> <%# ((MomentumTest.lib.Customer)Container.DataItem).email %></td>
                <td> <%# ((MomentumTest.lib.Customer)Container.DataItem).lastContact %></td>
                <td><a href="Contact.aspx?customerid=<%# ((MomentumTest.lib.Customer)Container.DataItem).id %>">Add/View Contact</a></td>
             </tr>
          </ItemTemplate>
 
           <AlternatingItemTemplate>
             <tr bgcolor="#E1FBFC">
                <td> <%# ((MomentumTest.lib.Customer)Container.DataItem).firstName %> <%# ((MomentumTest.lib.Customer)Container.DataItem).lastName %></td>
                <td> <%# ((MomentumTest.lib.Customer)Container.DataItem).phoneNumber %></td>
                <td> <%# ((MomentumTest.lib.Customer)Container.DataItem).email %></td>
                <td> <%# ((MomentumTest.lib.Customer)Container.DataItem).lastContact %></td>
                <td><a href="Contact.aspx?customerid=<%# ((MomentumTest.lib.Customer)Container.DataItem).id %>">Add/View Contact</a></td>
             </tr>
          </AlternatingItemTemplate>

          <FooterTemplate>
                </tbody>
             </table>
          </FooterTemplate>
             
       </asp:Repeater></td>
          </tr>
          </table>
                 



          </p>

</asp:Content>
