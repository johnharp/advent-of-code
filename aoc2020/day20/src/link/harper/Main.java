package link.harper;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.Scanner;

public class Main  {

    public static void main(String[] args) throws FileNotFoundException {
	    Scanner scanner = new Scanner(new File("example1-input.txt"));

	    //while(scanner.hasNext()) {
	        String tileNumStr = scanner.findInLine("[0-9]+");
            int tileNum = Integer.parseInt(tileNumStr);
            scanner.next();

            System.out.println("Tile: " + tileNum);
            for (int i = 0; i<10; i++) {
                String line = scanner.nextLine();
                System.out.println(line);
            }

        //}
    }
}
