package link.harper;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.Scanner;

public class Input {
    private ArrayList<String> ruleLines;
    private ArrayList<String> messageLines;

    public ArrayList<String> getRuleLines() {
        return ruleLines;
    }

    public ArrayList<String> getMessageLines() {
        return messageLines;
    }

    public Input(String filename) throws FileNotFoundException {
        ruleLines = new ArrayList<>();
        messageLines = new ArrayList<>();

        Scanner scanner = new Scanner(
                new File(filename));

        while(scanner.hasNext()) {
            String line = scanner.nextLine();
            if (line.equals("")) {
                break;
            }
            ruleLines.add(line);
        }

        while(scanner.hasNext()) {
            String line = scanner.nextLine();
            messageLines.add(line);
        }
    }
}
