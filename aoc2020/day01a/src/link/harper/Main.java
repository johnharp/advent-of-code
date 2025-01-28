package link.harper;

import java.util.List;

public class Main {

    public static void main(String[] args) {
        List<Integer> valuesList = Input.GetValues();
        Integer[] values = new Integer[valuesList.size()];
        values = valuesList.toArray(values);

        for(Integer i = 0; i<values.length; i++) {
            for(Integer j = i+1; j<values.length; j++) {
                for(Integer k = j+1; k<values.length; k++) {
                    Integer first = values[i];
                    Integer second = values[j];
                    Integer third = values[k];

                    if (first + second + third == 2020) {
                        System.out.println("Answer is: " + first * second * third);
                    }
                }
            }
        }
    }
}
