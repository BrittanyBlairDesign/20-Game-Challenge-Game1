
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class BaseGameObject
{
    protected Texture2D _texture;
    protected Vector2 _position = Vector2.One;
    
    public int zIndex;
    
    public int Width { get { return _texture.Width; } }
    public int Height { get { return _texture.Height; } }
    public Vector2 Position
    {
        get { return _position; }
        set { _position = value; }
    }

    public virtual void OnNotify(Event eventType, object argument = null) { }
    public virtual void Render(SpriteBatch spriteBatch)
    {
        // TODO: Drawing call here
        spriteBatch.Draw(_texture, _position, Color.White);
    }

    public Vector2 getPosition()
    {
        return _position;
    }
    public void setPosition(Vector2 newPos)
    {
        this._position = newPos;
    }
}

