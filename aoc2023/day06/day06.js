import { readLines } from '../util/input.js';

export function solve(filename) {
    const lines = readLines(filename);
    const times = lines[0].replace(/Time:\s+/, "").split(/\s+/).map(i => Number(i));
    const distances = lines[1].replace(/Distance:\s+/, "").split(/\s+/).map(i => Number(i));

    let part1 = 1;

    for (let i = 0; i<times.length; i++) {
        const win = [];
        const raceTime = times[i];
        const distanceToBeat = distances[i];

        for (let b = 1; b<raceTime; b++) {
            const d = dist(raceTime, b);
            if (d > distanceToBeat) {
                win.push(b);
            }
        }
        part1 *= win.length;
    }

    const part2time = Number(times.map(s => `${s}`).join(""));
    const part2dist = Number(distances.map(s => `${s}`).join(""));

    let part2 = 0;
    for (let b = 1; b<part2time; b++) {
        const d = dist(part2time, b);
        if (d > part2dist) {
            part2++;
        }
    }

    return [part1, part2];
}

function dist(t, b) {
    return (t-b)*b;
}

function maxPossibleDistance (time) {
    return Math.floor(time/2) * Math.ceil(time/2);
}


console.log(solve('day06/input.txt'));
