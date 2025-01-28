package link.harper;

// Introduced in part 2
public class Waypoint {
    public Vector relativeLocation = new Vector(10, 1);

    public void applyAction(Action action) throws Exception {
        relativeLocation = relativeLocation.rotate(action.rotateDegrees);
        relativeLocation = relativeLocation.add(
                action.moveInDirection.multiply(action.moveDistance));
    }

    @Override
    public String toString() {
        String eastUnits = relativeLocation.x == 0 ? "0" : ""+Math.abs(relativeLocation.x);
        String eastWest =
                (relativeLocation.x >= 0 ? "east" : "west") + " " + eastUnits;

        String northUnits = relativeLocation.y == 0 ? "0" : ""+Math.abs(relativeLocation.y);
        String northSouth =
                (relativeLocation.y >= 0 ? "north" : "south") + " " + northUnits;


        return "Waypoint is at " + eastWest + ", " +
                northSouth +
                " relative the ship";

    }
}
