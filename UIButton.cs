using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UserInterface
{
    public class UIButton:UIBase
    {
        public Texture2D Icon { get; set; }

        public override void Draw(SpriteBatch spriteBatch, GameTime gt)
        {
            base.Draw(spriteBatch, gt);
            if(Icon != null)
                spriteBatch.Draw(Icon, Area, Color);
        }
    }
}
