package link.harper;

import java.util.ArrayList;
import java.util.List;

public class Calculator2 {
    private List<Bus> buses = new ArrayList<>();


    public List<Bus> getBuses() {
        return buses;
    }

    public Bus getBus(int i) {
        return buses.get(i);
    }

    public boolean allBusesValidAtTime(long t) {
        for (Bus bus: buses) {
            if (!bus.isValidTime(t)) return false;
        }

        return true;
    }


    public void loadInput(String filename) {
        Input input = new Input();
        input.readAll(filename);

        // Discard first line
        String busIdsStr = input.getLines().get(1);

        // Second line is the bus info
        String[] components = busIdsStr.split(",");
        long index = 0;
        for(String component: components) {
            if (!component.equals("x")) {
                long busId = Long.parseLong(component);
                buses.add(new Bus(busId, index));
            }
            index++;
        }

        Bus b1 = buses.get(0);

        for (Bus bus: buses) {
            bus.setFirstValidTime(firstValidTime(b1, bus));
        }
        //buses.sort(new BusIdComparator().reversed());
    }

    public void syncToFirstBus(Bus bus) {
        Bus firstBus = buses.get(0);
        long index = bus.getIndex();
        long t = 0;
        long busT = 0;

        while (busT - t != index) {
            t += firstBus.getBusId();
            while ( busT < t) {
                busT += bus.getBusId();
            }
        }

        System.out.println(t);
    }

    public long firstValidTime(Bus b1, Bus b2) {
        int t = 0;

        while (!b2.isValidTime(t)) {
            t += b1.getBusId();
        }

        return t;
    }

    public long firstCoincidence(Bus b1, Bus b2) {
        int n = 0;

        System.out.println();
        while (n < 1000) {
            while (((b1.getBusId() * n) + b2.getIndex()) % b2.getBusId() != 0) {
                n++;
            }
            n++;
        }
        return n*b1.getBusId();
    }

    public long initialIterations(Bus b1, Bus b2) {
        int n = 0;
        while (((b1.getBusId() * n) + b2.getIndex()) % b2.getBusId() != 0) {
            n++;
        }

        return n ;
    }

    public void go() {
        int solvedUpToBus = 1;

        Bus b1 = buses.get(0);
        Bus b2 = buses.get(1);

        long t = b2.getFirstValidTime();
        long step = b1.getBusId() * b2.getBusId();

        while (!allBusesValidAtTime(t)) {
            if (solvedUpToBus < buses.size()-1) {
                Bus workingOnSolving = buses.get(solvedUpToBus + 1);
                if (workingOnSolving.isValidTime(t)) {
                    solvedUpToBus++;
                    System.out.println("SOLVED BUS #" + solvedUpToBus);
                    step *= workingOnSolving.getBusId();
                }
            }
            t += step;

            System.out.println(t);
        }

        System.out.println("Done?!:  " + t);
    }
}
