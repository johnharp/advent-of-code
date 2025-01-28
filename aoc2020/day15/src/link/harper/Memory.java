package link.harper;

import java.util.ArrayList;
import java.util.Hashtable;
import java.util.List;

public class Memory {
    private Hashtable<Integer, List<Integer>> history;
    private int turnNumber = 1;
    private int lastNumberAdded = -1;


    public Memory(String startingNumbers) {
        String[] strs = startingNumbers.split(",");
        history = new Hashtable<>();
        for (String str: strs) {
            int num = Integer.parseInt(str);

            placeNumber(num, turnNumber);
            turnNumber++;
        }
    }

    // Place number does not increment the turn number
    public void placeNumber(int number, int t) {
        List<Integer> thisNumHistory;

        if (history.containsKey(number)) {
            thisNumHistory = history.get(number);
        } else {
            thisNumHistory = new ArrayList<>();
            history.put(number, thisNumHistory);
        }

        thisNumHistory.add(t);
        lastNumberAdded = number;
    }

    public void placeNextNumberAndAdvanceTurn() {
        int nextNum = 0;

        List<Integer> lastNumberAddedHistory = history.get(lastNumberAdded);

        int numTimesLastNumberAddedWasPlaced = lastNumberAddedHistory.size();
        if (numTimesLastNumberAddedWasPlaced <= 1) {
            // The last number added had its first occurrence last
            // turn, so the next number will be 0
            nextNum = 0;
        } else {
            nextNum = lastNumberAddedHistory.get(numTimesLastNumberAddedWasPlaced - 1)
                    - lastNumberAddedHistory.get(numTimesLastNumberAddedWasPlaced - 2);
        }

        placeNumber(nextNum, turnNumber);
        System.out.println("TURN: " + turnNumber + "  NUM: " + nextNum);

        turnNumber++;
    }

    public void runUntilTurn(int t) {
        while (turnNumber <= t) {
            placeNextNumberAndAdvanceTurn();
        }
    }
}
