using System.Linq;
using System;
using Assessment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AssessmentTest
{
    [TestClass]
    public class PaginationTest
    {
        private const string COMMA_SAMPLE = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
        private const string PIPE_SAMPLE = "a|b|c|d|e|f|g|h|i|j|k|l|m|n|o|p|q|r|s|t|u|v|w|x|y|z";
        
        [TestMethod]
        public void TestFirstPage()
        {
            IElementsProvider<string> provider = new StringProvider(",");
            IPagination<string> pagination = new PaginationString(COMMA_SAMPLE, 5, provider);
            pagination.FirstPage();
            string [] expectedElements = {"a", "b", "c", "d", "e"};            
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());
        }

        [TestMethod]
        public void TestNextPage()
        {
            IElementsProvider<string> provider = new StringProvider(",");
            IPagination<string> pagination = new PaginationString(COMMA_SAMPLE, 5, provider);
            pagination.NextPage();
            string [] expectedElements = {"f", "g", "h", "i", "j"};
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());
        }

        [TestMethod]
        public void TestPreviousPage()
        {
            IElementsProvider<string> provider = new StringProvider(",");
            IPagination<string> pagination = new PaginationString(COMMA_SAMPLE, 5, provider);
            pagination.FirstPage();
            pagination.NextPage();
            pagination.PrevPage();
            string[] expectedElements = { "a", "b", "c", "d", "e" };
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());
        }

        [TestMethod]
        public void TestLastPage()
        {
            IElementsProvider<string> provider = new StringProvider(",");
            IPagination<string> pagination = new PaginationString(COMMA_SAMPLE, 5, provider);
            pagination.LastPage();
            string [] expectedElements = {"v", "w", "x", "y", "z"};        
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());
        }

        [TestMethod]
        public void TestFirstPageWith10PageSize()
        {
            IElementsProvider<string> provider = new StringProvider(",");
            IPagination<string> pagination = new PaginationString(COMMA_SAMPLE, 10, provider);
            pagination.FirstPage();
            string [] expectedElements = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j"};
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());           
        }

        [TestMethod]
        public void TestLastPageWith10PageSize()
        {
            IElementsProvider<string> provider = new StringProvider(",");
            IPagination<string> pagination = new PaginationString(COMMA_SAMPLE, 10, provider);
            pagination.LastPage();
            string[] expectedElements = { "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());
        }

        [TestMethod]
        public void TestGoToPageWith10PageSize()
        {
            IElementsProvider<string> provider = new StringProvider(",");
            IPagination<string> pagination = new PaginationString(COMMA_SAMPLE, 10, provider);
            pagination.GoToPage(2);
            string[] expectedElements = { "k", "l", "m", "n", "o", "p", "q", "r", "s", "t" };
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());
        }

        [TestMethod]
        public void TestFirstPageWithPipeSample()
        {
            IElementsProvider<string> provider = new StringProvider("|");
            IPagination<string> pagination = new PaginationString(PIPE_SAMPLE, 5, provider);
            pagination.FirstPage();
            string [] expectedElements = {"a", "b", "c", "d", "e"};
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());
        }

        [TestMethod]
        public void TestPreviousPageException()
        {
            IElementsProvider<string> provider = new StringProvider("|");
            IPagination<string> pagination = new PaginationString(PIPE_SAMPLE, 5, provider);
            pagination.PrevPage();
            Assert.ReferenceEquals(pagination.GetVisibleItems(), null);
        }

        [TestMethod]
        //[ExpectedException(typeof(InvalidOperationException), "Invalid page number.")]
        public void TestGoToPageException()
        {
            IElementsProvider<string> provider = new StringProvider(",");
            IPagination<string> pagination = new PaginationString(COMMA_SAMPLE, 5, provider);
            pagination.GoToPage(1000000);
            //AssertFailedException.ReferenceEquals(pagination.GetVisibleItems(), null);
            Assert.ReferenceEquals(pagination.GetVisibleItems(), null);
        }

        [TestMethod]
        public void TestSortAsc()
        {
            IElementsProvider<string> provider = new StringProvider(",");
            IPagination<string> pagination = new PaginationString(COMMA_SAMPLE, 5, provider);
            pagination.SortAsc();
            pagination.LastPage();
            string[] expectedElements = { "v", "w", "x", "y", "z" };
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());
        }

        [TestMethod]
        public void TestSortDesc()
        {
            IElementsProvider<string> provider = new StringProvider(",");
            IPagination<string> pagination = new PaginationString(COMMA_SAMPLE, 5, provider);
            pagination.SortDesc();         
            pagination.FirstPage();
            string[] expectedElements = { "z", "y", "x", "w", "v" };
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());
        }
    }
}
