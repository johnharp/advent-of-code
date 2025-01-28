package link.harper;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class Tester {

    @Test
    public void testReaderLine() {
        Input input = new Input();
        input.open("input.txt");

        Assertions.assertEquals("zvxc", input.line());
        Assertions.assertEquals("dv", input.line());
        Assertions.assertEquals("vh", input.line());
        Assertions.assertEquals("xv", input.line());
        Assertions.assertEquals("jvem", input.line());
        Assertions.assertEquals("", input.line());

        input.close();
    }

//    @Test
//    public void testCalculator() {
//        Calculator c = new Calculator();
//        List<String> data;
//
//        data = new ArrayList<>();
//        data.add("abc");
//        Assertions.assertEquals(3, c.numYesForGroup(data));
//
//        data = new ArrayList<>();
//        data.add("a");
//        data.add("b");
//        data.add("c");
//        Assertions.assertEquals(3, c.numYesForGroup(data));
//
//        data = new ArrayList<>();
//        data.add("ab");
//        data.add("ac");
//        Assertions.assertEquals(3, c.numYesForGroup(data));
//
//        data = new ArrayList<>();
//        data.add("a");
//        data.add("a");
//        data.add("a");
//        data.add("a");
//        Assertions.assertEquals(1, c.numYesForGroup(data));
//
//        data = new ArrayList<>();
//        data.add("b");
//        Assertions.assertEquals(1, c.numYesForGroup(data));
//    }

    @Test
    public void testCalculatorSecondCriteria() {
        Calculator c = new Calculator();
        List<String> data;

        data = new ArrayList<>();
        data.add("abc");
        Assertions.assertEquals(3, c.numYesForGroupSecondCriteria(data));

        data = new ArrayList<>();
        data.add("a");
        data.add("b");
        data.add("c");
        Assertions.assertEquals(0, c.numYesForGroupSecondCriteria(data));

        data = new ArrayList<>();
        data.add("ab");
        data.add("ac");
        Assertions.assertEquals(1, c.numYesForGroupSecondCriteria(data));

        data = new ArrayList<>();
        data.add("a");
        data.add("a");
        data.add("a");
        data.add("a");
        Assertions.assertEquals(1, c.numYesForGroupSecondCriteria(data));

        data = new ArrayList<>();
        data.add("b");
        Assertions.assertEquals(1, c.numYesForGroupSecondCriteria(data));
    }

}
