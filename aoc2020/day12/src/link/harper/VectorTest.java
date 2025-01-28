package link.harper;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class VectorTest {
    @Test
    public void testRotateVector() throws Exception{
        Vector v = new Vector(1, 0);

        v.rotate(90);
        Assertions.assertEquals(new Vector(0, -1), v);

        v.rotate(90);
        Assertions.assertEquals(new Vector(-1, 0), v);

        v.rotate(90);
        Assertions.assertEquals(new Vector(0, 1), v);

        v.rotate(90);
        Assertions.assertEquals(new Vector(1, 0), v);

        v.rotate(-90);
        Assertions.assertEquals(new Vector(0, 1), v);

        v.rotate(-90);
        Assertions.assertEquals(new Vector(-1, 0), v);

        v.rotate(-90);
        Assertions.assertEquals(new Vector(0, -1), v);

        v.rotate(-90);
        Assertions.assertEquals(new Vector(1, 0), v);

        v.rotate(180);
        Assertions.assertEquals(new Vector(-1, 0), v);

        v.rotate(180);
        Assertions.assertEquals(new Vector(1, 0), v);

        v.rotate(-180);
        Assertions.assertEquals(new Vector(-1, 0), v);

        v.rotate(90);
        Assertions.assertEquals(new Vector(0, 1), v);

        v.rotate(180);
        Assertions.assertEquals(new Vector(0, -1), v);
    }
}
