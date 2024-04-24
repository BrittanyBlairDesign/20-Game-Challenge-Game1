using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


public abstract class BaseGameState
{
    protected readonly List<BaseGameObject> _gameObjects = new List<BaseGameObject>();

    private const string FallbackTexture = "SplashScreens/Empty";

    private ContentManager _contentManager;

    // Loading and unloading content
    public abstract void LoadContent();
    public void UnloadContent()
    {
        _contentManager.Unload();
    }

    public void Initialize(ContentManager contentManager)
    {
        _contentManager = contentManager;
    }

    protected Texture2D LoadTexture(string textureName)
    {
        var texture = _contentManager.Load<Texture2D>(textureName);
        return texture ?? _contentManager.Load<Texture2D>(FallbackTexture);
    }

    // Input
    public abstract void HandleInput();

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

    public void Render(SpriteBatch spriteBatch)
    {
        foreach (var gameObject in _gameObjects.OrderBy(a => a.zIndex))
        {
            gameObject.Render(spriteBatch);
        }
    }

}
