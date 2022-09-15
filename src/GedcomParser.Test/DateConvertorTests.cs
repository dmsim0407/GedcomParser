using Shouldly;
using Xunit;
using GedcomParser.Utils;

namespace GedcomParser.Test
{
    public class DateConvertorTests
    {
        [Fact]
        public void YearOnly()
        {
            var date = DateConvertor.ConvertToDate("2022");
            date?.Year.ShouldBe(2022);
        }

        [Fact]
        public void FullDate_01()
        {
            var date = DateConvertor.ConvertToDate("24 Aug 2022");
            date?.Day.ShouldBe(24);
            date?.Month.ShouldBe(8);
            date?.Year.ShouldBe(2022);
        }

        [Fact]
        public void FullDate_02()
        {
            var date = DateConvertor.ConvertToDate("1 Aug 2022");
            date?.Day.ShouldBe(1);
            date?.Month.ShouldBe(8);
            date?.Year.ShouldBe(2022);
        }

        [Fact]
        public void MonthAndYear()
        {
            var date = DateConvertor.ConvertToDate("Aug 2022");
            date?.Day.ShouldBe(1);
            date?.Month.ShouldBe(8);
            date?.Year.ShouldBe(2022);
        }

        [Fact]
        public void ApproxYear()
        {
            var date = DateConvertor.ConvertToDate("Abt. 2022");
            date?.Day.ShouldBe(1);
            date?.Month.ShouldBe(1);
            date?.Year.ShouldBe(2022);
        }
    }
}
