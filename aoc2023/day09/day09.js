import { readLines } from '../util/input.js';

export function solve(filename) {
    const inputs = readLines(filename)
        .map(l => l.split(" ")
        .map(Number));

    const values = inputs.map(input => predict(input));

    const part1 = values
        .map(v => v[v.length-1])
        .reduce((sum, v) => sum + v, 0);
    const part2 = values
        .map(v => v[0])
        .reduce((sum, v) => sum + v, 0);

    return [part1, part2];
}

function differences(values) {
    if (values.length === 1)  {
        return 0;
    }

    const returnValues = [];
    for (let i = 1; i<values.length; i++) {
        returnValues.push(values[i] - values[i-1]);
    }

    return returnValues;
}

function predict(values) {

    // first keep creating diffs until we reach
    // 0 0 0 ...
    const stack = []
    stack.push(values);

    while (!onlyZeros(stack[stack.length - 1])) {
        stack.push(differences(stack[stack.length - 1]));
    }

    stack[stack.length - 1].push(0);

    for (let i = stack.length-1; i > 0; i--) {
        stack[i-1]
            .push(
                stack[i-1][stack[i-1].length-1] + 
                stack[i][stack[i].length-1]
            );
        stack[i-1]
            .unshift(
                stack[i-1][0] -
                stack[i][0]
            )
    }

    return stack[0];
}

function onlyZeros(values) {
    for (const value of values) {
        if (value !== 0) {
            return false;
        }
    }
    return true;
}

console.log(solve('day09/input.txt'));
