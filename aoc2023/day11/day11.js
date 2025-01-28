import { readLines } from '../util/input.js';

export function solve(filename) {
    const lines = readLines(filename);
    const colcounts = Array(lines[0].length);
    const rowcounts = Array(lines.length);

    for (let row = 0; row < lines.length; row++) {
        const line = lines[row];

        for (let col = 0; col < line.length; col++) {
            if (line[col] === "#") {
                if (colcounts[col] === undefined) colcounts[col] = 0;
                if (rowcounts[row] === undefined) rowcounts[row] = 0;

                colcounts[col] ++;
                rowcounts[row] ++;
            }
        }
    }

    const colexpansions = calcExpansionPoints(colcounts);
    const rowexpansions = calcExpansionPoints(rowcounts);

    let part1 = 0;

    part1 += sumDistances(colcounts, colexpansions, 1);
    part1 += sumDistances(rowcounts, rowexpansions, 1);

    let part2 = 0;

    part2 += sumDistances(colcounts, colexpansions, 999_999);
    part2 += sumDistances(rowcounts, rowexpansions, 999_999);

    return [part1, part2];
}

function sumDistances(counts, expansions, expansionsize) {
    let sum = 0;

    for (let i=0; i < counts.length - 1; i++) {
        if (counts[i] !== undefined) {
            for (let j = i+1; j < counts.length; j++) {
                if (counts[j] !== undefined) {
                    let expansion = 0;
                    for (let k = i; k<=j; k++) {
                        if (expansions.includes(k)) {
                            expansion += expansionsize;
                        }
                    }

                    sum += counts[i] * counts[j] * (expansion + j - i);
                }
            }

        }
    }

    return sum;
}


function calcExpansionPoints(array) {
    const points = [];
    for (let i = 0; i<array.length; i++) {
        if (array[i] === undefined) points.push(i);
    }
    return points;
}

console.log(solve('day11/input.txt'));
