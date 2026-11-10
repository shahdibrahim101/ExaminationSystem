using System;
using System.Collections.Generic;
using System.Text;

namespace ExaminationSystem
{
    class ChooseOneQuestion : Question
        {
            public string[] Choices { get; set; }
            public int CorrectChoice { get; set; }

            public ChooseOneQuestion(
                string header,
                int marks,
                QuestionLevel level,
                string[] choices,
                int correctChoice)
                : base(header, marks, level)
            {
                Choices = choices;
                CorrectChoice = correctChoice;
            }

            public override void Display()
            {
                Console.WriteLine(
                    Header + " (Choose One) - Marks: " + Marks
                );

                for (int i = 0; i < Choices.Length; i++)
                {
                    Console.WriteLine((i + 1) + "- " + Choices[i]);
                }
            }

            public override bool CheckAnswer(string answer)
            {
                int studentAnswer;

                if (int.TryParse(answer, out studentAnswer))
                {
                    return studentAnswer == CorrectChoice;
                }

                return false;
            }
        }
    }

