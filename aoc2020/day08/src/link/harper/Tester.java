package link.harper;


import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class Tester {

    @Test
    public void testCpuLoadProgram() throws Exception {
        Cpu cpu = new Cpu();
        cpu.loadProgram("example-input.txt");

        Instruction instruction = cpu.getInstructionAt(0);
        Assertions.assertEquals("nop", instruction.getOperation());

        instruction = cpu.getInstructionAt(4);
        Assertions.assertEquals("jmp", instruction.getOperation());
        Assertions.assertEquals(-3, instruction.getArgument());

        instruction = cpu.getInstructionAt(8);
        Assertions.assertEquals("acc", instruction.getOperation());
        Assertions.assertEquals(6, instruction.getArgument());
    }

    @Test
    public void testNopInstruction() throws Exception{
        Cpu cpu = new Cpu();
        cpu.loadInstruction("nop +0");

        cpu.setAccumulator(42);

        Assertions.assertEquals(0, cpu.getProgramCounter());
        Assertions.assertEquals(42, cpu.getAccumulator());
        cpu.step();
        Assertions.assertEquals(1, cpu.getProgramCounter());
        Assertions.assertEquals(42, cpu.getAccumulator());
    }

    @Test
    public void testAccInstruction() throws Exception{
        Cpu cpu = new Cpu();
        cpu.loadInstruction("acc +1");

        cpu.setAccumulator(42);

        Assertions.assertEquals(0, cpu.getProgramCounter());
        Assertions.assertEquals(42, cpu.getAccumulator());
        cpu.step();
        Assertions.assertEquals(1, cpu.getProgramCounter());
        Assertions.assertEquals(43, cpu.getAccumulator());
    }

    @Test
    public void testJmpInstruction() throws Exception{
        Cpu cpu = new Cpu();
        cpu.loadInstruction("jmp -4");

        cpu.setAccumulator(42);

        Assertions.assertEquals(0, cpu.getProgramCounter());
        Assertions.assertEquals(42, cpu.getAccumulator());
        cpu.step();
        Assertions.assertEquals(-4, cpu.getProgramCounter());
        Assertions.assertEquals(42, cpu.getAccumulator());
    }

    @Test
    public void testExampleInput() throws Exception {
        Cpu cpu = new Cpu();
        cpu.loadProgram("example-input.txt");

        Assertions.assertEquals(0, cpu.getProgramCounter());
        Assertions.assertEquals(0, cpu.getAccumulator());
        Assertions.assertEquals(false, cpu.isInfiniteLoopDetected());

        cpu.step();
        Assertions.assertEquals(1, cpu.getProgramCounter());
        Assertions.assertEquals(0, cpu.getAccumulator());
        Assertions.assertEquals(false, cpu.isInfiniteLoopDetected());

        cpu.step();
        Assertions.assertEquals(2, cpu.getProgramCounter());
        Assertions.assertEquals(1, cpu.getAccumulator());
        Assertions.assertEquals(false, cpu.isInfiniteLoopDetected());

        cpu.step();
        Assertions.assertEquals(6, cpu.getProgramCounter());
        Assertions.assertEquals(1, cpu.getAccumulator());
        Assertions.assertEquals(false, cpu.isInfiniteLoopDetected());

        cpu.step();
        Assertions.assertEquals(7, cpu.getProgramCounter());
        Assertions.assertEquals(2, cpu.getAccumulator());
        Assertions.assertEquals(false, cpu.isInfiniteLoopDetected());

        cpu.step();
        Assertions.assertEquals(3, cpu.getProgramCounter());
        Assertions.assertEquals(2, cpu.getAccumulator());
        Assertions.assertEquals(false, cpu.isInfiniteLoopDetected());

        cpu.step();
        Assertions.assertEquals(4, cpu.getProgramCounter());
        Assertions.assertEquals(5, cpu.getAccumulator());
        Assertions.assertEquals(false, cpu.isInfiniteLoopDetected());

        cpu.step();
        Assertions.assertEquals(1, cpu.getProgramCounter());
        Assertions.assertEquals(5, cpu.getAccumulator());
        Assertions.assertEquals(true, cpu.isInfiniteLoopDetected());
    }

    @Test
    public void testIsProgramTerminated() throws Exception{
        Cpu cpu = new Cpu();
        cpu.loadInstruction("nop +0");
        cpu.loadInstruction("acc +1");

        cpu.step();
        Assertions.assertEquals(false, cpu.isProgramTerminated());
        cpu.step();
        Assertions.assertEquals(true, cpu.isProgramTerminated());

    }

    @Test
    public void testToggleInstructionAt() throws Exception {
        Cpu cpu = new Cpu();
        cpu.loadInstruction("nop +0");
        cpu.loadInstruction("jmp +1");

        Assertions.assertEquals("jmp",
                cpu.getInstructionAt(1).getOperation());

        cpu.toggleInstructionAt(1);

        Assertions.assertEquals("nop",
                cpu.getInstructionAt(1).getOperation());

        cpu.toggleInstructionAt(1);
        Assertions.assertEquals("jmp",
                cpu.getInstructionAt(1).getOperation());

    }
}
