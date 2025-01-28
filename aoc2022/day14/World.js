var world = {
    data: [],

    floorY: 0,

    computeFloorY: function() {
        var maxY = Number.MIN_SAFE_INTEGER;

        this.data.forEach((el, x) => {
            col = this.data[x];

            if (col != undefined) {
                col.forEach((el, y) => {
                    if (y > maxY) maxY = y;
                });
            }
        });

        this.floorY = maxY + 2;
    },

    set: function(x, y, v) {
        if (this.data[x] === undefined) {
            this.data[x] = [];
        }
    
        this.data[x][y] = v;
    },

    get: function(x, y) {

        if (y >= this.floorY) return '#';

        if (this.data[x] === undefined ||
            this.data[x][y] === undefined)
        {
            return ".";
        }
        
        return this.data[x][y];
    },

    firstNonEmptyYBelow: function(sx, sy) {
        var returnValue = this.floorY;

        if (this.data[sx] !== undefined) {
            this.data[sx].forEach((value, y) => {
                if ( y < returnValue && y > sy) {
                    returnValue = y;
                }
            });
        }

        //console.log(`firstNonEmptyYBelow(${sx}, ${sy}) = ${returnValue}`);
        return returnValue;
    },

    dump: function() {
        var minX = Number.MAX_SAFE_INTEGER;
        var minY = Number.MAX_SAFE_INTEGER;

        var maxX = Number.MIN_SAFE_INTEGER;
        var maxY = Number.MIN_SAFE_INTEGER;

        this.data.forEach((el, x) => {
            if (x < minX) minX = x;
            if (x > maxX) maxX = x;

            col = this.data[x];

            if (col != undefined) {
                col.forEach((el, y) => {
                    if (y < minY) minY = y;
                    if (y > maxY) maxY = y;
                });
            }
        });

        // Override the minimum Y --
        // We just want to start the dump at Y = 0
        minY = 0;
        for (var y = minY; y <= maxY; y++) {
            var line = `${y}\t`;
            for (var x = minX; x <= maxX; x++) {
                var v = this.get(x, y);
                line += v;
            }
            console.log(line);
        }
    
    }
};

module.exports = world;