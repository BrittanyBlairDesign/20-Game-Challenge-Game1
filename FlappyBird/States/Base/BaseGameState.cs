using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


public abstract class BaseGameState
{
    protected readonly List<BaseGameObject> _gameObjects = new List<BaseGameObject>();

    private const string FallbackTexture = "SplashScreens/Empty";

    protected ContentManager _contentManager;
    protected int _viewportHeight;
    protected int _viewportWidth;


    protected InputManager _inputManager {  get; set; }

    // Loading and unloading content
    public abstract void LoadContent();
    public void UnloadContent()
    {
        _contentManager.Unload();
    }

    public virtual void Initialize(ContentManager contentManager, int viewportWidth, int viewportHeight)
    {
        _contentManager = contentManager;
        _viewportHeight = viewportHeight;
        _viewportWidth = viewportWidth;

        SetInputManager();
    }

    protected Texture2D LoadTexture(string textureName)
    {
        var texture = _contentManager.Load<Texture2D>(textureName);
        return texture ?? _contentManager.Load<Texture2D>(FallbackTexture);
    }

    // Input
    protected abstract void SetInputManager();
    public abstract void HandleInput(GameTime gameTime);
    public virtual void Update(GameTime gameTime) { }
            
    // Events
    public event EventHandler<Event> OnEventNotification;
    protected void NotifyEvent(Event eventType, object argument = null)
    {
        OnEventNotification?.Invoke(this, eventType);

        foreach(var gameObject in _gameObjects)
        {
            gameObject.OnNotify(eventType);
        }
    }

    public event EventHandler<BaseGameState> OnStateSwitched;
    protected void SwitchState(BaseGameState gameState)
    {
        OnStateSwitched?.Invoke(this, gameState);
    }

    // Objects
    protected void AddGameObject(BaseGameObject gameObject)
    {
        _gameObjects.Add(gameObject);
    }

    protected void RemoveGameObject(BaseGameObject gameObject)
    {
        _gameObjects.Remove(gameObject);
    }

    public void Render(SpriteBatch spriteBatch)
    {
        foreach (var gameObject in _gameObjects.OrderBy(a => a.zIndex))
        {
            gameObject.Render(spriteBatch);
        }
    }

}
