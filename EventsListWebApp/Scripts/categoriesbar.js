function loadCategoriesBar() {
    $("#categories").load("Category/CategoriesBar", function(response, status, xhr) {
        if (status === "error")
        {
            console.log("[CategoryBar]: "+xhr.status + " - " + xhr.statusText);
            var errorText = document.createElement("b");
            errorText.innerText = "Some errors occurred during getting data. Please, reload page or wait some time.";
            $(this).append(errorText);
        }
    });
}

function getEventsBySubcategoryId(subcategoryId) {
    $(".body-content").load("Event/EventsBySubcategory?subcategoryId=" + subcategoryId);
}
function getEventsByCategoryId(categoryId) {
    $(".body-content").load("Event/EventsByCategory?categoryId=" + categoryId);
}

function getAllEvents() {
    $(".body-content").load("Event/Events");
}