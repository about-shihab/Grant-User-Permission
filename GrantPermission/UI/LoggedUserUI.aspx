<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master"  CodeBehind="LoggedUserUI.aspx.cs" Inherits="GrantPermission.UI.LoggedUserUI" %>

<%@ MasterType VirtualPath="~/Site.Master" %>  
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--modal for grant user--%>
    <div class="modal fade" id="myModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Please Select an option below</h4>&nbsp&nbsp&nbsp&nbsp&nbsp
                   <%-- <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>--%>
                </div>
                <div class="modal-footer">
                    <a href="../UI/PermissionRequestUI.aspx" class="btn btn-success" role="button">
                        <i class="material-icons">reply</i> Request for Permission
                    </a>
                    
                    &nbsp&nbsp
                    <a href="../UI/PendingRequestTabUI.aspx" class="btn btn-danger" role="button">
                        <i class="material-icons">redo</i> Grant Permission
                    </a>
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
        data-toggle="modal" data-target="#myModal" data-backdrop="static" data-keyboard="false">
        Launch demo modal
    </button>

    <%--modal for grant user--%>
</asp:Content>
