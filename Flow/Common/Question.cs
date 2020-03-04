using System;

namespace TextGame.Flow.Common
{
    public class Question : ITakeInput
    {
        public string Prompt { get; protected set; }
        public string Message { get; protected set; }

        protected Func<string, bool> Validate { get; set;}
        public bool IsValid (string input) {
            return Validate?.Invoke(input) ?? true;
        }

        protected Question() { }

        public static Question With(string prompt, string message = null, Func<string, bool> validate = null) {
            return new Question
            { 
                Prompt       = prompt, 
                Message      = message,
                Validate     = validate,
            };
        }

        public static QuestionWithHint With(string prompt, string hint, string message = null,  Func<string, bool> validate = null) {
            return new QuestionWithHint(hint)
            { 
                Prompt       = prompt, 
                Message      = message,
                Validate     = validate,
            };
        }
    }

    public class QuestionWithHint : Question, IHaveAHint 
    {
        internal QuestionWithHint(string hint) { Hint = hint; }
        public string Hint { get; }
    }
}