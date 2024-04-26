

using Microsoft.Xna.Framework.Graphics;

public class FlappyTerrain : Terrain
{
    public FlappyTerrain(Texture2D texture2D, float speed = 2.0f, bool horizontal = true): base(texture2D)
    {
        SCROLLING_SPEED = speed;
        isSCROLL_HORIZONTAL = horizontal;
    }
}

