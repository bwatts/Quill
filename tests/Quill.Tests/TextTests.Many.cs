using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Green;
using Xunit;
using static Green.Local;

namespace Quill
{
  public partial class TextTests
  {
    [Fact]
    public void OfMany_WriteItems_WithIndex()
    {
      // Arrange
      var source = "ABC".ToCharArray();
      var text = Text.Of(source, (writer, item, index) =>
      {
        writer.Write(item);
        writer.Write(index);
      });

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is("A0B1C2");
    }

    [Fact]
    public void OfMany_WriteItems()
    {
      // Arrange
      var value = "ABC";
      var source = value.ToCharArray();
      var text = Text.Of(source, (writer, item) => writer.Write(item));

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is(value);
    }

    [Fact]
    public void OfMany_SelectText_WithIndex()
    {
      // Arrange
      var source = "ABC".ToCharArray();
      var text = Text.Of(source, (item, index) => $"{item}{index}");

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is("A0B1C2");
    }

    [Fact]
    public void OfMany_SelectText()
    {
      // Arrange
      var source = "ABC".ToCharArray();
      var text = Text.Of(source, (item, index) => $"{item}.");

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is("A.B.C.");
    }

    [Fact]
    public void OfMany()
    {
      // Arrange
      var value = "ABC";
      var source = value.ToCharArray();
      var text = Text.Of(source);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is(value);
    }

    [Fact]
    public void OfMany_ActionTextWriter_WithIndex()
    {
      // Arrange
      var value0 = "ABC";
      var value1 = "123";
      var text = Text.Of(new[]
      {
        new Action<TextWriter, int>((writer, index) => writer.Write(value0 + index)),
        new Action<TextWriter, int>((writer, index) => writer.Write(value1 + index))
      });

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}0{value1}1");
    }

    [Fact]
    public void OfMany_ActionTextWriter()
    {
      // Arrange
      var value0 = "ABC";
      var value1 = "123";
      var text = Text.Of(new[]
      {
        new Action<TextWriter>(writer => writer.Write(value0)),
        new Action<TextWriter>(writer => writer.Write(value1))
      });

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}{value1}");
    }

    [Fact]
    public void OfMany_TextReader()
    {
      // Arrange
      var value0 = "ABC";
      var value1 = "123";
      var text = Text.Of(new TextReader[]
      {
        new StringReader(value0),
        new StringReader(value1)
      });

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}{value1}");
    }

    [Fact]
    public void OfMany_Boolean()
    {
      // Arrange
      var value0 = true;
      var value1 = false;
      var text = Text.Of(new[] { value0, value1 });

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}{value1}");
    }

    [Fact]
    public void OfMany_Char()
    {
      // Arrange
      var value0 = 'A';
      var value1 = 'B';
      var text = Text.Of(new[] { value0, value1 });

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}{value1}");
    }

    [Fact]
    public void OfMany_Decimal()
    {
      // Arrange
      var value0 = 1.5m;
      var value1 = 3.0m;
      var text = Text.Of(new[] { value0, value1 });

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}{value1}");
    }

    [Fact]
    public void OfMany_Double()
    {
      // Arrange
      var value0 = 1.5;
      var value1 = 3.0;
      var text = Text.Of(new[] { value0, value1 });

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}{value1}");
    }

    [Fact]
    public void OfMany_Single()
    {
      // Arrange
      var value0 = 1.5f;
      var value1 = 3.0f;
      var text = Text.Of(new[] { value0, value1 });

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}{value1}");
    }

    [Fact]
    public void OfMany_Int32()
    {
      // Arrange
      var value0 = 1;
      var value1 = 3;
      var text = Text.Of(new[] { value0, value1 });

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}{value1}");
    }

    [Fact]
    public void OfMany_Int64()
    {
      // Arrange
      var value0 = 1L;
      var value1 = 3L;
      var text = Text.Of(new[] { value0, value1 });

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}{value1}");
    }

    [Fact]
    public void OfMany_String()
    {
      // Arrange
      var value0 = "ABC";
      var value1 = "123";
      var text = Text.Of(new[] { value0, value1 });

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}{value1}");
    }

    [Fact]
    public void OfMany_Text()
    {
      // Arrange
      var value0 = Text.Of("ABC");
      var value1 = Text.Of(123);
      var text = Text.Of(new[] { value0, value1 });

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}{value1}");
    }

    //
    // Separators
    //

    [Fact]
    public void OfMany_WriteItems_WithIndex_WithSeparator()
    {
      // Arrange
      var source = "ABC".ToCharArray();
      var separator = ", ";
      var text = Text.Of(
        source,
        (writer, item, index) =>
        {
          writer.Write(item);
          writer.Write(index);
        },
        separator);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"A0{separator}B1{separator}C2");
    }

    [Fact]
    public void OfMany_WriteItems_WithSeparator()
    {
      // Arrange
      var source = "ABC".ToCharArray();
      var separator = ", ";
      var text = Text.Of(source, (writer, item) => writer.Write(item), separator);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"A{separator}B{separator}C");
    }

    [Fact]
    public void OfMany_SelectText_WithIndex_WithSeparator()
    {
      // Arrange
      var source = "ABC".ToCharArray();
      var separator = ", ";
      var text = Text.Of(source, (item, index) => $"{item}{index}", separator);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"A0{separator}B1{separator}C2");
    }

    [Fact]
    public void OfMany_SelectText_WithSeparator()
    {
      // Arrange
      var source = "ABC".ToCharArray();
      var separator = ", ";
      var text = Text.Of(source, (item, index) => $"{item}.", separator);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"A.{separator}B.{separator}C.");
    }

    [Fact]
    public void OfMany_WithSeparator()
    {
      // Arrange
      var value = "ABC";
      var separator = ", ";
      var source = value.ToCharArray();
      var text = Text.Of(source, separator);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"A{separator}B{separator}C");
    }

    [Fact]
    public void OfMany_ActionTextWriter_WithIndex_WithSeparator()
    {
      // Arrange
      var value0 = "ABC";
      var value1 = "123";
      var separator = ", ";
      var text = Text.Of(
        new[]
        {
          new Action<TextWriter, int>((writer, index) => writer.Write(value0 + index)),
          new Action<TextWriter, int>((writer, index) => writer.Write(value1 + index))
        },
        separator);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}0{separator}{value1}1");
    }

    [Fact]
    public void OfMany_ActionTextWriter_WithSeparator()
    {
      // Arrange
      var value0 = "ABC";
      var value1 = "123";
      var separator = ", ";
      var text = Text.Of(
        new[]
        {
          new Action<TextWriter>(writer => writer.Write(value0)),
          new Action<TextWriter>(writer => writer.Write(value1))
        },
        separator);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}{separator}{value1}");
    }

    [Fact]
    public void OfMany_TextReader_WithSeparator()
    {
      // Arrange
      var value0 = "ABC";
      var value1 = "123";
      var separator = ", ";
      var text = Text.Of(
        new TextReader[]
        {
          new StringReader(value0),
          new StringReader(value1)
        },
        separator);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}{separator}{value1}");
    }

    [Fact]
    public void OfMany_Boolean_WithSeparator()
    {
      // Arrange
      var value0 = true;
      var value1 = false;
      var separator = ", ";
      var text = Text.Of(new[] { value0, value1 }, separator);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}{separator}{value1}");
    }

    [Fact]
    public void OfMany_Char_WithSeparator()
    {
      // Arrange
      var value0 = 'A';
      var value1 = 'B';
      var separator = ", ";
      var text = Text.Of(new[] { value0, value1 }, separator);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}{separator}{value1}");
    }

    [Fact]
    public void OfMany_Decimal_WithSeparator()
    {
      // Arrange
      var value0 = 1.5m;
      var value1 = 3.0m;
      var separator = ", ";
      var text = Text.Of(new[] { value0, value1 }, separator);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}{separator}{value1}");
    }

    [Fact]
    public void OfMany_Double_WithSeparator()
    {
      // Arrange
      var value0 = 1.5;
      var value1 = 3.0;
      var separator = ", ";
      var text = Text.Of(new[] { value0, value1 }, separator);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}{separator}{value1}");
    }

    [Fact]
    public void OfMany_Single_WithSeparator()
    {
      // Arrange
      var value0 = 1.5f;
      var value1 = 3.0f;
      var separator = ", ";
      var text = Text.Of(new[] { value0, value1 }, separator);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}{separator}{value1}");
    }

    [Fact]
    public void OfMany_Int32_WithSeparator()
    {
      // Arrange
      var value0 = 1;
      var value1 = 3;
      var separator = ", ";
      var text = Text.Of(new[] { value0, value1 }, separator);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}{separator}{value1}");
    }

    [Fact]
    public void OfMany_Int64_WithSeparator()
    {
      // Arrange
      var value0 = 1L;
      var value1 = 3L;
      var separator = ", ";
      var text = Text.Of(new[] { value0, value1 }, separator);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}{separator}{value1}");
    }

    [Fact]
    public void OfMany_String_WithSeparator()
    {
      // Arrange
      var value0 = "ABC";
      var value1 = "123";
      var separator = ", ";
      var text = Text.Of(new[] { value0, value1 }, separator);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}{separator}{value1}");
    }

    [Fact]
    public void OfMany_Text_WithSeparator()
    {
      // Arrange
      var value0 = Text.Of("ABC");
      var value1 = Text.Of(123);
      var separator = ", ";
      var text = Text.Of(new[] { value0, value1 }, separator);

      // Act
      var result = text.ToString();

      // Assert
      Expect(result).Is($"{value0}{separator}{value1}");
    }




    





    [Fact]
    public void Items_SelectText_WithIndex()
    {
      // Arrange
      var source = "ABC".ToCharArray();

      // Act
      var result = Text.Items(source, (item, index) => $"{item}{index}");

      // Assert
      ExpectMany(result).HasSameItems("A0", "B1", "C2");
    }

    [Fact]
    public void Items_SelectText()
    {
      // Arrange
      var source = "ABC".ToCharArray();

      // Act
      var result = Text.Items(source, (item, index) => $"{item}.");

      // Assert
      ExpectMany(result).HasSameItems("A.", "B.", "C.");
    }

    [Fact]
    public void Items()
    {
      // Arrange
      var source = "ABC".ToCharArray();

      // Act
      var result = Text.Items(source);

      // Assert
      ExpectMany(result).HasSameItems("A", "B", "C");
    }

    [Fact]
    public void Items_ActionTextWriter_WithIndex()
    {
      // Arrange
      var value0 = "ABC";
      var value1 = "123";
      var source0 = new Action<TextWriter, int>((writer, index) => writer.Write(value0 + index));
      var source1 = new Action<TextWriter, int>((writer, index) => writer.Write(value1 + index));



      // This is ultimately resolving to the object? overload;



      // Act
      var result = Text.Items(new[] { source0, source1 });

      // Assert
      ExpectMany(result).HasSameItems($"{value0}0", $"{value1}1");
    }

    //[Fact]
    //public void Items_ActionTextWriter()
    //{
    //  // Arrange
    //  var value0 = "ABC";
    //  var value1 = "123";
    //  var text = Text.Of(new[]
    //  {
    //    new Action<TextWriter>(writer => writer.Write(value0)),
    //    new Action<TextWriter>(writer => writer.Write(value1))
    //  });

    //  // Act
    //  var result = text.ToString();

    //  // Assert
    //  Expect(result).Is($"{value0}{value1}");
    //}

    //[Fact]
    //public void Items_TextReader()
    //{
    //  // Arrange
    //  var value0 = "ABC";
    //  var value1 = "123";
    //  var text = Text.Of(new TextReader[]
    //  {
    //    new StringReader(value0),
    //    new StringReader(value1)
    //  });

    //  // Act
    //  var result = text.ToString();

    //  // Assert
    //  Expect(result).Is($"{value0}{value1}");
    //}

    //[Fact]
    //public void Items_Boolean()
    //{
    //  // Arrange
    //  var value0 = true;
    //  var value1 = false;
    //  var text = Text.Of(new[] { value0, value1 });

    //  // Act
    //  var result = text.ToString();

    //  // Assert
    //  Expect(result).Is($"{value0}{value1}");
    //}

    //[Fact]
    //public void Items_Char()
    //{
    //  // Arrange
    //  var value0 = 'A';
    //  var value1 = 'B';
    //  var text = Text.Of(new[] { value0, value1 });

    //  // Act
    //  var result = text.ToString();

    //  // Assert
    //  Expect(result).Is($"{value0}{value1}");
    //}

    //[Fact]
    //public void Items_Decimal()
    //{
    //  // Arrange
    //  var value0 = 1.5m;
    //  var value1 = 3.0m;
    //  var text = Text.Of(new[] { value0, value1 });

    //  // Act
    //  var result = text.ToString();

    //  // Assert
    //  Expect(result).Is($"{value0}{value1}");
    //}

    //[Fact]
    //public void Items_Double()
    //{
    //  // Arrange
    //  var value0 = 1.5;
    //  var value1 = 3.0;
    //  var text = Text.Of(new[] { value0, value1 });

    //  // Act
    //  var result = text.ToString();

    //  // Assert
    //  Expect(result).Is($"{value0}{value1}");
    //}

    //[Fact]
    //public void Items_Single()
    //{
    //  // Arrange
    //  var value0 = 1.5f;
    //  var value1 = 3.0f;
    //  var text = Text.Of(new[] { value0, value1 });

    //  // Act
    //  var result = text.ToString();

    //  // Assert
    //  Expect(result).Is($"{value0}{value1}");
    //}

    //[Fact]
    //public void Items_Int32()
    //{
    //  // Arrange
    //  var value0 = 1;
    //  var value1 = 3;
    //  var text = Text.Of(new[] { value0, value1 });

    //  // Act
    //  var result = text.ToString();

    //  // Assert
    //  Expect(result).Is($"{value0}{value1}");
    //}

    //[Fact]
    //public void Items_Int64()
    //{
    //  // Arrange
    //  var value0 = 1L;
    //  var value1 = 3L;
    //  var text = Text.Of(new[] { value0, value1 });

    //  // Act
    //  var result = text.ToString();

    //  // Assert
    //  Expect(result).Is($"{value0}{value1}");
    //}

    //[Fact]
    //public void Items_String()
    //{
    //  // Arrange
    //  var value0 = "ABC";
    //  var value1 = "123";
    //  var text = Text.Of(new[] { value0, value1 });

    //  // Act
    //  var result = text.ToString();

    //  // Assert
    //  Expect(result).Is($"{value0}{value1}");
    //}

    //[Fact]
    //public void Items_Text()
    //{
    //  // Arrange
    //  var value0 = Text.Of("ABC");
    //  var value1 = Text.Of(123);
    //  var text = Text.Of(new[] { value0, value1 });

    //  // Act
    //  var result = text.ToString();

    //  // Assert
    //  Expect(result).Is($"{value0}{value1}");
    //}











  }
}