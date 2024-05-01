
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FlappyBird;

class FlappyBirdLooseSplash : SplashState
{
    protected TextObject Title;
    protected string titleTexture;
    protected string titleFont;


    protected string startTexture;
    protected ButtonObject start;

    protected string endTexture;
    protected ButtonObject end;

    protected string highScoreText;
    protected string roundScoreText;
    protected string scoreFont;
    protected string scoreTexture;
    protected TextObject highScore;
    protected TextObject roundScore;

    public FlappyBirdLooseSplash(string highScore, string roundScore)
    {
        highScoreText = "High Score : " + highScore;
        roundScoreText = "End Score : " + roundScore;
    }

    public override void Initialize(ContentManager contentManager, int viewportWidth, int viewportHeight)
    {
        titleTexture = "FlappyBird/UI/EndTitle";
        startTexture = "FlappyBird/UI/RetryButton";
        endTexture = "FlappyBird/UI/ExitButton";
        scoreTexture = "FlappyBird/UI/scoreFrame";

        titleFont = "FlappyBird/Font/ScoreFont";
        scoreFont = "FlappyBird/Font/ScoreFont";
        base.Initialize(contentManager, viewportWidth, viewportHeight);
    }

    public override void LoadContent()
    {
        Vector2 startpos = new Vector2(_viewportWidth / 2, _viewportHeight / 2 + 125);
        Vector2 endpos = new Vector2(_viewportWidth / 2, _viewportHeight / 2 + 200);

        Vector2 highScorepos = new Vector2((_viewportWidth / 8) , _viewportHeight / 2 + 50);
        Vector2 roundScorepos = new Vector2((_viewportWidth / 2), _viewportHeight / 2 + 50);
        Title = new TextObject(LoadTexture(titleTexture), " ", _contentManager.Load<SpriteFont>(titleFont));
        highScore = new TextObject(LoadTexture(scoreTexture), highScoreText, _contentManager.Load<SpriteFont>(scoreFont), highScorepos);
        roundScore = new TextObject(LoadTexture(scoreTexture), roundScoreText, _contentManager.Load<SpriteFont>(scoreFont), roundScorepos);
        start = new ButtonObject(LoadTexture(startTexture), startpos);
        end = new ButtonObject(LoadTexture(endTexture), endpos);

        AddGameObject(new FlappyTerrain(LoadTexture("FlappyBird/Backgrounds/Background"), 0.0f, true));
        AddGameObject(Title);
        AddGameObject(highScore);
        AddGameObject(roundScore);
        AddGameObject(start);
        AddGameObject(end);

        var track1 = LoadSound("FlappyBird/Sound/8bit Bossa").CreateInstance();
        _soundManager.SetSoundTrack(new List<SoundEffectInstance>() { track1 });


    }

    public override void LoadContent(GraphicsDevice graphics)
    {
        this.graphics = graphics;
        this.isDebug = true;
        this.LoadContent();
    }

    public override void HandleInput(GameTime gameTime)
    {
        _inputManager.GetCommands(cmd =>
        {
            if(cmd is SplashInputCommand.GameSelect)
            {
                if(start.isHovering() )
                {
                    NotifyEvent(new BaseGameStateEvent.GameStart());
                }
                else if ( end.isHovering())
                {
                    NotifyEvent(new BaseGameStateEvent.GameQuit());
                }
                
            }

            if(cmd is SplashInputCommand.GameExit)
            {
                NotifyEvent(new BaseGameStateEvent.GameQuit());
            }
        });
    }
}