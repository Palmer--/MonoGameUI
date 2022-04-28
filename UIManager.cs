using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UserInterface
{
    public class UIManager
    {
        public List<UIBase> UiElements { get; set; } = new List<UIBase>();
        KeyboardState oldKeyboardState;
        MouseState oldMouseState;
        SpriteBatch spriteBatch;

        public UIManager(GraphicsDevice graphicsDevice)
        {
            spriteBatch = new SpriteBatch(graphicsDevice); 
        
        }

        public void Update(GameTime gt)
        {
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();

            foreach (UIBase ibase in UiElements)
            {
                ibase.Update(gt, oldMouseState, mouseState, oldKeyboardState, keyboardState);
            }

            oldKeyboardState = keyboardState;
            oldMouseState = mouseState;
        }

        public UIBase? GetUIAtLocation(Point location)
        {
            foreach (UIBase ui in UiElements)
            {
                if (ui.Area.Contains(location))
                    return ui;
            }
            return null;
        }

        public bool IsPointOverGUI(Point location)
        {
            return GetUIAtLocation(location) != null;
        }

        public void Draw(GameTime gt)
        {
            spriteBatch.Begin();
            foreach (UIBase ibase in UiElements)
            {
                ibase.Draw(spriteBatch, gt);
            }
            spriteBatch.End();
        }
    }
}
