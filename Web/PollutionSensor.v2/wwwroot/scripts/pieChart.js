function createPie(value, maxValue, label, containter, color) {
    console.log("value:" + value);
    console.log("label:" + label);
    console.log("containter:" + containter);

    if (value > maxValue) {
        console.log("old maxValue:" + maxValue);
        maxValue = value;
        console.log("new maxValue:" + maxValue);
    }

    pie1 = new RGraph.SVG.Pie({
        id: containter,
        data: [value, maxValue - value],
        options:
        {
            colors: [color, 'transparent'],
            donut: true,
            donutWidth: 20
        }
    }).on('draw', function (obj) {
        RGraph.SVG.create({
            svg: obj.svg,
            type: 'circle',
            parent: obj.layers.background1,
            attr: {
                cx: obj.centerx,
                cy: obj.centery,
                r: obj.radius,
                fill: 'gray'
            }
        });
        RGraph.SVG.create({
            svg: obj.svg,
            type: 'circle',
            parent: obj.layers.background1,
            attr: {
                cx: obj.centerx,
                cy: obj.centery,
                r: obj.radius - obj.properties.donutWidth,
                fill: 'black'
            }
        });

        // Add the text label
        RGraph.SVG.text({
            object: obj,
            parent: obj.svg.all,
            text: obj.data[0],
            x: obj.centerx,
            y: obj.centery,
            halign: 'center',
            valign: 'center',
            size: 45,
            bold: true,
            color: '#999'
        });

        // Add the text label of the name
        RGraph.SVG.text({
            object: obj,
            parent: obj.svg.all,
            text: label,
            x: obj.centerx,
            y: obj.centery + 20,
            halign: 'center',
            valign: 'top',
            size: 16,
            bold: true,
            color: '#999'
        });
    }).roundRobin({ frames: 45 });
};