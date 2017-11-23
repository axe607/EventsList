function loadCategoriesBar() {
    $("#categories").load("Category/CategoriesBar");
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