package link.harper;


public class Action {
    // positive means clockwise, negative means counter-clockwise
    public int rotateDegrees;

    // unit vector - move the ship in this direction without regard
    // to its current orientation
    // if the direction is 0, and moveDistance is non-zero, means
    // move in direction of the ships current orientation
    public Vector moveInDirection;

    // number of units to move
    public int moveDistance;

    public Action() {
        rotateDegrees = 0;

        // Vector constructor will return a 0 vector
        moveInDirection = new Vector();
        moveDistance = 0;
    }

    public Action(int rotateDegrees, int dirX, int dirY, int dist) {
        this.rotateDegrees = rotateDegrees;
        this.moveInDirection = new Vector(dirX, dirY);
        this.moveDistance = dist;
    }

    @Override
    public boolean equals(Object obj) {
        if (this == obj) return true;
        if (!(obj instanceof Action)) return false;

        Action a = (Action) obj;


        return (this.rotateDegrees == a.rotateDegrees &&
                this.moveInDirection.equals(a.moveInDirection) &&
                this.moveDistance == a.moveDistance);
    }

    @Override
    public int hashCode() {
        int result = 17;
        result = 31 * result + rotateDegrees;
        result = 31 * result + moveDistance;
        result = 31 * result + moveInDirection.hashCode();
        return result;
    }

}
