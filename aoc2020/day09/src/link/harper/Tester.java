package link.harper;


import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class Tester {

    @Test
    public void testDoesNumberExist() throws Exception {
        Calculator calc = new Calculator();
        calc.loadInput("example-input.txt");


        Assertions.assertEquals(true,
                calc.doesNumberExist(25, 0, 3));
        Assertions.assertEquals(false,
                calc.doesNumberExist(25, 0, 2));
        Assertions.assertEquals(true,
                calc.doesNumberExist(576, 0, 19));

    }

    @Test
    public void testIsNumberAtIndexValid() throws Exception {
        Calculator calc = new Calculator();
        calc.loadInput("example-input.txt");

        Assertions.assertEquals(true,
                calc.isNumberAtIndexValid(5, 5));
    }

    @Test
    public void testSumNumbersBetween() throws ArithmeticException {
        Calculator calc = new Calculator();
        calc.loadInput("example-input.txt");

        Assertions.assertEquals(182,
                calc.sumNumbersBetween(0, 5));
    }

    @Test
    public void findRangeThatSumsToNum() throws ArithmeticException {
        Calculator calc = new Calculator();
        calc.loadInput("example-input.txt");

        Assertions.assertEquals(127,
                calc.sumNumbersBetween(2, 5));
    }
}
