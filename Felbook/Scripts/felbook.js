$(document).ready(function () {

    //přidání elementu pro upload obrázku
    $("#addImg").click(function () {
        var new_row = $("<tr><td><input type=\"file\" id=\"picture" + indexImg + "\" name=\"picture" + indexImg + "\" /></td><td><textarea id=\"description" + indexImg + "\" name=\"description" + indexImg + "\" rows=\"4\" cols=\"20\"></textarea></td></tr>").hide();
        $("#imageInput:first").append(new_row);
        new_row.show("slow");
        indexImg++;
    });

    //odebrání elementu pro upload obrázku
    $("#removeImg").click(function () {
        if ($("#imageInput tr").size() < 4) { //abych neodstraňoval řádky nad obrázky
            return;
        }
        var old_row = $("#imageInput tr:last");
        old_row.fadeOut(300, function () { $(this).remove(); });
        indexImg--;
    });

    //přidání elementu pro upload souboru
    $("#addFile").click(function () {
        var new_row = $("<tr><td><input type=\"file\" id=\"file" + indexFile + "\" name=\"file" + indexFile + "\" /></td><td><textarea id=\"filedescription" + indexFile + "\" name=\"description" + indexFile + "\" rows=\"4\" cols=\"20\"></textarea></td></tr>").hide();
        $("#fileInput:first").append(new_row);
        new_row.show("slow");
        indexFile++;
    });

    //odebrání elementu pro upload souboru
    $("#removeFile").click(function () {
        if ($("#fileInput tr").size() < 3) { //abych neodstraňoval řádky nad obrázky
            return;
        }
        var old_row = $("#fileInput tr:last");
        old_row.fadeOut(300, function () { $(this).remove(); });
        indexFile--;
    });


});

var indexImg = 2; //index pro označení elementů jejich jednoznačné name a id - obrázek
var indexFile = 2; //index pro označení elementů jejich jednoznačné name a id - soubor

var linkIndex = 1;
function AddLink(ajaxResponse) {
    var response = ajaxResponse.get_response().get_object();
    if (response == "null") {
        //zde jeste udelam aby byl textbox na pridavani enabled
        return;
    }
    var new_link;
    $("span").remove();
    var ele = document.getElementById("newLink");
    if (response == "error") {
        new_link = $("<span style=\"color: red;\">" + ele.value + " is not valid link</span>").hide();
    } else {
        new_link = $("<tr><td><input type=\"text\" name=\"link\" id=\"link\" disabled=\"disabled\" value=\"" + response + "\" /></td><td><input type=\"button\" onclick=\"$(this).parent().parent().fadeOut(300, function () { $(this).remove(); });\" value=\"delete\" /></td></tr>").hide();
    }
    $("#links").prepend(new_link);
    new_link.show("slow");
    $("#newLink").val("");
};

//funkce která se zavolá po předsubmitováním formuláře
function formSubmit() {
    $('input[name*="link"]').attr("disabled", false);
};