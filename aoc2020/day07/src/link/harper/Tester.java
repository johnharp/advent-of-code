package link.harper;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class Tester {

    @Test
    public void BagSpecParseLine() throws Exception {
        BagSpec spec;

        spec = BagSpec.parseLine("muted purple bags contain no other bags.");
        Assertions.assertEquals("muted purple", spec.getColor());
        Assertions.assertEquals(new ArrayList<String>(), spec.getContains());

        spec = BagSpec.parseLine("pale maroon bags contain 1 dim aqua bag, 3 dotted fuchsia bags, 1 clear brown bag, 1 drab turquoise bag.");
        Assertions.assertEquals("pale maroon", spec.getColor());
        Assertions.assertEquals(1, spec.getContains().get(0).getCount());
        Assertions.assertEquals("dim aqua", spec.getContains().get(0).getColor());
        Assertions.assertEquals(3, spec.getContains().get(1).getCount());
        Assertions.assertEquals("dotted fuchsia", spec.getContains().get(1).getColor());
        Assertions.assertEquals(1, spec.getContains().get(2).getCount());
        Assertions.assertEquals("clear brown", spec.getContains().get(2).getColor());
        Assertions.assertEquals(1, spec.getContains().get(3).getCount());
        Assertions.assertEquals("drab turquoise", spec.getContains().get(3).getColor());
    }
}
