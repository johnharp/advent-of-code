import { readLines } from '../util/input.js';

export function solve(filename) {
    const grid = makeGrid(filename);


    const start = grid.findStart();
    let prev = start;
    const startConns = grid.connections(start);
    let curr = startConns[0];
    const loop = new Set();
    loop.add(`${start[0]}|${start[1]}`);

    while(!grid.eq(curr, start)) {
        loop.add(`${curr[0]}|${curr[1]}`);
        const conns = grid.connections(curr);
        const openConns = conns.filter(c => !grid.eq(c, prev));

        if (openConns.length != 1) console.error("expected 1 connection!");
        prev = curr;
        curr = openConns[0];
    }
    
    const part1 = loop.size / 2;



    // determine start character
    let starttype = "S";
    let up = grid.add(start, [0, -1]);
    let right = grid.add(start, [1, 0]);
    let down = grid.add(start, [0, 1]);
    let left = grid.add(start, [-1, 0]);

    if (grid.connectsDown(up) && grid.connectsLeft(right)) {
        starttype = "L";
    } else if (grid.connectsLeft(right) && grid.connectsRight(left)) {
        starttype = "-";
    } else if (grid.connectsLeft(right) && grid.connectsUp(down)) {
        starttype  = "F";
    } else if (grid.connectsDown(up) && grid.connectsRight(left)) {
        starttype = "J";
    } else if (grid.connectsRight(left) && grid.connectsUp(down)) {
        starttype = "7";
    } else if (grid.connectsDown(up) && grid.connectsUp(down)) {
        starttype = "|";
    }

    grid.data[start[1]] = grid.data[start[1]].replace("S", starttype);

    let numInside = 0;

    for (let y = 0; y < grid.height; y++) {
        const output = [];

        let state = "O";

        for (let x = 0; x < grid.width; x++) {
            if (loop.has(`${x}|${y}`)) {
                let symbol = grid.get([x,y]);
                output.push(symbol);

                if (symbol === "|") {
                    state = state === "O" ? "I" : "O";
                } else if (symbol === "F") {
                    x++;
                    symbol = grid.get([x, y]);
                    output.push(symbol);
                    while (symbol === "-") {
                        x++;
                        symbol = grid.get([x, y]);
                        output.push(symbol);
                    }

                    if (symbol === "7") {
                        // no-op
                    } else if (symbol === "J") {
                        state = state === "O" ? "I" : "O";
                    }
                } else if (symbol === "L") {
                    x++;
                    symbol = grid.get([x, y]);
                    output.push(symbol);
                    while (symbol === "-") {
                        x++;
                        symbol = grid.get([x, y]);
                        output.push(symbol);
                    }

                    if (symbol === "J") {
                        // no-op
                    } else if (symbol === "7") {
                        state = state === "O" ? "I" : "O";
                    }
                }
            } else {
                if (state === "I") {
                    output.push("I");
                    numInside++;
                } else {
                    output.push(" ");
                }
            }
        }
        // uncomment to log the loop to console
        console.log(output.join(""));
    }

    const part2 = numInside;
    return [part1, part2];
}

/*
| is a vertical pipe connecting north and south.
- is a horizontal pipe connecting east and west.
L is a 90-degree bend connecting north and east.
J is a 90-degree bend connecting north and west.
7 is a 90-degree bend connecting south and west.
F is a 90-degree bend connecting south and east.
. is ground; there is no pipe in this tile.
S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has.
*/
function makeGrid(filename) {
    let lines = readLines(filename);
    const width = lines[0].length;
    const height = lines.length;

    return {
        data: lines,
        loop: new Map(),
        width: width,
        height: height,
        get: function([x, y]) {
            if (x < 0 || x > width-1 ||
                y < 0 || y > height-1) {
                    return ".";
                } else {
                    return this.data[y][x];
                }
        },
        eq: function(pt1, pt2) {
            return pt1[0] === pt2[0] && 
                pt1[1] === pt2[1];
        },
        isStart: function(pt) {
            return this.get(pt) === "S";
        },
        isGround: function(pt) {
            return this.get(pt) === ".";
        },
        isPipe: function(pt) {
            return !this.isStart(pt) && !this.isGround(pt)
        },
        connectsDown: function(pt) {
            const symbol = this.get(pt);
            return symbol === "|" ||
                symbol === "7" ||
                symbol === "F" ||
                symbol === "S";
        },
        connectsUp: function(pt) {
            const symbol = this.get(pt);
            return symbol === "|" ||
                symbol === "L" ||
                symbol === "J" ||
                symbol === "S";
        },
        connectsLeft: function(pt) {
            const symbol = this.get(pt);
            return symbol === "-" ||
                symbol ===  "J" ||
                symbol === "7" ||
                symbol === "S";
        },
        connectsRight: function(pt) {
            const symbol = this.get(pt);
            return symbol === "-" ||
                symbol === "F" ||
                symbol === "L" ||
                symbol === "S";
        },
        add: function(pt1, d) {
            return [pt1[0] + d[0], pt1[1] + d[1]];
        },
        findStart: function() {
            for (let y = 0; y<this.height; y++) {
                for (let x = 0; x<this.width;  x++) {
                    if (this.isStart([x, y])) {
                        return [x, y];
                    }
                }
            }
        },
        connections: function(pt) {
            const coords = [];

            const up = this.add(pt, [0, -1]);
            const right = this.add(pt, [1, 0]);
            const down = this.add(pt, [0, 1]);
            const left = this.add(pt, [-1, 0]);

            if (this.connectsUp(pt) && this.connectsDown(up)) coords.push(up);
            if (this.connectsRight(pt) && this.connectsLeft(right)) coords.push(right);
            if (this.connectsDown(pt) && this.connectsUp(down)) coords.push(down);
            if (this.connectsLeft(pt) && this.connectsRight(left)) coords.push(left);

            return coords;
        }
    }
}

console.log(solve('day10/input.txt'));
