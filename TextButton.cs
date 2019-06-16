using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UserInterface
{
    public class UITextButton:UIButton
    {
        Vector2 fontOrigin;

        public UITextButton()
        {
            Font = DefaultFont;
        }

        public SpriteFont Font { get; set; }

        public override Rectangle Area
        {
            get { return base.Area; }
            set
            {
                base.Area = value;
                RecalcFontOrigin();
            }
        }

        string text;
        public string Text
        {
            get => text;

            set
            {
                text = value;
                RecalcFontOrigin();
            }
        }


        private void RecalcFontOrigin()
        {
            if (Font == null)
                return;

            text = SplitMessage(text ?? string.Empty, Font, Area.Width - 3);
            Vector2 fontsize = Font.MeasureString(Text);
            fontOrigin = new Vector2(Area.X + Area.Width / 2 - fontsize.X / 2, Area.Y + (Area.Height / 2) - fontsize.Y / 2);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gt)
        {
            base.Draw(spriteBatch, gt);
            spriteBatch.DrawString(Font, text, fontOrigin, Color.White);
        }

        private string SplitMessage(string message, SpriteFont font, float maxWidth)
        {
            if (maxWidth < 0)
                return message;
            Vector2 originSize = font.MeasureString(message);
            StringBuilder retval = new StringBuilder();
            string[] substrings = message.Split(' ');
            for (int i = 0; i < substrings.Length; i++)
            {
                if (substrings[i] == "")
                    continue;
                if (font.MeasureString(retval + substrings[i]).X > maxWidth)
                    retval.Append('\n' + substrings[i]);
                else
                    retval.Append(' ' + substrings[i]);
            }
            return retval.ToString();
        }

        public void ScaleButtonHeightToText()
        {
            Vector2 size = Font.MeasureString(text);
            Rectangle area = Area;
            area.Height = (int)size.Y + 4;
            Area = area;
        }
    }
}
