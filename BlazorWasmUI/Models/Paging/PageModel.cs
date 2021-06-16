using System;
using System.Collections.Generic;

namespace BlazorWasmUI.Models.Paging
{
    public class PageModel
    {
        public List<int> RecordsPerPageCountList { get; set; }
        public int RecordsPerPageCount { get; set; }
        public List<int> PageNumbers { get; set; }
        public int PageNumber { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }

        public PageModel(int recordsCount = 0, int recordsPerPageCount = 2, int pageNumber = 1)
        {
            RecordsPerPageCountList = new List<int>() { 2, 3, 4, 5, 10, 25, 50, 100 }; // 2, 3, 4 ve 5 test için
            RecordsPerPageCount = recordsPerPageCount;
            Skip = (pageNumber - 1) * recordsPerPageCount;
            Take = recordsPerPageCount;
            PageNumber = pageNumber;
            PageNumbers = new List<int>();
            double totalPageCount = Math.Ceiling((double)recordsCount / (double)recordsPerPageCount);
            for (int pageNo = 1; pageNo <= totalPageCount; pageNo++)
            {
                PageNumbers.Add(pageNo);
            }
        }
    }
}
