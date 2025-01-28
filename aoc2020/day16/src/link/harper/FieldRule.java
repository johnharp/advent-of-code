package link.harper;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Hashtable;
import java.util.List;

public class FieldRule {
    public String fieldName;
    public int fieldNum;
    private Range range1;
    private Range range2;

    private static Hashtable<Integer, ArrayList<FieldRule>> ruleIndex = new Hashtable<>();
    // the number of fields = the number of rules
    private static int numRules = 0;
    public static ArrayList<FieldRule> allFieldRules = new ArrayList<>();
    public static ArrayList<FieldRule> getFieldRulesValidFor(int n) {
        return ruleIndex.get(n);
    }

    public static ArrayList<Integer> getFieldsValidFor(int n) {
        ArrayList<Integer> fieldNums = new ArrayList<>();
        if (ruleIndex.containsKey(n)) {
            for (FieldRule rule : ruleIndex.get(n)) {
                fieldNums.add(rule.fieldNum);
            }
        }
        Collections.sort(fieldNums);
        return fieldNums;
    }

    public static ArrayList<Integer> getFieldsNotValidFor(int n) {
        ArrayList<Integer> invalid = new ArrayList<>();
        ArrayList<Integer> valid = getFieldsValidFor(n);

        for(int i = 0; i<numRules; i++) {
            if (!valid.contains(i)) {
                invalid.add(i);
            }
        }

        return invalid;
    }


    public boolean contains(int n) {
        return range1.contains(n) ||
                range2.contains(n);
    }

    public FieldRule(String name, int min1, int max1, int min2, int max2) {
        fieldName = name;
        range1 = new Range(min1, max1);
        range2 = new Range(min2, max2);

        for (int i = min1; i<= max1; i++) {
            addToIndex(i);
        }

        for (int i = min2; i <= max2; i++) {
            addToIndex(i);
        }
        fieldNum = numRules;

        allFieldRules.add(this);
        numRules++;
    }

    private void addToIndex(int n)
    {
        ArrayList<FieldRule> validFieldRules;
        if (ruleIndex.containsKey(n)) {
            validFieldRules = ruleIndex.get(n);
        } else {
            validFieldRules = new ArrayList<>();
            ruleIndex.put(n, validFieldRules);
        }

        validFieldRules.add(this);
    }

    public static void printAll() {
        for(FieldRule rule: allFieldRules) {
            System.out.println(rule);
        }
    }

    @Override
    public String toString(){
        StringBuilder sb = new StringBuilder();
        sb.append(fieldName);
        sb.append(" (");
        sb.append(fieldNum);
        sb.append(") ");
        sb.append(" = ");
        sb.append(range1);
        sb.append(" ");
        sb.append(range2);
        return sb.toString();
    }
}
