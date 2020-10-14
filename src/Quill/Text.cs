using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;

namespace Quill
{
  [TypeConverter(typeof(Converter))]
  public struct Text : Text.IText, IFormattable
  {
    internal interface IText
    {
      Text Write(Action<TextWriter> next);
      void WriteTo(TextWriter writer);
    }

    readonly IText? _previous;
    readonly Action<TextWriter>? _source;

    Text(Action<TextWriter>? source) =>
      (_previous, _source) = (null, source);

    Text(Text previous, Action<TextWriter>? source) =>
      (_previous, _source) = (previous, source);

    public bool IsNone => _source == null;

    public Text Write(Action<TextWriter> next) =>
      new Text(this, next);

    public void WriteTo(TextWriter writer)
    {
      _previous?.WriteTo(writer);
      _source?.Invoke(writer);
    }

    public override string ToString() =>
      ToString(null);

    public string ToString(IFormatProvider? formatProvider)
    {
      using var writer = new StringWriter(formatProvider);

      WriteTo(writer);

      return writer.ToString();
    }

    string IFormattable.ToString(string format, IFormatProvider? formatProvider) =>
      ToString(formatProvider);

    //
    // Values
    //

    public static readonly Text None = default;

    public static Text Of(Action<TextWriter> source) =>
      new Text(source);

    public static Text Of(TextReader source) =>
      Of(writer =>
      {
        string line;

        while((line = source.ReadLine()) != null)
        {
          writer.WriteLine(line);
        }
      });

    public static Text Of(bool source) =>
      Of(writer => writer.Write(source));

    public static Text Of(char source) =>
      Of(writer => writer.Write(source));

    public static Text Of(decimal source) =>
      Of(writer => writer.Write(source));

    public static Text Of(double source) =>
      Of(writer => writer.Write(source));

    public static Text Of(float source) =>
      Of(writer => writer.Write(source));

    public static Text Of(int source) =>
      Of(writer => writer.Write(source));

    public static Text Of(long source) =>
      Of(writer => writer.Write(source));

    public static Text Of(object source) =>
      Of(writer => writer.Write(source));

    public static Text Of(string source) =>
      Of(writer => writer.Write(source));

    public static Text Of(string format, object arg0) =>
      Of(writer => writer.Write(format, arg0));

    public static Text Of(string format, object arg0, object arg1) =>
      Of(writer => writer.Write(format, arg0, arg1));

    public static Text Of(string format, object arg0, object arg1, object arg2) =>
      Of(writer => writer.Write(format, arg0, arg1, arg2));

    public static Text Of(string format, params object[] args) =>
      Of(writer => writer.Write(format, args));

    //
    // Conversion operators
    //

    public static implicit operator string(Text text) =>
      text.ToString();

    public static implicit operator Text(Action<TextWriter> source) =>
      Of(source);

    public static implicit operator Text(TextReader source) =>
      Of(source);

    public static implicit operator Text(bool source) =>
      Of(source);

    public static implicit operator Text(char source) =>
      Of(source);

    public static implicit operator Text(char[] source) =>
      Of(source);

    public static implicit operator Text(decimal source) =>
      Of(source);

    public static implicit operator Text(double source) =>
      Of(source);

    public static implicit operator Text(float source) =>
      Of(source);

    public static implicit operator Text(int source) =>
      Of(source);

    public static implicit operator Text(long source) =>
      Of(source);

    public static implicit operator Text(string source) =>
      Of(source);

    //
    // Concatentation operators
    //

    public static Text operator +(Text previous, Action<TextWriter> source) =>
      new Text(previous, source);

    public static Text operator +(Text previous, TextReader source) =>
      new Text(previous, writer => writer.Write(Of(source)));

    public static Text operator +(Text previous, bool source) =>
      new Text(previous, writer => writer.Write(source));

    public static Text operator +(Text previous, char source) =>
      new Text(previous, writer => writer.Write(source));

    public static Text operator +(Text previous, decimal source) =>
      new Text(previous, writer => writer.Write(source));

    public static Text operator +(Text previous, double source) =>
      new Text(previous, writer => writer.Write(source));

    public static Text operator +(Text previous, float source) =>
      new Text(previous, writer => writer.Write(source));

    public static Text operator +(Text previous, int source) =>
      new Text(previous, writer => writer.Write(source));

    public static Text operator +(Text previous, long source) =>
      new Text(previous, writer => writer.Write(source));

    public static Text operator +(Text previous, object source) =>
      new Text(previous, writer => writer.Write(source));

    public static Text operator +(Text previous, string source) =>
      new Text(previous, writer => writer.Write(source));

    public static Text operator +(Text previous, Text source) =>
      new Text(previous, writer => source.WriteTo(writer));

    //
    // Sequences
    //

    public static Text Of<T>(IEnumerable<T> source, Action<TextWriter, T, int> writeItem, Text separator = default) =>
      Of(writer =>
      {
        if(source == null || writer == null || writeItem == null)
        {
          return;
        }

        var index = 0;

        foreach(var item in source)
        {
          if(item == null)
          {
            continue;
          }

          if(index > 0)
          {
            separator.WriteTo(writer);
          }

          writeItem(writer, item, index);

          index++;
        }
      });

    public static Text Of<T>(IEnumerable<T> source, Action<TextWriter, T> writeItem, Text separator = default) =>
      Of(writer =>
      {
        if(source == null || writer == null || writeItem == null)
        {
          return;
        }

        var index = 0;

        foreach(var item in source)
        {
          if(item == null)
          {
            continue;
          }

          if(index > 0)
          {
            separator.WriteTo(writer);
          }

          writeItem(writer, item);

          index++;
        }
      });

    public static Text Of<T>(IEnumerable<T> source, Func<T, int, Text> selectText, Text separator = default) =>
      Of(source, (writer, item, index) => selectText?.Invoke(item, index).WriteTo(writer), separator);

    public static Text Of<T>(IEnumerable<T> source, Func<T, Text> selectText, Text separator = default) =>
      Of(source, (writer, item) => selectText?.Invoke(item).WriteTo(writer), separator);

    public static Text Of<T>(IEnumerable<T> source, Text separator = default) =>
      Of(source, (writer, item) => writer.Write(item), separator);

    public static Text Of(IEnumerable<Action<TextWriter, int>> source, Text separator = default) =>
      Of(source, (writer, item) => writer.Write(Of(item)), separator);

    public static Text Of(IEnumerable<Action<TextWriter>> source, Text separator = default) =>
      Of(source, (writer, item) => writer.Write(Of(item)), separator);

    public static Text Of(IEnumerable<TextReader> source, Text separator = default) =>
      Of(source, (writer, item) => writer.Write(Of(item)), separator);

    public static Text Of(IEnumerable<bool> source, Text separator = default) =>
      Of(source, (writer, item) => writer.Write(item), separator);

    public static Text Of(IEnumerable<char> source, Text separator = default) =>
      Of(source, (writer, item) => writer.Write(item), separator);

    public static Text Of(IEnumerable<decimal> source, Text separator = default) =>
      Of(source, (writer, item) => writer.Write(item), separator);

    public static Text Of(IEnumerable<double> source, Text separator = default) =>
      Of(source, (writer, item) => writer.Write(item), separator);

    public static Text Of(IEnumerable<float> source, Text separator = default) =>
      Of(source, (writer, item) => writer.Write(item), separator);

    public static Text Of(IEnumerable<int> source, Text separator = default) =>
      Of(source, (writer, item) => writer.Write(item), separator);

    public static Text Of(IEnumerable<long> source, Text separator = default) =>
      Of(source, (writer, item) => writer.Write(item), separator);

    public static Text Of(IEnumerable<string> source, Text separator = default) =>
      Of(source, (writer, item) => writer.Write(item), separator);

    public static Text Of(IEnumerable<Text> source, Text separator = default) =>
      Of(source, (writer, item) => writer.Write(item), separator);

    //
    // Items
    //

    public static IEnumerable<Text> Items<T>(IEnumerable<T> source, Func<T, int, Text> selectText, Text separator = default)
    {
      if(source == null || selectText == null)
      {
        yield break;
      }

      var index = 0;

      foreach(var item in source)
      {
        if(item == null)
        {
          continue;
        }

        if(index > 0)
        {
          yield return separator;
        }

        yield return selectText(item, index);

        index++;
      }
    }

    public static IEnumerable<Text> Items<T>(IEnumerable<T> source, Func<T, Text> selectText, Text separator = default)
    {
      if(source == null || selectText == null)
      {
        yield break;
      }

      var index = 0;

      foreach(var item in source)
      {
        if(item == null)
        {
          continue;
        }

        if(index > 0)
        {
          yield return separator;
        }

        yield return selectText(item);

        index++;
      }
    }

    public static IEnumerable<Text> Items<T>(IEnumerable<T> source, Text separator = default) =>
      Items(source, item => Of(item), separator);

    public static IEnumerable<Text> Items(IEnumerable<Action<TextWriter, int>> source, Text separator = default) =>
      Items(source, item => Of(item), separator);

    public static IEnumerable<Text> Items(IEnumerable<Action<TextWriter>> source, Text separator = default) =>
      Items(source, item => Of(item), separator);

    public static IEnumerable<Text> Items(IEnumerable<TextReader> source, Text separator = default) =>
      Items(source, item => Of(item), separator);

    public static IEnumerable<Text> Items(IEnumerable<bool> source, Text separator = default) =>
      Items(source, item => Of(item), separator);

    public static IEnumerable<Text> Items(IEnumerable<char> source, Text separator = default) =>
      Items(source, item => Of(item), separator);

    public static IEnumerable<Text> Items(IEnumerable<decimal> source, Text separator = default) =>
      Items(source, item => Of(item), separator);

    public static IEnumerable<Text> Items(IEnumerable<double> source, Text separator = default) =>
      Items(source, item => Of(item), separator);

    public static IEnumerable<Text> Items(IEnumerable<float> source, Text separator = default) =>
      Items(source, item => Of(item), separator);

    public static IEnumerable<Text> Items(IEnumerable<int> source, Text separator = default) =>
      Items(source, item => Of(item), separator);

    public static IEnumerable<Text> Items(IEnumerable<long> source, Text separator = default) =>
      Items(source, item => Of(item), separator);

    public static IEnumerable<Text> Items(IEnumerable<object> source, Text separator = default) =>
      Items(source, item => Of(item), separator);

    public static IEnumerable<Text> Items(IEnumerable<string> source, Text separator = default) =>
      Items(source, item => Of(item), separator);

    public static IEnumerable<Text> Items(IEnumerable<Text> source, Text separator = default) =>
      Items(source, item => Of(item), separator);

    //
    // Lines
    //

    public static Text Line() =>
      Of(writer => writer.WriteLine());

    public static Text Line(bool value) =>
      Of(writer => writer.WriteLine(value));

    public static Text Line(char value) =>
      Of(writer => writer.WriteLine(value));

    public static Text Line(char[] value) =>
      Of(writer => writer.WriteLine(value));

    public static Text Line(char[] value, int index, int count) =>
      Of(writer => writer.WriteLine(value, index, count));

    public static Text Line(decimal value) =>
      Of(writer => writer.WriteLine(value));

    public static Text Line(double value) =>
      Of(writer => writer.WriteLine(value));

    public static Text Line(float value) =>
      Of(writer => writer.WriteLine(value));

    public static Text Line(int value) =>
      Of(writer => writer.WriteLine(value));

    public static Text Line(long value) =>
      Of(writer => writer.WriteLine(value));

    public static Text Line(object value) =>
      Of(writer => writer.WriteLine(value));

    public static Text Line(string value) =>
      Of(writer => writer.WriteLine(value));

    public static Text Line(uint value) =>
      Of(writer => writer.WriteLine(value));

    public static Text Line(ulong value) =>
      Of(writer => writer.WriteLine(value));

    public static Text Line(string format, object arg0) =>
      Of(writer => writer.WriteLine(format, arg0));

    public static Text Line(string format, object arg0, object arg1) =>
      Of(writer => writer.WriteLine(format, arg0, arg1));

    public static Text Line(string format, object arg0, object arg1, object arg2) =>
      Of(writer => writer.WriteLine(format, arg0, arg1, arg2));

    public static Text Line(string format, params object[] args) =>
      Of(writer => writer.WriteLine(format, args));

    public static Text Lines(int count) =>
      Of(writer =>
      {
        for(var i = 0; i < count; i++)
        {
          writer.WriteLine();
        }
      });

    public static Text Lines<T>(IEnumerable<T> source, Func<T, int, Text> selectText) =>
      Of(source, selectText, separator: Line());

    public static Text Lines<T>(IEnumerable<T> source, Func<T, Text> selectText) =>
      Of(source, selectText, separator: Line());

    public static Text Lines<T>(IEnumerable<T> source) =>
      Of(source, separator: Line());

    public static Text Lines<T>(params T[] source) =>
      Of(source, separator: Line());

    //
    // If
    //

    public static Text If(bool condition, Text whenTrue, Text whenFalse) =>
      condition ? whenTrue : whenFalse;

    public static Text If(bool condition, Text whenTrue) =>
      If(condition, whenTrue, None);

    public static Text If(Func<bool> condition, Text whenTrue, Text whenFalse) =>
      Of(writer => writer.Write(condition?.Invoke() == true ? whenTrue : whenFalse));

    public static Text If(Func<bool> condition, Text whenTrue) =>
      If(condition, whenTrue, None);

    //
    // IfNot
    //

    public static Text IfNot(bool condition, Text whenFalse, Text whenTrue) =>
      condition ? whenTrue : whenFalse;

    public static Text IfNot(bool condition, Text whenFalse) =>
      IfNot(condition, whenFalse, None);

    public static Text IfNot(Func<bool> condition, Text whenFalse, Text whenTrue) =>
      Of(writer => writer.Write(condition?.Invoke() == true ? whenTrue : whenFalse));

    public static Text IfNot(Func<bool> condition, Text whenFalse) =>
      IfNot(condition, whenFalse, None);

    //
    // Counts
    //

    public static Text Plural(int count, Text singular, Text plural = default) =>
      If(count == 1, singular, plural.IsNone ? singular + 's' : plural);

    public static Text Plural(long count, Text singular, Text plural = default) =>
      If(count == 1, singular, plural.IsNone ? singular + 's' : plural);

    public static Text Count(int count, Text singular, Text plural = default) =>
      Of(writer =>
      {
        writer.Write(count);
        writer.Write(' ');
        writer.Write(Plural(count, singular, plural));
      });

    public static Text Count(long count, Text singular, Text plural = default) =>
      Of(writer =>
      {
        writer.Write(count);
        writer.Write(' ');
        writer.Write(Plural(count, singular, plural));
      });

    //
    // Types
    //

    public static Text Type<T>() =>
      Of(typeof(T));

    public static Text Type<T>(T instance) =>
      Of(instance?.GetType() ?? typeof(T));

    /// <summary>
    /// Converts instances of <see cref="Text"/> to and from <see cref="string"/>
    /// </summary>
    public sealed class Converter : TypeConverter
    {
      public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) =>
        true;

      public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) =>
        Of(value);

      public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) =>
        destinationType == typeof(string);

      public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) =>
        value == null ? "" : value.ToString();
    }
  }
}