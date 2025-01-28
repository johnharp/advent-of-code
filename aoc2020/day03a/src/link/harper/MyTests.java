package link.harper;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class MyTests {

    @Test
    public void verifyInputClass() {
        Input.Parse();

        Assertions.assertEquals(31, Input.numCols);
        Assertions.assertEquals(323, Input.numRows);

        Assertions.assertEquals(false, Input.treeAt(0,0));
        Assertions.assertEquals(true, Input.treeAt(1,9));

        // Pattern should repeat to the right
        Assertions.assertEquals(true, Input.treeAt(1,51));

    }
}
