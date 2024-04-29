

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Transactions;



public class FlappyObstacle : PlayerSprite
{
    public List<Vector2> TopCollisions = new List<Vector2>();
    protected int offset;   
    public FlappyObstacle(Texture2D texture, Vector2 StartPosition, float speed, Vector2 direction) : base(texture)
    {
        Position = StartPosition;
        this.speed = speed;
        this.direction = direction;

    }

    public FlappyObstacle(GraphicsDevice graphics, Texture2D texture, Vector2 StartPosition, float speed, Vector2 direction, int offset) : base( graphics, texture, StartPosition)
    {
        Position = StartPosition;
        this.speed = speed;
        this.direction = direction;
    }


    public void GenerateCollision()
    {
        collisions.Clear();

        Vector2 GoalCol = new Vector2(16, 37);

        List<Vector2>TopCol = new List<Vector2>();
        TopCol.Add(new Vector2(74, 966));
        TopCol.Add(new Vector2(151, 907));
        TopCol.Add(new Vector2(203, 617));
        TopCol.Add(new Vector2(265, 243));
        List<Vector2> BotCol = new List<Vector2>();
        BotCol.Add(new Vector2(30, 891));
        BotCol.Add(new Vector2(81, 822));
        BotCol.Add(new Vector2(121,627));
        BotCol.Add(new Vector2(196,366));

        int startPx = (int)Position.X;
        int startpy = (int)Position.Y;

        int GoalX = startPx + (Width - (int)GoalCol.X * 4) / 2;
        int GoalY = startpy + (Height - (int)GoalCol.Y * 4) / 2;
        int GoalW = (int)GoalCol.X * 4;
        int GoalH = (int)GoalCol.Y * 4;

        collisions.Add(new Rectangle(GoalX, GoalY, GoalW, GoalH));
        int i = 0;
        foreach (Vector2 r in TopCol)
        {
            int W = (int)r.X;
            int H = (int)r.Y;

            int x = startPx + (Width - W) / 2;
            int y = startpy - (Height / 2 - H) / 2;
            
            
            collisions.Add(new Rectangle(x, y, W, H));

        }

        foreach (Vector2 r in BotCol)
        {
            int W = (int)r.X;
            int H = (int)r.Y;

            int x = startPx + (Width - W) / 2;
            int y = startpy + (((Height - H) / 2) + (Height/2 - H/2))- 50 ;


            collisions.Add(new Rectangle(x, y, W, H));
            i++;

        }
   

    }

    public void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Vector2 velocity = speed * direction;
        Position += velocity * deltaTime;

        if (collisions != null)
        {
            for (int i = 0; i < collisions.Count; i++)
            {
                Rectangle rect = collisions[i];
                rect.X = (int)Position.X + (Width - rect.Width) / 2;         
                collisions[i] = rect;
            }
        }
    }

    public virtual Event CollisionCheck(PlayerSprite other)
    {
        if (other.collisions != null && collisions != null)
        {
            foreach (Rectangle r in collisions)
            {
                foreach (Rectangle pr in other.collisions)
                {

                    if (r.Intersects(pr))
                    {
                        if (r != collisions[0])
                        {
                            return Event.kLOOSE;
                        }
                        else
                        {
                            return Event.kPOINTS;
                        }
                    }
                }
            }
        }
        return Event.kNONE;
    }

    public float speed;
    Vector2 direction;
}
