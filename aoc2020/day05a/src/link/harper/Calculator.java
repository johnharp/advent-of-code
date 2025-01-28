package link.harper;

public class Calculator {
    public int SeatId(String spec) {
        int row = SeatRow(spec);
        int col = SeatColumn(spec);

        int id = (row * 8) + col;
        return id;
    }

    public int SeatRow(String spec) {
        int bitmask = 0x0001;
        int row = 0x0000;

        for (int i = 6; i>=0; i--) {
            char c = spec.charAt(i);

            if (c == 'B') {
                row = row | bitmask;
            }

            bitmask = bitmask << 1;
        }

        return row;
    }

    public int SeatColumn(String spec) {
        int bitmask = 0x0001;
        int column = 0x0000;

        for (int i = 9; i>=7; i--) {
            char c = spec.charAt(i);

            if (c == 'R') {
                column = column | bitmask;
            }

            bitmask = bitmask << 1;
        }

        return column;
    }
}
