from list import *
from functools import cmp_to_key

packets = []
pairs = []
indicies_of_right_ordered_pairs = []

# Compare two packets given the rules outlined
# in the problem description
#
# Returns
#  negative value if left < right
#   0 if left == right
#   positive value if left > right
def comp(left, right):

    if left == right:
        return 0

    if is_integer(left) and is_list(right):
        left = integer_to_list(left)
        return comp(left, right)
    elif is_list(left) and is_integer(right):
        right = integer_to_list(right)
        return comp(left, right)
    elif is_integer(left) and is_integer(right):
        if int(left) < int(right):
            return -1
        elif int(left) > int(right):
            return 1
        else:
            return 0
    elif is_list(left) and is_list(right):
        left_values = list_values(left)
        right_values = list_values(right)

        if len(left_values) == 0 and len(right_values) > 0:
            return -1

        if len(left_values) == 0 and len(right_values) == 0:
            return 0

        i = 0
        while i < len(left_values):

            # if right side ran out of values, not in the right order
            if i+1 > len(right_values):
                return 1

            left_value = left_values[i]
            right_value = right_values[i]

            result = comp(left_value, right_value)
            
            if result < 0:
                return result
            elif result > 0:
                return result   

            i += 1  

        # left side ran out of items so in right order
        return -1

    return -1

# Decides if a value is a list
# 
# Returns
#  True if v is a list
#  False if v is not a list
def is_list(v):
    if len(v) > 0 and v[0] == "[": return True
    else: return False

# Decides if a value is an integer
#
# Returns
#  True if v is and integer
#  False if v is not an integer
def is_integer(v):
    
    return not is_list(v) and len(v) > 0

def integer_to_list(v):
    return f"[{v}]"


with open('input.txt', 'r') as f:
    for line in f:
        line = line.strip()
        if line != "":
            packets.append(line)

pair_num = 1
total_pairs = len(packets) / 2


while pair_num <= total_pairs:
    left_index = (pair_num-1) * 2
    right_index = ((pair_num-1) * 2) + 1

    left = packets[left_index]
    right = packets[right_index]

    result = comp(packets[left_index], packets[right_index])
    if result <=0 :
        indicies_of_right_ordered_pairs.append(pair_num)
    pair_num += 1



print(f"Correct pair numbers: {indicies_of_right_ordered_pairs}")
print(f"Sum: {sum(indicies_of_right_ordered_pairs)}")

divider1 = "[[2]]"
divider2 = "[[6]]"

packets.append(divider1)
packets.append(divider2)

sorted_packets = sorted(packets, key=cmp_to_key(comp))

divider1_index = sorted_packets.index(divider1) + 1
divider2_index = sorted_packets.index(divider2) + 1

product_divider_indicies = divider1_index * divider2_index

print(f"divider 1 is at {divider1_index}")
print(f"divider 2 is at {divider2_index}")
print(f"Produce of marker indicies: {product_divider_indicies}")
