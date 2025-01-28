package link.harper;

import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.List;
import java.util.Set;

public class Main {

    public static void main(String[] args) throws Exception {
        Input in = new Input("example3-input.txt");

        System.out.print("8: 42");
        for (int i = 0; i < 60; i++) {
            System.out.print(" | 42");

            for (int j = 1; j <= i+1; j++) {
                System.out.print(" 42");
            }
        }
        System.out.println();

        System.out.print("11: 42 31");
        for (int i = 0; i < 60; i++) {
            System.out.print(" |");

            for (int j = 1; j <= i+1; j++) {
                System.out.print(" 42");
            }

            for (int j = 1; j <= i+1; j++) {
                System.out.print(" 31");
            }
        }
        System.out.println();
        System.out.println();

        for (String line: in.getRuleLines()) {
            Rule r = new Rule(line);
        }

        int numValid = 0;

        //System.out.println("Rule 0 refers to: " );
        //System.out.println(Rule.get(0).uniqueRuleIds());

        System.out.println(Rule.get(11));
        System.out.println(Rule.get(8));

        Rule r = Rule.get(0);
        List<String> valids = r.getValids();

        for(String valid: valids) {
            for (String line: in.getMessageLines()) {
                if (line.equals(valid)) {
                    //System.out.println("Matched: " + line);
                    numValid++;
                }
            }
        }

        System.out.println("Total valid: " + numValid);




//        ArrayList<String> l1 = new ArrayList<>(){{
//            add("A");
//            add("B");
//        }};
//        ArrayList<String> l2 = new ArrayList<>(){{
//            add("C");
//            add("D");
//        }};
//
//        System.out.println(Rule.permuteTwo(l1, l2));
    }
}
