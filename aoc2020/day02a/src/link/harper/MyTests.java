package link.harper;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class MyTests {
    @Test
    public void verifyPasswordLineConstructor() {
        PasswordLine l1 = new PasswordLine("13-16 k: kkkkkgmkbvkkrskhd\n");

        // assert statements
        Assertions.assertEquals(13,
                l1.minNumOccurrences,
                "minNumOccurrences should be 13");

        Assertions.assertEquals(16,
                l1.maxNumOccurrences,
                "maxNumOccurrences should be 16");

        Assertions.assertEquals("k",
                l1.letter,
                "letter should be 'k'");

        Assertions.assertEquals("kkkkkgmkbvkkrskhd",
                l1.password,
                "password should be 'kkkkkgmkbvkkrskhd'");
    }
}
