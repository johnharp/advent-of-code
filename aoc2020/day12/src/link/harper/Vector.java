package link.harper;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class Vector {
    public int x;
    public int y;

    public Vector() {
        x = 0;
        y = 0;
    }

    public Vector(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public boolean isNonZero() {
        return x != 0 || y != 0;
    }

    public boolean isZero() {
        return x == 0 && y == 0;
    }

    public Vector add(Vector v) {
        return new Vector(this.x + v.x, this.y + v.y);
    }

    public Vector multiply(int m) {
        return new Vector(this.x * m, this.y * m);
    }

    public String compassDirection() {
        String str = "??";

        if (x == 1 && y ==0 ) str = "East";
        else if (x == 0 && y == 1) str = "North";
        else if (x == -1 && y == 0) str = "West";
        else if (x == 0 && y == -1) str = "South";

        return str;
    }

    public Vector rotate(int deg) throws Exception {
        Vector v = new Vector(x, y);

        if (deg == 0) {
            // no-op
        } else if (deg == 90 || deg == -270) {
            // Clockwise rotation by 90
            // invert sign on x and swap x, y
            v.y = -x;
            v.x = y;
        } else if (deg == -90 || deg == 270) {
            // Counterclockwise rotation by 90
            // invert sign on y and swap x, y
            v.x = -y;
            v.y = x;
        } else if (deg == 180 || deg == -180) {
            // either direction 180 just flips sign on
            // both x and y
            v.x = -x;
            v.y = -y;
        } else {
            throw new Exception("Unhandled rotation! Value was " + deg);
        }

        return v;
    }

    @Override
    public boolean equals(Object obj) {
        if (this == obj) return true;
        if (!(obj instanceof Vector)) return false;

        Vector v = (Vector) obj;

        return (this.x == v.x &&
                this.y == v.y);
    }

    @Override
    public int hashCode() {
        int result = 17;
        result = 31 * result + x;
        result = 31 * result + y;
        return result;
    }


}
