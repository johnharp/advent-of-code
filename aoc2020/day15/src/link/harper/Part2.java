package link.harper;

public class Part2 {
    public static void main(String[] args) throws Exception {
        Input in = new Input("example1-input.txt");
        //Input in = new Input("input.txt");

        for(String line : in.getLines())
        {
            System.out.println(line);
        }
    }
}
