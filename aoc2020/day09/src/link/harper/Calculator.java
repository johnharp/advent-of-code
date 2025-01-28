package link.harper;

import java.util.ArrayList;
import java.util.List;

public class Calculator {
    private long[] data;

    public long get(int i) {
        return data[i];
    }

    public int length() {
        return data.length;
    }

    public boolean doesNumberExist(long num, int startIndex, int endIndex) {
        if (startIndex >= data.length ||
          endIndex >= data.length) {
            return true;
        }

        for (int i = startIndex; i<=endIndex; i++) {
            if (data[i] == num)
            {
                return true;
            }
        }

        return false;
    }

    public boolean isNumberAtIndexValid(int index, int lookBackSize) {
        long numToCheck = data[index];

        for (int i = index - lookBackSize; i < index-1; i ++) {
            long firstTarget = data[i];
            long lookingForNum = numToCheck - firstTarget;

            if (lookingForNum < 0) {
                // All numbers are positive ints
                // Assuming 0 isn't allowed, but maybe... so allow it
                continue;
            }
            for (int j = i + 1; j < index; j++) {
                long secondTarget = data[j];
                if (secondTarget == lookingForNum) {
                    System.out.println(firstTarget + " + " + secondTarget + " = " + numToCheck);
                    return true;
                }
            }
        }

        System.out.println("INVALID NUMBER at index " + index + " number is " + numToCheck);
        return false;
    }

    public int findFirstInvalidIndex(int lookBackSize) {
        for(int index = lookBackSize; index < data.length; index++) {
            if (!isNumberAtIndexValid(index, lookBackSize)) {
                return index;
            }
        }

        return -1; // No invalid index found
    }

    public long sumNumbersBetween(int startIndex, int endIndex) throws ArithmeticException {
        long sum = 0;

        for (int i = startIndex; i<=endIndex; i++) {
            sum = Math.addExact(sum, data[i]);
        }

        return sum;
    }

    public Range findRangeThatSumsToNum(long targetNum) {
        long sumSoFar;
        int startIndex = 0;
        int endIndex = 1;

        sumSoFar = data[startIndex] + data[endIndex];

        while(sumSoFar != targetNum) {
            System.out.println("sum[" + startIndex + "-" + endIndex + "] = " +
                    sumSoFar);
            if (sumSoFar < targetNum) {
                // If too small, expand the range downward
                // this is always safe (except for possibly walking off the
                // end of the data)

                endIndex++;
                sumSoFar = Math.addExact(sumSoFar, data[endIndex]);
            } else if (sumSoFar > targetNum) {
                // however, if too large, contract the range by moving the start
                // downward

                // But, be careful to not advance the start up to the end
                // index.  If contracting would do that, just advance the end
                // by one anyway even though this will make the sum too large
                // top of the next iteration will notice that and contract

                if (startIndex == endIndex - 1) {
                    endIndex++;
                    sumSoFar = Math.addExact(sumSoFar, data[endIndex]);
                } else {
                    sumSoFar = Math.addExact(sumSoFar, - data[startIndex]);
                    startIndex ++;
                }
            }
        }
        if (sumSoFar == targetNum) {
            // Found it!
            System.out.println("RANGE FROM [" + startIndex + " - " +
                    endIndex + "]  sums up to " + targetNum);
            System.out.println("data[" + startIndex + "] = " + data[startIndex]);
            System.out.println("data[" + endIndex + "] = " + data[endIndex]);

            long sum = Math.addExact(data[startIndex], data[endIndex]);

            System.out.println(data[startIndex] + " + " +
                    data[endIndex] + " = " +
                    sum);
            Range range = new Range(startIndex, endIndex);
            return range;
        }
        return null;
    }

    public void loadInput(String filename) {
        List<Long> dataList = new ArrayList<>();

        Input input = new Input();
        input.open(filename);

        String line = input.line();

        while(line != null) {
            long inputNum = Long.parseLong(line);
            dataList.add(inputNum);

            line = input.line();
        }

        data = dataList.stream().mapToLong(i -> i).toArray();
    }

    public long findSumOfMinMaxInRange(Range r) {
        long minValue = Long.MAX_VALUE;
        long maxValue = Long.MIN_VALUE;

        for (int i = r.getStart(); i <= r.getEnd(); i++) {
            if (data[i] > maxValue) maxValue = data[i];
            if (data[i] < minValue) minValue = data[i];
        }

        return minValue + maxValue;
    }
}
