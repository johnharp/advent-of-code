package link.harper;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;

public class PartA {
    private static Calculator _calc;
    private static HashSet<String> _masterList;

    public static void main(String[] args) throws Exception {
        System.out.println("Part A Answer");



        _calc = new Calculator();
        _calc.loadSpecs();

        _masterList = new HashSet<>();

        find("shiny gold");
        System.out.println(_masterList);
        System.out.println(_masterList.size());
    }

    private static void find(String color) {
        List<String> canContain = _calc.findAllThatCanContainColor(color);
        List<String> addedNewColors = new ArrayList<>();

        for (String c : canContain) {
            if (!_masterList.contains(c)) {
                _masterList.add(c);

                find(c);
            }
        }
    }
}
