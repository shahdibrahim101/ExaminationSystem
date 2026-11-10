namespace ExaminationSystem
{
    internal class Program
    {
            // Question Bank
            static List<Question> questions = new List<Question>();

            // Read Integer
            static int ReadInt(string message, int min, int max)
            {
                int number;

                while (true)
                {
                    Console.Write(message);

                    if (int.TryParse(Console.ReadLine(), out number)
                        && number >= min && number <= max)
                    {
                        return number;
                    }

                    Console.WriteLine("Invalid input. Try again.");
                }
            }

            // Read Level
            static QuestionLevel ReadLevel(string message)
            {
                while (true)
                {
                    Console.Write(message);

                    string input = Console.ReadLine();

                    QuestionLevel level;

                    if (Enum.TryParse(input, true, out level)
                        && Enum.IsDefined(typeof(QuestionLevel), level))
                    {
                        return level;
                    }

                    Console.WriteLine("Invalid level. Enter easy, medium or hard.");
                }
            }

            // Read True or False
            static bool ReadBoolean()
            {
                while (true)
                {
                    Console.Write("Enter Correct Answer (true/false): ");

                    string input = Console.ReadLine();

                    bool answer;

                    if (bool.TryParse(input, out answer))
                    {
                        return answer;
                    }

                    Console.WriteLine("Please enter true or false.");
                }
            }

            // Read Four Choices
            static string[] ReadChoices()
            {
                string[] choices = new string[4];

                for (int i = 0; i < 4; i++)
                {
                    while (true)
                    {
                        Console.Write("Enter choice " + (i + 1) + ": ");

                        string choice = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(choice))
                        {
                            choices[i] = choice;
                            break;
                        }

                        Console.WriteLine("Choice cannot be empty.");
                    }
                }

                return choices;
            }

            // Read Correct Answers for Multiple Choice
            static List<int> ReadCorrectAnswers()
            {
                while (true)
                {
                    Console.Write(
                        "Enter correct answers (comma separated, e.g. 1,2): "
                    );

                    string input = Console.ReadLine();

                    string[] parts = input.Split(',');

                    List<int> answers = new List<int>();

                    bool valid = true;

                    foreach (string part in parts)
                    {
                        int number;

                        if (!int.TryParse(part.Trim(), out number)
                            || number < 1 || number > 4
                            || answers.Contains(number))
                        {
                            valid = false;
                            break;
                        }

                        answers.Add(number);
                    }

                    if (valid && answers.Count > 0)
                    {
                        return answers;
                    }

                    Console.WriteLine("Invalid answers. Try again.");
                }
            }

            // Doctor Mode
            static void DoctorMode()
            {
                Console.WriteLine("\n===== Doctor Mode =====");

                int count = ReadInt(
                    "How many questions do you want to add? ", 1, 100
                );

                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine("\nAdding Question " + (i + 1) + ":");

                    Console.WriteLine(
                        "Select Question Type: 1- True/False, 2- Choose One, 3- Multiple Choice"
                    );

                    int type = ReadInt("", 1, 3);

                    QuestionLevel level = ReadLevel(
                        "Enter Question Level (easy/medium/hard): "
                    );

                    Console.Write("Enter Question Header: ");

                    string header = Console.ReadLine();

                    int marks = ReadInt("Enter Question Marks: ", 1, 100);

                    if (type == 1)
                    {
                        bool correctAnswer = ReadBoolean();

                        questions.Add(
                            new TrueFalseQuestion(
                                header, marks, level, correctAnswer
                            )
                        );
                    }
                    else if (type == 2)
                    {
                        string[] choices = ReadChoices();

                        int correctChoice = ReadInt(
                            "Enter correct choice number (1-4): ", 1, 4
                        );

                        questions.Add(
                            new ChooseOneQuestion(
                                header, marks, level,
                                choices, correctChoice
                            )
                        );
                    }
                    else if (type == 3)
                    {
                        string[] choices = ReadChoices();

                        List<int> correctAnswers = ReadCorrectAnswers();

                        questions.Add(
                            new MultipleChoiceQuestion(
                                header, marks, level,
                                choices, correctAnswers
                            )
                        );
                    }

                    Console.WriteLine("Question Added Successfully!");
                }

                Console.WriteLine("\nQuestion Bank Created Successfully!");
            }

            // Student Mode
            static void StudentMode()
            {
                Console.WriteLine("\n===== Student Mode =====");

                if (questions.Count == 0)
                {
                    Console.WriteLine("No questions available!");
                    return;
                }

                Console.WriteLine(
                    "Choose Exam Type: 1- Practical Exam, 2- Final Exam"
                );

                int examType = ReadInt("", 1, 2);

                QuestionLevel level = ReadLevel(
                    "Enter Exam Level (easy/medium/hard): "
                );

                // Get Questions According to Level
                List<Question> examQuestions = new List<Question>();

                foreach (Question q in questions)
                {
                    if (q.Level == level)
                    {
                        examQuestions.Add(q);
                    }
                }

                if (examQuestions.Count == 0)
                {
                    Console.WriteLine("No questions found for this level!");
                    return;
                }

                // Practical Exam = Half Questions
                if (examType == 1)
                {
                    int half = examQuestions.Count / 2;

                    if (half == 0)
                    {
                        Console.WriteLine(
                            "Not enough questions for Practical Exam!"
                        );
                        return;
                    }

                    examQuestions = examQuestions.GetRange(0, half);
                }

                int totalMarks = 0;
                int studentMarks = 0;

                Console.WriteLine("\n--- Exam Started ---");

                // Display Questions and Check Answers
                foreach (Question q in examQuestions)
                {
                    Console.WriteLine();

                    q.Display();

                    Console.Write("Your Answer: ");

                    string answer = Console.ReadLine();

                    totalMarks += q.Marks;

                    if (q.CheckAnswer(answer))
                    {
                        studentMarks += q.Marks;
                    }
                }

                // Show Result
                Console.WriteLine("\nYour Result: " +
                    studentMarks + " / " + totalMarks);
            }

            // Main Menu
            static void Main(string[] args)
            {
                while (true)
                {
                    Console.WriteLine("\nMain Menu:");
                    Console.WriteLine("1- Doctor Mode");
                    Console.WriteLine("2- Student Mode");
                    Console.WriteLine("3- Exit");

                    int choice = ReadInt("Choose: ", 1, 3);

                    switch (choice)
                    {
                        case 1:
                            DoctorMode();
                            break;

                        case 2:
                            StudentMode();
                            break;

                        case 3:
                            Console.WriteLine("Goodbye!");
                            return;
                    }
                }
            }
        }
    }

