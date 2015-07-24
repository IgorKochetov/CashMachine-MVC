$(document).ready(function() {
	$("#numericKeyboard :button")
		.click(function() {
			var self = $(this);
			
			produceOutput(self[0].value);
		});

	$("#formFullReset")
		.click(function() {
			var inputsToClear = $("input.form-control");
			inputsToClear.val("");
		});

});

function produceOutput(value) {
	// manually wiring all components which want to react to keyboard input
	// we can't do one size fits all solution since we need to have more control over how inputs are formatted
	cardDisplayNumberAddToInput(value);
	cardActualNumberAddToInput(value);
	pinNumberAddToInput(value);
	withdrawAmountAddToInput(value);
}

function cardDisplayNumberAddToInput (value){
	// update display number
	var display = $("#cc_number_display");
	if (display[0] === undefined) return;
	
	var currentVal = display.val();
	var currentLength = currentVal.length;
	if (currentLength === 19) return; // reached max size of a 16 char card number

	if (currentLength === 4 || currentLength === 9 || currentLength === 14) {
		currentVal = currentVal + "-";
	}
	display.val(currentVal + value);
}

function cardActualNumberAddToInput(value) {
	// update real number
	var real = $("#cc_number");
	if (real[0] === undefined) return;

	var currentVal = real.val();
	var currentLength = currentVal.length;
	if (currentLength === 16) return; // reached max size of a 16 char card number

	real.val(real.val() + value);
}

function pinNumberAddToInput(value) {
	// update pin number input
	var pinInput = $("#cc_pin");
	if (pinInput[0] === undefined) return;
	if (pinInput.val().length === 4) return; // no more than 4 digits for the pin
	pinInput.val(pinInput.val() + value);
}

function withdrawAmountAddToInput(value) {
	var withdrawInput = $("#withdrawAmountInput");
	if (withdrawInput[0] === undefined) return;
	withdrawInput.val(withdrawInput.val() + value);
}
