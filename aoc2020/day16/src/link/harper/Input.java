package link.harper;

import java.io.File;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Input {
    private static Pattern rulePattern = Pattern.compile("^([^:]+): ([0-9]+)-([0-9]+) or ([0-9]+)-([0-9]+)$");

    public Ticket getMyTicket() {
        return myTicket;
    }

    private Ticket myTicket;

    public void load(String prefix) throws Exception{
        String rulesFile = prefix + "-rules.txt";
        String ticketFile = prefix + "-ticket.txt";
        String nearbyTicketsFile = prefix + "-nearby.txt";

        loadRules(rulesFile);
        myTicket = loadTickets(ticketFile).get(0);
        loadTickets(nearbyTicketsFile);
    }

    private void loadRules(String rulesFile) throws Exception{
        Scanner scanner = new Scanner(
                new File(rulesFile));

        while (scanner.hasNextLine()) {
            String line = scanner.nextLine();
            Matcher ruleMatcher = rulePattern.matcher(line);

            if (ruleMatcher.find()) {
                String fieldName = ruleMatcher.group(1);
                int min1 = Integer.parseInt(ruleMatcher.group(2));
                int max1  = Integer.parseInt(ruleMatcher.group(3));
                int min2 = Integer.parseInt(ruleMatcher.group(4));
                int max2 = Integer.parseInt(ruleMatcher.group(5));

                FieldRule r = new FieldRule(fieldName, min1, max1, min2, max2);

            } else {
                throw new Exception("un-parsable rule: '" + line + "'");
            }

        }
    }

    private List<Ticket> loadTickets(String ticketFile) throws Exception{
        ArrayList<Ticket> tickets = new ArrayList<>();

        Scanner scanner = new Scanner(
                new File(ticketFile));

        while (scanner.hasNextLine()) {
            String line = scanner.nextLine();
            Ticket t = new Ticket(line);
            tickets.add(t);
        }

        return tickets;
    }


}
