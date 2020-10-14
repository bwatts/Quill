using System;
using System.IO;

namespace Quill
{
  public static partial class Textable
  {
    public static Text Write(this Text text, Action<TextWriter> source) =>
      text + source;

    public static Text Write(this Text text, TextReader source) =>
      text + source;

    public static Text Write(this Text text, bool source) =>
      text + source;

    public static Text Write(this Text text, char source) =>
      text + source;

    public static Text Write(this Text text, decimal source) =>
      text + source;

    public static Text Write(this Text text, double source) =>
      text + source;

    public static Text Write(this Text text, float source) =>
      text + source;

    public static Text Write(this Text text, int source) =>
      text + source;

    public static Text Write(this Text text, long source) =>
      text + source;

    public static Text Write(this Text text, object source) =>
      text + source;

    public static Text Write(this Text text, string source) =>
      text + source;

    public static Text Write(this Text text, Text source) =>
      text + source;

    //
    // Lines
    //

    public static Text Write(this Text text) =>
      text + Text.Line();

    public static Text WriteLine(this Text text, bool value) =>
      text + Text.Line(value);

    public static Text WriteLine(this Text text, char value) =>
      text + Text.Line(value);

    public static Text WriteLine(this Text text, char[] value) =>
      text + Text.Line(value);

    public static Text WriteLine(this Text text, char[] value, int index, int count) =>
      text + Text.Line(value, index, count);

    public static Text WriteLine(this Text text, decimal value) =>
      text + Text.Line(value);

    public static Text WriteLine(this Text text, double value) =>
      text + Text.Line(value);

    public static Text WriteLine(this Text text, float value) =>
      text + Text.Line(value);

    public static Text WriteLine(this Text text, int value) =>
      text + Text.Line(value);

    public static Text WriteLine(this Text text, long value) =>
      text + Text.Line(value);

    public static Text WriteLine(this Text text, object value) =>
      text + Text.Line(value);

    public static Text WriteLine(this Text text, string value) =>
      text + Text.Line(value);

    public static Text WriteLine(this Text text, uint value) =>
      text + Text.Line(value);

    public static Text WriteLine(this Text text, ulong value) =>
      text + Text.Line(value);

    public static Text WriteLine(this Text text, string format, object arg0) =>
      text + Text.Line(format, arg0);

    public static Text WriteLine(this Text text, string format, object arg0, object arg1) =>
      text + Text.Line(format, arg0, arg1);

    public static Text WriteLine(this Text text, string format, object arg0, object arg1, object arg2) =>
      text + Text.Line(format, arg0, arg1, arg2);

    public static Text WriteLine(this Text text, string format, params object[] args) =>
      text + Text.Line(format, args);

    //
    // If
    //

    public static Text WriteIf(this Text text, bool condition, Text whenTrue, Text whenFalse) =>
      text + Text.If(condition, whenTrue, whenFalse);

    public static Text WriteIf(this Text text, bool condition, Text whenTrue) =>
      text + Text.If(condition, whenTrue);

    public static Text WriteIf(this Text text, Func<bool> condition, Text whenTrue, Text whenFalse) =>
      text + Text.If(condition, whenTrue, whenFalse);

    public static Text WriteIf(this Text text, Func<bool> condition, Text whenTrue) =>
      text + Text.If(condition, whenTrue);

    //
    // IfNot
    //

    public static Text WriteIfNot(this Text text, bool condition, Text whenFalse, Text whenTrue) =>
      text + Text.IfNot(condition, whenFalse, whenTrue);

    public static Text WriteIfNot(this Text text, bool condition, Text whenFalse) =>
      text + Text.IfNot(condition, whenFalse);

    public static Text WriteIfNot(this Text text, Func<bool> condition, Text whenFalse, Text whenTrue) =>
      text + Text.IfNot(condition, whenFalse, whenTrue);

    public static Text WriteIfNot(this Text text, Func<bool> condition, Text whenFalse) =>
      text + Text.IfNot(condition, whenFalse);

    //
    // Types
    //

    public static Text WriteType<T>(this Text text) =>
      text + Text.Type<T>();

    public static Text WriteType<T>(this Text text, T instance) =>
      text + Text.Type(instance);

    //
    // Counts
    //

    public static Text WritePlural(this Text text, int count, Text singular, Text plural = default) =>
      text + Text.Plural(count, singular, plural);

    public static Text WritePlural(this Text text, long count, Text singular, Text plural = default) =>
      text + Text.Plural(count, singular, plural);

    public static Text WriteCount(this Text text, int count, Text singular, Text plural = default) =>
      text + Text.Count(count, singular, plural);

    public static Text WriteCount(this Text text, long count, Text singular, Text plural = default) =>
      text + Text.Count(count, singular, plural);
  }
}