import { readLines } from "../util/input.js";


export function solve(filename) {
    const lines = readLines(filename);

    var rx = /^Game (\d+): (.*)$/;
    var rx2 = /(\d+) (.*)$/;


    const games = [];

    for (const line of lines) {
        const matches = line.match(rx);
        const gameNumber = Number(matches[1]);
        const contents = matches[2].split(";");
        const items = contents.map(group => group.split(",").map(i => i.trim()));

        const game = { num: gameNumber, turns: [] };

        for (const turnItem of items) {
            const turn = [];
            for (const cube of turnItem) {
                const matches = cube.match(rx2);
                turn.push({count: Number(matches[1]), color: matches[2]});
            }
            game.turns.push(turn);
        }

        games.push(game);
    }

    // for(const game of games) {
    //     console.log(`Game #${game.num}:`);
    //     for (const turn of game.turns) {
    //         console.log(`\tturn: `);
    //         for (const cube of turn) {
    //             console.log(`\t\t${cube.count}: ${cube.color}`);
    //         }
    //     }
    // }

    let sum = 0;
    let power = 0;
    for (const game of games) {
        const red = findmax(game, "red");
        const green = findmax(game, "green");
        const blue = findmax(game, "blue");

        power += red * green * blue;
        if (red <= 12 && green <= 13 && blue <= 14) {
            sum += game.num;
        }
    }
    return [sum, power];
}

function findmax(game, color) {
    let max = 0;

    for (const turn of game.turns) {
        for (const cube of turn) {
            if (cube.color === color && cube.count > max) {
                max = cube.count;
            }
        }
    }
    return max;
}

console.log(solve("day02/input.txt"));