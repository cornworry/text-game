using System;

namespace TextGame.Flow
{
    public interface ITakeInput : IDetail
    {
        string Prompt { get; }
        bool IsValid(string input);
    }

    public static class ITakeInputExtensions
    {
        public static string GetInput(this ITakeInput inputDevice)
        {
            string input = null;
            while(input == null || !inputDevice.IsValid(input)) {
                input = TextGame.GetInput(inputDevice.Prompt);
            }
            return input;
        }
    }
}