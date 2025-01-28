package link.harper;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class ComputerTester {

    @Test
    public void testWriteRead() throws Exception {
        Computer comp = new Computer();
        comp.write(13197, 47579321);
        Assertions.assertEquals(
                47579321,
                comp.read(13197)
        );

        comp.write(13197, 1);
        Assertions.assertEquals(
                1,
                comp.read(13197)
        );
    }

    @Test
    public void testAddressOverflowException() {
        Computer comp = new Computer();
        try {
            comp.write(Computer.maxNumber+1, 111L);
        } catch (Exception ex) {
            return;
        }

        Assertions.assertEquals(false, true,
                "Should never reach here -- should have address overflow.");
    }

    @Test
    public void testValueOverflowException() {
        Computer comp = new Computer();
        try {
            comp.write(234, Computer.maxNumber+1);
        } catch (Exception ex) {
            return;
        }

        Assertions.assertEquals(false, true,
                "Should never reach here -- should have value overflow.");
    }
}
