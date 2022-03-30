using System.Collections.Generic;
using System.Linq;
using System;

namespace Assessment
{
    public class PaginationString : IPagination<string>
    {
        private IEnumerable<string> data;
        private readonly int pageSize;
        private int currentPage;
        private bool last;
        private bool reverse;
        public PaginationString(string source, int pageSize, IElementsProvider<string> provider)
        {
            data = provider.ProcessData(source);
            currentPage = 0;
            this.pageSize = pageSize;
            last = false;
            reverse = false;
        }       

        public void FirstPage()
        {           
            currentPage = 0;
            last = false;
        }

        public void NextPage()
        {           
            currentPage++;
            if (GetVisibleItems() == null) Console.WriteLine("Limit of page");
            last = false;
        }

        public void PrevPage()
        {           
            currentPage--;
            if (GetVisibleItems() == null) Console.WriteLine("Limit of page");
            last = false;
        }

        public IPagination<string> GoToPage(int page)
        {
            currentPage = page - 1;
            if(GetVisibleItems() == null) Console.WriteLine("Invalid Page");
            last = false;
            return null;
        }

        public void LastPage()
        {
            last = true;
        }

        public int CurrentPage()
        {
            Console.WriteLine("You are at the " + (currentPage + 1) + " page.");
            return currentPage + 1;
        }

        public int Pages()
        {           
            int items = data.ToList().Count;
            if(items % 2 != 0) items++;
            int pages = items / pageSize;
            Console.WriteLine("Pages: " + pages);
            currentPage = 0;
            return pages;
        }        

        public void SortAsc()
        {
            if (reverse)
            {
                data = data.Reverse();
                reverse = false;
                Console.WriteLine("Ascending list");
                Console.WriteLine(String.Join(" ", data.ToList()));
            }
        }

        public void SortDesc()
        {
            if (!reverse)
            {
                data = data.Reverse();
                reverse = true;
                Console.WriteLine("Descending list");
                Console.WriteLine(String.Join(" ", data.ToList()));
            }           
        }       

        public IEnumerable<string> GetVisibleItems()
        {
            var page = data.Skip(currentPage * pageSize).Take(pageSize).ToList();
            if (page.Count == 0 || currentPage < 0) return null;
            if (last) return data.TakeLast(pageSize).ToList();
            return page;           
        }
    }
}







//int items = GetTotalItems().ToList().Count;
//if (items % 2 != 0) items++;
//int pages = items / pageSize;
//Console.WriteLine("The last page is: " + String.Join(" ", GetTotalItems().Skip(pages * pageSize - pageSize).Take(pageSize).ToList()));



//while (true)
//{
//    if (GetVisibleItems().ToList().Count == 0) break;
//    currentPage++;
//}
//currentPage--;