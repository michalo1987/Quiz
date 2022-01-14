using System;
using System.Collections.Generic;
using System.IO;

namespace Quiz
{
    public class Quiz
    {
        public List<Question> Questions { get; set; }

        public Player Player { get; set; }

        public Quiz()
        {
            LoadQuestionsFromFile("Questions.txt");
        }

        private void LoadQuestionsFromFile(string filePath)
        {
            var lines = File.ReadLines(filePath);

            var counter = 0;

            Questions = new List<Question>();

            var currentQuestion = new Question();

            foreach (var line in lines)
            {
                if (counter == 6)
                {
                    counter = 0;
                }
                if (counter == 0)
                {
                    currentQuestion.Title = line;
                }
                if (counter == 1)
                {
                    currentQuestion.AnswerA = line;
                }
                if (counter == 2)
                {
                    currentQuestion.AnswerB = line;
                }
                if (counter == 3)
                {
                    currentQuestion.AnswerC = line;
                }
                if (counter == 4)
                {
                    currentQuestion.AnswerD = line;
                }
                if (counter == 5)
                {
                    currentQuestion.RightAnswerLetter = line[0].ToString();
                    currentQuestion.Score = int.Parse(line[1].ToString());

                    var newQuestion = new Question
                    {
                        Title = currentQuestion.Title,
                        AnswerA = currentQuestion.AnswerA,
                        AnswerB = currentQuestion.AnswerB,
                        AnswerC = currentQuestion.AnswerC,
                        AnswerD = currentQuestion.AnswerD,
                        RightAnswerLetter = currentQuestion.RightAnswerLetter,
                        Score = currentQuestion.Score
                    };

                    Questions.Add(newQuestion);
                }
               
                counter++;
            }
        }

        public void Start()
        {
            Player = new Player();

            Console.WriteLine("Tell me your name");

            Player.Name = Console.ReadLine();

            Player.Score = 0;

            Player.CurrentQuestion = 1;

            for (int i = 0; i < Questions.Count; i++)
            {
                var score = ShowQuestion(Player.CurrentQuestion);
                Player.Score += score;
                Player.CurrentQuestion++;
            }

            Console.WriteLine("Quiz is finished");
            Console.WriteLine($"{Player.Name} your score is: {Player.Score}");
        }

        public int ShowQuestion(int questionCunter)
        {
            var qurrentQuestionToShow = Questions[questionCunter - 1];

            Console.WriteLine($"Question: {qurrentQuestionToShow.Title}");
            Console.WriteLine($"A: {qurrentQuestionToShow.AnswerA}");
            Console.WriteLine($"B: {qurrentQuestionToShow.AnswerB}");
            Console.WriteLine($"C: {qurrentQuestionToShow.AnswerC}");
            Console.WriteLine($"D: {qurrentQuestionToShow.AnswerD}");

            var userResponse = Console.ReadLine().ToUpper();

            if(userResponse == qurrentQuestionToShow.RightAnswerLetter)
            {
                Console.WriteLine("Your answer was good");
                return qurrentQuestionToShow.Score;
            }

            Console.WriteLine("Your answer was wrong");

            return 0;
        }
    }
}
