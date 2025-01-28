import { read } from '../util/input.js';

export function solve(filename) {
    const input = read(filename);

    const parts = input.split('\n\n');

    const seeds1 = parts[0]
        .split(' ')
        .slice(1)
        .map((i) => [Number(i), Number(i)]);

    const seeds2 = [];
    for (let i = 0; i < seeds1.length; i += 2) {
        seeds2.push([seeds1[i][0], seeds1[i][0] + seeds1[i + 1][0] - 1]);
    }

    const sections = parts.slice(1).map((sec) => parseSection(sec));

    const outputs1 = applyAllSections(sections, seeds1);
    const outputs2 = applyAllSections(sections, seeds2);

    const part1 = findLowest(outputs1);
    const part2 = findLowest(outputs2);

    return [part1, part2];
}

function findLowest(items) {
    let min = Number.MAX_SAFE_INTEGER;

    for (const item of items) {
        if (item[0] < min) min = item[0]
    }

    return min;
}

function applyAllSections(sections, initialinputs) {
    let inputs = [...initialinputs];

    for (const section of sections) {
        inputs = applySectionToInputs(section, inputs);
    }

    return inputs;
}

function applySectionToInputs(section, inputs) {
    let outputs = [];

    for (const input of inputs) {
        const newoutputs = applySectionToInput(section, input);
        if (newoutputs.length > 0) outputs = outputs.concat(newoutputs);
        outputs = outputs.concat();
    }
    return outputs;
}

function applySectionToInput(section, input) {
    const inputs = [input];
    const maps = [...section];

    const outputs = [];

    while (maps.length > 0 && inputs.length > 0) {
        const input = inputs.pop();
        const map = maps.pop();

        const left = leftDifference(input, map);
        const inter = intersection(input, map);
        const right = rightDifference(input, map);

        if (isValid(left)) {
            inputs.push(left);
        }

        if (isValid(right)) {
            inputs.push(right);
        }

        if (isValid(inter)) {
            outputs.push(inter);
        }
    }

    for (const leftoverInput of inputs) {
        outputs.push(leftoverInput);
    }

    return outputs;
}

function isValid(item) {
    return (item[0] > 0 && item[0] <= item[1]);
}

function parseSection(txt) {
    const section = txt
        .split('\n')
        .slice(1)
        .map((i) => i.split(' '))
        .map((i) => [
            Number(i[1]),
            Number(i[1]) + Number(i[2]) - 1,
            Number(i[0]) - Number(i[1]),
        ]);

    return section;
}

function leftDifference(input, map) {
    return [
        input[0],
        Math.min(input[1], map[0]-1)
    ]
}

function rightDifference(input, map) {
    return [
        Math.max(map[1]+1, input[0]),
        input[1]
    ]
}

function intersection(input, map) {
    return [
        Math.max(input[0], map[0]) + map[2],
        Math.min(input[1], map[1]) + map[2]
    ]
}

console.log(solve('day05/input.txt'));
