const string INPUT_FILE_NAME = "input-sample.txt";

var lines = File.ReadAllLines(INPUT_FILE_NAME);

Block.HandleInput(lines);

Console.WriteLine(Block.Get(0, 0));
Console.WriteLine(Block.Get(Block.Height()-1, Block.Width()-1));
