package link.harper;


import java.util.Arrays;

public class Part2 {

    public static void main(String[] args) throws Exception {
        System.out.println("Part B Answer");

        Input input = new Input();
        //input.readAll("example1-input.txt");
        input.readAll("input.txt");

        Ship ship = new Ship();
        Navigator nav = new Navigator();
        Waypoint wp = new Waypoint();
        System.out.println(ship);
        System.out.println(wp);
        System.out.println();

        for(String line:input.getLines()) {
            Action a = nav.instructionToAction(line);
            wp.applyAction(a);
            ship.applyActionPart2(a, wp);
            System.out.println(ship);
            System.out.println(wp);
            System.out.println();
        }

    }




}