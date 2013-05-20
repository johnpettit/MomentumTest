<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="MomentumTest.Report"  MasterPageFile="~/Site.master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Reports

    </h2>
 
          <p>
           <table border=1 CellPadding="3" width="800" align="center" bgcolor="#99CCFF">
          <tr>
            <td align="center" width="33%"><b>Customers that have not been contacted in the last 6 months</b><br /><asp:Button 
                    ID="btnNoContact" runat="server" Text="Generate Report" 
                    onclick="btnNoContact_Click" />
              </td>
            <td align="center" width="33%"><b>Customer that have been contacted in the last week</b><br /><asp:Button 
                    ID="btnLastWeek" runat="server" Text="Generate Report" 
                    onclick="btnLastWeek_Click" />
              </td>
            <td align="center" width="33%"><b>Customer that have been contacted more than twice in the last wee</b>k<br /><asp:Button 
                    ID="btnManyContact" runat="server" Text="Generate Report" 
                    onclick="btnManyContact_Click" />
              </td>
          </tr>

          </table>

          </p>

          <p>
          
              <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
          
          </p>
          <p>
          
          <asp:Repeater id=Repeater1 runat="server">   
          <HeaderTemplate>
             <table border=1 CellPadding="3" width="800" align="center">
             <thead>
                <tr>
                    <td colspan="5" bgcolor="#99CCFF"><b>Report Output</b></td>
                </tr>
                <tr bgcolor="#CCFFFF">
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
             
       </asp:Repeater>

          </p>

</asp:Content>