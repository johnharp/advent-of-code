package link.harper;

import org.jetbrains.annotations.NotNull;

public class Mask {
    private final int numBits = 64;
    private long orMask;
    private long andMask;

    public Mask(String maskStr) {
        long oneBit = 1; // 000...00001
        long zeroBit = -2; // 111...11110

        long shiftedOneBit = oneBit;
        long shiftedZeroBit = zeroBit;

        String str = padLeftChar(maskStr, numBits, '0');

        char[] characters = str.toCharArray();
        for(int i = characters.length - 1; i >= 0; i--) {

            char c = characters[i];
            switch(c) {
                case '0':
                    andMask = andMask & shiftedZeroBit;
                    orMask = orMask & shiftedZeroBit;
                    break;
                case '1':
                    andMask = andMask | shiftedOneBit;
                    orMask = orMask | shiftedOneBit;
                    break;
                default:
                    andMask = andMask | shiftedOneBit;
                    orMask = orMask & shiftedZeroBit;
                    break;
            }

            if (i != 0) {
                shiftedOneBit = shiftedOneBit << 1;
                shiftedZeroBit = shiftedZeroBit << 1;
                shiftedZeroBit = shiftedZeroBit | oneBit;
            }
        }

    }



    public long apply(long value) {
        value = value | orMask;
        value = value & andMask;

        return value;
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

    public String toString() {
        StringBuilder sb = new StringBuilder();
        sb.append("AND: " + andMask + "  OR: "  + orMask);

        return sb.toString();
    }

}
