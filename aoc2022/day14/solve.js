const fs = require('fs');
const readline = require('readline');
const world = require('./World.js');






function handleInputLine(l) {
    var points = l.split(" -> ");

    for (i = 0; i < points.length - 1; i++) {
        var start = points[i].split(',');
        var end = points[i+1].split(',');
        var segmentpoints = handleSegment(
            {x: parseInt(start[0]), y: parseInt(start[1])},
            {x: parseInt(end[0]), y: parseInt(end[1])});
    }
}

// won't work for diagnals only vertical or horizontal
function handleSegment(p1, p2) {
    var magnitude = Math.abs((p2.x - p1.x) + (p2.y - p1.y));
    var step = {x: (p2.x - p1.x)/magnitude, y: (p2.y - p1.y)/magnitude};

    var p = {x: p1.x, y: p1.y};
    world.set(p1.x, p1.y, "#");
    while(p.x != p2.x || p.y != p2.y) {
        p.x += step.x;
        p.y += step.y;
        world.set(p.x, p.y, "#");
    }
}

function executeSolvers() {
    world.computeFloorY();
    world.dump();

    console.log(`floorY = ${world.floorY}`);

    var numDropped = 0;

    while(world.get(500, 0) === '.') {
        dropSand(500,0);
        numDropped++;
        //world.dump();
    }
    world.dump();

    console.log(`Total dropped = ${numDropped}`);
}

//
// Returns true if the dropped sand comes to rest
// Returns false if the dropped sand will fall forever
//
function dropSand(x, y) {
    //console.log(`called dropSand(${x}, ${y})`);
    var miny = world.firstNonEmptyYBelow(x, y);

    if (miny < world.floorY) {
        // there is something directly below, so try to step to the left
        // and find an empty spot just to the left of what's below
        if (world.get(x-1, miny) === '.') {
            return dropSand(x-1, miny);
        }

        // something already to the left at the same level as what's below
        // try right instead
        if (world.get(x+1, miny) === '.') {
            return dropSand(x+1, miny);
        }
    }

    // there's something left-below, direcly-below, and right-below
    // come to rest at x, miny-1
    world.set(x, miny-1, "o");

    return true;
}

var linereader = readline.createInterface({
    input: fs.createReadStream('input.txt')
});
linereader.on('line', handleInputLine);
linereader.on('close', executeSolvers);
