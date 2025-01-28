package link.harper;

public class Range {
    private int min;
    private int max;

    public Range(int min, int max) {
        this.min = min;
        this.max = max;
    }

    public boolean contains(int n) {
        return min <= n &&
                max >= n;
    }

    @Override
    public String toString(){
        StringBuilder sb = new StringBuilder();
        sb.append("[");
        sb.append(min);
        sb.append(",");
        sb.append(max);
        sb.append("]");
        return sb.toString();
    }
}
