using System.Globalization;
using Green;
using Xunit;
using static Green.Local;

namespace Quill
{
  public class TextTests
  {
    [Fact]
    public void StaticNone_IsNone()
    {
      // Act
      var none = Text.None;

      // Assert
      Expect(none.IsNone).IsTrue();
    }

    [Fact]
    public void Default_IsNone()
    {
      // Act
      var text = default(Text);

      // Assert
      Expect(text.IsNone).IsTrue();
    }

    [Fact]
    public void Default_ToString_IsEmpty()
    {
      // Arrange
      var text = default(Text);

      // Act
      var value = text.ToString();

      // Assert
      Expect(value).IsEmpty();
    }

    [Fact]
    public void ToString_UsesFormatProvider()
    {
      // Arrange
      var text = Text.Of(-1);
      var formatProvider = (CultureInfo) CultureInfo.CurrentCulture.Clone();
      formatProvider.NumberFormat.NegativeSign = "~";

      // Act
      var value = text.ToString(formatProvider);

      // Assert
      Expect(value).Is("~1");
    }
  }
}