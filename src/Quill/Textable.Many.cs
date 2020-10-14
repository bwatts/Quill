using System;
using System.Collections.Generic;
using System.IO;

namespace Quill
{
  public static partial class Textable
  {
    public static Text Write<T>(this Text text, IEnumerable<T> source, Action<TextWriter, T, int> writeItem, Text separator = default) =>
      text + Text.Of(source, writeItem, separator);

    public static Text Write<T>(this Text text, IEnumerable<T> source, Action<TextWriter, T> writeItem, Text separator = default) =>
      text + Text.Of(source, writeItem, separator);

    public static Text Write<T>(this Text text, IEnumerable<T> source, Func<T, int, Text> selectText, Text separator = default) =>
      text + Text.Of(source, selectText, separator);

    public static Text Write<T>(this Text text, IEnumerable<T> source, Func<T, Text> selectText, Text separator = default) =>
      text + Text.Of(source, selectText, separator);

    public static Text Write<T>(this Text text, IEnumerable<T> source, Text separator = default) =>
      text + Text.Of(source, separator);

    public static Text Write(this Text text, IEnumerable<Action<TextWriter, int>> source, Text separator = default) =>
      text + Text.Of(source, separator);

    public static Text Write(this Text text, IEnumerable<Action<TextWriter>> source, Text separator = default) =>
      text + Text.Of(source, separator);

    public static Text Write(this Text text, IEnumerable<TextReader> source, Text separator = default) =>
      text + Text.Of(source, separator);

    public static Text Write(this Text text, IEnumerable<bool> source, Text separator = default) =>
      text + Text.Of(source, separator);

    public static Text Write(this Text text, IEnumerable<char> source, Text separator = default) =>
      text + Text.Of(source, separator);

    public static Text Write(this Text text, IEnumerable<decimal> source, Text separator = default) =>
      text + Text.Of(source, separator);

    public static Text Write(this Text text, IEnumerable<double> source, Text separator = default) =>
      text + Text.Of(source, separator);

    public static Text Write(this Text text, IEnumerable<float> source, Text separator = default) =>
      text + Text.Of(source, separator);

    public static Text Write(this Text text, IEnumerable<int> source, Text separator = default) =>
      text + Text.Of(source, separator);

    public static Text Write(this Text text, IEnumerable<long> source, Text separator = default) =>
      text + Text.Of(source, separator);

    public static Text Write(this Text text, IEnumerable<object> source, Text separator = default) =>
      text + Text.Of(source, separator);

    public static Text Write(this Text text, IEnumerable<string> source, Text separator = default) =>
      text + Text.Of(source, separator);

    public static Text Write(this Text text, IEnumerable<Text> source, Text separator = default) =>
      text + Text.Of(source, separator);

    //
    // Lines
    //

    public static Text WriteLines(this Text text, int count) =>
      text + Text.Lines(count);

    public static Text WriteLines<T>(this Text text, IEnumerable<T> source, Func<T, int, Text> selectText) =>
      text + Text.Lines(source, selectText);

    public static Text WriteLines<T>(this Text text, IEnumerable<T> source, Func<T, Text> selectText) =>
      text + Text.Lines(source, selectText);

    public static Text WriteLines<T>(this Text text, IEnumerable<T> source) =>
      text + Text.Lines(source);

    public static Text WriteLines<T>(this Text text, params T[] source) =>
      text + Text.Lines(source);

    //
    // ToText
    //

    public static Text ToText<T>(this IEnumerable<T> source, Func<T, int, Text> selectText, Text separator = default) =>
      Text.Of(source, selectText, separator);

    public static Text ToText<T>(this IEnumerable<T> source, Func<T, Text> selectText, Text separator = default) =>
      Text.Of(source, selectText, separator);

    public static Text ToText<T>(this IEnumerable<T> source, Text separator = default) =>
      Text.Of(source, separator);

    public static IEnumerable<Text> ToTextItems<T>(this IEnumerable<T> source, Func<T, int, Text> selectText, Text separator = default) =>
      Text.Items(source, selectText, separator);

    public static IEnumerable<Text> ToTextItems<T>(this IEnumerable<T> source, Func<T, Text> selectText, Text separator = default) =>
      Text.Items(source, selectText, separator);

    public static IEnumerable<Text> ToTextItems<T>(this IEnumerable<T> source, Text separator = default) =>
      Text.Items(source, separator);
  }
}