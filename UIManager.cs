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

        public UIBase GetUIAtLocation(Point location)
        {
            foreach (UIBase ui in UiElements)
            {
                if (ui.Area.Contains(location))
                    return ui;
            }
            return null;
        }

        public bool UIAtLocation(Point location)
        {
            return GetUIAtLocation(location) != null;
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (UIBase ibase in UiElements)
            {
                ibase.Draw(sb);
            }
        } 
    }
}
