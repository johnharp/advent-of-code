using day11;

//CreateExampleMonkeys();

CreateMonkeys();


for (int i = 1; i <= 10_000; i++)
{
    Monkey.CompleteRound();
}

Monkey.PrintMonkeyBusinessLevel();



void CreateExampleMonkeys()
{
    new Monkey(op: "*", opv: 19,
        testv: 23, trueval: 2, falseval: 3,
        79, 98);
    new Monkey(op: "+", opv: 6,
        testv: 19, trueval: 2, falseval: 0,
        54, 65, 75, 74);
    new Monkey(op: "**", opv: 0,
        testv: 13, trueval: 1, falseval: 3,
        79, 60, 97);
    new Monkey(op: "+", opv: 3,
        testv: 17, trueval: 0, falseval: 1,
        74);
}

void CreateMonkeys()
{
    new Monkey(
        op: "*", opv: 11,
        testv: 7, trueval: 6, falseval: 7,
        66, 79);
    new Monkey(
        op: "*", opv: 17,
        testv: 13, trueval: 5, falseval: 2,
        84, 94, 94, 81, 98, 75);
    new Monkey(
        op: "+", opv: 8,
        testv: 5, trueval: 4, falseval: 5,
        85, 79, 59, 64, 79, 95, 67);
    new Monkey(
        op: "+", opv: 3,
        testv: 19, trueval: 6, falseval: 0,
        70);
    new Monkey(
        op: "+", opv: 4,
        testv: 2, trueval: 0, falseval: 3,
        57, 69, 78, 78);
    new Monkey(
        op: "+", opv: 7,
        testv: 11, trueval: 3, falseval: 4,
        65, 92, 60, 74, 72);
    new Monkey(
        op: "**", opv: 0,
        testv: 17, trueval: 1, falseval: 7,
        77, 91, 91);
    new Monkey(
        op: "+", opv: 6,
        testv: 3, trueval: 2, falseval: 1,
        76, 58, 57, 55, 67, 77, 54, 99);
}