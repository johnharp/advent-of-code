import { read } from '../util/input.js';

export function solve(filename) {

    const input = read(filename);
    const puzzles = input
        .split("\n\n")
        .map(puzzle => puzzle.split("\n"));

    let part1 = 0;
    let part2 = 0;

    for (const puzzle of puzzles) {
        const score1 = computeOnePuzzleScore(puzzle);
        part1 += score1[0];

        let found = false;
        for (let row = 0; row < puzzle.length && !found; row++) {
            for (let col = 0; col < puzzle[0].length && !found; col++) {
                const modifiedPuzzle = modifyPuzzle(puzzle, row, col);
                const score2 = computeOnePuzzleScore(modifiedPuzzle, score1[1], score1[2]);
                if (score2[0] > -1) {
                    part2 += score2[0];
                    found = true;
                }
            }
        }
    }

    return [part1, part2];
}

function modifyPuzzle(puzzle, r, c) {
    const row = puzzle[r];
    const char = row.charAt(c);
    const newRow =
        row.substring(0, c) + 
        (char === "#" ? "." : "#") +
        row.substring(c + 1);
    const newPuzzle = [
        ...puzzle.slice(0, r),
        newRow,
        ...puzzle.slice(r + 1) ];

    return newPuzzle;
}

function computeOnePuzzleScore(puzzle, skipHorizontal, skipVertical) {
    const rowValues = [];
    for (let r = 0; r < puzzle.length; r++) {
        let rowValue = 0;
        for (let c = 0; c < puzzle[0].length; c++) {
            rowValue = (rowValue << 1) + (puzzle[r].charAt(c) === "#" ? 1 : 0);
        }
        rowValues.push(rowValue);
    }

    const colValues = [];
    for (let c = 0; c < puzzle[0].length; c++) {
        let colValue = 0;
        for (let r = 0; r < puzzle.length; r++) {
            colValue = (colValue << 1) + (puzzle[r].charAt(c) === "#" ? 1 : 0);
        }
        colValues.push(colValue);
    }

    const horizontalPoint = mirrorPoint(colValues, skipHorizontal);
    const verticalPoint = mirrorPoint(rowValues, skipVertical);

    let score = -1;

    if (horizontalPoint  > -1) {
        score = horizontalPoint +  1;
    } else if (verticalPoint > -1) {
        score =  100 * (verticalPoint + 1);
    } 

    return [score, horizontalPoint, verticalPoint];
}

/**
 * If the values are mirrored, return the index of the mirror point.
 * @param {array} values   Array of values
 */
function mirrorPoint(values, skip) {
    let foundPoint = -1;

    for (let foldAt = 0; foldAt < values.length - 1; foldAt++) {
        if (foldAt !== skip) {
            let validFold = true;
            for (let i = 0; i <= foldAt  && i < values.length - foldAt - 1; i++) {
                if (values[foldAt - i] != values[foldAt + i + 1]) {
                    validFold = false;
                    break;
                }
            }

            if (validFold) {
                foundPoint = foldAt;
                break;
            }
        }
    }

    return foundPoint;
}

console.log(solve('day13/input.txt'));