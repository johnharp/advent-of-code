package link.harper;

import java.io.Console;

public class Main {

    public static long traverse(int incrCol, int incrRow) {
        final int startingCol = 0;
        final int startingRow = 0;

        int row = startingRow;
        int col = startingCol;

        long numTreesEncountered = 0;

        while(row < Input.numRows) {
            if (Input.treeAt(row, col)) numTreesEncountered++;

            row += incrRow;
            col += incrCol;
        }

        System.out.println("Number of trees encountered Right " +
                incrCol + ", down " + incrRow + " = " +
                numTreesEncountered);

        return numTreesEncountered;
    }
    public static void main(String[] args) {
        Input.Parse();

        long product =
            traverse(1, 1) *
            traverse(3, 1) *
            traverse(5, 1) *
            traverse(7, 1) *
            traverse(1, 2);

        System.out.println("Product = " + product);
    }
}
