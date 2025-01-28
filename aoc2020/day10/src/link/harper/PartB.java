package link.harper;


import java.util.Arrays;

public class PartB {

    public static void main(String[] args) throws Exception {
        System.out.println("Part B Answer");

        Calculator calc = new Calculator();
        calc.loadInput("input.txt");

        System.out.println(Arrays.toString(calc.getData()));
        System.out.println("num entries = " + calc.getData().length);

        System.out.println(calc.summarizeDifferences());
        System.out.println(calc.getGroupSizes());

        long totalPossibilities = 1;
        for (int groupSize: calc.getGroupSizes()) {
            long num = calc.numPossibilitiesInGroupSized(groupSize);

            totalPossibilities = totalPossibilities * num;
        }

        System.out.println("Total possibilities: " + totalPossibilities);
    }


}