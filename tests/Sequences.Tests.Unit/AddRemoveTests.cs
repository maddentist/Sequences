﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sequences.Tests
{
    public class AddRemoveTests
    {
        [Fact]
        public void Concat_ConcatenatesTwoSequences()
        {
            var first = Sequence.With(1, 2);
            var second = Sequence.With(3, 4);

            Assert.Equal(new[] {1, 2, 3, 4},
                first.Concat(() => second));
        }

        [Fact]
        public void Concat_ConcatenatesEmpty_WithNonEmpty()
        {
            var first = Sequence.Empty<int>();
            var second = Sequence.With(1, 2, 3, 4);

            Assert.Equal(new[] {1, 2, 3, 4},
                first.Concat(() => second));
        }

        [Fact]
        public void Concat_ConcatenatesNonEmpty_WithEmpty()
        {
            var first = Sequence.With(1, 2, 3, 4);
            var second = Sequence.Empty<int>();

            Assert.Equal(new[] {1, 2, 3, 4},
                first.Concat(() => second));
        }

        [Fact]
        public void Append_AppendsElementToSequence()
        {
            Assert.Equal(new[] {1, 2, 3, 4},
                Sequence.With(1, 2, 3).Append(4));
        }

        [Fact]
        public void Prepend_PrependsElementToSequence()
        {
            Assert.Equal(new[] {1, 2, 3, 4},
                Sequence.With(2, 3, 4).Prepend(1));
        }

        [Fact]
        public void Remove_RemovesElement()
        {
            var sequence = Sequence.Range(1, 6);
            var result = sequence.Remove(3);
            var expectedResult = new[] {1, 2, 4, 5};

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Remove_RemovesHead()
        {
            var sequence = Sequence.Range(1, 6);
            var result = sequence.Remove(1);
            var expectedResult = new[] {2, 3, 4, 5};

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Remove_Returns_EquivalentSequence_When_ElementIsNotFound()
        {
            var sequence = Sequence.Range(1, 6);
            var result = sequence.Remove(10);
            var expectedResult = new[] {1, 2, 3, 4, 5};

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Remove_Returns_SameSequence_When_SequenceIsEmpty()
        {
            var sequence = Sequence.Empty<int>();
            var result = sequence.Remove(10);

            Assert.Same(sequence, result);
        }

        [Fact]
        public void Updated_UpdatesElement()
        {
            var sequence = Sequence.Range(1, 6);
            var updated = sequence.Updated(2, 10);
            int[] expectedUpdated = {1, 2, 10, 4, 5};

            Assert.Equal(expectedUpdated, updated);
        }

        [Fact]
        public void Updated_DoesNothing_When_IndexGreaterThanCount()
        {
            var sequence = Sequence.Range(1, 6);
            var updated = sequence.Updated(5, 10);
            int[] expectedUpdated = {1, 2, 3, 4, 5};

            Assert.Equal(expectedUpdated, updated);
        }

        [Fact]
        public void Updated_ThrowsException_When_IndexLessThanZero()
        {
            var sequence = Sequence.Range(1, 6);
            Assert.Throws<ArgumentOutOfRangeException>(() => sequence.Updated(-1, 10));
        }
    }
}
