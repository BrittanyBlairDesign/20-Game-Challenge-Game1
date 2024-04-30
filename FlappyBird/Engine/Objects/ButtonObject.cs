

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;

public class ButtonObject : BaseGameObject
{
    private Rectangle boxCollider;
    protected Color color;

    private bool isDebug = false;
    private Texture2D collisionTexture;
    public ButtonObject(Texture2D texture, Vector2 location)
    {
        this._texture = texture;
        this.Position = location - new Vector2(Width / 2, 0);
        color = Color.White;
        boxCollider = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
    }

    public ButtonObject(GraphicsDevice graphics, Texture2D texture, Vector2 location)
    {
        this._texture = texture;
        this.Position = location - new Vector2(Width / 2, 0);
        color = Color.White;
        boxCollider = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

        isDebug = true;
        SetCollisionTexture(graphics);
    }

    public virtual bool isHovering()
    {
        MouseState state = Mouse.GetState();
        if (boxCollider.Contains(state.Position))
        {
            color = Color.WhiteSmoke;
            
            return true;
        }
        else
        {
            Trace.WriteLine("Mouse Position = " + state.Position);
            color = Color.White;
            return false;
        }
    }

    public override void SetCollisionTexture(GraphicsDevice graphics)
    {
        Texture2D Texture = new Texture2D(graphics, boxCollider.Width, boxCollider.Height);
        var colors = new List<Color>();

        for (int i = 0; i < boxCollider.Height; i++)
        {
            for (int j = 0; j < boxCollider.Width; j++)
            {
                if (i == 0 || i == boxCollider.Width - 1
                 || j == 0 || j == boxCollider.Height - 1)
                {
                    colors.Add(Color.Yellow);
                }
                else
                {

                    colors.Add(Color.Transparent);
                }
            }
        }

        if (colors != null)
        {
            Texture.SetData<Color>(colors.ToArray());
            collisionTexture = Texture;
        }
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, color);
        if(collisionTexture != null)
        {
            spriteBatch.Draw(collisionTexture, _position, color);
        }
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
        spriteBatch.DrawString(font, text, Position, color);
        base.Render(spriteBatch);
    }
}