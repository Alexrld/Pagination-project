using System.Collections.Generic;

namespace Assessment
{
    public class StringProvider : IElementsProvider<string>
    {
        private readonly string separator;
        public StringProvider(string separator)
        {
            this.separator = separator;
        }
        public IEnumerable<string> ProcessData(string source)
        {
            return source.Split(separator);           
        }
    }
}