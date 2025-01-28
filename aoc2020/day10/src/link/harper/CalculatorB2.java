package link.harper;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Hashtable;
import java.util.List;

public class CalculatorB2 {
    private int[] data;

    public boolean[] allOn;

    public int[] getData() {
        return data;
    }

    public void printOnValues(boolean[] isOn) {
        for (int i = 0; i < data.length; i++) {
            if (isOn[i]) System.out.print(data[i] + "  ");
        }
        System.out.println();
    }

    @Test
    public void validToTurnOff() {
        loadInput("example-input1.txt");
        data = new int[] {0, 1, 4, 5, 6, 7, 10, 11, 12, 15, 16, 19, 22};
        initAllOn();


        // If position 4 (the 6) is off, is it valid to turn off position 5 (the 7)?
        boolean[] t1 = allOn.clone();
        t1[4] = false;
        Assertions.assertEquals(false, validToTurnOff(5, t1));
    }

    public long numValidConfigurations() {
        int startingIndex = 1;

        return countValidConfigurations(startingIndex, allOn.clone());
    }
    public long countValidConfigurations(int startIndex, boolean[] isOn) {
        System.out.println("index: " + startIndex);
        long numValid = 0;

        if (startIndex == data.length-1) {
            return 1;
        }

        if (validToTurnOff(startIndex, isOn)) {
            boolean[] withPositionOff = isOn.clone();
            withPositionOff[startIndex] = false;
            numValid += countValidConfigurations(startIndex+1, withPositionOff);
        }

        numValid += countValidConfigurations(startIndex+1, isOn);
        return numValid;
    }

    public boolean validToTurnOff(int i, boolean[] isOn) {
        // returns 1 if the adaptor at position i
        // can be taken out of the chain (turned off)
        // (given the isOn vector of prior positions [0, i-1])
        // and still be a valid configuration

        // you can never take the 0 position adaptor out of the chain
        if (i == 0) return false;

        // you can never take the last position adaptor out of the chain
        if (i >= data.length-1) return false;

        // isOn[i] should always be true when calling this function
        Assertions.assertEquals(true, isOn[i]);
        int value = data[i];
        // find the next adaptor value "below" position i
        int nextValueBelow = 0;
        for (int j = i-1; j > 0; j--) {
            if (isOn[j]) {
                nextValueBelow = data[j];
                break;
            }
        }
        int nextValueAbove = data[i+1];

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
        initAllOn();
    }

    public void initAllOn() {
        allOn = new boolean[data.length];
        for(int i = 0; i<allOn.length; i++) {
            allOn[i] = true;
        }
    }
}
