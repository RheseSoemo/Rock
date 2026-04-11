<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RS-SmallGroupDetail.ascx.cs" Inherits="RockWeb.Blocks.Utility.RSSmallGroupDetail" %>

<asp:UpdatePanel ID="upnlContent" runat="server">
    <ContentTemplate>

        <asp:Panel ID="pnlView" runat="server" CssClass="panel panel-block">

            <div class="panel-heading">
                <h1 class="panel-title">
                    <i class="fa fa-users"></i>
                    <asp:Literal ID="lTitle" runat="server" Text="Group Detail" />
                </h1>
            </div>
            <Rock:PanelDrawer ID="pdAuditDetails" runat="server"></Rock:PanelDrawer>
            <div class="panel-body">

                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="control-label">Name</label>
                            <p class="form-control-static"><asp:Literal ID="lName" runat="server"></asp:Literal></p>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label class="control-label">Description</label>
                            <p class="form-control-static"><asp:Literal ID="lDescription" runat="server"></asp:Literal></p>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Date Created</label>
                            <p class="form-control-static"><asp:Literal ID="lDateCreated" runat="server"></asp:Literal></p>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Date Modified</label>
                            <p class="form-control-static"><asp:Literal ID="lDateModified" runat="server"></asp:Literal></p>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="control-label">Group Capacity</label>
                            <p class="form-control-static"><asp:Literal ID="lGroupCapacity" runat="server"></asp:Literal></p>
                        </div>
                    </div>
                </div>

            </div>

        </asp:Panel>

    </ContentTemplate>
</asp:UpdatePanel>
