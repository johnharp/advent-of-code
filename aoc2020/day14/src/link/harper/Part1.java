package link.harper;

import java.io.File;
import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Part1 {
    private static Pattern maskPattern = Pattern.compile("^mask = ([01X]+)$");
    private static Pattern memPattern = Pattern.compile("^mem\\[([0-9]+)\\] = ([0-9]+)$");

    private static long thirtySixOnesMask = 68719476735L;

    public static void main(String[] args) throws Exception{
        //Input in = new Input("example1-input.txt");
        Input in = new Input("input.txt");

        long totalmasks = 0;
        long totalwrites = 0;

        Computer comp = new Computer();
        Mask mask = new Mask("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");

        for(String line : in.getLines()) {
            Matcher maskMatcher = maskPattern.matcher(line);
            Matcher memMatcher = memPattern.matcher(line);

            if (maskMatcher.find()) {
                mask = new Mask(maskMatcher.group(1));
                totalmasks++;
            } else if (memMatcher.find()) {
                long addr = Long.parseLong(memMatcher.group(1));
                long value = Long.parseLong(memMatcher.group(2));

                long maskedValue = mask.apply(value);
                comp.write(addr, maskedValue);
                totalwrites++;
            } else {
                throw new Exception("Unknown input found: " + line);
            }
        }

        comp.coredump();

        long sum = comp.sumAllMemory();

        System.out.println("total masks " + totalmasks);
        System.out.println("total writes " + totalwrites);
        System.out.println("Sum of memory = " + sum);

        System.out.println("total memory spots = " + comp.getMemory().keySet().size());
    }

}
