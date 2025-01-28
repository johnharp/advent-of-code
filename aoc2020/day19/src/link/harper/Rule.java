package link.harper;

import org.junit.jupiter.api.Test;

import javax.management.relation.Role;
import java.util.*;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Rule {
    private static Hashtable<Integer, Rule> ruleIndex =
            new Hashtable<>();

    public static Set<Integer> getRuleNumbers() {
        return ruleIndex.keySet();
    }

    public static Rule get(Integer ruleNum) {
        return ruleIndex.get(ruleNum);
    }

    private final static Pattern rootPattern = Pattern.compile("^([0-9]+): \"([a-z])\"");
    private final static Pattern singlePattern =
            Pattern.compile("^([0-9]+): ([0-9]+(?: [0-9]+)*)$");
    private final static Pattern doublePattern =
            Pattern.compile("^([0-9]+): ([0-9]+(?: [0-9]+)* \\| [0-9]+(?: [0-9]+)*)");

    private int num;
    private String rootValid;
    private ArrayList<ArrayList<Integer>> subGroups;

    public Rule(String str) throws Exception {
        rootValid = null;
        subGroups = new ArrayList<>();

        Matcher m0 = rootPattern.matcher(str);
        Matcher m1 = singlePattern.matcher(str);
        Matcher m2 = doublePattern.matcher(str);

        if (m0.find()) {
            num = Integer.parseInt(m0.group(1));
            rootValid = m0.group(2);
        } else if (m1.find()) {
            num = Integer.parseInt(m1.group(1));
            ArrayList<Integer> group1 = new ArrayList<>();

            String[] split1 = m1.group(2).split(" " );


            for(int i =0; i<split1.length; i++) {
                Integer val = Integer.parseInt(split1[i]);
                group1.add(val);
            }
            subGroups.add(group1);

        } else if (m2.find()) {
            num = Integer.parseInt(m2.group(1));

            String[] split0 = str.split(":");

            String[] split1 = split0[1].split(" \\| " );
            for (int i = 0; i<split1.length; i++) {
                ArrayList<Integer> group = new ArrayList<>();

                String grpline = split1[i];

                String[] split2 = grpline.split(" ");
                for(int j =0; j<split2.length; j++) {
                    if (split2[j].equals("")) continue;
                    Integer val = Integer.parseInt(split2[j]);
                    group.add(val);
                }
                subGroups.add(group);
            }
        } else {
            throw new Exception("Unknown rule pattern: " + str);
        }

        ruleIndex.put(num, this);
    }

    /**
     *
     * @param msg
     * @return
     * "X" not valid
     * "" valid and consumed everything
     * "...anything else..." valid and some left
     */
    public String isValid(String msg) {
        if (rootValid != null) {
            if (msg.startsWith(rootValid)) {
                msg = msg.replaceFirst(rootValid, "");
                return msg;
            } else {
                return "X";
            }
        }

        for(ArrayList<Integer> group : subGroups) {
            String groupmsg = msg;

            for (int i = 0; i<group.size(); i++) {
                Integer num = group.get(i);
                Rule r = Rule.get(num);
                groupmsg = r.isValid(groupmsg);

                if (groupmsg.equals("X")) return "X";
            }

        }
        return false;
    }

    public boolean groupIsValid(ArrayList<Integer> ids, String msg) {

    }

    public ArrayList<String> getValids() {
        ArrayList<String> valids = new ArrayList<>();
        if (rootValid == null) {
            for (ArrayList<Integer> subGroup: subGroups) {
                for (String valid: getValidsForSubGroup(subGroup)) {
                    valids.add(valid);
                }

//                StringBuilder sb = new StringBuilder();
//                for (Integer ruleNum: subGroup) {
//                    Rule sub = ruleIndex.get(ruleNum);
//                    for (String v: sub.getValids()) {
//                        sb.append(v);
//                    }
//                }
//                valids.add(sb.toString());
            }
        } else {
            valids.add(rootValid);
        }

        return valids;
    }

    private ArrayList<String> getValidsForSubGroup(ArrayList<Integer> groupList) {

        ArrayList<ArrayList<String>> preResult = new ArrayList<>();

        for (Integer ruleid: groupList) {
            Rule rule = get(ruleid);
            ArrayList<String> ruleValids = rule.getValids();
            preResult.add(ruleValids);
        }

        while (preResult.size() > 1) {
            ArrayList<String> combined = permuteTwo(preResult.get(0), preResult.get(1));
            preResult.set(0, combined);
            preResult.remove(1);
        }

        return preResult.get(0);
    }

    public static ArrayList<String> permuteTwo(ArrayList<String> first, ArrayList<String> second) {
        ArrayList<String> result = new ArrayList<>();
        for (int i = 0; i<first.size(); i++) {
            for (int j = 0; j< second.size(); j++) {
                String s = first.get(i) + second.get(j);
                result.add(s);
            }
        }
        return result;
    }

    public Set<Integer> uniqueRuleIds() {
        Set<Integer> ids = new HashSet<>();

        if (rootValid != null) {
            ids.add(num);
        } else {
            for (ArrayList<Integer> groupList: subGroups) {
                for (Integer id: groupList) {
                    Rule r = Rule.get(id);
                    Set<Integer> moreIds = r.uniqueRuleIds();
                    ids.add(id);
                    ids.addAll(moreIds);
                }
            }
        }

        return ids;
    }

    @Override
    public String toString() {
        Set<String> uniqueRuleIds = new HashSet<>();

        StringBuilder sb = new StringBuilder();
        sb.append(num);
        sb.append(": ");
        sb.append(subGroups);
        sb.append(getValids());

        return sb.toString();

    }
}
