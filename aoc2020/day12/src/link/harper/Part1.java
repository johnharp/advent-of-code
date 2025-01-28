package link.harper;


import java.util.ArrayList;
import java.util.Hashtable;
import java.util.List;

public class Part1 {

    public static void main(String[] args) throws Exception {
        System.out.println("Part A Answer");
        Input input = new Input();
        input.readAll("input.txt");

        Ship ship = new Ship();
        Navigator nav = new Navigator();
        System.out.println(ship);

        for(String line:input.getLines()) {
            Action a = nav.instructionToAction(line);
            ship.applyAction(a);
            System.out.println(ship);
        }

    }
}