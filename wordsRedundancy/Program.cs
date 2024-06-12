namespace wordsRedundancy
{
    enum ReturnCodes
    {
        SUCCESS,
        INVALID_INPUT
    }
    internal class Program
    {
        static int Main()
        {
            static bool checkParagraphValidity(string paragraph)
            {
                if (string.IsNullOrEmpty(paragraph) || paragraph.Length >= 1000)
                {
                    return false;
                }
                return true;
            }

            static bool checkBannedWords(List<string> words)
            {
                if (words.Count() > 100)
                {
                    return false;
                }

                foreach (string word in words)
                {
                    if (word.Length > 10)
                    {
                        return false;
                    }
                }
                return true;
            }

            static List<string> removeBannedWords(List<string> list, List<string> bannedWords)
            {
                List<string> wordsList = new();

                for (int i = 0; i < list.Count(); i++)
                {
                    if (!bannedWords.Contains(list[i]))
                    {
                        wordsList.Add(list[i]);
                    }
                }

                return wordsList;
            }

            static List<string> parseParagraph(string par, List<string> bannedWords)
            {
                par = par.ToLower().Trim();
                List<string> wordsList = par.Split(' ').ToList();

                for (int i = 0; i < wordsList.Count(); i++)
                {
                    wordsList[i] = wordsList[i].Trim('\'').Trim('.').Trim(',');
                }

                wordsList = removeBannedWords(wordsList, bannedWords);

                //foreach (string word in wordsList)
                //{
                //    Console.Write(word + " ");
                //}

                return wordsList;
            }

            static List<Tuple<string, int>> FindMostOccurences(List<string> wordsList)
            {
                List<Tuple<string, int>> result = new();
                string currentMaxWord = "";
                int maxOccurence = 0;

                foreach (string word in wordsList)
                {
                    int occurence = wordsList.Count(x => x == word);
                    Tuple<string, int> winPair = Tuple.Create(word, occurence);

                    if (occurence > maxOccurence)
                    {
                        currentMaxWord = word;
                        maxOccurence = occurence;

                        result.Clear();
                        result.Add(winPair);
                    }
                    else if (occurence == maxOccurence && !result.Contains(winPair))
                    {
                        result.Add(winPair);
                    }
                }

                return result;
            }

            static void DisplayResult(List<Tuple<string, int>> result)
            {
                if (result.Count() > 1)
                {
                    Console.Write($"We have multiple words that occurs {result[0].Item2} times in the paragraph.\nThe words are:");
                }
                else
                {
                    Console.Write($"The maximum occurence of a word in this paragraph is {result[0].Item2}. The word is");
                }

                foreach (Tuple<string, int> winPair in result)
                {
                    Console.Write(" " + winPair.Item1);
                }
                Console.Write(".");
            }

            string paragraph = "Bob hit a ball, the hit BALL flew flew far far far after it was hit.";
            // only lowercase character
            List<string> bannedWords = ["hit", "far"];

            if (!checkParagraphValidity(paragraph) || !checkBannedWords(bannedWords))
            {
                Console.WriteLine("Invalid paragraph or list of banned words.");
                return (int)ReturnCodes.INVALID_INPUT;
            }

            List<string> validWords = parseParagraph(paragraph, bannedWords);
            List<Tuple<string, int>> mostOccurencesList = FindMostOccurences(validWords);
            DisplayResult(mostOccurencesList);

            return (int)ReturnCodes.SUCCESS;
        }
    }
}
