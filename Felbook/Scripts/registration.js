$(function () {

    //při editování existujícího profilu musím odstranit již zadané tituly ze select elementu
    $("#Title").each(function () {
        var titles = $("#Title").val().split('.');
        var actualTitle = "";

        jQuery.each(titles, function () {
            actualTitle = this;
            $("#selectTitle option").each(function (index) {
                if (actualTitle.toLowerCase() == $(this).attr("value")) {
                    $(this).remove();
                }
            });
        });
    });

    $("#TitleAfter").each(function () {
        var titles = $("#TitleAfter").val().split('.');
        var actualTitle = "";

        //ošetření titulů Ph.D, Th.D, dr. h. c.,
        var pointer = 0;
        jQuery.each(titles, function () {
            if (this == "Ph") {
                titles[pointer] = "ph.d";
                titles[pointer + 1] = "null";
            }
            if (this == "Th") {
                titles[pointer] = "th.d";
                titles[pointer + 1] = "null";
            }
            if (this == "dr") {
                titles[pointer] = "dr.h.c";
                titles[pointer + 1] = "null";
                titles[pointer + 2] = "null";
            }
            pointer++;
        });

        jQuery.each(titles, function () {
            actualTitle = this;
            $("#selectTitleAfter option").each(function (index) {
                if (actualTitle.toLowerCase() == jQuery.trim($(this).attr("value"))) {
                    $(this).remove();
                }
            });
        });
    });
    
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
			'ph.d': 'Ph.D.',
			'th.d': 'Th.D.',
			'csc': 'CSc.',
			'drsc': 'DrSc.',
			'dr.h.c': 'dr. h. c.',
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

