package link.harper;


import org.junit.jupiter.api.Test;

import java.util.Arrays;

public class PartB {

    public static void main(String[] args) throws Exception {
        System.out.println("Part B Answer");

        Input input = new Input();
        input.readAll("input.txt");

        GameState state = new GameState();
        state.initialize(input.getLines());

        state.runUntilStable(true);


        state.countOccupiedSeats();


    }

    @Test
    public void testCountVisiblePeople() {
        Input input = new Input();
        input.readAll("example1-input.txt");

        GameState state = new GameState();
        state.initialize(input.getLines());
        state.computeNextState(true);
        System.out.println(state);
    }


}