package link.harper;


import java.util.Hashtable;

public class PartA {

    public static void main(String[] args) throws Exception {
        System.out.println("Part A Answer");
        Calculator calc = new Calculator();
        calc.loadInput("input.txt");

        Hashtable<Integer, Integer> diffs =
            calc.summarizeDifferences();

        System.out.println(diffs);

        int ones = diffs.get(1);
        int threes = diffs.get(3);

        System.out.println(ones + " one diffs * " + threes +
                " three diffs = " + ones * threes);

    }


}