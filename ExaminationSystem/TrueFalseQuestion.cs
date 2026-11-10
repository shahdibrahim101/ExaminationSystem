using System;
using System.Collections.Generic;
using System.Text;

namespace ExaminationSystem
{
   
        class TrueFalseQuestion : Question
        {
            public bool CorrectAnswer { get; set; }

            public TrueFalseQuestion(
                string header,
                int marks,
                QuestionLevel level,
                bool correctAnswer)
                : base(header, marks, level)
            {
                CorrectAnswer = correctAnswer;
            }

            public override void Display()
            {
                Console.WriteLine(
                    Header + " (True/False) - Marks: " + Marks
                );
            }

            public override bool CheckAnswer(string answer)
            {
                bool studentAnswer;

                if (bool.TryParse(answer, out studentAnswer))
                {
                    return studentAnswer == CorrectAnswer;
                }

                return false;
            }
        }
    }
