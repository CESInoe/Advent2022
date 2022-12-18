namespace AdventOfCode
{
    internal class Tree
    {
        public Tree(string name, Tree? parent)
        {
            _name = name;
            _isFolder = true;
            _parent = parent;
            _contents = new();
        }

        public Tree(int value, Tree? parent)
        {
            _value = value;
            _isFolder = false;
            _parent = parent;
            _contents = new();
        }

        private bool _isFolder = false;

        public bool IsFolder { get => _isFolder; set => _isFolder = value; }

        private List<Tree> _contents;
        public List<Tree> Contents { get => _contents; set => _contents = value; }

        private Tree? _parent;
        internal Tree? Parent { get => _parent; set => _parent = value; }

        private string? _name;
        public string? Name { get => _name; set => _name = value; }
        public int Value { get => _value; set => this._value = value; }

        private int _value;

        public int Do(List<int> input)
        {
            int result = 0;

            return result;
        }
    }

    internal class Runner
    {
        internal static int SearchSmallFolders(Tree folder, List<Tree> smallFolders)
        {
            int result = 0;
            if (folder.IsFolder)
            {
                foreach(Tree file in folder.Contents)
                {
                    result += file.IsFolder ? SearchSmallFolders(file, smallFolders) : file.Value;
                }
            }
            folder.Value = result;
            if (result < 100000)
            {
                smallFolders.Add(folder);
            }
            return result;
        }

        internal static int SearchSmallestBigFolder(Tree folder, int value, int min)
        {
            int smallest = value;
            if(folder.Value > min && folder.Value < smallest)
            {
                smallest = folder.Value;
            }
            foreach(Tree file in folder.Contents)
            {
                if (file.IsFolder)
                {
                    int temp = SearchSmallestBigFolder(file, smallest, min);
                    if (temp > min && temp < smallest)
                    {
                        smallest = temp;
                    }
                }
            }
            return smallest;
        }

        internal static void Resolve()
        {
            var input = File.ReadLines("../../../input7.txt");
            Console.WriteLine(input.First());
            Tree root = new("/", null);
            Tree here = root;
            var result = 0;
            foreach (var line in input)
            {
                var splitedLine = line.Split(" ");
                if (splitedLine[0] == "$")
                {
                    if (splitedLine[1] == "cd")
                    {
                        if (splitedLine[2] == "..")
                        {
                            here = here.Parent!;
                        }
                        else
                        {
                            here = here.Contents.First(x => x.Name == splitedLine[2]);
                        }
                    }
                }
                else if (splitedLine[0] == "dir")
                {
                    here.Contents.Add(new(splitedLine[1], here));
                    Console.WriteLine(splitedLine[1]);
                }
                else if (int.TryParse(line.Split(" ")[0], out int temp))
                {
                    here.Contents.Add(new(temp, here));
                    Console.WriteLine(temp);
                }
            }
            List<Tree> smallFolders = new();
            Runner.SearchSmallFolders(root, smallFolders);
            foreach (Tree folder in smallFolders)
            {
                result += folder.Value;
            }


            Console.WriteLine("total sum of small folder =" + result.ToString());
            Console.WriteLine("smallest big folder = " + Runner.SearchSmallestBigFolder(root, 70000000, 30000000 - (70000000 - root.Value)));
        }

    }
}
