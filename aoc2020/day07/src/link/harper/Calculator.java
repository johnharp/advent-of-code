package link.harper;

import java.io.Console;
import java.util.ArrayList;
import java.util.List;

public class Calculator {
    private List<BagSpec> specs;

    public void loadSpecs() throws Exception {
        Input input = new Input();
        input.open("input.txt");

        specs = new ArrayList<>();

        String line = input.line();
        while(line != null) {
            BagSpec spec = BagSpec.parseLine(line);
            specs.add(spec);

            line = input.line();
        }
    }

    public BagSpec findByColor(String color) {
        for (BagSpec spec: specs) {
            if (spec.getColor().equals(color)) {
                return spec;
            }
        }

        return null;
    }

    public List<String> findAllThatCanContainColor(String color) {
        List<String> list = new ArrayList<>();

        for(BagSpec spec: specs) {
            for(BagSpec subspec: spec.getContains()) {
                if (subspec.getColor().equals(color)) {
                    list.add(spec.getColor());
                    break;
                }
            }
        }

        return list;
    }
}
