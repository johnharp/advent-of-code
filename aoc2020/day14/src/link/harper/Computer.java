package link.harper;

import org.jetbrains.annotations.NotNull;

import java.util.ArrayList;
import java.util.Hashtable;
import java.util.List;

public class Computer {
    private final int numBits = 64;

    public Hashtable<Long, Long> getMemory() {
        return memory;
    }

    private Hashtable<Long, Long> memory = new Hashtable<>();
    private static long thirtySixOnesMask = 68719476735L;
    public static long maxNumber = thirtySixOnesMask;


    public long read(long address) throws Exception{
        if (address > thirtySixOnesMask) throw new Exception("address overflow: " + address);
        if (memory.containsKey(address)) {
            return memory.get(address);
        } else {
            return 0;
        }
    }

    public void write(long address, long value) throws Exception {
        if (address > thirtySixOnesMask) throw new Exception("address overflow: " + address);
        if (value > thirtySixOnesMask) throw new Exception("value overflow: " + value);

        if (memory.keySet().contains(address)) {
            System.out.println(
                    "Overwriting address " + address + " old " + read(address)
                            + " new " + value);
        }
        memory.put(address, value);
    }

    public void coredump() {
        for(long key: memory.keySet()) {
            System.out.println("mem[" +
                    key + "]: " +
                    memory.get(key));
        }
    }

    public long sumAllMemory() {
        long sum = 0;

        for(long key: memory.keySet()) {
            long v = memory.get(key);
            //v = v & thirtySixOnesMask;

            sum += v;

            //sum = sum & thirtySixOnesMask;
        }

        return sum;
    }

    public void handlePart2(String mask, long address, long value) throws Exception{
        String addressString = Long.toBinaryString(address);
        addressString =  padLeftChar(addressString, 36, '0');

        char[] maskChars = mask.toCharArray();
        char[] addressChars = addressString.toCharArray();

        StringBuilder sb = new StringBuilder();
        for(int i = 0; i<maskChars.length; i++) {
            if (maskChars[i] == '0') sb.append(addressChars[i]);
            else if (maskChars[i] == '1') sb.append('1');
            else if (maskChars[i] == 'X') sb.append('X');
            else throw new Exception("Unknown mask character: '" + maskChars[i] + "'");
        }

        List<String> addressStrings = expandAddress(sb.toString());

        for(String addressStr: addressStrings) {
            long expandedAddress = addressFromString(addressStr);
            write(expandedAddress, value);
        }
    }

    public List<String> expandAddress(String address) {
        List<String> expanded = new ArrayList<>();

        if (address.contains("X")) {
            String[] parts = address.split("X", 2);
            String address1 = parts[0] + "0" + parts[1];
            String address2 = parts[0] + "1" + parts[1];
            expanded.addAll(expandAddress(address1));
            expanded.addAll(expandAddress(address2));
        } else {
            expanded.add(address);
        }

        return expanded;
    }

    public long addressFromString(String str) {
        return Long.parseLong(str, 2);
    }



    public String padLeftChar(@NotNull String str, int length, char pad) {
        if (str.length() >= length) {
            return str;
        }
        StringBuilder sb = new StringBuilder();
        while (sb.length() < length - str.length()) {
            sb.append(pad);
        }
        sb.append(str);

        return sb.toString();
    }
}
