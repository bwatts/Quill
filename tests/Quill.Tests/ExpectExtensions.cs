using System.Collections.Generic;
using System.Linq;
using Green;
using static Green.Local;

namespace Quill
{
  public static class ExpectExtensions
  {
    public static ExpectMany<Text> HasSameItems(this ExpectMany<Text> expect, IEnumerable<Text> items) =>
      expect.That(t =>
      {
        var target = t.Select(x => x.ToString()).ToList();
        var expected = items.Select(x => x.ToString()).ToList();

        return ExpectMany(target).HasSameInOrder(expected);
      });

    public static ExpectMany<Text> HasSameItems(this ExpectMany<Text> expect, params Text[] items) =>
      expect.HasSameItems(items.AsEnumerable());
  }
}