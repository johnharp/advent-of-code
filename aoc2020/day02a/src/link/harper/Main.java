package link.harper;

public class Main {

    public static void main(String[] args) {
        String[] strs = Input.GetLines();
        Integer numValid = 0;

        for(String str:  strs) {
            PasswordLine line = new PasswordLine((str));

            if (line.isValidPart2()) {
                numValid++;
            }
        }

        System.out.println("Number of valid passwords: " + numValid);
    }
}
