var filesData = [];
var svg;
var ontology = ['документ', 'акт', 'договор', 'дои', 'дпр', 'ду', 'жилой объект', 'мкд', 'общежитие', 'нежилой объект', 'музей', 'театр', 'объект', 'техническое строение', 'водоочистное сооружение', 'газораспределительная станция', 'котельная', 'электростанция', 'учебное заведение', 'университет','школа', 'институт', 'организация', 'государственная', 'частная', 'омс', 'рокр', 'ип', 'рсо', 'ук',
'показание', 'опрос', 'прибор', 'система', 'учет', 'сервер','синхронизация', 'пользователь', 'данные', 'модель', 'система'];

$(function() {
	//drop-zone
	var dropZone = document.getElementById('drop-zone');
	dropZone.addEventListener('dragover', handleDragOver, false);
    dropZone.addEventListener('drop', handleFileSelect, false);
	
	//Switch between results
	function chooseResult() {
		$('#list li').on('click', function() {
			for(var i = 0; i < filesData.length; i++) {
				if(filesData[i].fileName == $(this).data('filename')) {
					//Display result on chart
					displayResult(JSON.parse(filesData[i].contents).data);
				}
			}
		})
	}
	
	//Drop event
	function handleFileSelect(e) {
		e.stopPropagation();
		e.preventDefault();
		
		var files = e.dataTransfer.files;
		var filesList = [];
	    filesData = [];
		
		if(files) {
			for(var i = 0, f; f = files[i]; i++) {
				var r = new FileReader();
				r.onload = (function(f) {
					return function(e) {
						var contents = e.target.result;
						filesData.push({
							fileName: escape(f.name),
							contents: contents		
						});
					};
				})(f);
				r.readAsText(f);
				filesList.push('<li data-filename="' + escape(f.name) + '"><b>' + escape(f.name) + '</b></li>');
			}
		}
		$('#list > ul').html(filesList.join(''));
		chooseResult();
	}
	
	//Drag over event 
	function handleDragOver(e) {
		e.stopPropagation();
		e.preventDefault();
		e.dataTransfer.dropEffect = 'copy';
	}
 	
	//Displaying result on chart
	function displayResult(data) {
		//Delete all
		svg.select('.data').html('');
		
		//Create circles
		svg.select('.data')
			.selectAll("circle")
			.data(data)
			.enter()
				.append("circle")
				.attr("cx", function (d) {
					return xScale(d.values[0]);
				})
				.attr("cy", function (d) {
					return yScale(d.values[1]);
				})
				.attr("r", function (d) {
					return rScale(d.values[1]);
				})
			.on('mouseover', function(d) {
				displayPointData(d);
			});
		
		//Create labels
		svg.select('.data')
			.selectAll("text")
			.data(data)
			.enter()
				.append("text")
				.text(function (d) {
					//if(checkOntology(d.digram))
						return d.digram;
				})
				.attr("x", function (d) {
					return xScale(d.values[0]) + 5;
				})
				.attr("y", function (d) {
					return yScale(d.values[1]) - 5;
				})
				.attr("font-family", "sans-serif")
				.attr("font-size", "12px")
				.attr("fill", function(d) {
					if(checkOntology(d.digram))
						return 'red';
					else
						return 'black';
				})
			.on('mouseover', function(d) {
				displayPointData(d);
				d3.select(this)
					.attr('font-size', '30px');
			})
			.on('mouseout', function(d) {
				d3.select(this)
					.attr('font-size', '12px');
			});
	}
	
	//Checks whether the word is termin from ontology or not
	function checkOntology(str) {
		for (var i = 0; i < ontology.length; i++)
			if(str.includes(ontology[i]))
				return true;
		return false;
	}
	
	function displayPointData(d) {
		$('#popup').text(d.digram);
		if(checkOntology(d.digram))
			$('#popup').css('color', 'red');
		else
			$('#popup').css('color', 'black');
	}
	
	var width = $('#view').width();
	var height = $('#view').height();
	var padding = 50;
	
	//Scales
    var xScale = d3.scale.linear()
                         .domain([0, 1])
                         .range([padding, width - padding * 2]);
    var yScale = d3.scale.linear()
                         .domain([0, 1])
                         .range([height - padding, padding]);
    var rScale = d3.scale.linear()
                         .domain([0, 0.05])
                         .range([5, 5]);
	
	//Defines X axis
    var xAxis = d3.svg.axis()
                      .scale(xScale)
                      .orient("bottom")
                      .ticks(10);
    //Defines Y axis
    var yAxis = d3.svg.axis()
                      .scale(yScale)
                      .orient("left")
                      .ticks(10);
	
	 //Create SVG element
    svg = d3.select("#view")
		.append("svg")
		.attr("width", width)
		.attr("height", height);
		   
	//Create X axis
    svg.append("g")
        .attr("class", "axis")
        .attr("transform", "translate(0," + (height - padding) + ")")
        .call(xAxis);

    //Create Y axis
    svg.append("g")
        .attr("class", "axis")
        .attr("transform", "translate(" + padding + ",0)")
        .call(yAxis);
		
	//Creates container for data
	svg.append('g')
		.attr('class', 'data');
})