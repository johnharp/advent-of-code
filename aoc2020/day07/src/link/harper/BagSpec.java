package link.harper;

import java.util.ArrayList;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class BagSpec {
    private String color;
    private int count;
    private List<BagSpec> contains;

    public String getColor() {
        return color;
    }

    public void setColor(String color) {
        this.color = color;
    }

    public int getCount() {
        return count;
    }

    public void setCount(int count) {
        this.count = count;
    }

    public List<BagSpec> getContains() {
        return contains;
    }

    public void setContains(List<BagSpec> contains) {
        this.contains = contains;
    }

    public int getNumBagsWithin() {
        int num = 0;

        if (this.contains != null) {
            for (BagSpec b : this.contains) {
                num += b.count;
            }
        }
        return num;
    }
    public static BagSpec parseLine(String line) throws Exception {
        //  ([0-9]+)(,\s*[0-9]+)*.$
        BagSpec spec = new BagSpec();

        String linePattern = "^(\\S+\\s+\\S+)\\s+bag[s]?\\s+contain\\s+(.*)$";
        Pattern p = Pattern.compile(linePattern);
        Matcher m = p.matcher(line);

        if (m.find()) {
            spec.color = m.group(1);
            spec.count = 0;
            spec.contains = new ArrayList<>();

            if (m.group(2).contains("no other bags")) {
                // skip populating the children
            } else {
                String[] items = m.group(2).split(",");
                for(String item : items) {
                    String itemPatternStr = "(\\d+)\\s+(\\S+\\s+\\S+) bag";
                    Pattern itemPattern = Pattern.compile(itemPatternStr);
                    Matcher itemMatcher = itemPattern.matcher(item);

                    if (itemMatcher.find()) {
                        BagSpec subBag = new BagSpec();
                        subBag.count = Integer.parseInt(itemMatcher.group(1));
                        subBag.color = itemMatcher.group(2);
                        spec.contains.add(subBag);
                    }
                }
            }

        } else {
            throw new Exception("Malformed input: " + "'" + line + "'");
        }

        //System.out.println(spec);
        return spec;
    }

    @Override
    public String toString() {
        String str = "";
        if (this.count > 0) {
            str += this.count + " ";
        }

        str += this.color + ": [";

        if (this.contains != null) {
            for (int i = 0; i<this.contains.size(); i++) {
                if (i > 0) str += " ";
                str += this.contains.get(i).toString();
            }
        }
        str += "]";

        return str;
    }
}
