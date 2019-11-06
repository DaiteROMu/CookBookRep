function ShowIngridientsFunction() {    
    var url = "/Ingridient/ShowIngridients";
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

function AddOrEditIngridientFunction(ingridientId) {
    var url = "/Ingridient/AddOrEditIngridient?ingridientId=" + encodeURIComponent(ingridientId);
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

function DeleteIngridientFunction(ingridientId) {
    var url = "/Ingridient/DeleteIngridient?ingridientId=" + encodeURIComponent(ingridientId);
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