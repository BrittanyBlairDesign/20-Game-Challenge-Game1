
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace FlappyBird;

enum Scenes
{
    START,
    GAME,
    LOSE    
}
public class FlappyBirdGame : MainGame
{
    protected const string PATH = "save.json";
    protected SaveFile save;
    public FlappyBirdGame(int width, int height, BaseGameState firstState): base(width, height, firstState)
    {
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {


        save = Load();
        FlappyBirdStartSplash fs = _firstGameState as FlappyBirdStartSplash;
        fs.scoreText = "High Score : " + save.HighScore.ToString();

        _firstGameState = fs;
        base.LoadContent();
    }
    protected override void _currentGameState_OnEventNotification(object sender, BaseGameStateEvent e)
    {
        FlappyBirdGameplayState gs = _currentGameState as FlappyBirdGameplayState;

        switch (e)
        {
            case BaseGameStateEvent.GameStart:
                SwitchGameState(new FlappyBirdGameplayState());
                break;
            case BaseGameStateEvent.GameQuit:
                if(_currentGameState.GetType() == typeof(GameplayState))
                { save.SetScores(gs.score); }
                Save(save);
                Exit();
                break;
            case BaseGameStateEvent.GamePause:
                isPaused = !isPaused;
                break;
            case GameplayEvents.PlayerLoose:
                save.SetScores(gs.score);
                Save(save);
                isPaused = true;
                SwitchGameState(new FlappyBirdLooseSplash(save.HighScore.ToString(),gs.score.ToString()));
                break;
            case GameplayEvents.PlayerPoints:
                gs.AddScorePoints();
                break;
        }
    }

    protected override void Save(SaveFile save)
    {
        string serializedText = JsonSerializer.Serialize<SaveFile>(save);
        Trace.WriteLine(serializedText);
        File.WriteAllText(PATH, serializedText);
    }

    protected override SaveFile Load()
    {
        var FileContents = File.ReadAllText(PATH);
        return JsonSerializer.Deserialize<SaveFile>(FileContents);
    }
}
