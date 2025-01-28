package link.harper;

import java.util.ArrayList;
import java.util.List;

public class Calculator {
    private long earliestDeparture;
    private List<Long> busIds;

    public long getEarliestDeparture() {
        return earliestDeparture;
    }

    public void setEarliestDeparture(long earliestDeparture) {
        this.earliestDeparture = earliestDeparture;
    }

    public List<Long> getBusIds() {
        return busIds;
    }

    public void setBusIds(List<Long> busIds) {
        this.busIds = busIds;
    }

    public void loadInput(String filename) {
        Input input = new Input();
        input.readAll(filename);

        earliestDeparture = Long.parseLong(input.getLines().get(0));
        String busIdsStr = input.getLines().get(1);

        busIds = new ArrayList<>();
        String[] components = busIdsStr.split(",");
        for(String component: components) {
            if (component.equals("x")) continue;
            long busId = Long.parseLong(component);
            busIds.add(busId);
        }
    }

    public long nextDepartureForBus(long busId) {
        long nextTime = ((earliestDeparture / busId) + 1) *
                busId;

        return nextTime;
    }

    public long waitTimeForBus(long busId) {
        long nextTime = ((earliestDeparture / busId) + 1) *
                busId;

        return nextTime - earliestDeparture;
    }

    public long earliestPossibleBus() {
        long selectedBusId = -1;
        long minWaitTime = Long.MAX_VALUE;

        for(long busId: busIds) {
            long wait = waitTimeForBus((busId));
            if (wait < minWaitTime) {
                minWaitTime = wait;
                selectedBusId = busId;
            }
        }

        System.out.println("Optimal bus ID is " + selectedBusId);
        System.out.println("With a wait time of " + minWaitTime + " mins");
        System.out.println("Product: " + (selectedBusId * minWaitTime));

        return selectedBusId;
    }
}
