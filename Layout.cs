using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace UserInterface
{
    static public class Layout
    {
        static void Center(IEnumerable<UIBase> elements, GraphicsDeviceManager graphics)
        {
            throw new NotImplementedException();

        }

        public static void OrderHorizontal(IEnumerable<UIBase> elements, Point anchor, int padding=0)
        {
            int next = anchor.X;
            foreach (var elem in elements)
            {
                elem.Area = new Rectangle(next, anchor.Y, elem.Area.Width, elem.Area.Height);
                next = elem.Area.Right + padding;
            }
        }

        public static void Center(UIBase elem, Point center)
        {
            var x = center.X - elem.Area.Width / 2;
            var y = center.Y - elem.Area.Height / 2;
            elem.Area = new Rectangle(x, y, elem.Area.Width, elem.Area.Height);
        }

        public static void Center(IEnumerable<UIBase> elements, Point center)
        {
            foreach (var elem in elements)
            {
                Center(elem, center);
            }
        }

        public static void OrderVertical(IEnumerable<UIBase> elements, Point centerAnchor, int padding = 5)
        {
            int next = centerAnchor.Y;
            foreach (var elem in elements)
            {
                elem.Area = new Rectangle(elem.Area.X, next, elem.Area.Width, elem.Area.Height);
                next = elem.Area.Bottom + padding;
            }
        }
    }
}
