import { readLines } from '../util/input.js';

export function solve(filename) {
    const input = readLines(filename);
    const data = input.map((line) => line.split(''));
    const grid = makeGrid(data);

    const loads = new Map();

    grid.setOrientation('up');

    const iter = 300;
    for (let i = 0; i<iter; i++) {

        tiltGrid(grid);
        grid.setOrientation('left');
        tiltGrid(grid);
        grid.setOrientation('down');
        tiltGrid(grid);
        grid.setOrientation('right');
        tiltGrid(grid);
        grid.setOrientation('up');

        const load = calcLoad(grid);
        if (loads.has(load)) {
            loads.set(load, loads.get(load).concat([i]));
        } else {
            loads.set(load, [i]);
        }
    }

    // 95736

    // There must be a better way to do this, but -- ran the upper loop until the list map
    // of unique load values stabilized.
    // Then lookedfor one where 1000000000 - the last iteration number - 1 was evenly divisible
    // by the repeat cycle of the load value.
    // There were two matching values, one of which was the right answer.
    for (const [key, value] of loads.entries()) {
        if (value.length > 1) {
            const delta = value[value.length-1] - value[value.length-2];

            const last = value[value.length-1];

            const num = (1000000000 - last - 1)/delta;

            if (num % 1 === 0) {
                console.log(`${key} -- Delta: ${delta},  ${last},  ${num}`);
            }
        }
    }


    const part1 = calcLoad(grid);
    const part2 = calcLoad(grid);

    return [part1, part2];
}

function makeGrid(data) {
    return {
        data: data,
        orientation: 'up',
        setOrientation: function (orientation) {
            this.orientation = orientation;
        },
        xp: function (x, y) {
            const w = this.data[0].length;

            if (this.orientation == 'up') {
                return x;
            } else if (this.orientation == 'down') {
                return w-x-1;
            } else if (this.orientation == 'left') {
                return y;
            } else if (this.orientation == 'right') {
                return w-y-1;
            }
        },
        yp: function (x, y) {
            const h = this.data.length;

            if (this.orientation == 'up') {
                return y;
            } else if (this.orientation == 'down') {
                return h-y-1;
            } else if (this.orientation == 'left') {
                return h-x-1;
            } else if (this.orientation == 'right') {
                return x;
            }
        },
        get: function(x, y) {
            const xp = this.xp(x, y);
            const yp = this.yp(x, y);
            return data[yp][xp];
        },
        set: function (x, y, val) {
            const xp = this.xp(x, y);
            const yp = this.yp(x, y);
            data[yp][xp] = val;
        },
        getWidth: function () {
            return this.orientation == 'up' || this.orientation == 'down'
                ? data[0].length
                : data.length;
        },
        getHeight: function () {
            return this.orientation == 'up' || this.orientation == 'down'
                ? data.length
                : data[0].length;
        },
        log: function() {
            const w = this.getWidth();
            const h = this.getHeight();
            
            for(let y = 0; y < h; y++) {
                for(let x = 0; x < w; x++) {
                    process.stdout.write(this.get(x, y));
                }
                process.stdout.write('\n');
            }
        },
        toString: function() {
            const str = data.map((line) => line.join('')).join('|');
            return `${this.orientation}|${str}\n`;
        }
    };
}

function tiltGrid(grid) {

    for (let x = 0; x < grid.getWidth(); x++) {
        let open = -1;

        for (let y = 0; y < grid.getHeight(); y++) {
            if (grid.get(x, y) === '.' && open === -1) {
                open = y;
            } else if (grid.get(x, y) === '#') {
                open = -1;
            } else if (grid.get(x, y) === 'O') {
                if (open > -1) {
                    grid.set(x, open, 'O');
                    grid.set(x, y, '.');
                    open++;
                }
            }
        }
    }
}

function calcLoad(grid) {
    let load = 0;
    for (let y = 0; y < grid.getHeight(); y++) {
        for (let x = 0; x < grid.getWidth(); x++) {
            if (grid.get(x, y) === 'O') {
                load += grid.getHeight() - y;
            }
        }
    }

    return load;
}

console.log(solve('day14/input.txt'));
