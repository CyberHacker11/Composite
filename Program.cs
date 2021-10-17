using System;
using System.Collections.Generic;

namespace Composite
{
    interface IGraphic
    {
        void Move(int x, int y);
        void Draw();
    }

    class CompoundGraphic : IGraphic
    {
        public CompoundGraphic() {
            _children = new List<IGraphic>();
        }

        public void Add(IGraphic child) {
            _children.Add(child);
        }

        public void Remove(IGraphic child) {
            _children.Remove(child);
        }

        public void Move(int x, int y) {
            foreach (var child in _children) child.Move(x, y);
        }

        public void Draw() {
            foreach (var child in _children) child.Draw();
        }

        List<IGraphic> _children { get; set; }
    }

    class Dot : IGraphic
    {
        public Dot(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public void Move(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public void Draw() {
            Console.WriteLine($"x: {x} y: {y}");
        }

        protected int x { get; set; }
        protected int y { get; set; }
    }

    class Circle : Dot
    {
        public Circle(int x, int y, double radius) : base(x, y) {
            _radius = radius;
        }

        public new void Draw() {
            Console.WriteLine($"x: {x} y: {y} radius: {_radius}");
        }

        double _radius { get; set; }
    }

    class ImageEditor
    {
        public void Load() {
            All = new CompoundGraphic();
            All.Add(new Dot(1, 2));
            All.Add(new Circle(5, 3, 10));
        }

        public void GroupSelected(IGraphic[] components)
        {
            var group = new CompoundGraphic();
            foreach (var component in components) {
                group.Add(component);
                All.Remove(component);
            }
            All.Add(group);
            All.Draw();
        }

        public CompoundGraphic All { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ImageEditor imageEditor = new ImageEditor();
            IGraphic[] graphics = new IGraphic[] { new Dot(2,4), new Circle(3,6,2) };

            imageEditor.Load();
            imageEditor.GroupSelected(graphics);
        }
    }
}