using System.Collections.Generic;

namespace Inverted_index_and_text_search
{
    public struct StatisticalInformation
    {
        public string FileName { get; }
        public List<int> Positions { get; private set; }

        public StatisticalInformation()
        {
            FileName = string.Empty;
            Positions = new List<int>();
        }

        public StatisticalInformation(string fileName, int position) : this()
        {
            FileName = fileName;
            AddPosition(position);
        }

        public void AddPosition(int position)
        {
            Positions.Add(position);
        }
    }
}