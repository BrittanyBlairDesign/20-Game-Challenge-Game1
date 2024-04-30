
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

        save = new SaveFile() { HighScore = 0, score = 0 };
        Save(save);
        save = Load();

        FlappyBirdStartSplash fs = _firstGameState as FlappyBirdStartSplash;
        fs.scoreText = "High Score : " + save.HighScore.ToString();

        _firstGameState = fs;
        base.LoadContent();
    }
    protected override void _currentGameState_OnEventNotification(object sender, Event e)
    {
        FlappyBirdGameplayState gs = _currentGameState as FlappyBirdGameplayState;

        switch (e)
        {
            case Event.kSTART:
                SwitchGameState(new FlappyBirdGameplayState());
                break;
            case Event.kGAME_QUIT:
                if(_currentGameState.GetType() == typeof(GameplayState))
                { save.SetScores(gs.score); }
                Save(save);
                Exit();
                break;
            case Event.kPAUSE:
                isPaused = !isPaused;
                break;
            case Event.kLOOSE:
                save.SetScores(gs.score);
                Save(save);
                SwitchGameState(new FlappyBirdLooseSplash(save.HighScore.ToString(),gs.score.ToString()));
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
