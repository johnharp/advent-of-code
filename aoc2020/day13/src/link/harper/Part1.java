package link.harper;

public class Part1 {
    public static void main(String[] args) throws Exception {
        System.out.println("Part A Answer");
        Calculator calc = new Calculator();
        //calc.loadInput("example1-input.txt");
        calc.loadInput("input.txt");

        calc.earliestPossibleBus();

    }
}
