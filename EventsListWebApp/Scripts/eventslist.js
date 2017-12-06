function getEventById(id) {
    $(".body-content").load('/Event/DetailEvent?id=' + id);
}

function setEventsBySearch(categoryId, date, state) {
    $.ajax({
        url: '/Event/EventsBySearch',
        type: "Get",
        data:
        {
            categoryId: categoryId,
            date: date,
            state: state
        },
        success: function (result) {
            if (result != null) {
                console.log(result);
                $(".body-content").html(result);
            }
        },
        error: function(error) {
            console.log(error);
        }
    });
}