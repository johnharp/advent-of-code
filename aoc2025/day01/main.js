import fs from "fs";
import test from 'node:test';
import assert from 'node:assert';


const input = fs.readFileSync("input.txt")
    .toString()
    .split('\n')
    .map(convertInstruction);

const DIAL_MIN = 0;
const DIAL_MAX = 99;


let n = 50;
let part1 = 0;
let part2 = 0;

for (const m of input) {
    const {inc, numTicks} = m;
    const {value, zerosPassed} = apply(n, inc, numTicks);

    n = value;

    if (n === 0) {
        part1++;
        part2++;
    }

    part2 += zerosPassed;
}

console.log(`Part 1: ${part1}`);
console.log(`Part 2: ${part2}`);


// 
// Examples:
// INPUT        OUTPUT
// R75          [1, 75]
// L5           [-1, 5]
// L15          [-1, 15]
//
function convertInstruction(s) {
    const lr = s[0];
    const num = s.slice(1);

    const inc = (lr === "L" ? -1 : 1);
    const numTicks = parseInt(num);

    return {inc: inc, numTicks: numTicks};
}


function apply(origValue, inc, numTicks) {
    let value = origValue;
    let zerosPassed = 0;

    for (let i = 0; i<numTicks; i++) {
        value += inc;
        if (value === 100) value = 0;
        if (value === -1) value = 99;

        if (value === 0 && i < numTicks-1) {
            zerosPassed++;
        }
    }

    return {value: value, zerosPassed: zerosPassed};
}

function doTests() {
    test('Convert instruction L75', () => { assert.deepEqual(convertInstruction('L75'), {inc: -1, numTicks: 75}); })
    test('Convert instruction L75', () => { assert.deepEqual(convertInstruction('R2'), {inc: 1, numTicks: 2}); })
    test('example 1', () => { assert.deepEqual(apply(50, -1, 68), {value: 82, zerosPassed: 1}); })
    test('example 2', () => { assert.deepEqual(apply(82, -1, 30), {value: 52, zerosPassed: 0}); })
    test('example 3', () => { assert.deepEqual(apply(52, 1, 48), {value: 0, zerosPassed: 0}); })
    test('example 4', () => { assert.deepEqual(apply(0, -1, 5), {value: 95, zerosPassed: 0}); })
    test('example 5', () => { assert.deepEqual(apply(95, 1, 60), {value: 55, zerosPassed: 1}); })
    test('example 6', () => { assert.deepEqual(apply(55, -1, 55), {value: 0, zerosPassed: 0}); })
    test('example 7', () => { assert.deepEqual(apply(0, -1, 1), {value: 99, zerosPassed: 0}); })
    test('example 8', () => { assert.deepEqual(apply(99, -1, 99), {value: 0, zerosPassed: 0}); })
    test('example 9', () => { assert.deepEqual(apply(0, 1, 14), {value: 14, zerosPassed: 0}); })
    test('example 10', () => { assert.deepEqual(apply(14, -1, 82), {value: 32, zerosPassed: 1}); })
}
