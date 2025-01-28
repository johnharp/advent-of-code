package link.harper;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class Input {
    private List<String> lines = new ArrayList<>();

    public List<String> getLines() {
        return lines;
    }

    public Input(String filename) throws FileNotFoundException {
        load(filename);
    }


    private void load(String filename) throws FileNotFoundException {
        // https://docs.oracle.com/javase/8/docs/api/java/util/Scanner.html
        Scanner scanner = new Scanner(
                new File(filename));

        while (scanner.hasNextLine()) {
            String line = scanner.nextLine();
            lines.add(line);
        }
    }
}
