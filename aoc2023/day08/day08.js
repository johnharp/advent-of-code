import { read } from '../util/input.js';

export function solve(filename) {
    const input = read(filename);

    const sections = input.split("\n\n");

    const directions = sections[0];
    const nodes = sections[1]
        .split("\n")
        .map(line => parseNode(line))
        .reduce((acc, node) => {
            acc.set(node.name, node);
            return acc
        }, new Map());
    

    // let currentNode = "AAA";
    // let directionIndex = 0;
    // let steps = 0;
    // while (currentNode !== "ZZZ") {
    //     const direction = directions[directionIndex];
    //     directionIndex++;
    //     if (directionIndex >= directions.length) {
    //         directionIndex = 0;
    //     }

    //     currentNode = nodes.get(currentNode)[direction];

    //     steps++;
    // }

    // const part1 = steps;
    const part1 = 0;

    let currentNodes = [...nodes.keys()]
        .filter(k => k.endsWith("A"));

    const steps = currentNodes.map(node => numStepsUnitEnd(nodes, directions, node));
    console.log(steps);

    // gdc() and lcm() come from here:
    // https://www.geeksforgeeks.org/lcm-of-given-array-elements/
    function gcd(a, b) 
    { 
        if (b == 0) 
            return a; 
        return gcd(b, a % b); 
    } 
    
    function lcm(arr) 
    { 
        // Initialize result 
        let ans = arr[0]; 
    
        // ans contains LCM of arr[0], ..arr[i] 
        // after i'th iteration, 
        for (let i = 1; i < arr.length; i++) 
            ans = (((arr[i] * ans)) / 
                    (gcd(arr[i], ans))); 
    
        return ans; 
    } 
    const part2 = lcm(steps);
    
    return [part1, part2];
}

// function part2endCondition(ns) {
//     const zs = ns.filter(n => n.endsWith("Z"));

//     if (zs.length === ns.length) return true;
//     else return false;
// }

function parseNode(s) {
    let [name, contents] = s.split(" = ");
    contents = contents
        .replace("(", "")
        .replace(")", "");
    const [left, right] = contents.split(", ");
    return {
        name: name,
        L: left,
        R: right
    };
}

function numStepsUnitEnd(graph, directions, node) {
    let steps = 0;
    let i = 0;
    let current = node;

    while (!current.endsWith("Z")) {
        steps++;
        const dir = directions[i];
        i++;
        if (i >= directions.length) i = 0;

        current = graph.get(current)[dir];
    }
    return steps;
}

console.log(solve('day08/input.txt'));
