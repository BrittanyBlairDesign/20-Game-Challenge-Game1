

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


public class TextObject : BaseGameObject
{
    public string text;
    public SpriteFont font;
    public string fontStr;
    public TextObject(Texture2D texture,string text, SpriteFont font)
    {
        this._texture = texture;
        this.text = text;
        this.font = font;
    }

    public virtual void setText(string text)
    {
        this.text = text;
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        base.Render(spriteBatch);
        spriteBatch.DrawString(font, text, new Vector2(Height/2, 5 ), Color.Gold);
    }
}
