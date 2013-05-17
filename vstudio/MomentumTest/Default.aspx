<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="MomentumTest._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to MomentuM&#39;s CRM System

    </h2>
 
          <p>
                 <asp:Repeater id=Repeater1 runat="server">
             
          <HeaderTemplate>
             <table border=1 CellPadding="5">
             <thead>
                <tr>
                    <td>Full Name</td>
                    <td>Phone Number</td>
                    <td>Email</td>
                </tr>
             </thead>
             <tbody>
          </HeaderTemplate>
 
          <ItemTemplate>
             <tr>
                <td> <%# ((MomentumTest.lib.Customer)Container.DataItem).firstName %> <%# ((MomentumTest.lib.Customer)Container.DataItem).lastName %></td>
                <td> <%# ((MomentumTest.lib.Customer)Container.DataItem).phoneNumber %></td>
                <td> <%# ((MomentumTest.lib.Customer)Container.DataItem).email %></td>
             </tr>
          </ItemTemplate>
 
          <FooterTemplate>
                </tbody>
             </table>
          </FooterTemplate>
             
       </asp:Repeater>

          </p>

</asp:Content>
