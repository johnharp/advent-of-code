export function sumArray(items) {
    const sum = items.reduce((acc, item) => acc + item, 0);

    return sum;
}

export function sumArrayN(items, n) {
    const sum = sumArray(items.slice(0, n));

    return sum;
}