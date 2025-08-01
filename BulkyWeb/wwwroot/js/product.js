﻿$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    /*dataTable = */
    $('#tblData').DataTable({
        ajax: {
            url: '/admin/product/getall'
        },
        columns: [
            { data: 'id', width: "5%" },
            { data: 'title', "width": "20%" },
            { data: 'isbn', "width": "15%" },
            { data: 'price', "width": "10%" },
            { data: 'author', "width": "15%" },
            { data: 'category.name', width: "15%" },
            {
                data: 'id',
                width: "25%",
                render: function (data) {
                    //return `
                    //    <div class="text-center">
                    //        <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2">
                    //            <i class="bi bi-pencil-square"></i> Edit
                    //        </a>
                    //        <a href="/admin/product/delete?id=${data}" class="btn btn-danger mx-2">
                    //            <i class="bi bi-trash3-fill"></i> Delete
                    //        </a>
                    //    </div>`;
                    return `<div class="w-100 btn-group" role="group">
                        <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                        <a href="/admin/product/delete/${data}" class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>
                    `
                }
            }
        ],

        // pagination settings
        pageLength: 5,
        lengthMenu: [5, 10, 25, 50],
        paging: true,
        info: true,
    });
}