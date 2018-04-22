var startLine;
var answers = [];

$(function () {
	$(".field-column__start, .field-column__end").on('mousedown mouseup', function () {
		drawLine($(this));
	});

	$("#captchaAnswerInput").val("");

	var square = new Square("figure-rectangle");
	square.drawPart();

	var squarePart = new Square("figure-rectangle-bord");
	squarePart.drawBigPart();

	var figure = new Figure('figure-round');
	figure.drawBigPart();

	var figurePart = new Figure('figure-round-bord');
	figurePart.drawPart();

	var rhomb = new Rhomb('figure-rhomb');
	rhomb.drawBigPart();

	var rhombPart = new Rhomb('figure-rhomb-bord');
	rhombPart.drawPart();
});



//figure
function Figure(id) {
	var element = document.getElementById(id);
	fitToContainer(element);
	this.ctx = getContext2D(element);
	if (element) {
		this.width = element.width;
		this.heigth = element.height;
		this.centerX = element.width / 2;
		this.centerY = element.height / 2;
	}

	this.draw = function () {
		var ctx = this.ctx;
		if (!ctx) { return; }
		ctx.beginPath();
		ctx.arc(this.centerX, this.centerY, 40, 0, 2 * Math.PI);
		ctx.fill();
	}

	this.drawPart = function () {
		this.draw();
		var ctx = this.ctx;
		if (!ctx) { return; }
		ctx.beginPath();
		ctx.fillStyle = "#FFFFFF";
		ctx.arc(this.centerX, this.centerY, 40, Math.PI / 7, -Math.PI / 7);
		ctx.lineTo(this.centerX, this.centerY);
		ctx.fill();
	}

	this.drawBigPart = function () {
		var ctx = this.ctx;
		if (!ctx) { return; }
		ctx.beginPath();
		ctx.arc(this.centerX, this.centerY, 40, Math.PI / 7, -Math.PI / 7);
		ctx.lineTo(this.centerX, this.centerY);
		ctx.fill();
	}
}

//square
function Square(id) {
	Figure.apply(this, arguments);

	this.drawPart = function () {
		var ctx = this.ctx;
		if (!ctx) { return; }
		var yLength = 3 * this.centerY / 5;
		var x1 = this.centerX - yLength;
		var x2 = this.centerX + yLength;
		var y1 = this.centerY - yLength;
		var y2 = this.centerY + yLength;
		ctx.beginPath();
		ctx.moveTo(x1, y1);
		ctx.lineTo(this.centerX, this.centerY);
		ctx.lineTo(x1, y2);
		ctx.lineTo(x2, y2);
		ctx.lineTo(this.centerX, this.centerY);
		ctx.lineTo(x2, y1);
		ctx.fill();
	}

	this.drawBigPart = function () {
		var ctx = this.ctx;
		if (!ctx) { return; }
		var yLength = 3 * this.centerY / 5;
		var x1 = this.centerX - yLength;
		var x2 = this.centerX + yLength;
		var y1 = this.centerY - yLength;
		var y2 = this.centerY + yLength;
		ctx.beginPath();
		ctx.moveTo(x1, y1);
		ctx.lineTo(this.centerX, this.centerY);
		ctx.lineTo(x2, y1);
		ctx.lineTo(x2, y2);
		ctx.lineTo(this.centerX, this.centerY);
		ctx.lineTo(x1, y2);
		ctx.fill();
	}
}

Square.prototype = Object.create(Figure.prototype);

//rhomb
function Rhomb(id) {
	Figure.apply(this, arguments);

	this.drawPart = function () {
		var ctx = this.ctx;
		if (!ctx) { return; }
		var yLength = 4 * this.centerY / 5;
		var y2 = this.centerY + yLength;
		var x11 = this.centerX - yLength / 2;
		var x21 = this.centerX + yLength / 2;
		var y21 = this.centerY + yLength / 2;

		ctx.beginPath();
		ctx.moveTo(this.centerX, this.centerY);
		ctx.lineTo(x11, y21);
		ctx.lineTo(this.centerX, y2);
		ctx.lineTo(x21, y21);
		ctx.fill();
	}

	this.drawBigPart = function () {
		var ctx = this.ctx;
		if (!ctx) { return; }
		var yLength = 4 * this.centerY / 5;
		var x1 = this.centerX - yLength;
		var x2 = this.centerX + yLength;
		var y1 = this.centerY - yLength;
		var y2 = this.centerY + yLength;
		var x11 = this.centerX - yLength / 2;
		var x21 = this.centerX + yLength / 2;
		var y21 = this.centerY + yLength / 2;

		ctx.beginPath();
		ctx.moveTo(x1, this.centerY);
		ctx.lineTo(this.centerX, y1);
		ctx.lineTo(x2, this.centerY);
		ctx.lineTo(x21, y21);
		ctx.lineTo(this.centerX, this.centerY);
		ctx.lineTo(x11, y21);
		ctx.lineTo(this.centerX, y2);
		ctx.fill();
	}
}

Rhomb.prototype = Object.create(Figure.prototype);

function getContext2D(element) {
	if (element) {
		return element.getContext("2d");
	}
	return null;
}

function fitToContainer(canvas) {
	if (canvas) {
		canvas.style.width = '100%';
		canvas.style.height = '100%';
		canvas.width = canvas.offsetWidth;
		canvas.height = canvas.offsetHeight;
	}

}

function addLine($element) {
	$element.addClass('line-added');
}

function hasLine($element) {
	return $element.hasClass('line-added');
}

function drawLine($element) {
	if (!hasLine($element) && !startLine) {
		startLine = $element;
	} else
		if (!hasLine($element) && startLine && !$element.is(startLine)) {
			jqSimpleConnect.connect(startLine, $element);
			addLine($element);
			addLine(startLine);

			var answer = {
				from: startLine.find("canvas").prop('id'),
				to: $element.find("canvas").prop('id')
			};
			answers.push(answer);
			startLine = null;
		}

	if (checkAllLine()) {
		var jsonAnswer = JSON.stringify(answers);
		$("#captchaAnswerInput").val(jsonAnswer);
	}
}

function checkAllLine() {
	var $figures = $(".field-column__start, .field-column__end");
	$figures.each(function (index, item) {
		if (!hasLine($(item))) {
			return false;
		}
	});

	return true;
}