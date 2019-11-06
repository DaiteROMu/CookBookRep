function ShowUsersFunction() {    
    var url = "/User/ShowUsers";
    $.ajax(
        {
            url: url,
            dataType: 'html',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('#editor-container').html(data);
            }            
        });
}

function AddOrEditUserFunction(userId) {
    var url = "/User/AddOrEditUser?userId=" + encodeURIComponent(userId);
    $.ajax(
        {
            url: url,
            dataType: 'html',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('#editor-container').html(data);
            }
        });
}

function DeleteUserFunction(userId) {
    var url = "/User/DeleteUser?userId=" + encodeURIComponent(userId);
    $.ajax(
        {
            url: url,
            dataType: 'html',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('#editor-container').html(data);
            }
        });
}