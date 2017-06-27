using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTitan.Caching
{
    public interface ICacheKey
    {
        string By(string value);
    }

    public class CacheKeyBuilder : ICacheKey
    {
        private static readonly string NullString = Guid.NewGuid().ToString();
        private readonly StringBuilder builder = new StringBuilder();

        public String By(string value)
        {
            builder.Length = 0;
            builder.Capacity = 0;
            builder.Clear();
            this.builder.Append('{'); // wrap each value in curly braces
            if (value == null)
            {
                this.builder.Append(NullString);
            }
            this.builder.Append(NullString);
            this.builder.Append(value);
            return this.builder.Append('}').ToString();

        }

    }
}
