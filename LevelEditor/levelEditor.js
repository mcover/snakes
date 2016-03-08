$(function() {
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
    };

    $('#build-map-btn').click(buildMap);

    var printSetup = function() {
        var output = '';
        var objsCount = 0;
        $mapContainer.find('td').each(function() {
            var $this = $(this);
            if ($this.attr('class')) {
                objsCount += 1;
                output += $this.attr('class') + ' ' + $this.data('x') + ' ' + $this.data('y') + '\r';
            }
        });
        $outputBox.text(output);
        $outputBox.attr('rows', objsCount);
    };

    $('#print-map-btn').click(printSetup);
    // Start with default map vals
    buildMap();



});
