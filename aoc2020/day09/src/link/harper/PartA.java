package link.harper;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;

public class PartA {

    public static void main(String[] args) throws Exception {
        System.out.println("Part A Answer");

        Calculator calc = new Calculator();
        calc.loadInput("input.txt");

        calc.findFirstInvalidIndex(25);
    }


}
