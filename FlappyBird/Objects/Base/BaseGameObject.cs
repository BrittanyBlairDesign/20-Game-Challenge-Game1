
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class BaseGameObject
{
    protected Texture2D _texture;
    private Vector2 _position;
    public int zIndex;

    public virtual void OnNotify(Event eventType) { }
    public void Render(SpriteBatch spriteBatch)
    {
        // TODO: Drawing call here
        spriteBatch.Draw(_texture, Vector2.One, Color.White);
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

