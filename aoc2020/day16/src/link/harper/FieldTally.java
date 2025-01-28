package link.harper;

import java.util.ArrayList;

public class FieldTally {
    ArrayList<FieldRule> possibleRules = new ArrayList<>();
    public boolean hasSinglePossibleRule() {
        return possibleRules.size() == 1;
    }

    public void addRule(FieldRule rule) {
        boolean alreadyContains = false;
        for(FieldRule existingRule: possibleRules) {
            if (existingRule == rule) {
                alreadyContains = true;
                break;
            }
        }

        if (!alreadyContains) possibleRules.add(rule);
    }
}
