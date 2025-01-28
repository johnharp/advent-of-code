package link.harper;

public class Part1 {
    public static void main(String[] args) throws Exception {
        Input in = new Input("input.txt");
        //Input in = new Input("input.txt");
        String line = in.getLines().get(0);

        Memory mem = new Memory(line);
        mem.runUntilTurn(30000000);
    }

}
