$(document).ready(function () {
	$("#numericKeyboard :button")
		.click(function () {
			var self = $(this);
			console.log(self);
			console.log(self[0].value);
		});
})