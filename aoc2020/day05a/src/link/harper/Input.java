package link.harper;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;

public class Input {
    private BufferedReader reader;

    public void open(String name) {
        try {
            FileReader fileReader = new FileReader(name);
            reader = new BufferedReader(fileReader);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public String line() {
        String l = null;
        try {
            l = reader.readLine();

            if (l == null) {
                reader.close();
            }
        } catch (IOException e) {
            e.printStackTrace();
        }

        return l;
    }
}
