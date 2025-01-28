package link.harper;

public class Ship {
    public Vector facing = new Vector(1, 0);
    public Vector location = new Vector(0, 0);

    public int manhattanDistance() {
        return Math.abs(location.x) + Math.abs(location.y);
    }

    public void applyAction(Action action) throws Exception {
        // Apply rotation first
        facing = facing.rotate(action.rotateDegrees);

        // Get movement direction
        Vector dir = action.moveInDirection.isZero() ?
                facing : action.moveInDirection;

        Vector move = dir.multiply(action.moveDistance);

        this.location = location.add(move);
    }

    public void applyActionPart2(Action action, Waypoint wp) throws Exception {
        // in Part2, the ship only reacts to Forward actions

        if (action.moveInDirection.isZero()) {
            this.location = location.add(wp.relativeLocation.multiply(action.moveDistance));
        }
    }

    @Override
    public String toString() {
        String eastUnits = location.x == 0 ? "0" : ""+Math.abs(location.x);
        String eastWest =
                (location.x >= 0 ? "east" : "west") + " " + eastUnits;

        String northUnits = location.y == 0 ? "0" : ""+Math.abs(location.y);
        String northSouth =
                (location.y >= 0 ? "north" : "south") + " " + northUnits;


        return "Ship is at " + eastWest + ", " +
                northSouth + ", is facing " +
                facing.compassDirection() +
                ", and is at a manhattan distance of " + manhattanDistance() +
                " units from start.";

    }
}
