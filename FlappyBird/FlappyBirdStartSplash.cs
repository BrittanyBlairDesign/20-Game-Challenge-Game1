
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class FlappyBirdStartSplash : SplashState
{
    protected TextObject Title;
    protected string titleTexture;
    protected string titleFont;


    protected string startTexture;
    protected ButtonObject start;

    protected string endTexture;
    protected ButtonObject end;

    protected string scoreText;
    protected string scoreFont;
    protected string scoreTexture;
    protected TextObject highScore;

    public FlappyBirdStartSplash(string highScore)
    {
        scoreText = highScore;
    }

    public override void Initialize(ContentManager contentManager, int viewportWidth, int viewportHeight)
    {
        titleTexture = "FlappyBird/UI/Title";
        startTexture = "FlappyBird/UI/scoreFrame";
        endTexture = "FlappyBird/UI/scoreFrame";
        scoreTexture = "FlappyBird/UI/scoreFrame";
        

        titleFont = "FlappyBird/Font/ScoreFont";
        scoreFont = "FlappyBird/Font/ScoreFont";
        base.Initialize(contentManager, viewportWidth, viewportHeight);
    }


    public override void LoadContent()
    {
        Vector2 startpos = new Vector2(_viewportWidth / 2, _viewportHeight / 2 + 50);
        Vector2 endpos = new Vector2(_viewportWidth / 2, _viewportHeight / 2 + 100);
        
        Title = new TextObject(LoadTexture(titleTexture), "Flappy Duck Migration", _contentManager.Load<SpriteFont>(titleFont));
        highScore = new TextObject(LoadTexture(scoreTexture), scoreText, _contentManager.Load<SpriteFont>(scoreFont));
        start = new ButtonObject(LoadTexture(startTexture), startpos);
        end = new ButtonObject(LoadTexture(endTexture), endpos);

        AddGameObject(new FlappyTerrain(LoadTexture("FlappyBird/Backgorunds/Background"), -1.0f, true));
        AddGameObject(Title);
        AddGameObject(highScore);
        AddGameObject(start);
        AddGameObject(end);
    }

    public override void HandleInput(GameTime gameTime)
    {
        _inputManager.GetCommands(cmd =>
        {
            if(cmd is SplashInputCommand.GameSelect)
            {
                if(start.isHovering() )
                {
                    NotifyEvent(Event.kSTART);
                }
                else if ( end.isHovering())
                {
                    NotifyEvent(Event.kGAME_QUIT);
                }
                
            }

            if(cmd is SplashInputCommand.GameExit)
            {
                NotifyEvent(Event.kGAME_QUIT);
            }
        });
    }
}