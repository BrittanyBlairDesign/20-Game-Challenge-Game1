
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System;



public class PlayerSprite : BaseGameObject
{
    public List<Rectangle> collisions = new List<Rectangle>();
    protected List<Texture2D> collisionTexture = new List<Texture2D>();
    private bool isDebug;
    public PlayerSprite(Texture2D texture)
    {
        _texture = texture;
        isDebug = false;
    }

    public PlayerSprite(GraphicsDevice graphics, Texture2D texture)
    {
        _texture = texture;
        SetCollisionTexture(graphics);
        isDebug = true;
    }

    public PlayerSprite(GraphicsDevice graphics, Texture2D texture, Vector2 startPos)
    {
        _texture = texture;
        GenerateCollisions();
        SetCollisionTexture(graphics);
        isDebug = true;
    }
    
    public virtual void GenerateCollisions() { }

    public virtual void SetCollisionTexture(GraphicsDevice graphics)
    {
        collisionTexture.Clear();

        foreach (Rectangle r in collisions)
        {
            Texture2D Texture = new Texture2D(graphics, r.Width, r.Height);
            var colors = new List<Color>();

            /*
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (x > minW && x < maxW ||
                        y > minH && y < maxH)
                    {
                        if (x == minW || x == maxW ||
                           y == minH || y == maxH)
                        {
                            colors.Add(Color.White);
                        }
                        else
                        {
                            colors.Add(Color.Red);
                        }
                    }
                    else
                    {
                        colors.Add(Color.Transparent);
                    }
                }
            }*/

            for( int i = 0; i < r.Height; i++ )
            {
                for (int j = 0; j < r.Width; j++ )
                {
                    if( i == 0 || i == r.Width - 1
                     || j == 0 || j == r.Height - 1 )
                    {
                        colors.Add(Color.Red);
                    }
                    else
                    {
                        colors.Add(Color.DarkRed);
                    }
                }
            }

            if( colors != null)
            {
                Texture.SetData<Color>(colors.ToArray());
                collisionTexture.Add(Texture);
            }

        }
    }


    public override void Render(SpriteBatch spriteBatch)
    {
        base.Render(spriteBatch);
        if(isDebug)
        {
            if (collisionTexture != null)
            {
                for (int i = 0; i < collisionTexture.Count; i++)
                {
                    spriteBatch.Draw(collisionTexture[i], collisions[i], Color.White);
                }
            }
        }
    }
}
