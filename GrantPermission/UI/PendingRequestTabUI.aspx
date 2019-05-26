<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PendingRequestTabUI.aspx.cs" Inherits="GrantPermission.UI.PendingRequestTabUI" %>
<%@ MasterType VirtualPath="~/Site.Master" %>  
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--<script src="<%=ResolveUrl("~/Scripts/jquery-3.3.1.js")%>"></script>--%>
    <script src="<%=ResolveUrl("~/Scripts/jquery-ui-1.10.0.js")%>"></script>
    <link href="<%=ResolveUrl("~/Content/jquery-ui.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/DataTables/jquery.dataTables.css")%>" rel="stylesheet" />
    <script src="<%=ResolveUrl("~/DataTables/jquery.dataTables.js")%>"></script>
    <script src="<%=ResolveUrl("~/DataTables/dataTables.jqueryui.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/DataTables/dataTables.responsive.js")%>"></script>
    <link href="<%=ResolveUrl("~/Content/themeroller.css")%>" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {

            $('#tabs').tabs({
                activate: function () {
                    var newIdx = $('#tabs').tabs('option', 'active');
                    $('#<%=hidLastTab.ClientID%>').val(newIdx);

                }, 
                active: previouslySelectedTab,
                show: { effect: "fadeIn", duration: 100 }
            });

        });


</script>


    <div style="margin-top: 25px">
        <%--modal --%>
        <div class="modal fade" id="myModal">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" id="modalHeader" runat="server">
                        <h4><i class="material-icons">block</i></h4>
                        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                             <h4 class="modal-title">
                                                 <asp:Label ID="errorLabel" runat="server" ForeColor="White" /></h4>
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




        <asp:UpdatePanel ID="RequestUpdatePanel" runat="server">
            <ContentTemplate>

                <div id="tabs">

                    <ul>
                        <li><a href="#tabs-1">Pending Requests</a></li>
                        <li><a href="#tabs-2">Rejected Requests</a></li>
                        <li><a href="#tabs-3">Approved User</a></li>
                    </ul>
                    <div id="tabs-1">
                        <%--<asp:GridView ID="pendingRequestsGridView" runat="server"></asp:GridView>--%>
                        <asp:GridView ID="pendingRequestsGridView" runat="server" OnRowDataBound="pendingRequestsGridView_RowDataBound" DataKeyNames="user_id" AutoGenerateColumns="false"
                           CssClass="dt-responsive" OnRowCommand="pendingRequestsGridView_OnRowCommand">
                            <Columns>
                                <asp:BoundField DataField="user_id" HeaderText="User Id" />
                                <asp:BoundField DataField="BRANCH_NM" HeaderText="Branch Name" />
                                <asp:BoundField DataField="role_nm" HeaderText="Role Name" />
                                <asp:BoundField DataField="module_nm" HeaderText="Module Name" />
                                <asp:BoundField DataField="request_date" HeaderText="Request Date" />

                                <asp:TemplateField ShowHeader="true">
                                    <HeaderTemplate >
                                        <div style="width: 220px;">
                                            &nbsp&nbsp
                                            <asp:LinkButton ID="approveAllButton" class="btn btn-success btn-sm  btn-round" runat="server"
                                                CausesValidation="false" CommandName="Approve All"
                                                OnClientClick="return ApproveAllConfirmation();" Text="All"
                                                CommandArgument='<%#Eval("user_id") + ";" +Eval("role_nm")%>' />

                                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                            <asp:LinkButton ID="rejectAllButton" class="btn btn-danger btn-sm  btn-round" runat="server"
                                                CausesValidation="false" CommandName="Reject All"
                                                OnClientClick="return RejectAllConfirmation();" Text="All" CommandArgument='<%#Eval("user_id") + ";" +Eval("role_nm")%>' />
                                        </div>
                                            </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:LinkButton ID="approveButton" class="btn btn-success btn-sm" runat="server"
                                            CausesValidation="false" CommandName="Approve" Font-Size="9px"
                                            Text="Approve" CommandArgument='<%#Eval("user_id") + ";" +Eval("role_nm")%>' />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="rejectButton" class="btn btn-warning btn-sm" runat="server"
                                                CausesValidation="false" CommandName="Reject" Font-Size="9px"
                                                Text="Reject" CommandArgument='<%#Eval("user_id") + ";" +Eval("role_nm")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div id="tabs-2">
                        <%--<asp:GridView ID="rejectedRequestGridView" runat="server"></asp:GridView>--%>
                        <asp:GridView ID="rejectedRequestGridView" runat="server" OnRowDataBound="rejectedRequestGridView_RowDataBound" DataKeyNames="user_id" AutoGenerateColumns="false"
                            OnRowCommand="rejectedRequestGridView_OnRowCommand">
                            <Columns>
                                <asp:BoundField DataField="user_id" HeaderText="User Id" />
                                <asp:BoundField DataField="BRANCH_NM" HeaderText="Branch Name" />
                                <asp:BoundField DataField="role_nm" HeaderText="Role Name" />
                                <asp:BoundField DataField="module_nm" HeaderText="Module Name" />
                                <asp:BoundField DataField="request_date" HeaderText="Request Date" />
                                <asp:TemplateField ShowHeader="true">
                                    <HeaderTemplate>
                                        &nbsp&nbsp
                                            <asp:LinkButton ID="approveAllButton" class="btn btn-success btn-sm  btn-round" runat="server"
                                                CausesValidation="false" CommandName="Approve All"
                                                OnClientClick="return ApproveAllConfirmation();" Text="All"
                                                CommandArgument='<%#Eval("user_id") + ";" +Eval("role_nm")%>' />

                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="approveButton" class="btn btn-success btn-sm" runat="server"
                                             CausesValidation="false" CommandName="Approve" Font-Size="9px"
                                            Text="Approve" CommandArgument='<%#Eval("user_id") + ";" +Eval("role_nm")%>' />
                                        <%-- <asp:LinkButton ID="rejectButton" class="btn btn-warning btn-sm" runat="server" CausesValidation="false" CommandName="Reject"
                                                Text="Reject" CommandArgument='<%#Eval("user_id") + ";" +Eval("role_nm")%>' />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                      <div id="tabs-3">
                            <asp:GridView ID="approvedUserGridView" runat="server" OnRowDataBound="approvedUserGridView_RowDataBound" DataKeyNames="user_id" 
                                AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="user_id" HeaderText="User Id" />
                                    <asp:BoundField DataField="BRANCH_NM" HeaderText="Branch Name" />
                                    <asp:BoundField DataField="role_nm" HeaderText="Role Name" />
                                    <asp:BoundField DataField="module_nm" HeaderText="Module Name" />
                                    <asp:BoundField DataField="request_date" HeaderText="Request Date" />
                                    <asp:BoundField DataField="auth_by" HeaderText="Approved By" />
                                    
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <asp:HiddenField ID="hidLastTab" Value="0" runat="server" />
                    <br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>



        <script type="text/javascript">
            

            function RejectAllConfirmation() {
                return confirm("Do you want to Reject all?");

            };

            function ApproveAllConfirmation() {
                return confirm("Do you want to approve all?");
               //return $.confirm('A message', 'Title is optional');
            };


            $(document).ready(function () {
                $('#<%= pendingRequestsGridView.ClientID%>').DataTable({
                    "bJQueryUI": true,
                    columnDefs: [
                        { orderable: false, targets: [5] }
                        //{ 'width': '14%', targets: [1] },
                    ],
                    language: {
                        searchPlaceholder: "Search here..."
                    }
                    
                });
                $('#<%= rejectedRequestGridView.ClientID%>').DataTable({
                    "bJQueryUI": true,
                    columnDefs: [{ orderable: false, targets: [5] }],
                    language: {
                        searchPlaceholder: "Search here..."
                    }
                });
                $('#<%= approvedUserGridView.ClientID%>').DataTable({
                    "bJQueryUI": true,

                    language: {
                        searchPlaceholder: "Search here..."
                    },
                    "dom": ' <"search"f><"top"l>rt<"bottom"ip><"clear">'
                    });

            });

            var table = $('#approvedUserGridView').DataTable();
            $('#approvedUserGridView tbody').on('click', 'tr', function () {
                if ($(this).hasClass('selected')) {
                    $(this).removeClass('selected');
                }
                else {
                    table.$('tr.selected').removeClass('selected');
                    $(this).addClass('selected');
                }
            });

            $('#approvedUserGridView').click(function () {
                table.row('.selected').remove().draw(false);
            });



        </script>
    <style>
        .odd {
            
        }

        .even {
            
            background-color: #f2f2f2;
        }

        .dataTables_filter label {
            color: black;
        }

        .ui-widget-header {
            border: 1px solid #dddddd;
            color: #333333;
            font-weight: bold;
            background-color: aliceblue;
            padding: 5px;
        }
        table.dataTable tbody tr {
            border: lightblue;
        }
        table.dataTable tbody tr:hover {
            background-color: lightyellow;
        }
        table.dataTable thead th::before {
            content: '';
            display: block;
            min-width: 100px;
        }
    </style>

</asp:Content>
