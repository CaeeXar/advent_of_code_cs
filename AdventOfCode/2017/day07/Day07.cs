using System.Data;

namespace AOC2017.Day07
{
    internal class Day07
    {
        private int total { get; set; }
        string path = @"2017\day07\Input.txt";

        public string ReadFile()
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return null;
            }
        }

        public string Part1() 
        {
            Node root = CreateTree();
            return root.Name;
        }

        public int Part2()
        {
            Node root = CreateTree();
            Node unbalanced = FindUnbalanced(root);
            return WeigthDifference(unbalanced.Children);
        }

        private int WeigthDifference(List<Node> nodes)
        {
            Dictionary<int, Node> weigths = new Dictionary<int, Node>();
            Node balanced = null, unbalanced = null;
            foreach (Node n in nodes)
            {
                if (weigths.ContainsKey(n.TotalWeigth))
                {
                    balanced = weigths[n.TotalWeigth];
                }
                else
                {
                    weigths.Add(n.TotalWeigth, n);
                }
            }
            
            foreach (KeyValuePair<int, Node> kvp in weigths)
            {
                if (kvp.Value != balanced)
                {
                    unbalanced = kvp.Value;
                }
            }

            int diff = Math.Abs(balanced.TotalWeigth - unbalanced.TotalWeigth);
            if(unbalanced.TotalWeigth < balanced.TotalWeigth)
            {
                return unbalanced.Weight + diff;
            }
            else
            {
                return unbalanced.Weight - diff;
            }
        }

        private Node FindUnbalanced(Node current)
        {
            if (current == null || current.IsLeaf)
            {
                return null; // everything balanced
            }
            
            Dictionary<int, List<Node>> sums = new Dictionary<int, List<Node>>();
            foreach (Node child in current.Children)
            {
                int sum = MakeSum(child);
                if(!sums.ContainsKey(sum))
                {
                    List<Node> nodes = new List<Node>();
                    nodes.Add(child);
                    sums.Add(sum, nodes);
                }
                else
                {
                    sums[sum].Add(child);
                }
            }

            List<Node>? found = sums.FirstOrDefault(d => d.Value.Count == 1).Value;
            if(found == null)
            {
                return current.Parent; // found the unbalanced (sub-)tree
            }

            return FindUnbalanced(found[0]); // look for unbalanced (sub-)tree
        }

        private int MakeSum(Node root, bool debug = false)
        {
            if (root.IsLeaf) return root.Weight;

            int sum = root.Weight;
            foreach (Node child in root.Children)
            {
                sum += MakeSum(child, debug);
            }

            if (debug)
            {
                Console.WriteLine($"{root.Name} ({sum})");
            }

            return sum;
        }

        private void PrintTree(Node root, int level = 0)
        {
            if (root == null) return;

            foreach (Node child in root.Children)
            {
                PrintTree(child, level+1);
            }

            string padding = new string('\t', level);
            Console.WriteLine(padding + root.ToString());
        }

        private Node CreateTree()
        {
            string[] lines = ParseInput(ReadFile());
            List<Node> all = new List<Node>();
            foreach (string line in lines)
            {
                string[] data = line.Split(" ")
                                    .Select(d => d.Trim())
                                    .ToArray();

                Node n = new Node(data[0], int.Parse(data[1].Replace("(", "")
                                                            .Replace(")", "")));

                all.Add(n);
            }

            foreach (string line in lines)
            {
                if (!line.Contains(" -> ")) continue;

                string[] data = line.Split(" -> ");
                string parentName = data[0].Split(" ")
                                       .Select(d => d.Trim())
                                       .ToArray()[0];
                string[] children = data[1].Split(",")
                                            .Select(d => d.Trim())
                                            .ToArray();

                Node? parent = all.Find(d => d.Name == parentName);
                parent.IsRoot = true;
                foreach (string child in children)
                {
                    Node? childNode = all.Find(d => d.Name == child);
                    childNode.IsChild = true;
                    childNode.IsRoot = false;
                    childNode.Parent = parent;
                    parent?.Children.Add(childNode);
                }
            }

            foreach (Node node in all)
            {
                node.TotalWeigth = MakeSum(node);
            }

            return all.Find(d => d.IsRoot && !d.IsChild);
        }

        private string[] ParseInput(string input)
        {
            return input.Split('\n')
                        .Select(line => line.Trim())
                        .ToArray();
        }
    }
}
