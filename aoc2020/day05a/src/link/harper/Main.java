package link.harper;

public class Main {

    public static void main(String[] args) {
        Input input = new Input();
        input.open("input.txt");

        Calculator calc = new Calculator();

//        Part A --------------------------------------------------------------
//        int maxSeatId = 0;
//
//        for(String line = input.line(); line != null; line = input.line()) {
//            int seatId = calc.SeatId(line);
//            if (seatId > maxSeatId) {
//                maxSeatId = seatId;
//            }
//        }
//
//        System.out.println("Max seat ID = " + maxSeatId);


//        Part B ---------------------------------------------------------------
        int minOccupiedSeatId = Integer.MAX_VALUE;
        int maxOccupiedSeatId = Integer.MIN_VALUE;
        boolean[] occupiedSeats = new boolean[1024];

        for (String line = input.line(); line != null; line = input.line()) {
            int seatId = calc.SeatId(line);

            if (seatId > maxOccupiedSeatId) {
                maxOccupiedSeatId = seatId;
            }

            if (seatId < minOccupiedSeatId) {
                minOccupiedSeatId = seatId;
            }

            occupiedSeats[seatId] = true;
        }

        System.out.println("Min seat ID = " + minOccupiedSeatId);
        System.out.println("Max seat ID = " + maxOccupiedSeatId);

        for (int id = minOccupiedSeatId+1; id < maxOccupiedSeatId; id++) {
            if (occupiedSeats[id] == false) {
                System.out.println("Seat ID " + id + " is free");
            }
        }

    }
}
