﻿namespace BookFx
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;
    using Unit = System.ValueTuple;

    /// <summary>
    /// This is the entry point of BookFx.
    /// </summary>
    [PublicAPI]
    public static class Make
    {
        /// <summary>
        /// Creates an empty book.
        /// </summary>
        [Pure]
        public static Book Book() => BookFx.Book.Empty;

        /// <summary>
        /// Creates a book with <see cref="sheets"/>.
        /// </summary>
        /// <param name="sheets">IEnumerable of creating book sheets.</param>
        [Pure]
        public static Book Book(IEnumerable<Sheet> sheets) =>
            BookCore.Create(sheets.Map(x => x.Get));

        /// <summary>
        /// Creates a book with <see cref="sheets"/>.
        /// </summary>
        /// <param name="sheets">Arrays of creating book sheets.</param>
        [Pure]
        public static Book Book(params Sheet[] sheets) => Book(sheets.AsEnumerable());

        /// <summary>
        /// Creates an empty unnamed sheet.
        /// </summary>
        [Pure]
        public static Sheet Sheet() => BookFx.Sheet.Empty;

        /// <summary>
        /// Creates an empty named sheet.
        /// </summary>
        /// <param name="name">
        /// A name of creating sheet.
        /// Name length should be from 1 to 31
        /// and name should be free of following characters: ':', '\', '/', '?', '*', '[' or ']'.
        /// </param>
        [Pure]
        public static Sheet Sheet(string name) => SheetCore.Create().With(name: Some(name));

        /// <summary>
        /// Creates a named sheet with box.
        /// </summary>
        /// <param name="name">
        /// A name of creating sheet.
        /// Name length should be from 1 to 31
        /// and name should be free of following characters: ':', '\', '/', '?', '*', '[' or ']'.
        /// </param>
        /// <param name="box">A root box of creating sheet.</param>
        [Pure]
        public static Sheet Sheet(string name, Box box) =>
            SheetCore.Create().With(name: Some(name), box: box.Get);

        /// <summary>
        /// Creates an unnamed sheet with box.
        /// </summary>
        /// <param name="box">A root box of creating sheet.</param>
        [Pure]
        public static Sheet Sheet(Box box) => SheetCore.Create().With(box: box.Get);

        /// <summary>
        /// Creates an unnamed sheet as a copy of only sheet of <see cref="protoBook"/>.
        /// </summary>
        /// <param name="protoBook">Bytes of book package which contains the copying sheet.</param>
        [Pure]
        public static Sheet Sheet(byte[] protoBook) =>
            SheetCore.Create().With(protoBook: Some(protoBook));

        /// <summary>
        /// Creates an unnamed sheet as a copy of <see cref="protoName"/> sheet of <see cref="protoBook"/>.
        /// </summary>
        /// <param name="protoBook">Bytes of book package which contains the copying sheet.</param>
        /// <param name="protoName">A name of copying sheet.</param>
        [Pure]
        public static Sheet Sheet(byte[] protoBook, string protoName) =>
            SheetCore.Create().With(protoBook: Some(protoBook), protoName: Some(protoName));

        /// <summary>
        /// Creates a named sheet as a copy of <see cref="protoName"/> sheet of <see cref="protoBook"/>.
        /// </summary>
        /// <param name="name">
        /// A name of creating sheet.
        /// Name length should be from 1 to 31
        /// and name should be free of following characters: ':', '\', '/', '?', '*', '[' or ']'.
        /// </param>
        /// <param name="protoBook">Bytes of book package which contains the copying sheet.</param>
        /// <param name="protoName">A name of copying sheet.</param>
        [Pure]
        public static Sheet Sheet(string name, byte[] protoBook, string protoName) =>
            SheetCore.Create().With(name: Some(name), protoBook: Some(protoBook), protoName: Some(protoName));

        /// <summary>
        /// Creates an empty <see cref="RowBox"/>.
        /// </summary>
        [Pure]
        public static RowBox Row() => RowBox.Empty;

        /// <summary>
        /// Creates a <see cref="RowBox"/> with children.
        /// </summary>
        /// <param name="children">IEnumerable of children <see cref="Box"/>'es.</param>
        [Pure]
        public static RowBox Row(IEnumerable<Box> children) => RowBox.Empty.Add(children);

        /// <summary>
        /// Creates a <see cref="RowBox"/> with children.
        /// </summary>
        /// <param name="child">First child.</param>
        /// <param name="others">Other children.</param>
        [Pure]
        public static RowBox Row(Box child, params Box[] others) =>
            Row(others.Prepend(child));

        /// <summary>
        /// Creates an empty <see cref="ColBox"/>.
        /// </summary>
        [Pure]
        public static ColBox Col() => ColBox.Empty;

        /// <summary>
        /// Creates a <see cref="ColBox"/> with children.
        /// </summary>
        /// <param name="children">IEnumerable of children <see cref="Box"/>'es.</param>
        [Pure]
        public static ColBox Col(IEnumerable<Box> children) =>
            ColBox.Empty.Add(children);

        /// <summary>
        /// Creates a <see cref="ColBox"/> with children.
        /// </summary>
        /// <param name="child">First child.</param>
        /// <param name="others">Other children.</param>
        [Pure]
        public static ColBox Col(Box child, params Box[] others) =>
            Col(others.Prepend(child));

        /// <summary>
        /// Creates an empty <see cref="StackBox"/>.
        /// </summary>
        [Pure]
        public static StackBox Stack() => StackBox.Empty;

        /// <summary>
        /// Creates a <see cref="StackBox"/> with children.
        /// </summary>
        /// <param name="children">IEnumerable of children <see cref="Box"/>'es.</param>
        [Pure]
        public static StackBox Stack(IEnumerable<Box> children) => StackBox.Empty.Add(children);

        /// <summary>
        /// Creates a <see cref="StackBox"/> with children.
        /// </summary>
        /// <param name="child">First child.</param>
        /// <param name="others">Other children.</param>
        [Pure]
        public static StackBox Stack(Box child, params Box[] others) =>
            Stack(others.Prepend(child));

        /// <summary>
        /// Creates an empty <see cref="ValueBox"/>. The box content will not be set.
        /// </summary>
        [Pure]
        public static ValueBox Value() => ValueBox.Empty;

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="Unit"/>. The box content will be cleared.
        /// </summary>
        /// <param name="value"><see cref="Unit"/>.</param>
        [Pure]
        public static ValueBox Value(Unit value) => BoxCore.Create(type: BoxType.Value, value: value);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="string"/> or formula.
        /// </summary>
        /// <param name="value">Value of <see cref="string"/> or formula. The formula must begin with '='.</param>
        [Pure]
        public static ValueBox Value(string value) => BoxCore.Create(type: BoxType.Value, value: value);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="bool"/>.
        /// </summary>
        /// <param name="value">Value of <see cref="bool"/>.</param>
        [Pure]
        public static ValueBox Value(bool value) => BoxCore.Create(type: BoxType.Value, value: value);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="byte"/>.
        /// </summary>
        /// <param name="value">Value of <see cref="byte"/>.</param>
        [Pure]
        public static ValueBox Value(byte value) => BoxCore.Create(type: BoxType.Value, value: value);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="sbyte"/>.
        /// </summary>
        /// <param name="value">Value of <see cref="sbyte"/>.</param>
        [Pure]
        public static ValueBox Value(sbyte value) => BoxCore.Create(type: BoxType.Value, value: value);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="short"/>.
        /// </summary>
        /// <param name="value">Value of <see cref="short"/>.</param>
        [Pure]
        public static ValueBox Value(short value) => BoxCore.Create(type: BoxType.Value, value: value);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="ushort"/>.
        /// </summary>
        /// <param name="value">Value of <see cref="ushort"/>.</param>
        [Pure]
        public static ValueBox Value(ushort value) => BoxCore.Create(type: BoxType.Value, value: value);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="int"/>.
        /// </summary>
        /// <param name="value">Value of <see cref="int"/>.</param>
        [Pure]
        public static ValueBox Value(int value) => BoxCore.Create(type: BoxType.Value, value: value);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="uint"/>.
        /// </summary>
        /// <param name="value">Value of <see cref="uint"/>.</param>
        [Pure]
        public static ValueBox Value(uint value) => BoxCore.Create(type: BoxType.Value, value: value);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="long"/>.
        /// </summary>
        /// <param name="value">Value of <see cref="long"/>.</param>
        [Pure]
        public static ValueBox Value(long value) => BoxCore.Create(type: BoxType.Value, value: value);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="ulong"/>.
        /// </summary>
        /// <param name="value">Value of <see cref="ulong"/>.</param>
        [Pure]
        public static ValueBox Value(ulong value) => BoxCore.Create(type: BoxType.Value, value: value);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="float"/>.
        /// </summary>
        /// <param name="value">Value of <see cref="float"/>.</param>
        [Pure]
        public static ValueBox Value(float value) => BoxCore.Create(type: BoxType.Value, value: value);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="double"/>.
        /// </summary>
        /// <param name="value">Value of <see cref="double"/>.</param>
        [Pure]
        public static ValueBox Value(double value) => BoxCore.Create(type: BoxType.Value, value: value);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="decimal"/>.
        /// </summary>
        /// <param name="value">Value of <see cref="decimal"/>.</param>
        [Pure]
        public static ValueBox Value(decimal value) => BoxCore.Create(type: BoxType.Value, value: value);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="DateTime"/>.
        /// </summary>
        /// <param name="value">Value of <see cref="DateTime"/>.</param>
        [Pure]
        public static ValueBox Value(DateTime value) => BoxCore.Create(type: BoxType.Value, value: value);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="Guid"/>.
        /// </summary>
        /// <param name="value">Value of <see cref="Guid"/>.</param>
        [Pure]
        public static ValueBox Value(Guid value) => BoxCore.Create(type: BoxType.Value, value: value);

        /// <summary>
        /// Creates an empty <see cref="ValueBox"/> with style. The box content will not be set.
        /// </summary>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(BoxStyle style) => ValueBox.Empty.Style(style);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="Unit"/> and style. The box content will be cleared.
        /// </summary>
        /// <param name="value"><see cref="Unit"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(Unit value, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: value, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="string"/> or formula and style.
        /// </summary>
        /// <param name="value">Value of <see cref="string"/> or formula. The formula must begin with '='.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(string value, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: value, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="bool"/> and style.
        /// </summary>
        /// <param name="value">Value of <see cref="bool"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(bool value, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: value, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="byte"/> and style.
        /// </summary>
        /// <param name="value">Value of <see cref="byte"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(byte value, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: value, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="sbyte"/> and style.
        /// </summary>
        /// <param name="value">Value of <see cref="sbyte"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(sbyte value, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: value, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="short"/> and style.
        /// </summary>
        /// <param name="value">Value of <see cref="short"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(short value, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: value, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="ushort"/> and style.
        /// </summary>
        /// <param name="value">Value of <see cref="ushort"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(ushort value, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: value, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="int"/> and style.
        /// </summary>
        /// <param name="value">Value of <see cref="int"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(int value, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: value, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="uint"/> and style.
        /// </summary>
        /// <param name="value">Value of <see cref="uint"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(uint value, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: value, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="long"/> and style.
        /// </summary>
        /// <param name="value">Value of <see cref="long"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(long value, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: value, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="ulong"/> and style.
        /// </summary>
        /// <param name="value">Value of <see cref="ulong"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(ulong value, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: value, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="float"/> and style.
        /// </summary>
        /// <param name="value">Value of <see cref="float"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(float value, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: value, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="double"/> and style.
        /// </summary>
        /// <param name="value">Value of <see cref="double"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(double value, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: value, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="decimal"/> and style.
        /// </summary>
        /// <param name="value">Value of <see cref="decimal"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(decimal value, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: value, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="DateTime"/> and style.
        /// </summary>
        /// <param name="value">Value of <see cref="DateTime"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(DateTime value, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: value, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="Guid"/> and style.
        /// </summary>
        /// <param name="value">Value of <see cref="Guid"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(Guid value, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: value, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ProtoBox"/>.
        /// </summary>
        /// <param name="book">Bytes of workbook package with box prototype.</param>
        /// <param name="reference">Reference to a box prototype in the <see cref="book"/>.</param>
        [Pure]
        public static ProtoBox Proto(byte[] book, Reference reference) =>
            BoxCore.Create(BoxType.Proto).With(proto: ProtoCore.Create(book, reference));

        /// <summary>
        /// Creates an empty <see cref="BoxStyle"/>.
        /// </summary>
        [Pure]
        public static BoxStyle Style() => BoxStyle.Empty;

        /// <summary>
        /// Mixes two styles.
        /// </summary>
        /// <param name="a">The first style.</param>
        /// <param name="b">The second style.</param>
        [Pure]
        public static BoxStyle Style(BoxStyle a, BoxStyle b) => BoxStyleCore.Mix(a.Get, b.Get);

        /// <summary>
        /// Mixes many styles.
        /// </summary>
        /// <param name="styles">IEnumerable of styles.</param>
        [Pure]
        public static BoxStyle Style(IEnumerable<BoxStyle> styles) =>
            styles.Aggregate(BoxStyle.Empty, Style);

        /// <summary>
        /// Mixes many styles.
        /// </summary>
        /// <param name="a">The first style.</param>
        /// <param name="b">The second style.</param>
        /// <param name="others">Other styles.</param>
        [Pure]
        public static BoxStyle Style(
            BoxStyle a,
            BoxStyle b,
            params BoxStyle[] others) =>
            Style(others.Prepend(b).Prepend(a));

        /// <summary>
        /// Creates an empty <see cref="BoxBorder"/>.
        /// </summary>
        [Pure]
        public static BoxBorder Border() => BoxBorder.Empty;

        /// <summary>
        /// Creates a <see cref="BoxBorder"/> with restriction to a <see cref="part"/>.
        /// </summary>
        /// <param name="part">The part of border.</param>
        [Pure]
        public static BoxBorder Border(BorderPart part) => BoxBorder.Empty.Restrict(part);

        /// <summary>
        /// Creates a <see cref="BoxBorder"/> a border style.
        /// </summary>
        /// <param name="style">The border style.</param>
        [Pure]
        public static BoxBorder Border(BorderStyle style) => BoxBorder.Empty.Style(style);

        /// <summary>
        /// Creates a <see cref="BoxBorder"/> with a <see cref="style"/> applied to a <see cref="part"/>.
        /// </summary>
        /// <param name="part">The part of border.</param>
        /// <param name="style">The border style.</param>
        [Pure]
        public static BoxBorder Border(BorderPart part, BorderStyle style) =>
            BoxBorder.Empty.Restrict(part).Style(style);
    }
}