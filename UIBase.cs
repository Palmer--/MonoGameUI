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
    public abstract class UIBase
    {
        public static SpriteFont DefaultFont { get; set; }

        public virtual Rectangle Area { get; set; }
        public virtual Texture2D DefaultTexture { get; set; }
        public virtual Texture2D MouseOverTexture { get; set; }
        public virtual Texture2D ClickTexture { get; set; }
        public static UIBase GotFocus { get; set; }
        private Texture2D ActiveTexture { get; set; }
        protected Color Color = Color.White;
        public event EventHandler Clicked;

        public virtual Point Center{ get => Area.Center; set => Area = new Rectangle(value.X, value.Y, Area.Width, Area.Height); }


        public void AutoSetAllTextures(Texture2D texture, GraphicsDevice graphics)
        {
            DefaultTexture = texture;
            MouseOverTexture = CreateModifiedTexture(texture, 1.1f, graphics);
            ClickTexture = CreateModifiedTexture(texture, 1.2f, graphics);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ActiveTexture, Area, Color);
        }
        public virtual void Update(GameTime gameTime,MouseState oldMState,MouseState mState, KeyboardState oldKState,KeyboardState kState)
        {
            if (!Area.Contains(mState.Position))
            {
                ActiveTexture = DefaultTexture;
                return;
            }
            if (LeftMousePressedThisTick(oldMState, mState))
            {
                ActiveTexture = ClickTexture;
                GotFocus = this;
                return;
            }
            if (GotFocus == this && LeftMouseReleasedThisTick(oldMState, mState))
            {
                Clicked?.Invoke(this, new EventArgs());
                ActiveTexture = MouseOverTexture;
                return;
            }
            ActiveTexture = MouseOverTexture;
        }

        private bool LeftMouseReleasedThisTick(MouseState oldMState, MouseState mState)
        {
            return oldMState.LeftButton == ButtonState.Pressed
                   && mState.LeftButton == ButtonState.Released;
        }

        private static bool LeftMousePressedThisTick(MouseState oldMState, MouseState mState)
        {
            return oldMState.LeftButton == ButtonState.Released && mState.LeftButton == ButtonState.Pressed;
        }

        public virtual Texture2D CreateModifiedTexture(Texture2D defaultTexture,float multiplyValue, GraphicsDevice graphics)
        {
            Color[] colors = new Color[DefaultTexture.Width * defaultTexture.Height];
            DefaultTexture.GetData(colors);

            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.Multiply(colors[i], multiplyValue);
            }

            var newTexture = new Texture2D(graphics, defaultTexture.Width, defaultTexture.Height);
            newTexture.SetData(colors);
            return newTexture;
        }
    }
}
