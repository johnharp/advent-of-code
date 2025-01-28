package link.harper;

import java.util.Collections;
import java.util.Hashtable;

public class Universe {
    private Hashtable<Integer, Hashtable<Integer, Hashtable<Integer, Hashtable<Integer, Boolean>>>> cubes =
            new Hashtable<>();

    public Boolean get(int x, int y, int z, int w) {

        if (cubes.containsKey(x)) {
            Hashtable<Integer, Hashtable<Integer, Hashtable<Integer, Boolean>>> xSlice;
            xSlice = cubes.get(x);

            if (xSlice.containsKey(y)) {
                Hashtable<Integer, Hashtable<Integer, Boolean>> ySlice;
                ySlice = xSlice.get(y);

                if (ySlice.containsKey(z)) {
                    Hashtable<Integer, Boolean> zSlice;
                    zSlice = ySlice.get(z);

                    if (zSlice.containsKey(w)) {
                        return zSlice.get(w);
                    }
                }
            }
        }

        return false;
    }

    public void set(int x, int y, int z, int w, Boolean value) {
        Hashtable<Integer, Hashtable<Integer, Hashtable<Integer, Boolean>>> xSlice;
        Hashtable<Integer, Hashtable<Integer, Boolean>> ySlice;
        Hashtable<Integer, Boolean> zSlice;

        if (cubes.containsKey(x)) {
            xSlice = cubes.get(x);
        } else {
            xSlice = new Hashtable<>();
            cubes.put(x, xSlice);
        }

        if (xSlice.containsKey(y)) {
            ySlice = xSlice.get(y);
        } else {
            ySlice = new Hashtable<>();
            xSlice.put(y, ySlice);
        }

        if (ySlice.containsKey(z)) {
            zSlice = ySlice.get(z);
        } else {
            zSlice = new Hashtable<>();
            ySlice.put(z, zSlice);
        }

        zSlice.put(w, value);

        // don't allow contraction
//        if (value == false) {
//            ySlice.remove(z);
//        } else {
//            ySlice.put(z, true);
//        }
    }

    public Extent measureExtent() {
        Extent ext = new Extent();
        if (cubes.keySet().size() == 0) {
            return ext;
        }
        ext.minX = Collections.min(cubes.keySet());
        ext.maxX = Collections.max(cubes.keySet());

        for (int x = ext.minX; x <= ext.maxX; x++) {
            if (!cubes.containsKey(x)) continue;

            Hashtable<Integer, Hashtable<Integer, Hashtable<Integer, Boolean>>> xSlice;
            xSlice = cubes.get(x);
            int minYInSlice = Collections.min(xSlice.keySet());
            int maxYInSlice = Collections.max(xSlice.keySet());

            if (minYInSlice < ext.minY) ext.minY = minYInSlice;
            if (maxYInSlice > ext.maxY) ext.maxY = maxYInSlice;

            for (int y = ext.minY; y <= ext.maxY; y++) {
                if (!xSlice.containsKey(y)) continue;

                Hashtable<Integer, Hashtable<Integer, Boolean>> ySlice;
                ySlice = xSlice.get(y);

                int minZInSlice = Collections.min(ySlice.keySet());
                int maxZInSlice = Collections.max(ySlice.keySet());

                if (minZInSlice < ext.minZ) ext.minZ = minZInSlice;
                if (maxZInSlice > ext.maxZ) ext.maxZ = maxZInSlice;

                for (int z = ext.minZ; z <= ext.maxZ; z++) {
                    if (!ySlice.containsKey(z)) continue;

                    Hashtable<Integer, Boolean> zSlice;
                    zSlice = ySlice.get(z);

                    int minWInSlice = Collections.min(zSlice.keySet());
                    int maxWInSlice = Collections.max(zSlice.keySet());

                    if (minWInSlice < ext.minW) ext.minW = minWInSlice;
                    if (maxWInSlice > ext.maxW) ext.maxW = maxWInSlice;

                    for (int w = ext.minW; w <= ext.maxW; w++) {
                        if (zSlice.containsKey(w) &&
                            zSlice.get(w) == true) ext.count++;
                    }
                }
            }

        }

        return ext;
    }
    public void expand() {
        Extent ext = measureExtent();
        set(ext.minX-1, ext.minY-1, ext.minZ-1, ext.minW-1,false);
        set(ext.maxX+1, ext.maxY+1, ext.maxZ+1, ext.maxW+1,false);
    }

    public Universe step() {
        expand();
        Extent ext = measureExtent();

        Universe uni = new Universe();

        for (int x = ext.minX; x <= ext.maxX; x++) {
            for (int y = ext.minY; y <= ext.maxY; y++) {
                for (int z = ext.minZ; z <= ext.maxZ; z++ ) {
                    for (int w = ext.minW; w <= ext.maxW; w++) {
                        int sum = numNeighbors(x, y, z, w);

                        // if the cell is alive
                        if (get(x, y, z, w)) {
                            // and if the cell has 2 or 3 neighbors
                            if (sum >= 2 && sum <= 3) {
                                // then it stays alive
                                uni.set(x, y, z, w,true);

                            }
                        } else { // else if the cell is inactive
                            if (sum == 3) {
                                // and it has 3 neighbors
                                // ... then it becomes alive
                                uni.set(x, y, z, w,true);
                            }
                        }
                    }

                }
            }
        }

        return uni;
    }

    public int numNeighbors(int x, int y, int z, int w) {
        int sum = 0;
        for (int dx = -1; dx <= 1; dx ++) {
            for (int dy = -1; dy <= 1; dy ++) {
                for (int dz = -1; dz <= 1; dz ++) {
                    for (int dw = -1; dw <= 1; dw ++) {
                        if (!(dx == 0 && dy == 0 && dz == 0 && dw == 0)) {
                            if (get(x + dx, y + dy, z + dz, w + dw)) sum++;
                        }
                    }
                }
            }
        }

        return sum;
    }

    @Override
    public String toString() {
        StringBuilder sb = new StringBuilder();
        Extent ext = measureExtent();

        for (int w = ext.minW; w <= ext.maxW; w++) {
            for (int z = ext.minZ; z <= ext.maxZ; z++) {
                sb.append("z=" + z + ", w=" + w);
                sb.append(System.lineSeparator());
                for (int y = ext.maxY; y >= ext.minY; y--) {
                    for (int x = ext.minX; x <= ext.maxX; x++) {
                        if (get(x, y, z, w)) {
                            sb.append('#');
                        } else {
                            sb.append('.');
                        }
                    }
                    sb.append(System.lineSeparator());
                }
                sb.append(System.lineSeparator());
            }
        }

        return sb.toString();
    }
}
