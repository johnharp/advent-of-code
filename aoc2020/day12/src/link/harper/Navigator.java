package link.harper;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

import java.util.regex.Pattern;
import java.util.regex.Matcher;

public class Navigator {
    public Action instructionToAction(String instruction) {
        Action action = null;

        Pattern pattern = Pattern.compile("^([NSEWLRF])([0-9]+)");
        Matcher matcher = pattern.matcher(instruction);

        if (matcher.find()) {
            String command = matcher.group(1);
            int argument = Integer.parseInt(matcher.group(2));

            action = constructAction(command, argument);
        }

        return action;
    }

    private Action constructAction(String command, int argument) {
        Action action = new Action();

        // Apply the argument depending on the command
        switch(command) {
            case "N":
            case "S":
            case "E":
            case "W":
            case "F":
                action.moveDistance = argument;
                break;
            case "R":
                action.rotateDegrees = argument;
                break;
            case "L":
                action.rotateDegrees = - argument;
                break;
        }

        // Then apply the command
        switch(command) {
            case "N":
                action.moveInDirection = new Vector(0, 1);
                break;
            case "S":
                action.moveInDirection = new Vector(0, -1);
                break;
            case "E":
                action.moveInDirection = new Vector(1, 0);
                break;
            case "W":
                action.moveInDirection = new Vector(-1, 0);
                break;
        }

        return action;
    }

    @Test
    public void testInstructionNorth()
    {
        Assertions.assertEquals(
                new Action(0, 0, 1, 10),
                instructionToAction("N10")
        );
    }

    @Test
    public void testInstructionSouth()
    {
        Assertions.assertEquals(
                new Action(0, 0, -1, 20),
                instructionToAction("S20")
        );
    }

    @Test
    public void testInstructionEast()
    {
        Assertions.assertEquals(
                new Action(0, 1, 0, 15),
                instructionToAction("E15")
        );
    }

    @Test
    public void testInstructionWest()
    {
        Assertions.assertEquals(
                new Action(0, -1, 0, 13),
                instructionToAction("W13")
        );
    }

    @Test
    public void testInstructionLeft()
    {
        Assertions.assertEquals(
                new Action(-90, 0, 0, 0),
                instructionToAction("L90")
        );
    }

    @Test
    public void testInstructionRight()
    {
        Assertions.assertEquals(
                new Action(180, 0, 0, 0),
                instructionToAction("R180")
        );
    }

    @Test
    public void testInstructionForward()
    {
        Assertions.assertEquals(
                new Action(0, 0, 0, 12),
                instructionToAction("F12")
        );
    }
}
