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