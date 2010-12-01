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

	// flash message
	$('.flash').click(function () {
		$(this).fadeOut("fast", function () {
			$(this).remove();
		});
	});

	// ajax který reaguje zprávou (flash message)
	$('a.ajax-default').click(function (e) {
		e.preventDefault();
		$.getJSON(this.href, defaultAjaxCallback);
	});
});

// ajax - obnovování počtu nepřečtených věcí
function loadUnreadNumbers() {
	$.getJSON("/Home/UnreadNumbers", function (data) {
	    $("#wall-link .number").html(data.wall)[data.wall > 0 ? "addClass" : "removeClass"]("active");
	    $("#messages-link .number").html(data.messages)[data.messages > 0 ? "addClass" : "removeClass"]("active");
	});
}

// zobrazení zprávy v hlavičce
function defaultAjaxCallback(data) {
	if (data.flash) {
		$('#flash').empty();

		var message = $('<p class="flash" />').text(data.flash.message).addClass(data.flash.type).click(function () {
			$(this).fadeOut("fast", function () {
				$(this).remove();
			});
		});

		$('#flash').append(message.hide());

		message.slideDown();
	}
}