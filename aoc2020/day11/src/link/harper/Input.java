package link.harper;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

public class Input {
    private BufferedReader reader = null;


    private List<String> lines;
    public List<String> getLines() {
        return lines;
    }

    public void readAll(String name) {
        try {
            FileReader fileReader = new FileReader(name);
            reader = new BufferedReader(fileReader);

            lines = new ArrayList<>();
            String line = line();
            while(line != null) {
                lines.add(line);
                line = line();
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }


    public String line() {
        String l = null;

        if (reader != null) {
            try {
                l = reader.readLine();

                if (l == null) {
                    reader.close();
                    reader = null;
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
        }

        return l;
    }

    public List<String> group() {
        List<String> lines = new ArrayList<>();
        String line = line();
        if (line == null) {
            reader = null;
            return null;
        }

        while (line != null && !line.equals("")) {
            lines.add(line);
            line = line();
        }
        return lines;
    }

    public void close() {
        try {
            reader.close();
        } catch (IOException e) {
            e.printStackTrace();
        }

        reader = null;
    }
}
