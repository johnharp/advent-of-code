package link.harper;

import java.util.ArrayList;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Validator {

    public static boolean isValidYear(String v, int min, int max) {
        boolean ret = true;

        try {
            Integer intVal = Integer.parseInt(v);
            if (intVal < min || intVal > max) {
                ret = false;
            }
        } catch (Exception e) {
            ret = false;
        }

        return ret;
    }

    public static boolean isValidHeight(
            String v,
            int cmMin, int cmMax,
            int inMin, int inMax) {
        int max;
        int min;

        if (v.endsWith("cm")) {
            min = cmMin;
            max = cmMax;
        } else if (v.endsWith("in")){
            min = inMin;
            max = inMax;
        } else {
            return false;
        }

        String value = v
                .replace("cm", "")
                .replace("in", "");

        try {
            Integer intValue = Integer.parseInt(value);

            if (intValue >= min && intValue <= max) {
                return true;
            } else {
                return false;
            }
        } catch(Exception e) {
            return false;
        }
    }

    public static boolean isValidHairColor(String value) {
        boolean ret = true;

        List<String> allFields = new ArrayList<String>();

        String pattern = "^#[0-9a-f]{6}$";
        Pattern p = Pattern.compile(pattern);
        Matcher m = p.matcher(value);
        if (m.find()) {
            ret = true;
        } else {
            ret = false;
        }

        return ret;
    }

    public static boolean isValidEyeColor(String value) {
        boolean ret = true;

        List<String> allFields = new ArrayList<String>();

        String pattern = "^amb$|^blu$|^brn$|^gry$|^grn$|^hzl$|^oth$";
        Pattern p = Pattern.compile(pattern);
        Matcher m = p.matcher(value);
        if (m.find()) {
            ret = true;
        } else {
            ret = false;
        }

        return ret;
    }

    public static boolean isValidPassportId(String value) {
        boolean ret = true;

        List<String> allFields = new ArrayList<String>();

        String pattern = "^[0-9]{9}$";
        Pattern p = Pattern.compile(pattern);
        Matcher m = p.matcher(value);
        if (m.find()) {
            ret = true;
        } else {
            ret = false;
        }

        return ret;
    }

    public static boolean isValid(String field) {
        boolean ret = true;

        String[] split = field.split(":");
        String tag = split[0];
        String value = split[1];


        switch(tag) {
            case "byr":
                ret = isValidYear(value, 1920, 2002);
                break;
            case "iyr":
                ret = isValidYear(value, 2010, 2020);
                break;
            case "eyr":
                ret = isValidYear(value, 2020, 2030);
                break;
            case "hgt":
                ret = isValidHeight(value, 150, 193, 59, 76);
                break;
            case "hcl":
                ret = isValidHairColor(value);
                break;
            case "ecl":
                ret = isValidEyeColor(value);
                break;
            case "pid":
                ret = isValidPassportId(value);
                break;
        }

        return ret;
    }



    public static  boolean isValid(List<String>fields) {
        for(String field: fields) {
            if (!isValid(field)) {
                return false;
            }
        }

        return true;
    }
}
