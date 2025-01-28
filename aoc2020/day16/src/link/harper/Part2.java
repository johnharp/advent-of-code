package link.harper;

import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.regex.Pattern;

public class Part2 {
    public static void main(String[] args) throws Exception {
        final int numFields = 20;
        final int numTickets = 241;

        Input in = new Input();
        in.load("puzzle");

        ArrayList<Ticket> validTickets = Ticket.getValidTickets();
        int numValidTickets = validTickets.size();

        System.out.println("num Fields " + numFields);
        System.out.println("num tickets " + numTickets);
        System.out.println("num valid tickets " + numValidTickets);

        // outer array - the field position # in the ticket
        // the inner array - the field index in the rules
        ArrayList<ArrayList<Integer>> solution = new ArrayList<>();
        for (int i = 0; i<numFields; i++) {
            ArrayList<Integer> ok = new ArrayList<>();
            for (int j = 0; j<numFields; j++) {
                ok.add(j);
            }
            solution.add(ok);
        }

        for (Ticket ticket: validTickets) {
            for (int position = 0; position < numFields; position++) {

                int value = Ticket.getValueTicketField(ticket.ticketNum, position);
                ArrayList<Integer> notValid = FieldRule.getFieldsNotValidFor(value);

                ArrayList<Integer> valid = solution.get(position);
                for(int fieldNumber: notValid) {
                    if (valid.contains(fieldNumber)) {
                        valid.remove(Integer.valueOf(fieldNumber));
                    }
                }
            }
        }

        boolean solved = false;
        int[] solvedFields = new int[numFields];
        for (int i = 0; i<numFields; i++) {
            solvedFields[i] = -1;
        }

        while (!solved(solvedFields)) {

            for (int i = 0; i<numFields; i++) {
                ArrayList<Integer> valid = solution.get(i);
                if (valid.size() == 1) {
                    int solvedFieldNum = -1;
                    solvedFieldNum = valid.get(0);
                    solvedFields[i] = solvedFieldNum;

                    System.out.println("Solved - position " + i + " must be field " + solvedFieldNum);

                    for (int j = 0; j<numFields; j++) {
                        if (j != i) {
                            ArrayList<Integer> otherValid = solution.get(j);
                            if (otherValid.contains(solvedFieldNum)) {
                                otherValid.remove(Integer.valueOf(solvedFieldNum));

                                if (otherValid.size() == 1) {
                                    solvedFields[j] = otherValid.get(0);

                                }
                            }
                        }
                    }
                }
            }
        }

        System.out.println(Arrays.toString(solvedFields));

        long product = 1;
        Ticket myTicket = in.getMyTicket();

        for (int i = 0; i< numFields; i++) {
            int index = solvedFields[i];
            FieldRule rule = FieldRule.allFieldRules.get(index);

            if (rule.fieldName.startsWith("departure")) {
                System.out.println(rule.fieldName + " has value " + myTicket.fieldValues[i]);
                product *= myTicket.fieldValues[i];
            }
        }


        System.out.println("product is " + product);
    }

    public static boolean solved(int[] v) {
        boolean s = true;

        for (int i = 0; i<v.length; i++) {
            if (v[i] == -1) {
                s = false;
                break;
            }
        }
        return s;
    }
}
