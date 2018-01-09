function setCategories(idToAddData, idCategorySelected, exceptId) {
    $.ajax({
        url: "/Json/GetCategories",
        type: "GET",
        success: function (result) {
            var defaultOption = document.createElement("option");
            defaultOption.value = "";
            defaultOption.textContent = "Default/All";
            $("#" + idToAddData).append(defaultOption);
            $.each(result,
                function (key, value) {
                    if (value["Id"] != exceptId) {
                        var option = document.createElement("option");
                        option.value = value["Id"];
                        if (value["Pid"] === null) {
                            option.textContent = value["Name"];
                        } else {
                            option.textContent = "--- " + value["Name"];
                        }
                        if (option.value === idCategorySelected) {
                            option.selected = true;
                        }
                        $("#" + idToAddData).append(option);
                    }
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

function setRolesNotInUser(idToAddData, userName) {
    $.ajax({
        url: "/Json/GetRolesNotInUser",
        type: "GET",
        data:
        {
            userName: userName
        },
        success: function (result) {
            $("#" + idToAddData).empty();
            $.each(result,
                function (key, value) {
                    var option = document.createElement("option");
                    option.value = value["Id"];
                    option.textContent = value["RoleName"];
                    $("#" + idToAddData).append(option);
                });
        },
        error: function () {
            console.log("Error on getting roles");
        }
    });
}