package link.harper;


import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.Hashtable;
import java.util.List;

public class PartA {

    public static void main(String[] args) throws Exception {
        System.out.println("Part A Answer");
        Input input = new Input();
        input.readAll("input.txt");

        GameState state = new GameState();
        state.initialize(input.getLines());

        state.runUntilStable(false);
        state.countOccupiedSeats();

    }
}