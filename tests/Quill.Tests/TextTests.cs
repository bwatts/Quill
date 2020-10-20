using System;
using System.Globalization;
using System.IO;
using Green;
using Xunit;
using static Green.Local;

namespace Quill
{
  public partial class TextTests
  {
    [Fact]
    public void StaticNone_IsNone()
    {
      // Act
      var result = Text.None;

      // Assert
      Expect(result.IsNone).IsTrue();
    }

    [Fact]
    public void Default_IsNone()
    {
      // Act
      var result = default(Text);

      // Assert
      Expect(result.IsNone).IsTrue();
    }

    [Fact]
    public void Default_ToString_IsEmpty()
    {
      // Arrange
      var text = default(Text);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).IsEmpty();
    }

    [Fact]
    public void ToString_UsesFormatProvider()
    {
      // Arrange
      var text = Text.Of(-1);
      var formatProvider = (CultureInfo) CultureInfo.CurrentCulture.Clone();
      formatProvider.NumberFormat.NegativeSign = "~";

      // Act
      var result = text.ToString(formatProvider);

      // Assert
      Expect(result).Is("~1");
    }

    //
    // Of
    //

    [Fact]
    public void Of_ActionTextReader()
    {
      // Arrange
      var source = "ABC";
      var text = Text.Of(writer => writer.Write(source));

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is(source);
    }

    [Fact]
    public void Of_TextReader()
    {
      // Arrange
      var source = "ABC";
      var reader = new StringReader(source);
      var text = Text.Of(reader);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is(source);
    }

    [Fact]
    public void Of_Boolean()
    {
      // Arrange
      var source = true;
      var text = Text.Of(source);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is(source.ToString());
    }

    [Fact]
    public void Of_Char()
    {
      // Arrange
      var source = 'A';
      var text = Text.Of(source);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is(source.ToString());
    }

    [Fact]
    public void Of_Decimal()
    {
      // Arrange
      var source = 1.5m;
      var text = Text.Of(source);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is(source.ToString());
    }

    [Fact]
    public void Of_Double()
    {
      // Arrange
      var source = 1.5;
      var text = Text.Of(source);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is(source.ToString());
    }

    [Fact]
    public void Of_Single()
    {
      // Arrange
      var source = 1.5f;
      var text = Text.Of(source);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is(source.ToString());
    }

    [Fact]
    public void Of_Int32()
    {
      // Arrange
      var source = 1;
      var text = Text.Of(source);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is(source.ToString());
    }

    [Fact]
    public void Of_Int64()
    {
      // Arrange
      var source = 1L;
      var text = Text.Of(source);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is(source.ToString());
    }

    [Fact]
    public void Of_Object()
    {
      // Arrange
      var source = new object();
      var text = Text.Of(source);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is(source.ToString()!);
    }

    [Fact]
    public void Of_String()
    {
      // Arrange
      var source = "ABC";
      var text = Text.Of(source);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is(source);
    }

    //
    // Format
    //

    [Fact]
    public void Format_OneArg()
    {
      // Arrange
      var format = "{0}";
      var arg = 1;
      var text = Text.Format(format, arg);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is(string.Format(format, arg));
    }

    [Fact]
    public void Format_TwoArgs()
    {
      // Arrange
      var format = "{0} {1}";
      var arg0 = 0;
      var arg1 = 1;
      var text = Text.Format(format, arg0, arg1);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is(string.Format(format, arg0, arg1));
    }

    [Fact]
    public void Format_ThreeArgs()
    {
      // Arrange
      var format = "{0} {1} {2}";
      var arg0 = 0;
      var arg1 = 1;
      var arg2 = 2;
      var text = Text.Format(format, arg0, arg1, arg2);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is(string.Format(format, arg0, arg1, arg2));
    }

    [Fact]
    public void Format_Args()
    {
      // Arrange
      var format = "{0} {1} {2} {3}";
      var arg0 = 0;
      var arg1 = 1;
      var arg2 = 2;
      var arg3 = 3;
      var text = Text.Format(format, arg0, arg1, arg2, arg3);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is(string.Format(format, arg0, arg1, arg2, arg3));
    }

    //
    // Conversions
    //

    [Fact]
    public void Converts_ToString()
    {
      // Arrange
      var source = "ABC";
      var text = Text.Of(source);

      // Act
      string result = text;

      // Assert
      Expect(result).Is(source);
    }

    [Fact]
    public void Converts_FromActionTextWriter()
    {
      // Arrange
      var value = "ABC";
      var source = new Action<TextWriter>(writer => writer.Write(value));

      // Act
      Text result = source;

      // Assert
      Expect(result.ToString()).Is(value);
    }

    [Fact]
    public void Converts_FromTextReader()
    {
      // Arrange
      var value = "ABC";
      var source = new StringReader(value);

      // Act
      Text result = source;

      // Assert
      Expect(result.ToString()).Is(value);
    }

    [Fact]
    public void Converts_FromBoolean()
    {
      // Arrange
      var source = true;

      // Act
      Text result = source;

      // Assert
      Expect(result.ToString()).Is(source.ToString());
    }

    [Fact]
    public void Converts_FromChar()
    {
      // Arrange
      var source = 'A';

      // Act
      Text result = source;

      // Assert
      Expect(result.ToString()).Is(source.ToString());
    }

    [Fact]
    public void Converts_FromCharArray()
    {
      // Arrange
      var value = "ABC";
      var source = value.ToCharArray();

      // Act
      Text result = source;

      // Assert
      Expect(result.ToString()).Is(value);
    }

    [Fact]
    public void Converts_FromDecimal()
    {
      // Arrange
      var source = 1.5m;

      // Act
      Text result = source;

      // Assert
      Expect(result.ToString()).Is(source.ToString());
    }

    [Fact]
    public void Converts_FromDouble()
    {
      // Arrange
      var source = 1.5;

      // Act
      Text result = source;

      // Assert
      Expect(result.ToString()).Is(source.ToString());
    }

    [Fact]
    public void Converts_FromSingle()
    {
      // Arrange
      var source = 1.5f;

      // Act
      Text result = source;

      // Assert
      Expect(result.ToString()).Is(source.ToString());
    }

    [Fact]
    public void Converts_FromInt32()
    {
      // Arrange
      var source = 1;

      // Act
      Text result = source;

      // Assert
      Expect(result.ToString()).Is(source.ToString());
    }

    [Fact]
    public void Converts_FromInt64()
    {
      // Arrange
      var source = 1L;

      // Act
      Text result = source;

      // Assert
      Expect(result.ToString()).Is(source.ToString());
    }

    [Fact]
    public void Converts_FromString()
    {
      // Arrange
      var source = "ABC";

      // Act
      Text result = source;

      // Assert
      Expect(result.ToString()).Is(source);
    }

    //
    // Concatentation
    //

    [Fact]
    public void Concatenates_ActionTextWriter()
    {
      // Arrange
      var previous = Text.Of("ABC");
      var value = 123;
      var source = new Action<TextWriter>(writer => writer.Write(value));

      // Act
      var result = previous + source;

      // Assert
      Expect(result.ToString()).Is($"{previous}{value}");
    }

    [Fact]
    public void Concatenates_TextReader()
    {
      // Arrange
      var previous = Text.Of("ABC");
      var value = "123";
      var source = new StringReader(value);

      // Act
      var result = previous + source;

      // Assert
      Expect(result.ToString()).Is($"{previous}{value}");
    }

    [Fact]
    public void Concatenates_Boolean()
    {
      // Arrange
      var previous = Text.Of("ABC");
      var source = true;

      // Act
      var result = previous + source;

      // Assert
      Expect(result.ToString()).Is($"{previous}{source}");
    }

    [Fact]
    public void Concatenates_Char()
    {
      // Arrange
      var previous = Text.Of("ABC");
      var source = '1';

      // Act
      var result = previous + source;

      // Assert
      Expect(result.ToString()).Is($"{previous}{source}");
    }

    [Fact]
    public void Concatenates_CharArray()
    {
      // Arrange
      var previous = Text.Of("ABC");
      var value = "123";
      var source = value.ToCharArray();

      // Act
      var result = previous + source;

      // Assert
      Expect(result.ToString()).Is($"{previous}{value}");
    }

    [Fact]
    public void Concatenates_Decimal()
    {
      // Arrange
      var previous = Text.Of("ABC");
      var source = 1.5m;

      // Act
      var result = previous + source;

      // Assert
      Expect(result.ToString()).Is($"{previous}{source}");
    }

    [Fact]
    public void Concatenates_Double()
    {
      // Arrange
      var previous = Text.Of("ABC");
      var source = 1.5;

      // Act
      var result = previous + source;

      // Assert
      Expect(result.ToString()).Is($"{previous}{source}");
    }

    [Fact]
    public void Concatenates_Single()
    {
      // Arrange
      var previous = Text.Of("ABC");
      var source = 1.5f;

      // Act
      var result = previous + source;

      // Assert
      Expect(result.ToString()).Is($"{previous}{source}");
    }

    [Fact]
    public void Concatenates_Int32()
    {
      // Arrange
      var previous = Text.Of("ABC");
      var source = 1;

      // Act
      var result = previous + source;

      // Assert
      Expect(result.ToString()).Is($"{previous}{source}");
    }

    [Fact]
    public void Concatenates_Int64()
    {
      // Arrange
      var previous = Text.Of("ABC");
      var source = 1L;

      // Act
      var result = previous + source;

      // Assert
      Expect(result.ToString()).Is($"{previous}{source}");
    }

    [Fact]
    public void Concatenates_Object()
    {
      // Arrange
      var previous = Text.Of("ABC");
      var source = new object();

      // Act
      var result = previous + source;

      // Assert
      Expect(result.ToString()).Is($"{previous}{source}");
    }

    [Fact]
    public void Concatenates_String()
    {
      // Arrange
      var previous = Text.Of("ABC");
      var source = "123";

      // Act
      var result = previous + source;

      // Assert
      Expect(result.ToString()).Is($"{previous}{source}");
    }

    [Fact]
    public void Concatenates_Text()
    {
      // Arrange
      var previous = Text.Of("ABC");
      var source = Text.Of("123");

      // Act
      var result = previous + source;

      // Assert
      Expect(result.ToString()).Is($"{previous}{source}");
    }
  }
}