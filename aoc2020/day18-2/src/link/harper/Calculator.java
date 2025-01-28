package link.harper;

import java.util.List;
import java.util.Stack;

public class Calculator {
    private Stack<String> operators = new Stack<>();
    private Stack<Long> operands = new Stack<>();

    public Long calculate(List<String>tokens) {

        //for (int i = tokens.size()-1; i >=0; i--) {
        for (int i = 0; i <tokens.size(); i++) {
            String token = tokens.get(i);


            if (token.equals("(")) {
                operators.push(token);
            }

            else if (token.equals(")")) {
                while(!operators.peek().equals("(")) {
                    doOp();
                }
                operators.pop(); // remove the "("
            }
            else if (token.equals("+") || token.equals("*")) {
                if (operators.empty()) {
                    operators.push(token);
                } else if (token.equals("+")) {
                    operators.push(token);
                } else if (token.equals("*")){
                    while (!operators.empty() && operators.peek().equals("+")) {
                        doOp();
                    }
                    operators.push(token);
                }
            } else {
                Long v = Long.parseLong(token);
                operands.push(v);
            }
            System.out.println("Operators: " + operators);
            System.out.println("Operands:  " + operands);
            System.out.println();
        }

        while (!operators.empty()) {
            doOp();
        }
//        processUntilLeftParenOrEnd();
//        System.out.println("Operators: " + operators);
//        System.out.println("Operands:  " + operands);
//        System.out.println();
        return operands.get(0);
    }

    public void doOp() {
        Long result = 0L;

        String operator = operators.pop();
        Long operand1 = operands.pop();
        Long operand2 = operands.pop();

        if (operator.equals("+")) {
            result = operand1 + operand2;
        } else if (operator.equals("*")) {
            result = operand1 * operand2;
        }

        System.out.println(operand1 + " " + operator + " " + operand2 + " = " + result);

        operands.push(result);
    }

//    public void processUntilLeftParenOrEnd() {
//        String operator = "?";
//
//        while (!operators.empty()) {
//            operator = operators.pop();
//            if (operator.equals("(")) return;
//
//            Long operand1 = operands.pop();
//            Long operand2 = operands.pop();
//            doOp(operator, operand1, operand2);
//        }
//    }

//    public void processUntilLTimesOrEnd() {
//        String operator = "?";
//
//        while (!operators.empty()) {
//            if (operators.peek().equals("*")) return;
//
//            operator = operators.pop();
//            if (operator.equals("(")) return;
//
//            Long operand1 = operands.pop();
//            Long operand2 = operands.pop();
//            doOp(operator, operand1, operand2);
//        }
//    }


    public boolean isLong(String s) {
        try {
            Long.parseLong(s);
            return true;
        } catch (NumberFormatException nfe) {}
        return false;
    }
}
