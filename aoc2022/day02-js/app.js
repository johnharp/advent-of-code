let fs = require('fs');

const inputString = fs
    .readFileSync('./input.txt', 'utf-8')
    .toString();

const input = inputString
    .split('\n');

// Rock = 0
// Paper = 1
// Scissors = 2

function winScore() { return 6; }
function loseScore() { return 0; }
function drawScore() { return 3; }

function moveToDraw(opponent) {
    if (opponent === 'A') return 'X';
    if (opponent === 'B') return 'Y';
    if (opponent === 'C') return 'Z';
}

function moveToWin(opponent) {
    if (opponent === 'A') return 'Y';
    if (opponent === 'B') return 'Z';
    if (opponent === 'C') return 'X';
}

function moveToLose(opponent) {
    if (opponent === 'A') return 'Z';
    if (opponent === 'B') return 'X';
    if (opponent === 'C') return 'Y';
}


function part1score(line) {
    const opponent = line.charCodeAt(0) - 'A'.charCodeAt(0);
    const mine = line.charCodeAt(2) - 'X'.charCodeAt(0);
    const difference = mine - opponent;

    let score = 0;

    if (difference === 1 || difference === -2) {
        score += winScore();
    }

    if (difference === -1 || difference === 2) {
        score += loseScore();
    }

    // draw
    if (difference === 0) {
        score += drawScore();
    }

    // 1 point for playing rock
    // 2 points for playing paper
    // 3 points for playing scissors
    score += mine + 1;

    return score;
}

function part2score(line) {
    score = 0;

    const opponent = line[0];
    let mine = '';

    if (line[2] === 'X') {
        mine = moveToLose(opponent);
    }

    if (line[2] === 'Y') {
        mine = moveToDraw(opponent);
    }

    if (line[2] === 'Z') {
        mine = moveToWin(opponent);
    }

    score = part1score(`${line[0]} ${mine}`);

    return score;
}





let part1Total = input.reduce((acc, curr) => part1score(curr) + acc, 0);
let part2Total = input.reduce((acc, curr) => part2score(curr) + acc, 0);

console.log('Part 1', part1Total);
console.log('Part 2', part2Total);