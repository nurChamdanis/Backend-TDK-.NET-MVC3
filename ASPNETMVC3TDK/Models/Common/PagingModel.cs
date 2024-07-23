using System;
using System.Collections.Generic;

namespace ProjectStarter.Models.Common
{
    public class PagingModel
    {
        public int CountData { get; set; }
        public int StartData { get; set; }
        public int EndData { get; set; }
        public int PositionPage { get; set; }
        public int DataPerPage { get; set; }
        public double CountPage { get; set; }
        public int First { get; set; }
        public int Last { get; set; }
        public int Next { get; set; }
        public int Prev { get; set; }
        public int MaxPage { get; set; }
        public int MinPage { get; set; }

        public int MaxRange { get; set; }
        public int MinRange { get; set; }

        public string DataName {get; set; }
        public List<int> ListIndex { get; set; }

        public int mxpg = 3;

        public const int DEFAULT_RECORD_PER_PAGE = 10;
        public static readonly int[] RECORD_PER_PAGES = new int[] { 3, 5, 10, 25, 50, 100 };
        public PagingModel(int countdata, int positionpage, int dataperpage, string name)
        {
            List<int> list = new List<int>();
            EndData = positionpage * dataperpage;
            CountData = countdata;
            PositionPage = positionpage;
            DataPerPage = dataperpage;
            StartData = (positionpage - 1) * dataperpage + 1;

            CountPage = Math.Ceiling((double)countdata / dataperpage);
            CountPage = CountPage == 0 ? 1 : CountPage;
            First = 1;
            Last = (int)CountPage;
            Next = positionpage < (int)CountPage ? positionpage + 1 : (int)CountPage;
            Prev = positionpage == 1 ? 1 : positionpage - 1;
            MaxPage = positionpage + mxpg;
            MinPage = positionpage - mxpg;

            MinRange = Math.Max(MinPage, positionpage - 1);
            MaxRange = Math.Min(MaxPage, positionpage + 1);

            Double jml = Math.Ceiling((Double)countdata / (Double)dataperpage);
            for (int i = 1; i <= jml; i++)
            {
                list.Add(i);
            }
            ListIndex = list;
            DataName = name;
            
        }

        public PagingModel(int countdata, int positionpage, int dataperpage) :  this(countdata, positionpage, dataperpage, null)
        {
            
        }
    }
}