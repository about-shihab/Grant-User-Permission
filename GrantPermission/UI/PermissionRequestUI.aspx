<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PermissionRequestUI.aspx.cs" Inherits="GrantPermission.UI.PermissionRequestUI" %>
<%@ MasterType VirtualPath="~/Site.Master" %>  
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--<script src="<%=ResolveUrl("~/Scripts/jquery-ui-1.10.0.js")%>"></script>--%>



    <div>

            <%--modal --%>
    <div class="modal fade" id="myModal" >
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" id="modalHeader" runat="server">
                    <h4><i class="material-icons">block</i></h4>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    <h4 class="modal-title"><asp:Label ID="errorLabel" runat="server" ForeColor="White" /></h4>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <script type="text/javascript">
        function ShowPopup() {
            $("#btnShowPopup").click();
        }
    </script>
    <button type="button" style="display: none;" id="btnShowPopup" class="btn btn-primary btn-lg"
        data-toggle="modal" data-target="#myModal">
        Launch demo modal
    </button>

    <%--modal --%>



        <div class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-2"></div>
                    <div class="col-md-8">
                        <div class="card">
                            <div class="card-header card-header-success" style="background: seagreen">
                                <h5 class="card-title">Request For Permission</h5>
                            </div>
                            <div class="card-body">
                                <br />
                                <div class="row">
                                    <div class="col-md-4 control-label" style="text-align:right">
                                        <strong>
                                            <asp:Label ID="grantUserIdLabel" runat="server" Text="User Id"></asp:Label></strong>
                                    </div>
                                    <div class="col-md-6 control-label">
                                        <h4 ><span class="badge  badge-secondary" style="background-color:teal">
                                            <asp:Label ID="userIdLabel" runat="server" Text=""></asp:Label></span></h4>

                                    </div>
                                    <div class="col-md-2"></div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-4 control-label" style="text-align:right">
                                        <strong>
                                            <asp:Label ID="moduleLabe" runat="server" Text="Module"></asp:Label></strong>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownListChosen ID="moduleDropDownList" runat="server"
                                            NoResultsText="No results match." Width="320px" Height="500px"
                                            DataPlaceHolder="Select Module..." AllowSingleDeselect="true"
                                            OnSelectedIndexChanged="moduleDropDownList_SelectedIndexChanged"
                                             AutoPostBack="true" required>
                                        </asp:DropDownListChosen>
                                    </div>
                                    <div class="col-md-2"></div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-4 control-label" style="text-align:right">
                                        <strong>
                                            <asp:Label ID="roleLabe" runat="server" Text="Role"></asp:Label></strong>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownListChosen ID="roleDropDownList" runat="server" 
                                            NoResultsText="No results match." Width="320px" Height="500px" 
                                            DataPlaceHolder="Select Role..." AllowSingleDeselect="true" required>
                                        </asp:DropDownListChosen>
                                    </div>
                                    <div class="col-md-2"></div>

                                </div>
                                <br />

                                <div class="row">
                                    <div class="col-md-7"></div>
                                    <div class="col-md-3">
                                        <asp:Button ID="submitRequest" runat="server" Text="Submit Request" Class="btn btn-success" Style="background: seagreen" OnClick="submitRequest_Click" />

                                    </div>
                                    <div class="col-md-1"></div>

                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>