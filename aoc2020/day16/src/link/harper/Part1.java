package link.harper;

import java.util.ArrayList;
import java.util.List;

public class Part1 {
    public static void main(String[] args) throws Exception {
        Input in = new Input();
        in.load("puzzle");

        List<Integer> invalidFieldValues = new ArrayList<>();

        System.out.println("Invalid Tickets:");
        for(Ticket t: Ticket.getAllTickets()) {
            boolean isValid = true;
            for(int fieldValue: t.fieldValues) {
                List<FieldRule> fieldRules = FieldRule.getFieldRulesValidFor(fieldValue);
                if (fieldRules == null || fieldRules.size() == 0) {
                    isValid = false;
                    invalidFieldValues.add(fieldValue);
                }
            }

            if (!isValid) {
                System.out.println(t);
            }
        }
        System.out.println("Invalid Field Values:");
        System.out.println(invalidFieldValues);

        System.out.println("Sum of Invalid Field Values: ");
        int sum = 0;
        for(int invalidFieldValue: invalidFieldValues) {
            sum += invalidFieldValue;
        }
        System.out.println(sum);
    }
}
