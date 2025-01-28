import * as fs from "fs";


export function read(fileName) {
    const input = fs
        .readFileSync(fileName, "utf8")
        .toString()
        .trim();
    return input;
}

export function readLines(fileName) {
    const input = read(fileName);
    const lines = input.split("\n");

    return lines;
}