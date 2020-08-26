using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Linq;

namespace CSV
{
    public class CSVManager
    {
        public bool m_IsWriting = false;
        public string[,] ReadCsv(string filePath)
        {
            string value = "";
            StreamReader reader = new StreamReader(filePath, Encoding.UTF8);
            value = reader.ReadToEnd();
            reader.Close();

            string[] lines = value.Split("\n"[0]);

            int width = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                string[] row = SplitCsvLine(lines[i]);
                width = Math.Max(width, row.Length);
            }

            string[,] outputGrid = new string[width + 1, lines.Length + 1];
            for (int y = 0; y < lines.Length; y++)
            {
                string[] row = SplitCsvLine(lines[y]);
                for (int x = 0; x < row.Length; x++)
                {
                    outputGrid[x, y] = row[x];
                    outputGrid[x, y] = outputGrid[x, y].Replace("\"\"", "\"");
                }
            }

            return outputGrid;
        }

        public string[] SplitCsvLine(string line)
        {
            return (from Match m in System.Text.RegularExpressions.Regex.Matches(line,
            @"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)",
            RegexOptions.ExplicitCapture)
                    select m.Groups[1].Value).ToArray();
        }

        public void WriteCsv(List<string[]> rowData, string filePath)
        {
            if(m_IsWriting)
            {
                string[][] output = new string[rowData.Count][];

                for (int i = 0; i < output.Length; i++)
                {
                    output[i] = rowData[i];
                }

                int length = output.GetLength(0);
                string delimiter = ",";

                StringBuilder stringBuilder = new StringBuilder();

                for (int index = 0; index < length; index++)
                    stringBuilder.AppendLine(string.Join(delimiter, output[index]));

                Stream fileStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write);
                StreamWriter outStream = new StreamWriter(fileStream, Encoding.UTF8);
                outStream.WriteLine(stringBuilder);
                outStream.Close();

                m_IsWriting = false;
            }
        }
    }
}
