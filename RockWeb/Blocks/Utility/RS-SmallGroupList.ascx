<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RS-SmallGroupList.ascx.cs" Inherits="RockWeb.Blocks.Utility.RSSmallGroupList" %>

<asp:UpdatePanel ID="upnlContent" runat="server">
    <ContentTemplate>

        <asp:Panel ID="pnlView" runat="server" CssClass="panel panel-block">

            <div class="panel-heading">
                <h1 class="panel-title">
                    <i class="fa fa-users"></i>
                    Small Groups
                </h1>
            </div>
            <div class="panel-body">

                <div class="grid grid-panel">
                    <Rock:Grid ID="gSmallGroups" runat="server" AllowSorting="true" RowItemText="Group" DataKeyNames="Id" OnRowSelected="gSmallGroups_RowSelected">
                        <Columns>
                            <Rock:RockBoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                            <Rock:RockBoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                            <Rock:RockBoundField DataField="GroupCapacity" HeaderText="Capacity" SortExpression="GroupCapacity" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                        </Columns>
                    </Rock:Grid>
                </div>

            </div>

        </asp:Panel>

    </ContentTemplate>
</asp:UpdatePanel>
