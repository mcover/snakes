$(function() {
    var headingMap = {
        'up': [0,-1],
        'right': [1, 0],
        'left': [-1, 0],
        'down': [0, 1]
    };
    var $mapContainer = $('#map-container');
    var $outputBox = $('#output-box');

    // drag stuff
    var $lastOver;
    $mapContainer.on('dragover', function(e) {
        $lastOver = $(e.toElement);
    });

    $('#tile-container').find('td').on('dragend', function() {
        var $this = $(this);
        if ($lastOver && $lastOver.length) {
            $lastOver.removeClass();
            $lastOver.attr('class', $this.attr('class'));
            $lastOver.attr('heading', $this.attr('heading'));
        }
    });

    // build the map
    var buildMap = function() {
        var width = Number($('#map-width-input').val());
        var height = Number($('#map-height-input').val());
        $mapContainer.empty();

        for (var j = 0; j < height; j++) {
            var $row = $('<tr />');
            for (var i = 0; i < width; i++) {
                var $box = $('<td />');
                $box.data('x', i);
                $box.data('y', j);
                $row.append($box);
            }
            $mapContainer.append($row);
        }

        $mapContainer.find('td').click(function() {
           $(this).removeClass().addClass('wall').attr('heading', '');
	});
    };

    $('#build-map-btn').click(buildMap);

    var printSetup = function() {
        var width = Number($('#map-width-input').val());
        var height = Number($('#map-height-input').val());
        var output = 'dims,' + width + ',' + height + '\r';
        var objsCount = 0;
        $mapContainer.find('td').each(function() {
            var $this = $(this);
            var classStr = $this.attr('class');
            if (classStr) {
                objsCount += 1;
                var split = classStr.split('-');
                var type = split[0];
                if (type == 'snake') {
                    var color = split[1];
                    var heading = headingMap[$this.attr('heading')];
                    var snakeLength = $('#' + color + '-snake-length-input').val();
                    output += type + ',' + $this.data('x') + ',' + $this.data('y') + ',' + snakeLength + ',' + heading[0] + ',' + heading[1] + ',' + color +  '\r';
                } else if (type == 'goal') {
                    var color = split[1];
                    output += type + ',' + $this.data('x') + ',' + $this.data('y') + ',' + color + '\r';

                } else if (type == 'wall') {
                    output += type + ',' + $this.data('x') + ',' + $this.data('y') + '\r';
                }
            }
        });
        $outputBox.text(output);
        $outputBox.attr('rows', objsCount+3);
    };

    $('#print-map-btn').click(printSetup);
    // Start with default map vals
    buildMap();



});
