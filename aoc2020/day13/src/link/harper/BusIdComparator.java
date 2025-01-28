package link.harper;

import java.util.Comparator;

public class BusIdComparator implements Comparator<Bus> {
    public int compare(Bus bus1, Bus bus2) {
        return bus1.getBusId() > bus2.getBusId() ? 1 :
                bus1.getBusId() < bus2.getBusId() ? -1 :
                        0;
    }
}
