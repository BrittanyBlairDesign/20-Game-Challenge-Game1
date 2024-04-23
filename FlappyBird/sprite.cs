
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace FlappyBird
{
    internal class Sprite
    {
        public Texture2D texture;
        public Vector2 position;
        public Color color;
        public Sprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            this.color = Color.White;
        }
        public Sprite(Texture2D texture, Vector2 position, Color color)
        {
            this.texture = texture;
            this.position = position;
            this.color = color;
        }

        public virtual void Update()
        {

        }

        public virtual void Draw(GameTime gametime)
        {

        }
    }

    internal class ScaledSprite : Sprite
    {
        
        public Rectangle Rect
        {
            get { return new Rectangle((int)position.X, (int)position.Y, 100, 200); }
        }

        public ScaledSprite(Texture2D texture2D, Vector2 position, Color color) : base(texture2D, position, color)
        {

        }

    }
}