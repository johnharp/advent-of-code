package link.harper;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import org.junit.jupiter.api.Test;


public class CalculatorB {
    private ArrayList<Integer> allAdaptors;

    public ArrayList<Integer> getAllAdaptors() {
        return allAdaptors;
    }


    public long calcNumValidChains() {

        ArrayList<Integer> start = new ArrayList<>() {
            { add(0); }
        };
        ArrayList<Integer> rest = new ArrayList<>(allAdaptors);
        int max = rest.get(rest.size()-1);

        return validNum(start, rest, max);
    }

    public long validNum(
            ArrayList<Integer> start,
            ArrayList<Integer> rest,
            int max
    ) {
        if (start.get(start.size() - 1) == max)
        {
            System.out.println("VALID: " + start);
            return 1;
        }

        // If we've already applied all the adaptors, "first" is a valid chain
        if (rest.size() == 0) {
            System.out.println("VALID: " + start);
            return 1;
        }

        // weird case -- start and rest should never both be empty
        if (start.size() == 0 && rest.size() == 0) return 0;

        // If "start" is empty, take the first element of "rest" and evaluate
        if (start.size() == 0) {
            start = new ArrayList<>();
            start.add(rest.get(0));

            rest = new ArrayList<>(rest);
            rest.remove(0);

            return validNum(start, rest, max);
        }

        // "start" is not empty, "rest" is not empty
        // assuming all the "first" up to now is fixed (and valid so far),
        // we want to find the next item we can move from "rest" to the end of first
        // the item we pick can be no bigger than first+3
        int maxStart = start.get(start.size()-1);
        ArrayList<Integer> newRest = new ArrayList<Integer>(rest);
        ArrayList<Integer> candidates =
            sliceUpToValue(newRest, maxStart+3);

        // No candidates in the "rest" array are small enough
        if (candidates.size() == 0) return 0;

        long num = 0;

        for (int i = 0; i<candidates.size(); i++) {
            ArrayList<Integer> tryStart = new ArrayList<>(start);
            ArrayList<Integer> remainingCandidates = new ArrayList<>();
            tryStart.add(candidates.get(i));

            if (i+1 < candidates.size()) {
                remainingCandidates = new ArrayList<>(
                        candidates.subList(i+1, candidates.size())
                );
            }
            ArrayList<Integer> tryRest =  remainingCandidates;
            tryRest.addAll(newRest);

            num += validNum(tryStart, tryRest, max);
        }

        return num;
    }

    public void loadInput(String filename) {
        allAdaptors = new ArrayList<>();

        Input input = new Input();
        input.open(filename);

        String line = input.line();

        while(line != null) {
            int inputNum = Integer.parseInt(line);
            allAdaptors.add(inputNum);

            line = input.line();
        }

        // seat adapter
        //dataList.add(0);
        Collections.sort(allAdaptors);

        int max = allAdaptors.get(allAdaptors.size()-1);

        // built-in adaptor
        allAdaptors.add(max + 3);
    }

    /**
     * Given an original list and a "sliceUpTo" value,
     * create a new list that includes items from the first
     * up to the last item with the given slice value.
     * Remove those items from the original list.
     * WARNING: this modifies the original list -- copy before calling.
     *
     * Example, origList is [0, 1, 4, 6, 7, 7, 12, 15, 16]
     * sliceUpToValue(origList, 7)
     *    returns a new list [0, 1, 4, 6, 7, 7]
     *    modifies origList to be [12, 15, 16]
     * @param origList the list to modify
     * @param sliceValue the value to slice up to (and including)
     * @return the first part of the list sliced up to and including the values
     */
    public ArrayList<Integer> sliceUpToValue(
            ArrayList<Integer> origList,
            int sliceValue) {
        ArrayList<Integer> firstPart = new ArrayList<>();

        for (int i = 0; i < origList.size() && origList.get(i) <= sliceValue; i++) {
            firstPart.add(origList.get(i));
        }

        if (firstPart.size() > 0) {
            origList.subList(0, firstPart.size()).clear();
        }

        return firstPart;
    }

    @Test
    public void testSliceUpToValue() {
        ArrayList<Integer> origList = new ArrayList<>() {
            {
                add(0);
                add(1);
                add(4);
                add(6);
                add(7);
                add(7);
                add(12);
                add(15);
                add(16);
            }
        };

        // create a copy of the original list
        ArrayList<Integer> test1List = new ArrayList<>(origList);

        int[] expectedFirstPart = new int[] {
            0, 1, 4, 6, 7, 7
        };

        int[] expectedModifiedOrig = new int[] {
                12, 15, 16
        };

        ArrayList<Integer> firstPart = sliceUpToValue(test1List, 7);

        Assertions.assertArrayEquals(
                expectedFirstPart,
                firstPart.stream().mapToInt(i -> i).toArray());
        Assertions.assertArrayEquals(
                expectedModifiedOrig,
                test1List.stream().mapToInt(i -> i).toArray());

        ArrayList<Integer> test2List = new ArrayList<>(origList);

        expectedFirstPart = new int[]{
                0, 1, 4, 6, 7, 7, 12, 15, 16
        };

        expectedModifiedOrig = new int[] {
        };

        firstPart = sliceUpToValue(test2List, 22);
        System.out.println(firstPart);
        System.out.println(test2List);

        Assertions.assertArrayEquals(
                expectedFirstPart,
                firstPart.stream().mapToInt(i -> i).toArray());
        Assertions.assertArrayEquals(
                expectedModifiedOrig,
                test2List.stream().mapToInt(i -> i).toArray());

        ArrayList<Integer> test3List = new ArrayList<>(origList);

        expectedFirstPart = new int[]{
        };

        expectedModifiedOrig = new int[] {
                0, 1, 4, 6, 7, 7, 12, 15, 16
        };

        firstPart = sliceUpToValue(test3List, -1);
        System.out.println(firstPart);
        System.out.println(test3List);

        Assertions.assertArrayEquals(
                expectedFirstPart,
                firstPart.stream().mapToInt(i -> i).toArray());
        Assertions.assertArrayEquals(
                expectedModifiedOrig,
                test3List.stream().mapToInt(i -> i).toArray());
    }
}
