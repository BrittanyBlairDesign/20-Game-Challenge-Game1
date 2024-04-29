

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class ButtonObject : BaseGameObject
{
    private Rectangle boxCollider;
    private Color color;
    public ButtonObject(Texture2D texture, Vector2 location)
    {
        this._texture = texture;
        this.Position = location;
        color = Color.White;
        boxCollider = new Rectangle((int)location.X, (int)location.Y, Width, Height);
    }

    public virtual bool isHovering()
    {
        MouseState state = Mouse.GetState();
        if (state.X == Position.X && state.Y == Position.Y)
        {
            color = Color.WhiteSmoke;
            return true;
        }
        else
        {
            color = Color.White;
            return false;
        }
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, color);
    }
}

public class TextButton :ButtonObject
{

    private string text;
    private SpriteFont font;

    public TextButton(Texture2D texture, Vector2 location, string text, SpriteFont font) : base(texture, location)
    {
        this.text = text;
        this.font = font;
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, text, Position, Color.White);
        base.Render(spriteBatch);
    }
}