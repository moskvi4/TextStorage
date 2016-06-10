$(document).ready(function () {
    function clearTextsList() {
        $("#searchResultsList").empty();
    }
    function loadSingleText(id, callback) {
        var url = "/odata/Texts(" + id + ")";
        $.getJSON(url, function (text) {
            callback(text);
        });
    }
    function showTermDescriptionsList(termId) {
        $("#termDescriptionsContainer").show();
    }
    $("#hideTermDescriptionsContainer").on("click", function () {
        $("#termDescriptionsContainer").hide();
    });
    function loadTextsList(name) {
        clearTextsList();
        var url = "/odata/Texts";
        var parameters = {
            "$select": "Id,Name"
        };
        if (name) {
            parameters["$filter"] = "substringof('" + name + "',Name)";
        }
        $.getJSON(url, parameters, function (texts) {
            var list = $("#searchResultsList");
            texts.value.forEach(function (text) {
                list.append("<li id='text-" + text.Id + "'>" + text.Name);
            });
            $("#searchResultsList > li").on("click", function () {
                loadSingleText(this.id.substring(5), function (text) {
                    $("#textContent").html(text.TextContent);
                    $(".textContent > span").on("click", function () {
                        showTermDescriptionsList(this.id.substring(5));
                    });
                });
            });
        });
    }

    $("#loadNewTextButton").on("click", function () {
        var textName = $("#newTextName").val();
        if (!textName) {
            alert("Input text name");
            return;
        }
        var data = new FormData();
        data.append("UploadedTextName", textName);
        var files = $("#textUpload").get(0).files;
        if (files.length > 0) {
            data.append("UploadedText", files[0]);
        } else {
            alert("File is not selected");
            return;
        }

        var ajaxRequest = $.ajax({
            type: "POST",
            url: "/odata/Texts",
            contentType: false,
            processData: false,
            data: data
        });

        ajaxRequest.done(function (xhr, textStatus) {
            alert(textStatus);
            $("#newTextName").val("");
            $("#textUpload").val("");
            var name = $("#textName").val();
            loadTextsList(name);
        });
    });
    $("#searchTextButton").on("click", function () {
        var name = $("#textName").val();
        loadTextsList(name);
    });

    loadTextsList();
});