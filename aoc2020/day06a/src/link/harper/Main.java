package link.harper;

import java.util.List;

public class Main {

    public static void main(String[] args) {
        Input input = new Input();
        input.open("input.txt");

        Calculator calc = new Calculator();

        List<String> group = input.group();

        long sum = 0;
        while(group != null) {
          //sum += calc.numYesForGroup(group);
          sum += calc.numYesForGroupSecondCriteria(group);

          group = input.group();
        }

        System.out.println(sum);
    }
}
