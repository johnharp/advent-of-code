package link.harper;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;

public class PartB {

    private static Calculator _calc;
    private static HashSet<String> _masterList;

    public static void main(String[] args) throws Exception {
        System.out.println("Part B Answer");

        _calc = new Calculator();
        _calc.loadSpecs();


        long totalNumber = findContents("shiny gold");
        System.out.println("Total contained: " + totalNumber);

    }

    /**
     * find contents of a bag
     * @param color
     */
    private static long findContents(String color) {
        String str = "";
        BagSpec spec = _calc.findByColor(color);
        long numContained = 0;

        str += color + " contains [";
        for (BagSpec subSpec : spec.getContains()) {
            str += subSpec.getCount() + " " + subSpec.getColor() + "  ";
            numContained += subSpec.getCount() + (
                    subSpec.getCount()
                    * findContents(subSpec.getColor()));
        }
        str += "]";

        System.out.println(str);
        return numContained;
    }
}
