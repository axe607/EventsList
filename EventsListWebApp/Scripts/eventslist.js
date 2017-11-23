function getEventById(id) {
    $(".body-content").load('@Url.Action("DetailEvent","Event")?id=' + id);
}