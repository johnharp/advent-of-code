package link.harper;

public class Bus {
    private long busId;
    private long index;
    private long firstValidTime;

    public long getFirstValidTime() {
        return firstValidTime;
    }

    public void setFirstValidTime(long firstValidTime) {
        this.firstValidTime = firstValidTime;
    }


    public long getBusId() {
        return busId;
    }

    public void setBusId(long busId) {
        this.busId = busId;
    }

    public long getIndex() {
        return index;
    }

    public void setIndex(long index) {
        this.index = index;
    }



    public Bus(long id, long index) {
        this.busId = id;
        this.index = index;
    }

    public boolean isValidTime(long time) {
        return ((time + index) % busId) == 0;
    }

    @Override
    public String toString() {
        return "Bus " + busId + " at index " + index + " is first valid at t = " + firstValidTime;
    }



}


