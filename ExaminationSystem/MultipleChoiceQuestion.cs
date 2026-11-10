using System;
using System.Collections.Generic;
using System.Text;

namespace ExaminationSystem
{
   
        class MultipleChoiceQuestion : Question
        {
            public string[] Choices { get; set; }
            public List<int> CorrectAnswers { get; set; }

            public MultipleChoiceQuestion(
                string header,
                int marks,
                QuestionLevel level,
                string[] choices,
                List<int> correctAnswers)
                : base(header, marks, level)
            {
                Choices = choices;
                CorrectAnswers = correctAnswers;
            }

            public override void Display()
            {
                Console.WriteLine(
                    Header + " (Multiple Choice) - Marks: " + Marks
                );

                for (int i = 0; i < Choices.Length; i++)
                {
                    Console.WriteLine((i + 1) + "- " + Choices[i]);
                }
            }

            public override bool CheckAnswer(string answer)
            {
                string[] parts = answer.Split(',');

                List<int> studentAnswers = new List<int>();

                foreach (string part in parts)
                {
                    int number;

                    if (!int.TryParse(part.Trim(), out number))
                    {
                        return false;
                    }

                    if (number < 1 || number > 4)
                    {
                        return false;
                    }

                    if (studentAnswers.Contains(number))
                    {
                        return false;
                    }

                    studentAnswers.Add(number);
                }

                if (studentAnswers.Count != CorrectAnswers.Count)
                {
                    return false;
                }

                foreach (int correct in CorrectAnswers)
                {
                    if (!studentAnswers.Contains(correct))
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
