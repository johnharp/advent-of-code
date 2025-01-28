import { readLines } from "../util/input.js";
console.log(solve("day17/input-sample.txt"));

export function solve(filename) {
    const lines = readLines(filename);

    const grid = makeGrid(lines);
    const mins = new Map();

    mins.set('0,0', {totalLoss: 0, path: []});

    let active = [[0, 0]];

    while (active.length > 0) {
        const curr = active.shift();
        let neighbors = grid.getNeighbors(curr);

        for (const neighbor of neighbors) {
            const currMin = mins.get(curr.join(','));

            const key = neighbor.join(',');
            const totalLoss = currMin.totalLoss + grid.get(neighbor);

            if (mins.has(key)) {
                const min = mins.get(key);
                if (totalLoss < min.totalLoss) {
                    min.totalLoss = totalLoss;
                    min.path = [...currMin.path, curr];
                    active.push(neighbor);
                }
            } else {
                mins.set(key, {
                    totalLoss: totalLoss,
                    path: [...currMin.path, curr]
                });
                active.push(neighbor);
            }
        }
    }
    
    for (let y = 0; y < grid.getHeight(); y++) {
        for (let x = 0; x < grid.getWidth(); x++) {
            const key = [x, y].join(',');
            if (mins.has(key)) {
                const min = mins.get(key);
                console.log(`${key}: ${min.totalLoss} (${min.path.length})`);
            }
        }
    }


    const part1 = lines.length;
    const part2 = 0;
    return [part1, part2];

}

function makeGrid(lines) {
    return {
        data: lines.map(line => line.split('').map(n => parseInt(n))),
        get([x, y]) {
            return this.data[y][x];
        },
        set([x, y], value) {
            this.data[y][x] = value;
        },
        getNeighbors([x, y]) {
            const neighbors = [];
            const deltas = [[-1, 0], [1, 0], [0, -1], [0, 1]];
            for (const [dx, dy] of deltas) {
                const [nx, ny] = [x + dx, y + dy];
                if (nx < 0 || nx >= this.data[0].length) continue;
                if (ny < 0 || ny >= this.data.length) continue;
                neighbors.push([nx, ny]);
            }

            return neighbors;
        },
        getWidth() {
            return this.data[0].length;
        },
        getHeight() {
            return this.data.length;
        }
    }
}

