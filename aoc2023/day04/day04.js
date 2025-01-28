import { readLines } from "../util/input.js";

export function solve(filename) {
    const lines = readLines(filename);

    const cards = [];
    let part1 = 0;

    for (const line of lines) {
        const card = parseCard(line);
        card.numMatches = countMatches(card);
        cards.push(card);
    }

    for (const card of cards) {
        if (card.numMatches > 0) {
            part1 += 2 ** (card.numMatches-1);
        }

        for (let i = card.cardNum; (i < card.cardNum + card.numMatches) && (i < cards.length); i++) {
            cards[i].copies += card.copies;
        }
    }

    const part2 = cards.reduce((sum, card) => sum + card.copies, 0);

    return [part1, part2];
}

function parseCard(line) {
    const [cardinfo, numberinfo] = line.split(":");
    const [,cardnum] = cardinfo.split(/\s+/);
    const [winningnums, mynums] = numberinfo.split("|");

    return {
        cardNum: Number(cardnum),
        winningNumbers: parseNumbers(winningnums.trim()),
        myNumbers: parseNumbers(mynums.trim()),
        copies: 1,
    }
}

function parseNumbers(str) {
    const nums = new Map();

    for (const s of str.split(/\s+/)) {
        const num = Number(s);
        nums.set(num, true);
    }

    return nums;
}

function countMatches(card) {
    let numMatches = 0;
    for (const key of card.winningNumbers.keys()) {
        if (card.myNumbers.has(key)) {
            numMatches++;
        }
    }

    return numMatches;
}

console.log(solve("day04/input.txt"));
