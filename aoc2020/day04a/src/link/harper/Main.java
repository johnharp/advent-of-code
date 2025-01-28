package link.harper;

import java.util.ArrayList;
import java.util.Collections;
import java.util.HashSet;
import java.util.List;

public class Main {


    public static void main(String[] args) {
    	int numValid = 0;
    	boolean doneReading = false;

	    Input input = new Input();
		List<String> fields = new ArrayList<String>();

	    input.open("input.txt");

	    while(!doneReading) {
			String line = input.line();
			List<String> newFields = new ArrayList<>();

			if (line == null) {
	    		doneReading = true;
			} else {
				newFields = input.fields(line);
			}

	    	if (newFields.size() == 0) {
	    		fields = dedupFields(fields);
	    		removeCidField(fields);
	    		//sortFields(fields);

	    		if (fields.size() == 7 &&
					Validator.isValid((fields))) {
	    			numValid++;
				}
	    		//System.out.println(fields);
	    		fields = new ArrayList<String>();
	    		//System.out.println("------------");

			} else {
	    		fields.addAll(newFields);
			}

        }

	    System.out.println("Num valid: " + numValid);
    }

    public static List<String> dedupFields(List<String> fields) {
		return new ArrayList<>(
				new HashSet<>(fields));
	}

	public static void sortFields(List<String> fields) {
    	Collections.sort(fields);
	}

	public static void removeCidField(List<String> fields) {
    	List<String> remove = new ArrayList<>();

    	for(String field : fields) {
    		if (field.startsWith("cid:")) {
    			remove.add(field);
			}
		}

    	for(String removeItem : remove) {
    		fields.remove(removeItem);
		}
	}
}
