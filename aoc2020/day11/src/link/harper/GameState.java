package link.harper;

import org.junit.jupiter.api.Test;

import java.util.List;

public class GameState {
    int numCols;
    int numRows;

    int[][] chairs;
    int[][] currentState;
    int[][] nextState;

    public int getNumModifiedLastStateChange() {
        return numModifiedLastStateChange;
    }

    int numModifiedLastStateChange ;

    public void setCurrentStateAt(int r, int c, int v) {
        currentState[r][c] = v;
    }

    public int countOccupiedSeats() {
        int sum = 0;

        for (int row = 1; row <= numRows; row++) {
            for (int col = 1; col <= numCols; col++) {
                sum += currentState[row][col];
            }
        }

        System.out.println("Total of " + sum + " seats occupied");
        return sum;
    }

    public void initialize(List<String> lines) {
        String firstLine = lines.get(0);
        numCols = firstLine.length();
        numRows = lines.size();
        numModifiedLastStateChange = 0;

        // Give the board a margin of one space all the way around
        // This top, bottom, left, right margin will never have seats
        // or occupants and will never be calculated, but will allow
        // us to not bound check when calculating the inner spaces
        chairs = new int[numRows+2][numCols+2];
        currentState = new int[numRows+2][numCols+2];
        nextState = new int[numRows+2][numCols+2];

        int row = 1;
        for(String line: lines) {
            int col=1;
            char[] chars = line.toCharArray();
            for(char c: chars) {
                if (c == 'L') {
                    chairs[row][col] = 1;
                }
                col++;
            }

            row++;
        }
    }

    public void computeNextState(boolean isPart2) {
        numModifiedLastStateChange = 0;
        for (int row = 1; row <= numRows; row++) {
            for (int col = 1; col <= numCols; col++) {
                if (chairs[row][col] == 1) {
                    int oldCellValue = nextState[row][col];
                    int newCellValue;

                    if (isPart2) newCellValue  = computeCellPart2(row, col);
                    else newCellValue = computeCell(row, col);

                    if (oldCellValue != newCellValue) {
                        numModifiedLastStateChange++;
                        nextState[row][col] = newCellValue;
                    }
                }
            }
        }

        swapNextAndCurrentStates();
    }

    public void swapNextAndCurrentStates() {
        int[][] temp;

        temp = currentState;
        currentState = nextState;
        nextState = temp;
    }

    public int runUntilStable(boolean isPart2) {
        int numSteps = 0;
        numModifiedLastStateChange = Integer.MAX_VALUE;
        while (numModifiedLastStateChange > 0) {
            computeNextState(isPart2);
            numSteps++;
        }

        System.out.println("Became stable after " + (numSteps-1));
        return numSteps-1;
    }

    public int computeCell(int r, int c) {
        int currentCellState = currentState[r][c];
        int newCellState;

        int sum = currentState[r-1][c-1] + currentState[r-1][c] + currentState[r-1][c+1] +
                currentState[r][c-1]                            + currentState[r][c+1] +
                currentState[r+1][c-1] + currentState[r+1][c] + currentState[r+1][c+1];

        if (currentCellState == 0 && sum <= 0) newCellState = 1;
        else if (currentCellState == 1 && sum >= 4) newCellState = 0;
        else newCellState = currentCellState;

        return newCellState;
    }

    public int computeCellPart2(int r, int c) {
        int currentCellState = currentState[r][c];
        int newCellState;

        int sum = 0;
        sum += countVisiblePeople(r, c, -1, -1);
        sum += countVisiblePeople(r, c, -1, 0);
        sum += countVisiblePeople(r, c, -1, 1);
        sum += countVisiblePeople(r, c, 0, 1);
        sum += countVisiblePeople(r, c, 1, 1);
        sum += countVisiblePeople(r, c, 1, 0);
        sum += countVisiblePeople(r, c, 1, -1);
        sum += countVisiblePeople(r, c, 0, -1);


        if (currentCellState == 0 && sum <= 0) newCellState = 1;
        else if (currentCellState == 1 && sum >= 5) newCellState = 0;
        else newCellState = currentCellState;

        return newCellState;
    }


    public int countVisiblePeople(int startRow, int startCol, int deltaRow, int deltaCol) {
        int r = startRow + deltaRow;
        int c = startCol + deltaCol;

        while (r >= 1 && r <= numRows && c >=1 && c <= numCols) {
            if (chairs[r][c] == 1 && currentState[r][c]==0) {
                // empty chair we can't see past
                return 0;
            }
            if (currentState[r][c] == 1) {
                return 1;
            }
            r += deltaRow;
            c += deltaCol;
        }
        return 0;
    }

    @Override
    public String toString() {
        StringBuilder sb = new StringBuilder();

        // Remember the margin is:
        // row = 0, row = numRows+1
        // col = 0, col = numCols+1
        for (int row = 1; row <= numRows; row++ ) {

            for (int col = 1; col <= numCols; col++) {
                char c;

                if (currentState[row][col] == 1) {
                    c = '#';
                } else if (chairs[row][col] == 1) {
                    c = 'L';
                } else {
                    c = '.';
                }

                sb.append(c);
            }

            // Don't append a trailing newline on the end of the
            // last row
            if (row < numRows) sb.append(System.lineSeparator());
        }

        return sb.toString();
    }


    public int getNumCols() {
        return numCols;
    }

    public void setNumCols(int numCols) {
        this.numCols = numCols;
    }

    public int getNumRows() {
        return numRows;
    }

    public void setNumRows(int numRows) {
        this.numRows = numRows;
    }

    public int[][] getChairs() {
        return chairs;
    }

    public void setChairs(int[][] chairs) {
        this.chairs = chairs;
    }

    public int[][] getCurrentState() {
        return currentState;
    }

    public void setCurrentState(int[][] currentState) {
        this.currentState = currentState;
    }

    public int[][] getNextState() {
        return nextState;
    }

    public void setNextState(int[][] nextState) {
        this.nextState = nextState;
    }
}
