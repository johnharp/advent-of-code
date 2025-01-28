package link.harper;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class CalculatorB3 {
    private int[] data;
    public boolean[] allOn;
    public ArrayList<Integer> togglePositions;
    public ArrayList<ArrayList<Integer>> toggleGroups;


    public int[] getData() {
        return data;
    }

    public long numValidConfigurations(int minIndex, int maxIndex) {
        return 0;
        //return countValidConfigurations(minIndex, , allOn.clone());
    }

    public long countValidConfigurations(int index, boolean[] isOn) {
        if (index > togglePositions.size()-1) {
            return 1;
        }

        int togglePosition = togglePositions.get(index);
        //System.out.println("toggling: " + togglePosition);
        long numValid = 0;

        if (validToTurnOff(index, isOn)) {
            boolean[] withPositionOff = isOn.clone();
            withPositionOff[togglePosition] = false;
            numValid += countValidConfigurations(index+1, withPositionOff);
        }

        numValid += countValidConfigurations(index+1, isOn);
        return numValid;
    }


    @Test
    public void testFindPositionsThatCanBeTurnedOff() {
        loadInput("example-input1.txt");

        Assertions.assertArrayEquals(
                new int[] {3, 4, 7},
                findPositionsThatCanBeTurnedOff().stream().mapToInt(i -> i).toArray()
        );
    }

    /**
     * Scan through all the loaded data.
     * Find any index where it is possible to turn off (take out) the adaptor
     * since the neighboring adaptor values are close enough to not violate the
     * 3-away rule
     * @return
     */
    public ArrayList<Integer> findPositionsThatCanBeTurnedOff() {
        ArrayList<Integer> positions = new ArrayList<>();

        for (int i = 1; i<data.length-1; i++) {
            if ((data[i+1] - data[i-1]) <= 3) {
                positions.add(i);
            }
        }

        return positions;
    }

    public ArrayList<ArrayList<Integer>> computeGroups() {
        ArrayList<Integer> positions = findPositionsThatCanBeTurnedOff();

        ArrayList<ArrayList<Integer>> groups = new ArrayList<>();
        ArrayList<Integer> group = new ArrayList<>();
        groups.add(group);
        boolean isNewGroup = true;

        for(int i = 0; i<positions.size(); i++) {
            if (isNewGroup) {
                group.add(positions.get(i));
                isNewGroup = false;
            } else {
                if (positions.get(i) == group.get(group.size()-1)+1) {
                    group.add(positions.get(i));
                } else {
                    group = new ArrayList<>();
                    group.add(positions.get(i));
                    groups.add(group);
                    isNewGroup = true;
                }
            }
        }

        return groups;
    }

    public boolean validToTurnOff(int i, boolean[] isOn) {
        int pos = togglePositions.get(i);
        // returns 1 if the adaptor at position i
        // can be taken out of the chain (turned off)
        // (given the isOn vector of prior positions [0, i-1])
        // and still be a valid configuration

        // you can never take the 0 position adaptor out of the chain
        if (pos == 0) return false;

        // you can never take the last position adaptor out of the chain
        if (pos >= data.length-1) return false;

        // isOn[i] should always be true when calling this function
        Assertions.assertEquals(true, isOn[pos]);
        int value = data[pos];
        // find the next adaptor value "below" position i
        int nextValueBelow = 0;
        for (int j = pos-1; j > 0; j--) {
            if (isOn[j]) {
                nextValueBelow = data[j];
                break;
            }
        }
        int nextValueAbove = data[pos+1];

        if (nextValueBelow < nextValueAbove-3) return false;

        return true;
    }

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
        System.out.println("data:       " + dataList);
        initAllOn();
        togglePositions = findPositionsThatCanBeTurnedOff();
        toggleGroups = computeGroups();
        System.out.println("can toggle: " + togglePositions);
    }

    public void initAllOn() {
        allOn = new boolean[data.length];
        for(int i = 0; i<allOn.length; i++) {
            allOn[i] = true;
        }
    }
}
