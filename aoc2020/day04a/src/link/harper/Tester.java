package link.harper;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class Tester {

    @Test
    public void testIsValidHairColor() {
        Assertions.assertEquals(true, Validator.isValidHairColor("#123abc"));
        Assertions.assertEquals(false, Validator.isValidHairColor("#123abz"));
        Assertions.assertEquals(false, Validator.isValidHairColor("123abc"));
        Assertions.assertEquals(false, Validator.isValidHairColor("#123abca"));

    }

    @Test
    public void testIsValidEyeColor() {
        Assertions.assertEquals(true, Validator.isValidEyeColor("amb"));
        Assertions.assertEquals(true, Validator.isValidEyeColor("blu"));
        Assertions.assertEquals(true, Validator.isValidEyeColor("brn"));
        Assertions.assertEquals(true, Validator.isValidEyeColor("gry"));
        Assertions.assertEquals(true, Validator.isValidEyeColor("grn"));
        Assertions.assertEquals(true, Validator.isValidEyeColor("hzl"));
        Assertions.assertEquals(true, Validator.isValidEyeColor("oth"));
        Assertions.assertEquals(false, Validator.isValidEyeColor("aamb"));
    }

    @Test
    public void testIsValidPassportId() {
        Assertions.assertEquals(true, Validator.isValidPassportId("000000012"));
        Assertions.assertEquals(false, Validator.isValidPassportId("0000000120"));
        Assertions.assertEquals(true, Validator.isValidPassportId("123456789"));
        Assertions.assertEquals(false, Validator.isValidPassportId("1234a6789"));
    }
}
