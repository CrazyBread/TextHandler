$(function() {
	var w = $('#view').width();
	var h = $('#view').height();
	var padding = 8;
	var scale_padding = 20; // space to show axes

	var dataset = [
	 [80, 20, 10, 60] ];  // x, y, w, h

	//Axis scales
	var xScale = d3.scale.linear()
		.domain([0, d3.max(dataset, function (d) {
			return d;
		})])
		.range([padding + scale_padding, w - padding]);
	var yScale = d3.scale.linear()
		.domain([0, d3.max(dataset, function (d) {
			return d;
		})])
		.range([h - padding - scale_padding, padding]);

	//Defines Ox axis
	var xAxis = d3.svg.axis()
		.scale(xScale)
		.orient("bottom")
		.ticks(5);

	//Defines Oy axis
	var yAxis = d3.svg.axis()
		.scale(yScale)
		.orient("left")
		.ticks(5);

	//Creates SVG element
	var svg = d3.select("#view")
		.append("svg")
		.attr("width", w)
		.attr("height", h);

	//displaying data
	
	
	//Create circles
	svg.selectAll("rect")
		.data(dataset)
		.enter()
		.append("rect")
/*
Your y-axis is -linear and SVG doesn't accept negative widths, not does it accept x2/y2 < x1/y1 if you
would use <rect x1 y1 x2 y2> instead, so coordinate pair flipping is in order...

This is the way to do it, particularly important when your scales are not linear (but, say, logarithmic),
but keep in mind that YOU must still guarantee that those width & hieight values are always positive!

Of course, such work might best be done in the preparation phase rather than right here.
*/
    .attr("x", function (d) {
        var x = Math.min(xScale(d[0]), xScale(d[0] + d[2]));
        return x;
    })
    .attr("y", function (d) {
        var y = Math.min(yScale(d[1]), yScale(d[1] + d[3]));
        return y;
    })
    .attr("width", function (d) {
        // return xScale(d[2]);  -- only works like that for +linear scale;
        var x2 = Math.max(xScale(d[0]), xScale(d[0] + d[2]));
        var x1 = Math.min(xScale(d[0]), xScale(d[0] + d[2]));
        return x2 - x1;
    })
    .attr("height", function (d) {
        var y2 = Math.max(yScale(d[1]), yScale(d[1] + d[3]));
        var y1 = Math.min(yScale(d[1]), yScale(d[1] + d[3]));
        return y2 - y1;
    });

	//Create X axis
	svg.append("g")
		.attr("class", "axis")
		.attr("transform", "translate(0," + (h - padding - scale_padding) + ")")
		.call(xAxis);
	//Create Y axis
	svg.append("g")
		.attr("class", "axis")
		.attr("transform", "translate(" + (padding + scale_padding) + ",0)")
		.call(yAxis);
});