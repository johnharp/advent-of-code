package link.harper;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class TestMask {

    @Test
    public void test21212() throws Exception {
        String maskStr = "0010X10111000100110001X0X1000100X101";
        //                111111111111111111111111111111111111
        // value is             110011111000100000101001001100
        // masked value   001001011100010011000100110001001101

        long value = 870451788;
        Mask m = new Mask(maskStr);
        value = m.apply(value);

        Assertions.assertEquals(10138438733L,
                value);
    }

    @Test
    public void test26924() throws Exception {
        String maskStr = "11X0X0X1011001XX1001111X011100XX0000";
        // value is                                  100010000
        // masked value   110000010110010010011110011100010000

        long value = 272;
        Mask m = new Mask(maskStr);
        value = m.apply(value);

        Assertions.assertEquals(51913549584L,
                value);
    }

    @Test
    public void test62803() throws Exception {
        String maskStr = "11X00110111001001001100X10X1XX00X100";
        // value is             111010010110100110110101111101
        // masked value   110001101110010010011000100101001100

        long value = 979004797;
        Mask m = new Mask(maskStr);
        value = m.apply(value);

        Assertions.assertEquals(53389920588L,
                value);
    }

    @Test
    public void test26867() throws Exception {
        String maskStr = "0X000001111001010X1011100100001X0X0X";
        // value is                              1100001011100
        // masked value   000000011110010100101110010000110100

        long value = 6236;
        Mask m = new Mask(maskStr);
        value = m.apply(value);

        Assertions.assertEquals(508748852,
                value);
    }
}
