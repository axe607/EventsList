function setCategories(idToAddData) {
    $.ajax({
        url: "/Json/GetCategories",
        type: "GET",
        success: function (result) {
            var a = result;
            $.each(a,
                function (key, value) {
                    var option = document.createElement("option");
                    option.value = value["Id"];
                    option.textContent = value["Name"];
                    $("#" + idToAddData).append(option);
                });
        },
        error: function (error) {
            console.log(error);
        }
    });
}

function setOrganizers(idToAddData) {
    $.ajax({
        url: "/Json/GetOrganizers",
        type: "GET",
        success: function (result) {
            var a = result;
            $.each(a,
                function (key, value) {
                    var option = document.createElement("option");
                    option.value = value["Id"];
                    option.textContent = value["Name"];
                    $("#" + idToAddData).append(option);
                });
        },
        error: function (error) {
            console.log(error);
        }
    });
}