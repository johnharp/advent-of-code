sums = [0, 0, 0, 0]

def accumulateValue(v):
    sums[0] += v

def finishGroup():
    if sums[0] >= sums[1]:
        sums[3] = sums[2]
        sums[2] = sums[1]
        sums[1] = sums[0]
    elif sums[0] >= sums[2]:
        sums[3] = sums[2]
        sums[2] = sums[0]
    elif sums[0] >= sums[3]:
        sums[3] = sums[0]

    sums[0] = 0

with open('input.txt', 'r') as f:
    for line in f:
        line = line.strip()

        if line == "": 
            finishGroup()
        else:
            accumulateValue(int(line))

finishGroup()

print(f"First Place Sum = {sums[1]}")
print(f"Second Place Sum = {sums[2]}")
print(f"Third Place Sum = {sums[3]}")
print(f"Sum of top 3: {sums[1] + sums[2] + sums[3]}")