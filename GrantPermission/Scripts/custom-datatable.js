$(document).ready(function ()
{
    $('#TableId').DataTable(
    {
        "columnDefs": [
            { "width": "5%", "targets": [0] },
            { "width": "10%", "searchable": false, "orderable": false,  "targets": [7] },
            { "className": "text-center custom-middle-align", "targets": [0, 1, 2, 3, 4, 5, 6, 7] },
        ],
        "language":
            {
                "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>"
            },
        "processing": true,
        "serverSide": true,
        "ajax":
            {
                "url": "PermissionRequestUI.aspx/GetAllPendingUser",
                "contentType": "application/json",
                "type": "GET",
                "dataType": "JSON",
                "data": function (d)
                {
                    return d;
                },
                "dataSrc": function (json)
                {
                    json.draw = json.d.draw;
                    json.recordsTotal = json.d.recordsTotal;
                    json.recordsFiltered = json.d.recordsFiltered;
                    json.data = json.d.data;

                    var return_data = json;
                    return return_data.data;
                }
            },
        "columns": [
                    { "data": "GrantUserId" },
                    { "data": "RoleName" },
                    { "data": "ModuleName" },
                    { "data": "RequestDate" },
                    { "data": "Action" }
        ]
    });
});
