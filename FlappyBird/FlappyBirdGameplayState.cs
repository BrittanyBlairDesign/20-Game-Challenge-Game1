

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;


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
        _playerSprite = new FlappyPlayer(LoadTexture(Player));
        AddGameObject(new FlappyTerrain(LoadTexture(BackgroundTexture),-1.0f, true));
        AddGameObject(_playerSprite);
        AddGameObject(new FlappyTerrain(LoadTexture(ForegroundTexture), -2.0f, true));

        var playerXPos = _viewportWidth / 2 - _playerSprite.Width / 2;
        var playerYPos = _viewportHeight /2 - _playerSprite.Height - 30;

        obstacleDirection = new Vector2(-1, 0);


        _playerSprite.Position = new Vector2(playerXPos, playerYPos);
    }
    public override void HandleInput(GameTime gameTime)
    {
        _inputManager.GetCommands(cmd =>
        {
            if (cmd is GameplayInputCommand.GameExit)
            {
                NotifyEvent(Event.kGAME_QUIT);
            }

            if (cmd is GameplayInputCommand.PlayerJump)
            {
                FlappyPlayer aPlayer = _playerSprite as FlappyPlayer;
                aPlayer.Jump();  
            }
        });
    }

    public override void Update(GameTime gameTime)
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
            
            FlappyObstacle Obstacle = new FlappyObstacle(LoadTexture(ObstacleTexture), obstaclePos, obstacleSpeed, obstacleDirection);
            obstaclePos = new Vector2(_viewportWidth + 500 - Obstacle.Width/2, (_viewportHeight / 2 - Obstacle.Height/2) + (r.Next(0 , 400) - r.Next(0, 400)));
            Obstacle.Position = obstaclePos;
            _Obstacles.Add(Obstacle);
            InsertGameObject(Obstacle);

            spawnTimer = r.Next(2, 5);
        }


        if (_Obstacles.Count >= 1)
        {
            obstacleSpeed += .007f;

            foreach (FlappyObstacle o in _Obstacles)
            {
                o.speed = obstacleSpeed;
                o.Update(gameTime);
                if (o.Position.X + 50 < -800)
                {
                    RemoveGameObject(o);
                }
            }
        }

        base.Update(gameTime);
    }

    public virtual void InsertGameObject(BaseGameObject gameObject)
    {
        int index = _gameObjects.Count -1;
        _gameObjects.Insert(index, gameObject);
    }

}

    