using FluentAssertions;
using System.Globalization;
using System.Threading;
using Wps.Client.Utils;
using Xunit;

namespace Wps.Client.Tests
{
    public class StringHelperTests
    {

        [Fact]
        public void CustomFormatJoin_EmptyFormatGiven_With_1_SpaceSeparator_With_5_Doubles_With_InvariantCulture_ShouldGenerateInvariantString()
        {
            const string expectedResult = "1.1 2.2 3.3 4.4 5.5";

            var elements = new [] { 1.1, 2.2, 3.3, 4.4, 5.5 };
            var result = StringHelper.CustomFormatJoin(string.Empty, CultureInfo.InvariantCulture, " ", elements);

            result.Should().Be(expectedResult);
        }

        [Fact]
        public void CustomFormatJoin_NoFormatGiven_With_1_SpaceSeparator_With_5_Doubles_With_InvariantCulture_ShouldGenerateInvariantString()
        {
            const string expectedResult = "1.1 2.2 3.3 4.4 5.5";

            var elements = new[] { 1.1, 2.2, 3.3, 4.4, 5.5 };
            var result = StringHelper.CustomFormatJoin(CultureInfo.InvariantCulture, " ", elements);

            result.Should().Be(expectedResult);
        }

        [Fact]
        public void CustomFormatJoin_NoFormatGiven_With_1_SpaceSeparator_With_5_Doubles_With_GermanCulture_ShouldGenerateCultureVariantString()
        {
            const string expectedResult = "1,1 2,2 3,3 4,4 5,5";

            var elements = new[] { 1.1, 2.2, 3.3, 4.4, 5.5 };
            var result = StringHelper.CustomFormatJoin(CultureInfo.CreateSpecificCulture("de-DE"), " ", elements);

            result.Should().Be(expectedResult);
        }

        [Fact]
        public void CustomFormatJoin_NoFormatGiven_With_1_SpaceSeparator_With_5_Doubles_With_GermanCultureThreadOnThread_ShouldGenerateInvariantString()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("de-DE");

            const string expectedResult = "1.1 2.2 3.3 4.4 5.5";

            var elements = new[] { 1.1, 2.2, 3.3, 4.4, 5.5 };
            var result = StringHelper.CustomFormatJoin(CultureInfo.InvariantCulture, " ", elements);

            result.Should().Be(expectedResult);
        }

    }
}
