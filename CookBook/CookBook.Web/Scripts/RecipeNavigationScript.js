function ShowAllRecipesFunction() {
    var url = "/Recipe/ShowAllRecipes";
    $.ajax(
        {
            url: url,
            dataType: 'html',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('#main-recipe-container').html(data);
            }
        });
}

function ShowRecipesByCategoryIdFunction(categoryId) {    
    var url = "/Recipe/ShowRecipesByCategoryId?categoryId=" + encodeURIComponent(categoryId);
    $.ajax(
        {
            url: url,
            dataType: 'html',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('#main-recipe-container').html(data);
            }
        });
}

function ShowFullRecipeFunction(recipeId) {
    var url = "/Recipe/ShowFullRecipe?recipeId=" + encodeURIComponent(recipeId);
    $.ajax(
        {
            url: url,
            dataType: 'html',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('#main-recipe-container').html(data);
            }
        });
}

function SearchFunction() {
    var searchRecipeTbx = document.getElementById("searchRecipeTbx");
    var searchCategoryTbx = document.getElementById("searchCategoryTbx");
    var searchIngridientTbx = document.getElementById("searchIngridientTbx");
    var searchRecipeName = searchRecipeTbx.value;
    var searchCategoryName = searchCategoryTbx.value;
    var searchIngridientName = searchIngridientTbx.value;

    if (searchRecipeName !== "" || searchCategoryName !== "" || searchIngridientName !== "")
    {
        var url = "/Recipe/ShowSearchResult?searchRecipeName=" + encodeURIComponent(searchRecipeName) + "&searchCategoryName=" + encodeURIComponent(searchCategoryName) + "&searchIngridientName=" + encodeURIComponent(searchIngridientName);
        $.ajax(
            {
                url: url,
                dataType: 'html',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    $('#main-recipe-container').html(data);
                }
            });
    }    
}

function AddOrEditRecipeFunction(recipeId) {
    var url = "/Recipe/AddOrEditRecipe?recipeId=" + encodeURIComponent(recipeId);
    $.ajax(
        {
            url: url,
            dataType: 'html',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('#main-recipe-container').html(data);
            }
        });
}

function DeleteRecipeFunction(recipeId) {
    var url = "/Recipe/DeleteRecipe?recipeId=" + encodeURIComponent(recipeId);
    $.ajax(
        {
            url: url,
            dataType: 'html',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('#main-recipe-container').html(data);
            }
        });
}

function ShowIngridientsByRecipeIdFunction(recipeId) {
    var url = "/Recipe/ShowIngridientsByRecipeId?recipeId=" + encodeURIComponent(recipeId);
    $.ajax(
        {
            url: url,
            dataType: 'html',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('#main-recipe-container').html(data);
            }
        });
}

function AddOrEditRecipeIngridientFunction(recipeId, ingridientId) {
    var url = "/Recipe/AddOrEditRecipeIngridient?recipeId=" + encodeURIComponent(recipeId) + "&ingridientId=" + encodeURIComponent(ingridientId);
    $.ajax(
        {
            url: url,
            dataType: 'html',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('#main-recipe-container').html(data);
            }
        });
}

function DeleteRecipeIngridientFunction(recipeId, ingridientId) {
    var url = "/Recipe/DeleteRecipeIngridient?recipeId=" + encodeURIComponent(recipeId) + "&ingridientId=" + encodeURIComponent(ingridientId);
    $.ajax(
        {
            url: url,
            dataType: 'html',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('#main-recipe-container').html(data);
            }
        });
}

function ShowSequencingByRecipeIdFunction(recipeId) {
    var url = "/Recipe/ShowSequencingByRecipeId?recipeId=" + encodeURIComponent(recipeId);
    $.ajax(
        {
            url: url,
            dataType: 'html',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('#main-recipe-container').html(data);
            }
        });
}