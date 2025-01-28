package link.harper;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class PasswordLine {
    public Integer minNumOccurrences;
    public Integer maxNumOccurrences;
    public String letter;
    public String password;

    public PasswordLine(String txt) {
        Pattern p = Pattern.compile("^([0-9]+)\\-([0-9]+)\\s+([a-z]):\\s+(.+)");
        Matcher m = p.matcher(txt);

        if (m.find()) {
            String minNumStr = m.group(1);
            minNumOccurrences = Integer.parseInt(minNumStr);

            String maxNumStr = m.group(2);
            maxNumOccurrences = Integer.parseInt(maxNumStr);

            letter = m.group(3);
            password = m.group(4);
        }
    }

    public Boolean isValid() {
        int count = 0;

        for (int i = 0; i<password.length(); i++) {
            char c = password.charAt(i);
            String cStr = String.valueOf(c);

            if (cStr.equals(letter)) {
                count++;
            }
        }


        if (count >= minNumOccurrences &&
            count <= maxNumOccurrences)
        {
            return true;
        }
        else {
            return false;
        }
    }

    public boolean isValidPart2() {
        boolean isOK = false;
        int count = 0;

        if (minNumOccurrences <= password.length() &&
        maxNumOccurrences <= password.length()) {
            char c1 = password.charAt(minNumOccurrences-1);
            char c2 = password.charAt(maxNumOccurrences-1);

            String c1Str = String.valueOf(c1);
            String c2Str = String.valueOf(c2);

            isOK = (c1Str.equals(letter) && !c2Str.equals(letter)) ||
                    (!c1Str.equals(letter) && c2Str.equals(letter));
        }

        return isOK;
    }
}
