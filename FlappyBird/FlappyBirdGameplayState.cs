
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
namespace FlappyBird;



class FlappyBirdGameplayState : GameplayState
{
    protected string ForegroundTexture;
    protected string ObstacleTexture = "FlappyBird/Sprites/Obstacle_Scaled";

    private float spawnTimer;
    private Random r = new Random();

    private float obstacleSpeed = 100.5f;
    private Vector2 obstaclePos;
    private Vector2 obstacleDirection;

    public double score = 0;
    private TextObject scoreText;
    private String scoreTexture = "FlappyBird/UI/ScoreFrame";
    private string scoreFont = "FlappyBird/Font/ScoreFont";

    public TextObject instructions;
    private string instructionText = " Use 'W' , the Up arrow, or Left Mouse click to jump.";
    private bool knowsMovement = false;

    protected List<FlappyObstacle> _Obstacles = new List<FlappyObstacle>();
    public override void Initialize(ContentManager contentManager, int viewportHeight, int viewportWidth)
    {
        Player = "FlappyBird/Sprites/Player";
        BackgroundTexture = "FlappyBird/Backgrounds/Background";
        ForegroundTexture = "FlappyBird/Backgrounds/Foreground";
        spawnTimer = 0.0f;
        base.Initialize(contentManager, viewportHeight, viewportWidth);
    }
    public override void LoadContent()
    {
        if(isDebug && graphics != null)
        {
            _playerSprite = new FlappyPlayer(graphics,LoadTexture(Player));
        }
        else
        {
            _playerSprite = new FlappyPlayer(LoadTexture(Player));
        }
 
        AddGameObject(new FlappyTerrain(LoadTexture(BackgroundTexture),-1.0f, true));
        AddGameObject(_playerSprite);
        AddGameObject(new FlappyTerrain(LoadTexture(ForegroundTexture), -2.0f, true));

        scoreText = new TextObject(LoadTexture(scoreTexture), "Score : ",_contentManager.Load<SpriteFont>(scoreFont));
        instructions = new TextObject(LoadTexture("FlappyBird/empty"), instructionText, _contentManager.Load<SpriteFont>(scoreFont), new Vector2(10, _viewportHeight - 100));
        var playerXPos = _viewportWidth / 2 - _playerSprite.Width / 2;
        var playerYPos = _viewportHeight /2 - _playerSprite.Height - 30;

        obstacleDirection = new Vector2(-1, 0);

        _playerSprite.Position = new Vector2(playerXPos, playerYPos);


        // Audio
        var jumpSound = LoadSound("FlappyBird/Sound/melee sound");
        _soundManager.RegisterSound(new GameplayEvents.PlayerJumps(), jumpSound);

        var track1 = LoadSound("FlappyBird/Sound/wind1").CreateInstance();
        _soundManager.SetSoundTrack(new List<SoundEffectInstance>() { track1 });
 
    }

    public override void LoadContent(GraphicsDevice graphics)
    {
        this.graphics = graphics;
        isDebug = true;

        this.LoadContent();
    }

    public override void HandleInput(GameTime gameTime)
    {
        _inputManager.GetCommands(cmd =>
        {
            if (cmd is GameplayInputCommand.GameExit)
            {
                NotifyEvent(new BaseGameStateEvent.GameQuit());
            }

            if (cmd is GameplayInputCommand.PlayerJump)
            {
                FlappyPlayer aPlayer = _playerSprite as FlappyPlayer;
                
                if (!aPlayer.isJumping)
                { 
                    aPlayer.Jump(); 
                    NotifyEvent(new GameplayEvents.PlayerJumps());
                }
                knowsMovement = true;
            }
            else
            {
                FlappyPlayer aPlayer = _playerSprite as FlappyPlayer;
                aPlayer.isJumping = false;
            }

        });
    }

    public override void UpdateGameState(GameTime gameTime)
    {

        float DeltaTime = (float)gameTime.ElapsedGameTime.Seconds;
        FlappyPlayer p = _playerSprite as FlappyPlayer;
        p.Update(gameTime);
        KeepPlayerInBounds();

        if(spawnTimer > 0)
        {
            spawnTimer -= .01f;
        }
        else
        {

            FlappyObstacle Obstacle;
            int offset = (r.Next(0, 400) - r.Next(0, 400));
            if (isDebug)
            {
                Obstacle = new FlappyObstacle(graphics, LoadTexture(ObstacleTexture), obstaclePos, obstacleSpeed, obstacleDirection, offset);
            }
            else
            {
                Obstacle = new FlappyObstacle(LoadTexture(ObstacleTexture), obstaclePos, obstacleSpeed, obstacleDirection);
            }

            obstaclePos = new Vector2(_viewportWidth + 500 - Obstacle.Width/2, (_viewportHeight / 2 - Obstacle.Height/2) + offset);
            Obstacle.Position = obstaclePos;
            Obstacle.GenerateCollision();
            if(isDebug)
            {
                Obstacle.SetCollisionTexture(graphics);
            }
            _Obstacles.Add(Obstacle);
            InsertGameObject(Obstacle);

            spawnTimer = r.Next(2, 5);
        }


        if (_Obstacles.Count >= 1)
        {
            obstacleSpeed += .007f;

            foreach (FlappyObstacle o in _Obstacles)
            {
                BaseGameStateEvent e = o.CollisionCheck(p);
                if (e != null)
                {
                    NotifyEvent(e);
                }

                o.speed = obstacleSpeed;
                o.Update(gameTime);
                if (o.Position.X + 50 < -800)
                {
                    RemoveGameObject(o);
                }
            }
        }
    }

    public void AddScorePoints()
    {
        score += 100; 
        scoreText.setText("Score : " +  score.ToString());
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        base.Render(spriteBatch);
        scoreText.Render(spriteBatch);

        if(!knowsMovement)
        {
            instructions.Render(spriteBatch);
        }
     
    }

    public virtual void InsertGameObject(BaseGameObject gameObject)
    {
        int index = _gameObjects.Count -1;
        _gameObjects.Insert(index, gameObject);
    }

}

    