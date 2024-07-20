var dataTable;
$(document).ready(function () {
    LoadDataTable();
})

function LoadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Category/GetAll"
        },
        "columns": [
            { "data": "name", "width": "70%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                       <div class="text-center">
                       <a href="/Admin/Category/Upsert/${data}" class="btn btn-info">
                       <i class="fas fa-edit"></i>
                       </a>
                       <a class="btn btn-danger"onclick=Delete("/Admin/Category/Delete/${data}")>
                       <i class="fas fa-trash-alt"></i>
                       </a>
                       </div>
                    `;
                }
            }
        ]
    })
}
function Delete(url) {
    swal({
        title: "want to delete data",
        text: "Delete information ",
        buttons: true,
        icon: "warning",
        dangerModel: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: url,
                type: "Delete",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}