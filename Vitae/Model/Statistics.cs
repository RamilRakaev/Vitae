using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vitae
{
    
    public class Statistics
    {
        static string Path;

        List<string> Games;
        public List<string> Games_Names;
        List<string> Levels;
        static List<string> Str_Crtiterions = new List<string>();

        List<int> Criterions = new List<int>();

        //результыты статистики
        public List<string> Result;
        private static void Establish_Value()
        {
            Str_Crtiterions.Clear();
            Str_Crtiterions.Add("Количество верных ответов: ");
            Str_Crtiterions.Add("Количество неверных ответов: ");
            Str_Crtiterions.Add("Пройдено: ");
        }

        public Statistics(List<string> _games, List<string> _levels)
        {
            Games_Names = new List<string>();
            Games_Names.AddRange(_games);
            Get_Games();
            Establish_Value();

            Result = new List<string>();
            List<ulong> Values;


            int ind = 0;
            foreach (string game in Games)
            {
                Get_Levels(game);
                Values = Load_Statistics(game);
                //Текст
                string list = "";
                for (int j = 0; j < Levels.Count; j++)
                {
                    //заголовок 
                    list += _levels[j] + "\n";
                    foreach (string str in Str_Crtiterions)
                    {
                        //критерий и результат
                        list += str + Values[ind] + "\n";
                        ind++;
                    }
                    list += "\n";
                }
                ind = 0;
                Result.Add(list);
            }

        }
        /// <summary>
        /// Загрузка всех значений
        /// </summary>
        /// <returns></returns>
        public List<ulong> Load_Statistics(string game)
        {
            List<ulong> values = new List<ulong>();

            sbyte ind = 0;
            foreach (string l in Levels)
            {
                List<ulong> lon = FileSave.Load_Ulong(l);
                Check(ref lon);
                values.AddRange(lon);
                ind++;
                if (ind == Str_Crtiterions.Count)
                    ind = 0;
            }
            return values;
        }

        private void Get_Games()
        {
            string path = Environment.CurrentDirectory + @"\Tests";
            if (Directory.Exists(path))
            {
                Games = new List<string>();
                Games.AddRange(Directory.EnumerateDirectories(path));
            }
        }

        private void Get_Levels(string game)
        {
            Levels = new List<string>();
            Levels.AddRange(Directory.EnumerateFiles(game));
        }


        /// <summary>
        /// Возвращает выбранную коллекцию
        /// </summary>
        /// <param name="game"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static List<ulong> Load_Collection(string game, string level)
        {

            List<ulong> load;
            Path = Environment.CurrentDirectory + @"\Tests" + @"\" + game + @"\" + level + ".bin";

            load = FileSave.Load_Ulong(Path);

            Check(ref load);
            return load;
        }

        private static void Check(ref List<ulong> load)
        {
            if (load.Count == 0)
            {
                Establish_Value();
                for (int i = 0; i < Str_Crtiterions.Count; i++)
                    load.Add(0);
            }
        }

        public static void Save_Collection(List<ulong> col)
        {
            FileSave.Save_Ulong(col, Path);

        }

    }
}
