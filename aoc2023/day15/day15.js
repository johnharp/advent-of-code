import { read } from '../util/input.js';

export function solve(filename) {
    const input = read(filename).split(",");

    // create an array of 256 elements
    const boxes = new Array(256);

    const part1 = input.reduce((acc, v) => acc + createHash(v), 0);

    for (const line of input)  {
        const [label, boxnum, op, focalLength] = parseInstruction(line);
        
        let box;
        if (boxes[boxnum] === undefined) {
            box = createBox();
            boxes[boxnum] = box;
        } else {
            box = boxes[boxnum];
        }

        if (op === '=') {
            box.addLens(label, focalLength);
        } else if (op === '-') {
            box.removeLense(label);
        }
    }

    let part2 = 0;

    for (let i = 0; i < boxes.length; i++) {
        const box = boxes[i];
        if (box !== undefined && box.lenses.size > 0) {
            let fp = 0;
            const lenses  = Array.from(box.lenses.values());

            for (let j = 0; j < lenses.length; j++) {
                const focalLength = lenses[j];
                fp = (i+1) * (j+1) * focalLength;
                part2 += fp;
            }
        }
    }

    return [part1, part2];
}

function createBox() {
    const box = {
        lenses: new Map(),
        addLens: function (label, focalLength) {
            this.lenses.set(label, focalLength);
        },
        removeLense: function (label) {
            this.lenses.delete(label);
        },
        log: function () {
            console.log(this.lenses);
        }
    };
    return box;
}

function createHash(str) {
    let v = 0;
    for (const c of str) {
        v = v + c.charCodeAt(0);
        v = v * 17;
        v = v % 256;
    }

    return v;
}

function parseInstruction(line) {
    const match = line.match(/^([a-z]+)([=-])(\d*)$/);
    const label = match[1];
    const hash = createHash(label);
    const op = match[2];
    const focalLength = parseInt(match[3]);

    return [label, hash, op, focalLength];
}

console.log(solve('day15/input.txt'));
