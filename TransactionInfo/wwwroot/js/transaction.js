var dataTable;
$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/TransactionsData/GetAll' },
        "columns": [
            { data: 'product_Name', "width": "30%" },
            { data: 'transaction_Values', "width": "30%" },
            { data: 'transaction_Count', "width": "30%" },
        ]
    });
}