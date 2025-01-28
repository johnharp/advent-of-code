import { readLines } from '../util/input.js';
const cache = new Map();

export function solve(filename) {

    const lines = readLines(filename);

    let part1 = 0;
    let part2 = 0;

    for (const line of lines) {
        let [springs, groupsStr] = line.split(" ");
        let groups = groupsStr.split(",").map(g => Number(g));

        part1 += checkCacheAndCalc(springs, groups);
        part2 += checkCacheAndCalc(
            `${springs}?${springs}?${springs}?${springs}?${springs}`,
            [...groups, ...groups, ...groups, ...groups, ...groups]
        )
    }

    return [part1, part2];
}

function checkCacheAndCalc(springs, groups) {
    const key = `${springs}|${groups.join(",")}"`;
 
    if (cache.has(key)) {
        return cache.get(key);
    }

    const value = calc(springs, groups);

    cache.set(key, value);
    return value;
}


function calc(springs, groups) {
    
    if (groups.length === 0) {
        return (!springs.includes("#")) ? 1 : 0;
    }
    if (springs.length === 0) return 0;

    const thisChar = springs[0];
    const thisGroupLength = groups[0];

    const dot = () => checkCacheAndCalc(springs.slice(1), groups);
    const pound = () => {
        const thisGroup = springs
            .slice(0, thisGroupLength)
            .replaceAll("?", "#");

        const numPounds = (thisGroup.match(/#/g) || []).length;
        if (numPounds != thisGroupLength) return 0;

        if (springs.length === thisGroupLength) {
            return groups.length === 1 ? 1 : 0;
        }

        if ("?.".includes(springs[thisGroupLength])) {
            return checkCacheAndCalc(springs.slice(thisGroupLength+1), groups.slice(1));
        }

        return 0;
    };

    let out = 0;
    if (thisChar === "#") {
        out = pound();
    } else if (thisChar === ".") {
        out = dot();
    } else if (thisChar === "?") {
        out = dot() + pound();
    } else {
        console.error("unexpected first char: ", thisChar);
    }

    return out;
}

console.log(solve('day12/input.txt'));