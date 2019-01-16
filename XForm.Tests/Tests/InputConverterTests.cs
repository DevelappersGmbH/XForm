using System.Globalization;
using XForm.InputConverters;
using Xunit;

namespace XForm.Tests.Tests
{
    public class InputConverterTests
    {
        [Fact]
        public void IntInputConverterTest()
        {
            var converter = new IntInputConverter(CultureInfo.InvariantCulture);
            
            Assert.Null(converter.ConvertText(""));
            Assert.Null(converter.ConvertText(null));
            Assert.Null(converter.ConvertText("abc"));
            
            Assert.Equal(1, converter.ConvertText("1"));
            Assert.Equal(1000, converter.ConvertText("1000"));
            Assert.Equal(-1000, converter.ConvertText("-1000"));
            Assert.Equal(1000, converter.ConvertText("1,000"));
            Assert.Equal(-1000, converter.ConvertText("-1,000"));
        }
        
        [Fact]
        public void DoubleInputConverterTest()
        {
            var converter = new DoubleInputConverter(CultureInfo.InvariantCulture);
            
            Assert.Null(converter.ConvertText(""));
            Assert.Null(converter.ConvertText(null));
            Assert.Null(converter.ConvertText("abc"));
            
            Assert.Equal(1, converter.ConvertText("1"));
            Assert.Equal(1000.5, converter.ConvertText("1000.5"));
            Assert.Equal(-1000.5, converter.ConvertText("-1000.5"));
            Assert.Equal(1000.5, converter.ConvertText("1,000.5"));
            Assert.Equal(-1000, converter.ConvertText("-1,000"));
        }
    }
}