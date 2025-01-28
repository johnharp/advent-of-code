package link.harper;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;

public class PartB {
    final static long InvalidNum = 69316178;

    public static void main(String[] args) throws Exception {
        System.out.println("Part B Answer");

        Calculator calc = new Calculator();
        calc.loadInput("input.txt");

        Range answer = calc.findRangeThatSumsToNum(InvalidNum);

        System.out.println("Sum of min and max values in this range = " +
                calc.findSumOfMinMaxInRange(answer));
    }


}
