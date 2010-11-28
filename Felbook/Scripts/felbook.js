// všechny stránky - akce po spuštění
$(function () {
	// taby
	$("div.tabs").tabs();

	// "lightbox" galerie
	$("a.colorbox").colorbox();

	// ajax - obnovování počtu nepřečtených věcí
	$('<span class="number">0</span>').appendTo("#wall-link");
	$('<span class="number">0</span>').appendTo("#messages-link");
	loadUnreadNumbers();
	setInterval(loadUnreadNumbers, 20000);
});

// ajax - obnovování počtu nepřečtených věcí
function loadUnreadNumbers() {
	$.getJSON("/Home/UnreadNumbers", function (data) {
	    $("#wall-link .number").html(data.wall)[data.wall > 0 ? "addClass" : "removeClass"]("active");
	    $("#messages-link .number").html(data.messages)[data.messages > 0 ? "addClass" : "removeClass"]("active");
	});
}


$(function () {
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
        var new_row = $("<tr><td><input type=\"file\" id=\"file" + indexFile + "\" name=\"file" + indexFile + "\" /></td><td><textarea id=\"filedescription" + indexFile + "\" name=\"filedescription" + indexFile + "\" rows=\"4\" cols=\"20\"></textarea></td></tr>").hide();
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

//funkce která se zavolá před submitováním formuláře
function formSubmit() {
    $('input[name*="link"]').attr("disabled", false);
};


//REGISTRACE, REGISTRATION
$(function () {
    //přidání titulu do titulů
    $("#addTitle").click(function () {
        $("#Title").val($("#Title").val() + $("#selectTitle option:selected").text());
        $("#selectTitle option:selected").remove();
    });

    //přidání titulu do titulů za jménem
    $("#addTitleAfter").click(function () {
        $("#TitleAfter").val($("#TitleAfter").val() + $("#selectTitleAfter option:selected").text());
        $("#selectTitleAfter option:selected").remove();
    });

    //resetování pole s titulama
    $("#resetTitle").click(function () {
        $("#Title").val("");

        $("#selectTitle option").each(function () {
            $(this).remove();
        });

        var newOptions = {
            'bc': 'Bc.',
            'bca': 'BcA.',
            'ing': 'Ing.',
            'ingarch': 'Ing.arch.',
            'mudr': 'MUDr.',
            'mvdr': 'MVDr.',
            'mga': 'MgA.',
            'mgr': 'Mgr.',
            'judr': 'JUDr.',
            'phdr': 'PhDr.',
            'rndr': 'RNDr.',
            'pharmdr': 'PharmDr.',
            'thlic': 'ThLic.',
            'thdr': 'ThDr.',
            'prof': 'prof.',
            'doc': 'doc.',
            'peadr': 'PaedDr.',
            'dr': 'Dr.',
            'phmr': 'PhMr.'
        };
        var selectedOption = 'bc';
        var select = $('#selectTitle');
        var options = select.attr('options');
        $('option', select).remove();

        $.each(newOptions, function (val, text) {
            options[options.length] = new Option(text, val);
        });
        select.val(selectedOption);
    });

    //resetování pole s titulama za jménem
    $("#resetTitleAfter").click(function () {
        $("#TitleAfter").val("");

        $("#selectTitleAfter option").each(function () {
            $(this).remove();
        });

        var newOptions = {
            'phd': 'Ph.D.',
            'thd': 'Th.D.',
            'csc': 'CSc.',
            'drsc': 'DrSc.',
            'drhc': 'dr. h. c.',
            'dr': 'Dr.',
            'phmr': 'PhMr.',
            'dis': 'DiS.'
        };
        var selectedOption = 'phd';
        var select = $('#selectTitleAfter');
        var options = select.attr('options');
        $('option', select).remove();

        $.each(newOptions, function (val, text) {
            options[options.length] = new Option(text, val);
        });
        select.val(selectedOption);
    });
});


// Funkce pro automatické doplňování uživatelů při posílání zprávy
$(function () {
    function split(val) {
        return val.split(/;\s*/);
    }
    function extractLast(term) {
        return split(term).pop();
    }

    $("#ToUsers").autocomplete({
        minLength: 0,
        source: function (request, response) {
            response($.ui.autocomplete.filter(
					    availableTagsUsers, extractLast(request.term)));
        },
        focus: function () {
            return false;
        },
        select: function (event, ui) {
            var terms = split(this.value);
            terms.pop();
            terms.push(ui.item.value);
            terms.push("");
            this.value = terms.join("; ");
            return false;
        }
    });

    $("#ToGroups").autocomplete({
        minLength: 0,
        source: function (request, response) {
            response($.ui.autocomplete.filter(
					    availableTagsGroups, extractLast(request.term)));
        },
        focus: function () {
            return false;
        },
        select: function (event, ui) {
            var terms = split(this.value);
            terms.pop();
            terms.push(ui.item.value);
            terms.push("");
            this.value = terms.join("; ");
            return false;
        }
    });
});
                
 