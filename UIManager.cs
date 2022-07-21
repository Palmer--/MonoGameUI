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
        public IEnumerable<UIBase> UiElements { get => elements;}
        private List<UIBase> elements { get; set; } = new List<UIBase>();
        private LinkedList<UIBase> waitingRemove = new LinkedList<UIBase>();
        private LinkedList<UIBase> waitingAdd = new LinkedList<UIBase>();
        KeyboardState oldKeyboardState;
        MouseState oldMouseState;
        SpriteBatch spriteBatch;

        public UIManager(GraphicsDevice graphicsDevice)
        {
            spriteBatch = new SpriteBatch(graphicsDevice); 
        }


        public void Add(UIBase ui)
        { 
            waitingAdd.AddLast(ui);
        }

        public void Remove(UIBase ui)
        {
            waitingRemove.AddLast(ui);
        }

        public void Update(GameTime gt)
        {
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();
            foreach (UIBase ibase in UiElements)
            {
                ibase.Update(gt, oldMouseState, mouseState, oldKeyboardState, keyboardState);
            }
            ProcessRemovedAddedElements();
            oldKeyboardState = keyboardState;
            oldMouseState = mouseState;
        }

        public void ProcessRemovedAddedElements()
        {
            ProccessRemovedElements();
            ProccessAddedElements();
        }

        private void ProccessAddedElements()
        {
            foreach (var e in waitingAdd)
            {
                elements.Add(e);
            }
            waitingAdd.Clear();
        }

        private void ProccessRemovedElements()
        {
            foreach (var e in waitingRemove)
            {
                elements.Remove(e);
            }
            waitingRemove.Clear();
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
