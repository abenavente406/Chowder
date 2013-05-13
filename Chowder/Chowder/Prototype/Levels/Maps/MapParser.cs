using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Chowder.Prototype.Levels.Maps
{
    public class MapParser
    {
        private string path = "";

        public MapParser(string path)
        {
            this.path = path;
        }

        public int[,] ParseMap()
        {
            int[,] result;
            int width = 0, height = 0;
            height = File.ReadAllLines(path).Length;
            var stream = File.Open(path, FileMode.Open);
            var reader = new StreamReader(stream);

            try
            {
                width = reader.ReadLine().Length;
                stream.Close();
                reader.Close();
                stream = File.Open(path, FileMode.Open);
                reader = new StreamReader(stream);
                result = new int[width, height];

                int yy = 0, xx = 0;

                while (!reader.EndOfStream)
                {
                    string currentLine = reader.ReadLine();
                    for (xx = 0; xx < width; xx++)
                    {
                        result[xx, yy] = Convert.ToInt32(currentLine[xx].ToString());
                    }
                    yy++;
                }
            }
            catch (IOException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                result = null;
            }
            finally
            {
                stream.Close();
                reader.Close();
            }

            return result;
        }
    }
}
