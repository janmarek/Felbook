$(function () {
	// pictures
	$('#status-pictures-btn').click(function (e) {
		e.preventDefault();

		var number = $('#status-pictures div').size();

		var el = $('<div>' +
			'<table><tr><td>Picture</td><td><input type="file" class="img" name="Images[' + number + ']"></td></tr>' +
			'<tr><td>Description</td><td><input type="text" class="imgdesc" name="ImageDescriptions[' + number + ']"></td></tr></table>' +
			'<p><a href="">Remove</a></p>' +
		'</div>');

		el.find('a').button({ icons: { primary: 'ui-icon-closethick'} }).click(function (e) {
			e.preventDefault();
			var el = $(this).parent().parent();
			var number = el.prevAll().size();
			el.nextAll().each(function () {
				$(this).find(".img").attr("name", "Images[" + number + "]");
				$(this).find(".imgdesc").attr("name", "ImageDescriptions[" + number + "]");
				number++;
			});
			el.fadeOut("fast", function () {
				$(this).remove();
			});
		});

		$('#status-pictures').append(el);

		if ($('#status-pictures-cont').is(':hidden')) {
			$('#status-pictures-cont').slideDown();
		}
	});


	// links
	$('#status-links-btn').click(function (e) {
		e.preventDefault();

		var number = $('#status-links div').size();

		var el = $('<div>' +
			'<table><tr><td>Link</td><td><input type="text" class="link" name="Links[' + number + ']"></td></tr>' +
			'<tr><td>Description</td><td><input type="text" class="linkdesc" name="LinkDescriptions[' + number + ']"></td></tr></table>' +
			'<p><a href="">Remove</a></p>' +
		'</div>');

		el.find('a').button({ icons: { primary: 'ui-icon-closethick'} }).click(function (e) {
			e.preventDefault();
			var el = $(this).parent().parent();
			var number = el.prevAll().size();
			el.nextAll().each(function () {
				$(this).find(".link").attr("name", "Links[" + number + "]");
				$(this).find(".linkdesc").attr("name", "LinkDescriptions[" + number + "]");
				number++;
			});
			el.fadeOut("fast", function () {
				$(this).remove();
			});
		});

		$('#status-links').append(el);

		if ($('#status-links-cont').is(':hidden')) {
			$('#status-links-cont').slideDown();
		}
	});

	// files
	$('#status-files-btn').click(function (e) {
		e.preventDefault();

		var number = $('#status-files div').size();

		var el = $('<div>' +
			'<table><tr><td>File</td><td><input type="file" class="file" name="Files[' + number + ']"></td></tr>' +
			'<tr><td>Description</td><td><input type="text" class="filedesc" name="FileDescriptions[' + number + ']"></td></tr></table>' +
			'<p><a href="">Remove</a></p>' +
		'</div>');

		el.find('a').button({ icons: { primary: 'ui-icon-closethick'} }).click(function (e) {
			e.preventDefault();
			var el = $(this).parent().parent();
			var number = el.prevAll().size();
			el.nextAll().each(function () {
				$(this).find(".file").attr("name", "Files[" + number + "]");
				$(this).find(".filedesc").attr("name", "FileDescriptions[" + number + "]");
				number++;
			});
			el.fadeOut("fast", function () {
				$(this).remove();
			});
		});

		$('#status-files').append(el);

		if ($('#status-files-cont').is(':hidden')) {
			$('#status-files-cont').slideDown();
		}
	});
});