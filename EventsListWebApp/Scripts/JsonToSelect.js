function setCategories(idToAddData, idCategorySelected) {
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
                    if (option.value === idCategorySelected) {
                        option.selected = true;
                    }
                    $("#" + idToAddData).append(option);
                });
        },
        error: function () {
            console.log("Error on getting categories");
        }
    });
}

function setAddresses(idToAddData, idAddressSelected) {
    $.ajax({
        url: "/Json/GetAddresses",
        type: "GET",
        success: function (result) {
            var a = result;
            $.each(a,
                function (key, value) {
                    var option = document.createElement("option");
                    option.value = value["Id"];
                    option.textContent = value["AddressString"];
                    if (option.value === idAddressSelected) {
                        option.selected = true;
                    }
                    $("#" + idToAddData).append(option);
                });
        },
        error: function () {
            console.log("Error on getting addresses");
        }
    });
}