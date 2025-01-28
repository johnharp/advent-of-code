package link.harper;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;

public class PartB {

    public static void main(String[] args) throws Exception {
        System.out.println("Part B Answer");
        Cpu cpu = new Cpu();
        cpu.loadProgram("input.txt");


        for (int i = 0; i<cpu.getTotalNumberOfInstructionsLoaded(); i++) {
            cpu.toggleInstructionAt(i);
            cpu.run();

            if (cpu.isProgramTerminated()) {
                System.out.print("Repair was ==> ");
                cpu.printLine(i);
                cpu.printState();
                break;
            }
            cpu.toggleInstructionAt(i);
        }

    }


}
