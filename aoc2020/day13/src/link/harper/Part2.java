package link.harper;

public class Part2 {
    public static void main(String[] args) throws Exception {
        System.out.println("Part B Answer");

        Calculator2 calc = new Calculator2();
        calc.loadInput("input.txt");

        Bus b1 = calc.getBus(0);
        for(int i=1; i<calc.getBuses().size(); i++) {
            Bus b2 = calc.getBus(i);
            System.out.println(b2);
            System.out.println("step = " + b1.getBusId()*b2.getBusId());
        }

        calc.go();



    }
}
