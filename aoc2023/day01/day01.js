import { readLines } from "../util/input.js";


export function solve(filename) {
    const rawInput = readLines(filename);
    const names = [
        "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"
    ];
    const numbers = [
        "1", "2", "3", "4", "5", "6", "7", "8", "9"
    ];

    const part1 = rawInput
        .map(line => [findOne(line, numbers, true), findOne(line, numbers, false)])
        .map(vals => `${vals[0]}${vals[1]}`)
        .map(val => Number(val))
        .reduce((sum, val) => sum += val, 0);

    const part2 = rawInput
        .map(line => [
            findOne(line, [...numbers, ...names], true),
            findOne(line, [...numbers, ...names], false)
        ])
        .map(vals => `${vals[0]}${vals[1]}`)
        .map(val => replaceAllNames(val, names))
        .reduce((sum, val) => sum += Number(val), 0);

    return [part1, part2];
}

function replaceAllNames(s, names) {
    for (let i = 0; i<names.length; i++) {
        s = s.replaceAll(names[i], `${i+1}`);
    }
    return s;
}

function findOne(line, patterns, forward) {
    const start = forward ? 0 : line.length;
    const end = forward ? line.length + 1 : -1;
    const incr = forward ? 1 : -1;

    for (let i = start; i !== end; i += incr) {
        const match = matchAnyAt(line, patterns, i);
        if (match) return match;
    }

    return undefined;
}

function matchAt(line, pattern, index) {
    for (let i = 0; i<pattern.length; i++) {
        if (line[index + i] !== pattern[i]) return false;
    }
    return true;
}

function matchAnyAt(line, patterns, index) {
    for (const pattern of patterns) {
        if (matchAt(line, pattern, index)) return pattern;
    }

    return undefined;
}

console.log(solve("day01/input.txt"));