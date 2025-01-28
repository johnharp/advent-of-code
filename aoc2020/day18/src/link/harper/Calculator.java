package link.harper;

import java.util.List;
import java.util.Stack;

public class Calculator {
    private Stack<String> operators = new Stack<>();
    private Stack<Long> operands = new Stack<>();

    public Long calculate(List<String>tokens) {

        for (int i = tokens.size()-1; i >=0; i--) {
            String token = tokens.get(i);
            if (token.equals("+") || token.equals("*") || token.equals(")")) {
                operators.push(token);
            }
            else if (token.equals("(")) {
                processUntilRightParenOrEnd();
            }
            else
            {
                Long v = Long.parseLong(token);
                operands.push(v);
            }
            System.out.println("Operators: " + operators);
            System.out.println("Operands:  " + operands);
            System.out.println();
        }
        processUntilRightParenOrEnd();
//        System.out.println("Operators: " + operators);
//        System.out.println("Operands:  " + operands);
//        System.out.println();
        return operands.get(0);
    }

    public void doOp(String operator, Long operand1, Long operand2) {
        Long result = 0L;

        if (operator.equals("+")) {
            result = operand1 + operand2;
        } else if (operator.equals("*")) {
            result = operand1 * operand2;
        }

        System.out.println(operand1 + " " + operator + " " + operand2 + " = " + result);

        operands.push(result);
    }

    public void processUntilRightParenOrEnd() {
        String operator = "?";

        while (!operators.empty()) {
            operator = operators.pop();
            if (operator.equals(")")) return;

            Long operand1 = operands.pop();
            Long operand2 = operands.pop();
            doOp(operator, operand1, operand2);
        }
    }


    public boolean isLong(String s) {
        try {
            Long.parseLong(s);
            return true;
        } catch (NumberFormatException nfe) {}
        return false;
    }
}
