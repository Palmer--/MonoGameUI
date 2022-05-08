using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserInterface
{
    public class Panel: UIBase
    {
        public List<UIBase> Elements = new List<UIBase>();

        public override Rectangle Area { get => base.Area; set => AreaChanged(value); }
        public Panel(Texture2D texture):base(texture)
        { 
        
        }

        private void AreaChanged(Rectangle newArea)
        {
            if (newArea == Area)
                return;

            base.Area = newArea;
            var deltaX = newArea.X - Area.X;
            var deltaY = newArea.Y - Area.Y;

            foreach(var element in Elements)
                element.Area = new Rectangle(element.Area.X + deltaX, element.Area.Y + deltaY, Area.Width, Area.Height);
        
        }

        public override UIBase? GetElementAtLocation(Point location)
        {
            foreach (UIBase element in Elements)
            { 
                var e = GetElementAtLocation(location);
                if (e != null)
                    return e;
            }
            return base.GetElementAtLocation(location);
        }

        public override void Update(GameTime gameTime, MouseState oldMState, MouseState mState, KeyboardState oldKState, KeyboardState kState)
        {
            base.Update(gameTime, oldMState, mState, oldKState, kState);
            foreach (UIBase element in Elements)
                element.Update(gameTime, oldMState, mState, oldKState, kState);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gt)
        {
            base.Draw(spriteBatch, gt);
            foreach (var element in Elements)
                element.Draw(spriteBatch, gt);
        }
    }
}
