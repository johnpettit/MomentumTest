<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="MomentumTest.Contact"  MasterPageFile="~/Site.master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Add Contact to Customer : 

        <asp:Label ID="lblCustomerName" runat="server"></asp:Label>

    </h2>
 
          <p>
           <table border=1 CellPadding="3" width="800" align="center">
           <tr><td colspan="2">
               <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
               </td></tr>
                <tr>
                    <td width="50%" align="right">Contact DateTime</td>
                    <td width="50%">
                        <asp:TextBox ID="txtAddContactDateTime" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="50%" align="right">Contact Notes</td>
                    <td width="50%">
                        <asp:TextBox ID="txtAddContactNotes" runat="server" Columns="30" Rows="6" 
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnDoAddContact" runat="server"  
                            Text="Add Contact" onclick="btnDoAddContact_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnContactCancel" runat="server" 
                            onclick="btnContactCancel_Click" Text="Cancel" />
                    </td>
                </tr>
                </table>

          </p>

          <p>
          
          <asp:Repeater id=Repeater1 runat="server">   
          <HeaderTemplate>
             <table border=1 CellPadding="3" width="800" align="center">
             <thead>
                <tr>
                    <td colspan="2"><b>Customer Contacts</b></td>
                </tr>
                <tr>
                    <td>Contact Time</td>
                    <td>Notes</td>
                </tr>
             </thead>
             <tbody>
          </HeaderTemplate>
 
          <ItemTemplate>
             <tr>
                <td> <%# ((MomentumTest.lib.Contact)Container.DataItem).createDate %></td>
                <td> <%# ((MomentumTest.lib.Contact)Container.DataItem).note %></td>
             </tr>
          </ItemTemplate>
 
          <FooterTemplate>
                </tbody>
             </table>
          </FooterTemplate>
             
       </asp:Repeater>
          
          </p>

</asp:Content>