function ShowCategoriesFunction() {
    var url = "/Category/ShowCategories";
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

function AddOrEditCategoryFunction(categoryId) {    
    var url = "/Category/AddOrEditCategory?categoryId=" + encodeURIComponent(categoryId);
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

function DeleteCategoryFunction(categoryId) {
    var url = "/Category/DeleteCategory?categoryId=" + encodeURIComponent(categoryId);
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