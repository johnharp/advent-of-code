import unittest

def list_values(l):
    l = l[1:-1]
    v = []

    acc=""
    i = 0
    braces = 0

    while i < len(l):
        char = l[i]
        if char == "[":
            braces += 1
            acc += char
        elif char == "]":
            braces -= 1
            acc += char
        elif char == ",":
            if braces > 0:
                acc += char
            elif braces == 0:
                v.append(acc)
                acc = ""
        else:
             acc += char

        i += 1

    if acc != "": v.append(acc)
    return v

class TestList(unittest.TestCase):
    
    def test_list_values1(self):
        self.assertEqual(
            list_values("[[10,[],0],[[],[],[[1,2,6],8,[1,2,6,2],[3,4,6,5,4],[2,3,7,8,1]],[[7,3],0,6,[0,2,8,2],4],[[],5]]]"),
            [ "[10,[],0]", "[[],[],[[1,2,6],8,[1,2,6,2],[3,4,6,5,4],[2,3,7,8,1]],[[7,3],0,6,[0,2,8,2],4],[[],5]]" ]
        )

    def test_list_values2(self):
        self.assertEqual(
            list_values("[[],[],[[1,2,6],8,[1,2,6,2],[3,4,6,5,4],[2,3,7,8,1]],[[7,3],0,6,[0,2,8,2],4],[[],5]]"),
            [ "[]", "[]", "[[1,2,6],8,[1,2,6,2],[3,4,6,5,4],[2,3,7,8,1]]", "[[7,3],0,6,[0,2,8,2],4]", "[[],5]"]
        )

    def test_list_values3(self):
        self.assertEqual(
            list_values("[]"),
            []
        )

if __name__ == '__main__':
    unittest.main()