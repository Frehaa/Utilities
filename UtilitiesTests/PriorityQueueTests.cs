using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;
using System.Collections.Generic;
using System.Collections;

namespace UtilitiesTests
{
    [TestClass]
    public class PriorityQueueTests
    {
        private PriorityQueue<int> pq;

        [TestInitialize]
        public void Initialize()
        {
            pq = new PriorityQueue<int>();
        }

        [TestMethod]
        public void Count_EmptyQueue_IsZero()
        {
            Assert.AreEqual(expected: 0, actual: pq.Count);
        }

        [TestMethod]
        public void Count_AfterSingleElementAdded_IsOne()
        {
            pq.Add(1);

            Assert.AreEqual(expected: 1, actual: pq.Count);
        }

        [TestMethod]
        public void Count_AfterAddingAndRemovingElement_IsZero()
        {
            pq.Add(31);
            pq.RemoveMin();

            Assert.AreEqual(expected: 0, actual: pq.Count);
        }

        [TestMethod]
        public void TestMethod4()
        {
            pq.Add(5);
            int actual = pq.RemoveMin();

            Assert.AreEqual(expected: 5, actual: actual);
        }

        [TestMethod]
        public void CollectionContainsSameElementsAsList()
        {
            pq.Add(-5);
            pq.Add(42);
            pq.Add(1051);

            ICollection<int> expected = new List<int>()
            {
                -5,
                42,
                1051
            };

            foreach (int item in pq)
            {
                if (!expected.Contains(item))
                    throw new AssertFailedException();
            }
        }

        [TestMethod]
        public void Add_SmallerElementAfterLarger_SmallElementGetsRemovedFirst()
        {
            pq.Add(1407);
            pq.Add(-16);
            int actual = pq.RemoveMin();

            Assert.AreEqual(expected: -16, actual: actual);
        }

        [TestMethod]
        public void Test7()
        {
            pq.Add(199);
            pq.Add(-53);
            pq.Add(0);
            pq.Add(-1092);
            int actual = pq.RemoveMin();

            Assert.AreEqual(expected: -1092, actual: actual);
        }

        [TestMethod]
        public void Test8()
        {
            pq.Add(140051);
            pq.Add(-5123);
            pq.Add(0);
            pq.Add(-11092);
            pq.Add(410);

            int actual = pq.RemoveMin();
            Assert.AreEqual(expected: -11092, actual: actual);

            actual = pq.RemoveMin();
            Assert.AreEqual(expected: -5123, actual: actual);

            actual = pq.RemoveMin();
            Assert.AreEqual(expected: 0, actual: actual);

            actual = pq.RemoveMin();
            Assert.AreEqual(expected: 410, actual: actual);

            actual = pq.RemoveMin();
            Assert.AreEqual(expected: 140051, actual: actual);
        }

        [TestMethod]
        public void Test9()
        {
            pq.Add(140051);
            pq.Add(-5123);
            pq.Add(0);
            pq.Add(-11092);
            pq.Add(410);

            int actual = pq.RemoveMin();
            Assert.AreEqual(expected: -11092, actual: actual);

            actual = pq.RemoveMin();
            Assert.AreEqual(expected: -5123, actual: actual);

            actual = pq.RemoveMin();
            Assert.AreEqual(expected: 0, actual: actual);

            actual = pq.RemoveMin();
            Assert.AreEqual(expected: 410, actual: actual);

            actual = pq.RemoveMin();
            Assert.AreEqual(expected: 140051, actual: actual);

            pq.Add(3);
            pq.Add(-3);
            pq.Add(9);

            actual = pq.RemoveMin();
            Assert.AreEqual(expected: -3, actual: actual);

            actual = pq.RemoveMin();
            Assert.AreEqual(expected: 3, actual: actual);

            actual = pq.RemoveMin();
            Assert.AreEqual(expected: 9, actual: actual);
        }

        [TestMethod][ExpectedException(typeof(InvalidOperationException))]
        public void RemoveMin_OnEmptyQueue_ThrowInvalidOperationException()
        {
            pq.RemoveMin();
        }
    }
}
