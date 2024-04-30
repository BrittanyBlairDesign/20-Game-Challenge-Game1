
using FlappyBird;
using System;


public static class Program
{
    private const int WIDTH = 888;
    private const int HEIGHT = 1016;

    [STAThread]
    static void Main()
    {
        using (var game = new FlappyBirdGame(WIDTH, HEIGHT, new FlappyBirdStartSplash()))
            game.Run();
    }
}