using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class ControllerScript : MonoBehaviour
{
    public float SecondsBetweenUpdates = 0.1f;

    public List<KnotScript> KnotScripts = new List<KnotScript>();

    private List<(int, int)> knots = new List<(int, int)>();
    private int CurrentLineNumber = 0;

    private string[] MoveCommands;

    // Start is called before the first frame update
    void Start()
    {
        MoveCommands = System.IO.File.ReadAllLines("Assets/Data/input.txt");
        // head is knot 0, tail is knot 9
        // set all the initial knot positions to (0, 0)
        for (int i = 0; i < 10; i++)
        {
            var knot = (0, 0);
            knots.Add(knot);
        }

        InvokeRepeating("Step", 0, SecondsBetweenUpdates);
    }

    void Step()
    {
        if (CurrentLineNumber < MoveCommands.Length)
        {
            string line = MoveCommands[CurrentLineNumber];
            HandleLine(line);
            Debug.Log($"{knots[0]}");

            CurrentLineNumber = (CurrentLineNumber + 1) % MoveCommands.Length;
        }
    }



    // Update is called once per frame
    void Update()
    {

    }

    void HandleLine(string line)
    {
        string[] parts = line.Split();
        string dir = parts[0];
        int numsteps = int.Parse(parts[1]);

        (int, int) step = (0, 0);

        switch (dir)
        {
            case "R":
                step = (1, 0);
                break;
            case "L":
                step = (-1, 0);
                break;
            case "U":
                step = (0, 1);
                break;
            case "D":
                step = (0, -1);
                break;
            default:
                throw new Exception("Bad input");
        }

        for (int i = 0; i < numsteps; i++)
        {
            knots[0] = Move(knots[0], step);
            // use Follow on each knot 1 - 9
            for (int j = 1; j <= 9; j++)
            {
                knots[j] = Follow(knots[j - 1], knots[j]);
            }
        }

        for (int i=0; i<KnotScripts.Count; i++)
        {
            var knot = knots[i];
            var knotscript = KnotScripts[i];
            Vector3 target = new Vector3(
                knot.Item1,
                0,
                knot.Item2
                );
            knotscript.TargetPosition = target;
        }

    }

    (int, int) Move((int, int) head, (int, int) step)
    {
        return (head.Item1 + step.Item1, head.Item2 + step.Item2);
    }

    // Move the tail (if necessary) to keep it next to the head.
    // If a tail move was made return true, else return false
    (int, int) Follow((int, int) leader, (int, int) follower)
    {
        (int, int) diff = (
            leader.Item1 - follower.Item1,
            leader.Item2 - follower.Item2);

        // "normalize" the x and y differences
        // for example, if x is off by +2, xnorm = +1
        // if y is off by -2, ynorm = -1
        // if y is off by 1, ynorm = 1
        (int, int) norm = (
            diff.Item1 == 0 ? 0 : diff.Item1 / Math.Abs(diff.Item1),
            diff.Item2 == 0 ? 0 : diff.Item2 / Math.Abs(diff.Item2)
            );

        // if they are touching, nothing to do
        if (Math.Abs(diff.Item1) <= 1 &&
            Math.Abs(diff.Item2) <= 1)
        {
            return follower;
        }

        // if they are not touching, take one step
        // toward the head.
        return (follower.Item1 + norm.Item1,
            follower.Item2 + norm.Item2);
    }
}

/*
//string input = "../../../input-example.txt";
//string input = "../../../input-example2.txt";
string input = "../../../input.txt";

string[] lines = File.ReadAllLines(input);
List<string> tailPositionLog = new List<string>();

// playfield --
// positive X to the right
// positive Y upward
// Head of rope starts at origin (0,0)
// Input is 2000 lines so we won't exceed an int

List<(int, int)> knots = new List<(int, int)>();
// head is knot 0, tail is knot 9
// set all the initial knot positions to (0, 0)
for (int i = 0; i < 10; i++)
{
    var knot = (0, 0);
    knots.Add(knot);
}

// log the starting position of the tail
tailPositionLog.Add(knots[9].ToString());

foreach (var line in lines)
{
    HandleLine(line);
}

//foreach(var log in tailPositionLog.Distinct())
//{
//    Console.WriteLine(log);
//}
Console.WriteLine($"The tail visited {tailPositionLog.Distinct().Count()} " +
    $"unique positions");







*/