function getEventById(id) {
    $(".body-content").load('Event/DetailEvent?id=' + id);
}