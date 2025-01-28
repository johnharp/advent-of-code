package link.harper;

import java.io.File;
import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Part2 {
    private static Pattern maskPattern = Pattern.compile("^mask = ([01X]+)$");
    private static Pattern memPattern = Pattern.compile("^mem\\[([0-9]+)\\] = ([0-9]+)$");

    private static long thirtySixOnesMask = 68719476735L;

    public static void main(String[] args) throws Exception{
        //Input in = new Input("example2-input.txt");
        Input in = new Input("input.txt");

        Computer comp = new Computer();
        String mask = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
        //Mask mask = new Mask("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");

        for(String line : in.getLines()) {
            Matcher maskMatcher = maskPattern.matcher(line);
            Matcher memMatcher = memPattern.matcher(line);

            if (maskMatcher.find()) {
                mask =maskMatcher.group(1);
            } else if (memMatcher.find()) {
                long addr = Long.parseLong(memMatcher.group(1));
                long value = Long.parseLong(memMatcher.group(2));

                comp.handlePart2(mask, addr, value);
            } else {
                throw new Exception("Unknown input found: " + line);
            }
        }

        comp.coredump();

        long sum = comp.sumAllMemory();
        System.out.println("Sum of memory = " + sum);

    }

}
