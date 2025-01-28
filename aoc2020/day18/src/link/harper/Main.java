package link.harper;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Main {

    public static void main(String[] args) throws FileNotFoundException {
        Scanner scanner = new Scanner(
                new File("input.txt"));

        Long sum = 0L;
        while(scanner.hasNext()) {
            Calculator calc = new Calculator();

            String line = scanner.nextLine();
            List<String> tokens = parse(line);
            Long v = calc.calculate(tokens);
            sum += v;
        }

        System.out.println("Sum: " + sum);
        //calc.calculate(tokens);


    }

    public static List<String> parse(String str) {
        List<String> results = new ArrayList<>();

        Pattern p = Pattern.compile("([1-9]+|\\+|\\*|\\(|\\))");
        Matcher m = p.matcher(str);

        while(m.find()) {
            results.add(m.group(1));
        }

        return results;
    }
}
