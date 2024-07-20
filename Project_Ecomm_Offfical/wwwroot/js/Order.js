var dataTable;
$(document).ready(function () {
    LoadDataTable();
})
function LoadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Order/GetAll"
        },
        "columns": [
            { "data":"id", "width":"15%"},
            { "data":"orderDate", "width":"15%"},
            { "data":"name", "width":"15%"},
            { "data":"state", "width":"15%"},
            { "data": "city", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                       <div class="text-center">
                       <a href="/Admin/Product/Upsert/${data}" class="btn btn-info">
                       <i class="fas fa-edit"></i>
                       </a>
                       <a class="btn btn-danger"onclick=Delete("/Admin/Product/Delete/${data}")>
                       <i class="fas fa-trash-alt"></i>
                       </a>
                       </div>
                    `;
                }
            }
        ]
    })
}