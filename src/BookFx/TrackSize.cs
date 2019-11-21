﻿namespace BookFx
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    /// <summary>
    /// Track size is a column or row size.
    /// </summary>
    [PublicAPI]
    public struct TrackSize : IEquatable<TrackSize>
    {
        /// <summary>
        /// Track size is absense.
        /// </summary>
        public static readonly TrackSize None = new TrackSize(Mode.None);

        /// <summary>
        /// Auto fit using EPPlus AutoFit() feature. Wrapped and merged cells are ignored.
        /// </summary>
        public static readonly TrackSize Fit = new TrackSize(Mode.Fit);

        private readonly Mode _mode;
        private readonly float _value;

        private TrackSize(Mode mode)
        {
            _mode = mode;
            _value = default;
        }

        private TrackSize(float value)
        {
            _mode = Mode.Some;
            _value = value;
        }

        private enum Mode
        {
            None,
            Some,
            Fit,
        }

        /// <summary>
        /// Gets a value indicating whether is a track size absense.
        /// </summary>
        public bool IsNone => _mode == Mode.None;

        /// <summary>
        /// Implicit convert from <see cref="float"/> to <see cref="TrackSize"/>.
        /// </summary>
        [Pure]
        public static implicit operator TrackSize(float value) => Some(value);

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(TrackSize left, TrackSize right) => left.Equals(right);

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(TrackSize left, TrackSize right) => !(left == right);

        /// <summary>
        /// Creates a <see cref="TrackSize"/> with value.
        /// </summary>
        /// <param name="value">A track size.</param>
        [Pure]
        public static TrackSize Some(float value) => new TrackSize(value);

        /// <summary>
        /// Pattern matching.
        /// </summary>
        /// <typeparam name="T">A type of the result.</typeparam>
        /// <param name="none">A function for the none case.</param>
        /// <param name="fit">A function for the fit case.</param>
        /// <param name="some">A function for the some case.</param>
        /// <returns>
        /// A result either of the <paramref name="none"/>
        /// or the <paramref name="fit"/>
        /// or the <paramref name="some"/> function.
        /// </returns>
        public T Match<T>(Func<T> none, Func<T> fit, Func<float, T> some) =>
            _mode switch
            {
                Mode.None => none(),
                Mode.Some => some(_value),
                Mode.Fit => fit(),
                _ => throw new InvalidOperationException()
            };

        /// <summary>
        /// Converts a <see cref="TrackSize"/> to an <see cref="IEnumerable{Float}"/>.
        /// </summary>
        /// <returns>
        /// Either an empty <see cref="IEnumerable{T}"/> or an <see cref="IEnumerable{Float}"/> containing one value.
        /// </returns>
        [Pure]
        public IEnumerable<float> ValueAsEnumerable()
        {
            if (_mode == Mode.Some)
            {
                yield return _value;
            }
        }

        /// <inheritdoc />
        public bool Equals(TrackSize other) =>
            _mode == other._mode &&
            (_mode != Mode.Some || _value.Equals(other._value));

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is TrackSize other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)_mode * 397) ^ _value.GetHashCode();
            }
        }
    }
}