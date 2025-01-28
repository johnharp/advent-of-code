import { readLines } from "../util/input.js";

export function solve(filename) {
    const lines = readLines(filename);
    const grid = Grid(lines, '.');

    const numbers = grid.extractNumbers();

    for (const number of numbers) {
        number.adjCoords = surroundingCoords(number);
    }

    const numbersAdjacentToSymbols = [];

    for (const number of numbers) {
        const numAdjSymbols = number.adjCoords.reduce((count, coord) => {
            if (grid.isSymbolAt(coord.col, coord.row)) {
                count++;
            }
            return count;
        }, 0);

        if (numAdjSymbols > 0) {
            numbersAdjacentToSymbols.push(number);
        }
    }

    const part1 = numbersAdjacentToSymbols.reduce((sum, num) => sum += num.number, 0);
    let part2 = 0;

    for (const gearCoord of grid.gearCoords()) {
        const adjacentNumbers = numbers.filter(num => 
            numAdjacentTo(num, gearCoord.col, gearCoord.row));

        if (adjacentNumbers.length === 2) {
            part2 += (adjacentNumbers[0].number * adjacentNumbers[1].number);
        }
    }

    return [part1, part2];
}

function surroundingCoords(num) {
    const coords = [];

    for (let row = num.row - 1; row <= num.row +1; row++) {
        for (let col = num.col-1; col <= num.col + num.length; col++) {
            if (!numContainsCoord(num, col, row)) {
                coords.push({row: row, col: col});
            }
        }
    }
    return coords;
}

function numAdjacentTo(num, col, row) {
    for (const coord of num.adjCoords) {
        if (coord.col === col && coord.row === row)  {
            return true;
        }
    }

    return false;
}

function numContainsCoord(num, col, row) {
    if (num.row === row &&
        col >= num.col &&
        col <= num.col + num.length - 1) {
            return true;
        } else {
            return false;
        }
}

function Grid(lines, fill) {
    return {
        data: lines,
        numRows: lines.length,
        numCols: lines[0].length,
        get: function(c, r) {
            if (c<0 || c>=this.numCols || r<0 || r>=this.numRows) {
                return fill;
            } else {
                return lines[r][c];
            }
        },

        isNumberAt: function(col, row) {
            const value = this.get(col, row);
            return value >= "0" && value <= "9";
        },

        isSpaceAt(col, row) {
            const value = this.get(col, row);
            return value === '.';
        },

        isSymbolAt: function(col, row) {
            return !this.isNumberAt(col, row) && 
                !this.isSpaceAt(col, row);
        },

        isGearAt: function(col, row) {
            const value = this.get(col, row);
            return value === '*';
        },

        extractNumberAt: function(col, row) {
            const digits = [];

            let c = col;
            while(this.isNumberAt(c, row)) {
                digits.push(this.get(c, row));
                c++;
            }

            if (digits.length > 0) {
                const numberString = digits.join("");
                const number = Number(numberString);

                return {
                    number: number,
                    col: col,
                    row: row,
                    length: numberString.length
                }
            } else {
                return null;
            }
        },

        extractNumbers() {
            const nums = [];

            for (let row = 0;  row < this.numRows; row++) {
                for (let col = 0; col < this.numCols; col++) {
                    const num = this.extractNumberAt(col, row);

                    if (num) {
                        nums.push(num);
                        col += num.length - 1;
                    }
                }
            }

            return nums;
        },

        gearCoords: function() {
            const coords = [];

            for (let row = 0;  row < this.numRows; row++) {
                for (let col = 0; col < this.numCols; col++) {
                    if (this.isGearAt(col, row)) {
                        coords.push({ col: col, row: row });
                    }
                }
            }

            return coords;
        }
    };
}


console.log(solve("day03/input.txt"));
