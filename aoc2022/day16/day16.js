import { readLines } from '../util/input.js';

export function solve(filename) {
    const lines = readLines(filename);

    const graph = lines
        .map(parseLine)
        .reduce((acc, node) => {
            acc.set(node.key, node);
            return acc;
        }, new Map());

    // give each graph entry a "dist" attribute that 
    // summarizes distances to all nodes in the graph
    for (const key of graph.keys()) {
        const node = graph.get(key);
        node.dist = dijkstra(graph, node).dist;
    }

    const order = ["DD", "BB", "JJ", "HH", "EE", "CC" ];

    const onStarting = calcOnAtTimes(graph, "AA", order);
    let part1 = calcTotalPressureReleased(graph, order, onStarting);


    const part2 = lines.length;
    return [part1, part2];
}

function parseLine(line) {
    const re = /Valve ([A-Z]+) has flow rate=(\d+); tunnels? leads? to valves? (.+)$/;
    const found = line.match(re);
    const key = found[1];
    const rate = Number(found[2]);
    const neighbors = found[3].split(", ");

    return {
        key: key,
        rate: rate,
        neighbors: neighbors
    }
}




/*
https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
 1  function Dijkstra(Graph, source):
 2      
 3      for each vertex v in Graph.Vertices:
 4          dist[v] ← INFINITY
 5          prev[v] ← UNDEFINED
 6          add v to Q
 7      dist[source] ← 0
 8      
 9      while Q is not empty:
10          u ← vertex in Q with min dist[u]
11          remove u from Q
12          
13          for each neighbor v of u still in Q:
14              alt ← dist[u] + Graph.Edges(u, v)
15              if alt < dist[v]:
16                  dist[v] ← alt
17                  prev[v] ← u
18
19      return dist[], prev[]
*/

function dijkstra(graph, start) {
    const dist = new Map();
    const prev = new Map();
    const Q = [];

    for (const v of graph.keys()) {
        dist.set(v, Infinity);
        prev.set(v, undefined);
        Q.push(v);
    }
    dist.set(start.key, 0);

    while (Q.length > 0) {
        Q.sort((a, b) => dist.get(a) - dist.get(b));
        const u  = Q.shift();

        for (const v of graph.get(u).neighbors) {
            if (Q.includes(v)) {
                // this is a special case of dijkstra where the distance
                // (weight) to all neighbors is 1
                const alt = dist.get(u) + 1;
                if (alt < dist.get(v)) {
                    dist.set(v, alt);
                    prev.set(v, u);
                }
            }
        }
    }

    return {
        dist: dist,
        prev: prev
    }
}

function calcOnAtTimes(graph, start, valves) {
    const onStarting = []
    let current = start;
    let time = 0;

    for (const dest of valves) {
        const travel = graph.get(current).dist.get(dest);
        time = time + travel + 1;
        onStarting.push(time);

        current = dest;
    }

    return onStarting;
}

function calcTotalPressureReleased(graph, valves, onAtTime) {
    let sum = 0;

    for (let i = 0; i<valves.length; i++) {
        const valve = graph.get(valves[i]);
        const t = onAtTime[i];

        sum += valve.rate * (30 - t);
    }

    return sum;
}

console.log(solve('day16/input-sample.txt'));
