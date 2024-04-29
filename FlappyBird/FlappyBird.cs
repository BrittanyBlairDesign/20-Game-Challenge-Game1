using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FlappyBird;

enum Scenes
{
    START,
    GAME,
    LOSE    
}
public class FlappyBird : Game
{
    private BaseGameState _currentGameState;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private const string PATH = "save.json";
    private SaveFile save;
    private bool isDebug = true;
    private bool isPaused = false;
//  Render Target
    private RenderTarget2D _renderTarget;
    private Rectangle _renderScaleRectangle;

//  Window Size
    private const int DESIGNED_RESOLUTION_WIDTH = 888;
    private const int DESIGNED_RESOLUTION_HEIGHT = 1016;
    private const float DESIGNED_RESOLUTION_ASPECT_RATIO = DESIGNED_RESOLUTION_WIDTH/ (float)DESIGNED_RESOLUTION_HEIGHT;
   
    public FlappyBird()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = DESIGNED_RESOLUTION_WIDTH;
        _graphics.PreferredBackBufferHeight = DESIGNED_RESOLUTION_HEIGHT;
        _graphics.IsFullScreen = false;
        _graphics.ApplyChanges();

        _renderTarget = new RenderTarget2D(_graphics.GraphicsDevice, DESIGNED_RESOLUTION_WIDTH, DESIGNED_RESOLUTION_HEIGHT, false,
                        SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

        _renderScaleRectangle = GetScaleRectangle();
        this.IsMouseVisible = true;

        base.Initialize();
    }

    private Rectangle GetScaleRectangle()
    {
        var variance = 0.5;
        var actualAspectRatio = Window.ClientBounds.Width / (float)Window.ClientBounds.Height;

        Rectangle scaleRectangle;

        if (actualAspectRatio <= DESIGNED_RESOLUTION_ASPECT_RATIO)
        {
            var presentHeight = (int)(Window.ClientBounds.Width / DESIGNED_RESOLUTION_ASPECT_RATIO + variance);
            var barHeight = (Window.ClientBounds.Height - presentHeight) / 2;

            scaleRectangle = new Rectangle(0, barHeight, Window.ClientBounds.Width, presentHeight);
        }
        else
        {
            var presentWidth = (int)(Window.ClientBounds.Height * DESIGNED_RESOLUTION_ASPECT_RATIO + variance);
            var barWidth = (Window.ClientBounds.Width - presentWidth) / 2;

            scaleRectangle = new Rectangle(barWidth, 0, presentWidth, Window.ClientBounds.Height);
        }

        return scaleRectangle;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        save = Load();
        SwitchGameState(new FlappyBirdGameplayState());
    }

    private void CurrentGameState_OnStateSwitched(object sender, BaseGameState e)
    {
        SwitchGameState(e);
    }

    private void SwitchGameState(BaseGameState gameState)
    {
        if (_currentGameState != null)
        {
            _currentGameState.OnStateSwitched -= CurrentGameState_OnStateSwitched;
            _currentGameState.OnEventNotification -= _currentGameState_OnEventNotification;
            _currentGameState.UnloadContent();
        }

        
        _currentGameState = gameState;

        _currentGameState.Initialize(Content, DESIGNED_RESOLUTION_WIDTH, DESIGNED_RESOLUTION_HEIGHT);

        if (isDebug)
        {
            _currentGameState.LoadContent(_graphics.GraphicsDevice);
        }
        else
        {
            _currentGameState.LoadContent();
        }
        _currentGameState.OnStateSwitched += CurrentGameState_OnStateSwitched;
        _currentGameState.OnEventNotification += _currentGameState_OnEventNotification;
    }

    private void _currentGameState_OnEventNotification(object sender, Event e)
    {
        FlappyBirdGameplayState gs = _currentGameState as FlappyBirdGameplayState;

        switch (e)
        {
            case Event.kSTART:
                SwitchGameState(new FlappyBirdGameplayState());
                break;
            case Event.kGAME_QUIT:
                save.SetScores(gs.score);
                Save(save);
                Exit();
                break;
            case Event.kPAUSE:
                isPaused = !isPaused;
                break;
            case Event.kLOOSE:
                save.SetScores(gs.score);
                Save(save);
                SwitchGameState(new FlappyBirdStartSplash(save.HighScore.ToString()));
                break;
        }
    }

    protected override void UnloadContent()
    {
        _currentGameState?.UnloadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        _currentGameState.HandleInput(gameTime);

        if(!isPaused)
        {
            _currentGameState.Update(gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
         // Render to the Render Target
        GraphicsDevice.SetRenderTarget(_renderTarget);

        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _currentGameState.Render(_spriteBatch);

        _spriteBatch.End();

        // Now render the scaled content
        _graphics.GraphicsDevice.SetRenderTarget(null);

        _graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0f, 0);

        _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);

        _spriteBatch.Draw(_renderTarget, _renderScaleRectangle, Color.White);

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    public void Quit()
    {
        this.Exit();
    }

    public void Save(SaveFile save)
    {
        string serializedText = JsonSerializer.Serialize<SaveFile>(save);
        Trace.WriteLine(serializedText);
        File.WriteAllText(PATH, serializedText);
    }

    public SaveFile Load()
    {
        var FileContents = File.ReadAllText(PATH);
        return JsonSerializer.Deserialize<SaveFile>(FileContents);
    }
}
