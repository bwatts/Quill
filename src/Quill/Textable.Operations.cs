using System;
using System.Collections.Generic;
using System.Linq;

namespace Quill
{
  public static partial class Textable
  {
    public static Text Repeat(this Text text, int times) =>
      times switch
      {
        int n when n <= 0 => Text.None,
        1 => text,
        _ => Text.Of(Enumerable.Repeat(text, times))
      };

    //
    // In
    //

    public static Text InParentheses(this Text text) =>
      text.In('(', ')');

    public static Text InBraces(this Text text) =>
      text.In('{', '}');

    public static Text InBrackets(this Text text) =>
      text.In('[', ']');

    public static Text InAngleBrackets(this Text text) =>
      text.In('<', '>');

    public static Text InSingleQuotes(this Text text) =>
      text.In('\'', '\'');

    public static Text InDoubleQuotes(this Text text) =>
      text.In('"', '"');

    static Text In(this Text text, char start, char end) =>
      Text.Of(writer =>
      {
        writer.Write(start);
        writer.Write(text);
        writer.Write(end);
      });

    //
    // Split
    //

    public static IEnumerable<Text> Split(this Text text, Text separator, bool keepEmpty = true, int maximumSubstrings = int.MaxValue) =>
      Text.Items(text.ToString().Split(
        new string[] { separator },
        maximumSubstrings,
        keepEmpty ? StringSplitOptions.None : StringSplitOptions.RemoveEmptyEntries));

    public static IEnumerable<Text> SplitSpaces(this Text text, bool keepEmpty = true, int maximumSubstrings = int.MaxValue) =>
      text.Split(' ', keepEmpty, maximumSubstrings);

    public static IEnumerable<Text> SplitCommas(this Text text, bool keepEmpty = true, int maximumSubstrings = int.MaxValue) =>
      text.Split(',', keepEmpty, maximumSubstrings);

    public static IEnumerable<Text> SplitLines(this Text text, bool keepEmpty = true, int maximumSubstrings = int.MaxValue) =>
      text.Split(Text.Line(), keepEmpty, maximumSubstrings);

    //
    // Indent
    //

    public const string TabIndent = "\t";
    public const string TwoSpaceIndent = "  ";
    public const string FourSpaceIndent = "    ";
    public const string Ellipsis = "…";

    public static Text Indent(this Text text, string indent = TwoSpaceIndent, int level = 1, bool retainLevel = false) =>
      Text.Of(writer =>
      {
        var wroteFirst = false;

        foreach(var lineText in text.SplitLines())
        {
          if(wroteFirst)
          {
            writer.WriteLine();
          }
          else
          {
            wroteFirst = true;
          }

          var line = lineText.ToString();
          var lineLevel = 0;

          while(line.Length > 0 && line.StartsWith(indent))
          {
            line = line.Substring(indent.Length);
            lineLevel++;
          }

          var effectiveLevel = retainLevel ? level + lineLevel : level;

          for(var i = 0; i < effectiveLevel; i++)
          {
            writer.Write(indent);
          }

          writer.Write(line);
        }
      });

    public static Text IndentWithTabs(this Text text, int level = 1, bool retainLevel = false) =>
      text.Indent("\t", level, retainLevel);

    public static Text IndentWithSpaces(this Text text, int level = 1, bool retainLevel = false, int tabSize = 2) =>
      text.Indent(new string(' ', tabSize), level, retainLevel);

    public static Text IndentScope(this Text text, string startToken = "{", string endToken = "}", string indent = TwoSpaceIndent, int level = 0, bool retainLevel = false) =>
      Text.Of(writer =>
      {
        writer.WriteLine(startToken);

        text.Indent(indent, level + 1, retainLevel).WriteTo(writer);

        writer.WriteLine();
        writer.Write(endToken);
      });

    //
    // Compact
    //

    public static Text Compact(this Text text, string ellipsis = Ellipsis) =>
      Text.Of(writer =>
      {
        var source = text.ToString();

        if(source.Length < 3)
        {
          writer.Write(source);
        }
        else
        {
          text.Compact(source.Length / 2, ellipsis).WriteTo(writer);
        }
      });

    public static Text Compact(this Text text, int maxLength, string ellipsis = Ellipsis) =>
      Text.Of(writer =>
      {
        var source = text.ToString();

        if(source.Length <= maxLength)
        {
          writer.Write(source);
        }
        else
        {
          var partLength = maxLength / 2;
          var leftPart = source.Substring(0, partLength);
          var rightPart = source.Substring(source.Length - partLength + 1);

          writer.Write(leftPart);
          writer.Write(ellipsis);
          writer.Write(rightPart);
        }
      });

    public static Text CompactLeft(this Text text, int maxLength, string ellipsis = Ellipsis) =>
      Text.Of(writer =>
      {
        var source = text.ToString();

        if(source.Length <= maxLength)
        {
          writer.Write(source);
        }
        else
        {
          writer.Write(ellipsis);
          writer.Write(source.Substring(source.Length - maxLength + 1));
        }
      });

    public static Text CompactRight(this Text text, int maxLength, string ellipsis = Ellipsis) =>
      Text.Of(writer =>
      {
        var source = text.ToString();

        if(source.Length <= maxLength)
        {
          writer.Write(source);
        }
        else
        {
          writer.Write(source.Substring(0, maxLength));
          writer.Write(ellipsis);
        }
      });

    //
    // Writes
    //

    public static Text WriteRepeated(this Text text, Text value, int times) =>
      text + value.Repeat(times);

    public static Text WriteInParentheses(this Text text, Text value) =>
      text + value.InParentheses();

    public static Text WriteInBraces(this Text text, Text value) =>
      text + value.InBraces();

    public static Text WriteInBrackets(this Text text, Text value) =>
      text + value.InBrackets();

    public static Text WriteInAngleBrackets(this Text text, Text value) =>
      text + value.InAngleBrackets();

    public static Text WriteInSingleQuotes(this Text text, Text value) =>
      text + value.InSingleQuotes();

    public static Text WriteInDoubleQuotes(this Text text, Text value) =>
      text + value.InDoubleQuotes();

    public static Text WriteIndented(this Text text, Text toIndent, string indent = TwoSpaceIndent, int level = 1, bool retainLevel = false) =>
      text + toIndent.Indent(indent, level, retainLevel);

    public static Text WriteIndentedWithTabs(this Text text, Text toIndent, int level = 1, bool retainIndent = true) =>
      text + toIndent.IndentWithTabs(level, retainIndent);

    public static Text WriteIndentedWithSpaces(this Text text, Text toIndent, int level = 1, bool retainIndent = true, int tabSize = 2) =>
      text + toIndent.IndentWithSpaces(level, retainIndent, tabSize);

    public static Text WriteIndentedScope(
      this Text text,
      Text toIndent,
      string startToken = "{",
      string endToken = "}",
      string indent = TwoSpaceIndent,
      int level = 0,
      bool retainLevel = true) =>
      text + toIndent.IndentScope(startToken, endToken, indent, level, retainLevel);

    public static Text WriteCompacted(this Text text, Text value, string ellipsis = Ellipsis) =>
      text + value.Compact(ellipsis);

    public static Text WriteCompacted(this Text text, Text value, int maxLength, string ellipsis = Ellipsis) =>
      text + value.Compact(maxLength, ellipsis);

    public static Text WriteCompactedLeft(this Text text, Text value, int maxLength, string ellipsis = Ellipsis) =>
      text + value.CompactLeft(maxLength, ellipsis);

    public static Text WriteCompactedRight(this Text text, Text value, int maxLength, string ellipsis = Ellipsis) =>
      text + value.CompactRight(maxLength, ellipsis);
  }
}