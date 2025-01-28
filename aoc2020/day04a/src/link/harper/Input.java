package link.harper;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

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

    public List<String> fields(String line) {
        List<String> allFields = new ArrayList<String>();

        String fieldPattern = "\\S\\S\\S:\\S*";
        Pattern p = Pattern.compile(fieldPattern);
        Matcher m = p.matcher(line);

        while (m.find()) {
            allFields.add(m.group());
        }
        return allFields;
    }
}
