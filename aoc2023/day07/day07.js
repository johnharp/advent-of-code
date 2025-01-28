import { readLines } from '../util/input.js';

export function solve(filename) {
    const lines = readLines(filename);
    const hands = lines
        .map(l => l.split(" "))
        .map(l => ({
            cards: parseCard(l[0]),
            bid: Number(l[1])
        }));

    const sorted = hands.sort((a, b) => {return a.cards.sortString.localeCompare(b.cards.sortString)});
    let part1 = 0;

    for (let i = 0; i<sorted.length; i++) {
        part1 += sorted[i].bid * (i+1);
    }

    const sorted2 = hands.sort((a, b) => {return a.cards.sortString2.localeCompare(b.cards.sortString2)});
    let part2 = 0;

    for (let i = 0; i<sorted2.length; i++) {
        part2 += sorted2[i].bid * (i+1);
    }
    return [part1, part2];
}

function parseCard(s) {
    const type = computeType(s, 1);
    const type2 = computeType(s, 2);

    const sortableCards = s
        .replaceAll("A", "Z")
        .replaceAll("K", "Y")
        .replaceAll("Q", "X")
        .replaceAll("J", "W")
        .replaceAll("T", "V");

    const part2SortableCards = s
        .replaceAll("A", "Z")
        .replaceAll("K", "Y")
        .replaceAll("Q", "X")
        .replaceAll("T", "V")
        .replaceAll("J", "1");

    const sortString = `${type}${sortableCards}`;
    const sortString2 = `${type2}${part2SortableCards}`;

    return {
        orig: s,
        type: type,
        type2: type2,
        sortString: sortString,
        sortString2: sortString2
    }
}

/**
 * 
 * @param {*} s 
 * @returns 
 * 
 * 7 - five of a kind, one unique value
 * 6 - four of a kind, two unique values, highest count 4
 * 5 - full house, two unique values, highest count 3
 * 4 - three of a kind, three unique values, highest count 3
 * 3 - two pair, three unique values, hightest count 2
 * 2 - one pair, four unique values, hightest count 2
 * 1 - high card, 5 unique values
 */
function computeType(s, partNum) {
    const counts = new Map();

    for (const char of s) {
        if (counts.has(char)) {
            const currCount = counts.get(char);
            counts.set(char, currCount+1);
        } else {
            counts.set(char, 1);
        }
    }

    let numUnique = counts.size;
    let maxCount = Math.max(...counts.values());
    let maxCountNotJack = 0;
    let maxCardNotJack = "";
    
    for (const key of counts.keys()) {
        if (key !== "J" && counts.get(key) > maxCountNotJack) {
            maxCountNotJack = counts.get(key);
            maxCardNotJack = key;
        }
    }

    if (partNum === 2) {
        if (counts.has("J")) {
            const numJacks = counts.get("J");

            if (maxCardNotJack !== "") {
                numUnique--;
                maxCount = maxCountNotJack + numJacks;
            }
        }
    }

    const typekey = `${numUnique}${maxCount}`;
    let type = 0;

    switch (typekey) {
        case "15":
            type = 7;
            break;
        case "24":
            type = 6;
            break;
        case "23":
            type = 5;
            break;
        case "33":
            type = 4;
            break;
        case "32":
            type = 3;
            break;
        case "42":
            type = 2;
            break;
        case "51":
            type = 1;
            break;

    }

    return type;
}

console.log(solve('day07/input.txt'));
