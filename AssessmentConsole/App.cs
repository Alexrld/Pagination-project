using System;
using System.Linq;
using Assessment;

namespace AssessmentConsole
{
    public class App
    {
        public bool ProcessOption(string option) 
        {
            if (option != "1") return true;          
            StartPagination();
            return false;                     
        }
        
        private void StartPagination()
        {        
            string option = IRead.PutMessageGetOption(
                @"Pagination commands\n
                1. Source data
                0. Back
                ");
            if (option == "1") ProcessPagination();     
        }

        private void ProcessPagination()
        {
            bool getOption = true;
            string option = IRead.PutMessageGetOption(
                @"Type: \n
                1. Comma separated data(,)
                2. Pipe separated data(|)
                3. Space separated data( )
                0. Go Back
                ");
            string separator="";
            switch (option)
            {
                case "1":
                    separator = ",";
                    break;
                case "2":
                    separator = "|";
                    break;
                case "3":
                    separator = " ";
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    getOption = false;
                    break;
            }
            if (getOption)
            {
                string data = IRead.PutMessageGetOption("Source data");
                NavigateData(data, separator);
            }              
        }

        private void NavigateData(string data, string separator)
        {
            string pageSize = IRead.PutMessageGetOption("Type the Page size");
            IElementsProvider<string> provider = new StringProvider(separator);
            IPagination<string> pagination = new PaginationString(data, int.Parse(pageSize), provider);        
            DoNavigation(pagination);
        }

        private void DoNavigation(IPagination<string> pagination)
        {
            bool exit = false;
            while(!exit)
            {              
                string option = IRead.PutMessageGetOption(
                @"Type: \n
                1. First page
                2. Next page
                3. Previous page
                4. Last page
                5. Go to page
                6. Current page
                7. Pages
                0. Go Back
                ");

                switch (option)
                {
                    case "1":
                        pagination.FirstPage();                      
                        break;
                    case "2":
                        pagination.NextPage();
                        break;
                    case "3":
                        pagination.PrevPage();
                        break;
                    case "4":
                        pagination.LastPage();
                        break;
                    case "5":                    
                        pagination.GoToPage(int.Parse(IRead.PutMessageGetOption("Type the page number:")));
                        break;
                    case "6":
                        pagination.CurrentPage();
                        break;
                    case "7":
                        pagination.Pages();
                        break;
                    case "8":
                        pagination.SortAsc();
                        break;
                    case "9":
                        pagination.SortDesc();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Option.");
                        break;
                }
                if(pagination.GetVisibleItems() != null) Console.WriteLine(string.Join(" ", pagination.GetVisibleItems()));
            }
        }          
    }
}