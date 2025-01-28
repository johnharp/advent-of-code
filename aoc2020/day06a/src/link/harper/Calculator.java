package link.harper;

import java.util.List;

public class Calculator {
    public int numYesForGroup(List<String> group) {
        // default value of boolean is false
        // if question is answered yes, set position to true
        boolean[] answers = new boolean[26];
        int sum = 0;

        for(String person : group) {
            for(char c : person.toCharArray()) {
                int index = c - 'a';

                answers[index] = true;
            }
        }

        for(boolean v : answers) {
            if (v) {
                sum++;
            }
        }
        return sum;
    }

    public int numYesForGroupSecondCriteria(List<String> group) {
        // default value of int is 0
        // if question is answered yes, add one to the position
        // we'll only consider the question a yes if total is num people
        int[] answers = new int[26];
        int sum = 0;
        int numPeopleInGroup = 0;

        for(String person : group) {
            numPeopleInGroup++;
            for(char c : person.toCharArray()) {
                int index = c - 'a';

                answers[index]++;
            }
        }

        for(int v : answers) {
            if (v == numPeopleInGroup) {
                sum++;
            }
        }
        return sum;
    }
}
