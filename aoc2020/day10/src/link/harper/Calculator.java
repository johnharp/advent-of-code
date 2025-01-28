package link.harper;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Hashtable;
import java.util.List;

public class Calculator {
    int[] data;
    public int[] getData() {
        return data;
    }

    Hashtable<Integer, Integer> differences = new Hashtable<>();

    public void loadInput(String filename) {
        List<Integer> dataList = new ArrayList<>();

        Input input = new Input();
        input.open(filename);

        String line = input.line();

        while(line != null) {
            int inputNum = Integer.parseInt(line);
            dataList.add(inputNum);

            line = input.line();
        }

        // seat adapter
        dataList.add(0);
        Collections.sort(dataList);

        int max = dataList.get(dataList.size()-1);

        // built-in adaptor
        dataList.add(max + 3);

        data = dataList.stream().mapToInt(i -> i).toArray();
    }

    public ArrayList<Integer> getGroupSizes() {
        ArrayList<Integer> groupSizes = new ArrayList<>();

        int numInGroup = 0;

        for(int i = 0; i<data.length-1; i++) {
            if (data[i+1] - data[i] >=3) {
                if (numInGroup > 0) {
                    groupSizes.add(numInGroup);
                }

                numInGroup = 0;
            } else {
                numInGroup++;
            }
        }

        return groupSizes;
    }

    public int numPossibilitiesInGroupSized(int size) {

        switch(size) {
            case 1:
                // One gap of size 1 means just two numbers in the group
                // because of the 3 to either side, neither adaptor
                // can be removed or the gap will be > 3
                // So only one possibility: both on
                // Delta:              3      1      3
                // Number:      |  n-3 |  n   | n+1  |  n+4 |
                // Group:       |      | =====+===== |      |
                return 1;
            case 2:
                // One gap of size 2 means just two numbers in the group
                // because of the gap to either side, neither adaptor
                // can be removed or the gap will be > 3
                // So only one possibility: both on
                // Delta:              3      1      1      3
                // Number:      |  n-3 |  n   | n+1  |  n+2 | n+5  |
                // Group:       |      +======+======+======+      |
                return 2;
            case 3:
                return 4;
            case 4:
                return 7;
            case 5:
                return 13;
            default:
                return 0;
        }
    }

    public Hashtable<Integer, Integer> summarizeDifferences() {
        for (int i = 1; i<data.length; i++) {
            int diff = data[i] - data[i-1];

            if (!differences.containsKey(diff)) {
                differences.put(diff, 0);
            }

            differences.put(diff, differences.get(diff) + 1);
        }

        return differences;
    }
}
