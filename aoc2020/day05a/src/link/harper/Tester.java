package link.harper;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class Tester {

    @Test
    public void testCalculator() {
        Calculator c = new Calculator();

        String spec;

        spec = "BFFFBBFRRR";
        Assertions.assertEquals(70, c.SeatRow(spec));
        Assertions.assertEquals(7, c.SeatColumn(spec));
        Assertions.assertEquals(567, c.SeatId(spec));

        spec = "FFFBBBFRRR";
        Assertions.assertEquals(14, c.SeatRow(spec));
        Assertions.assertEquals(7, c.SeatColumn(spec));
        Assertions.assertEquals(119, c.SeatId(spec));

        spec = "BBFFBBFRLL";
        Assertions.assertEquals(102, c.SeatRow(spec));
        Assertions.assertEquals(4, c.SeatColumn(spec));
        Assertions.assertEquals(820, c.SeatId(spec));
    }
}
