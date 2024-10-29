using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2017.Day07
{
    internal class Node
    {
        private bool isRoot = false;

        public Node(string name, int weight)
        {
            this.Name = name;
            this.Weight = weight;
            this.IsChild = false;
            this.IsRoot = false;
            this.Children = new List<Node>();
        }

        public List<Node> Children { get; set; }

        public string Name { get; set; }

        public int Weight { get; set; }

        public bool IsChild { get; set; }

        public bool IsRoot 
        { 
            get
            {
                return this.isRoot;
            }

            set
            {
                if(value == true && this.IsChild == true)
                {
                    this.isRoot = false;
                }
                else
                {
                    this.isRoot = value;
                }
            }
        }

        public Node Parent { get; set; }

        public bool IsLeaf 
        {
            get
            {
                return this.Children.Count == 0;
            }
        }

        public int TotalWeigth { get; set; }

        public override string ToString()
        {
            string suffix = this.IsRoot ? 
                                "root" : 
                                (this.IsChild && !this.IsLeaf) ? 
                                    "parent["+this.Children.Count+"]" :
                                    "leaf";

            return $"{this.Name}({this.Weight}/{this.TotalWeigth}) [{suffix}]";
        }
    }
}
