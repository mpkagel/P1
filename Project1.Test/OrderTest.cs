using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using P1B = Project1.BLL;

namespace Project1.Test
{
    public class OrderTest
    {
        [Theory]
        [InlineData(500)]
        [InlineData(499)]
        [InlineData(250)]
        public void TestCheckCupcakeQuantityTrue(int qnty)
        {
            // Act and Assert
            Assert.True(P1B.Order.CheckCupcakeQuantity(qnty));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(501)]
        [InlineData(1000)]
        [InlineData(-500)]
        [InlineData(0)]
        public void TestCheckCupcakeQuantityFalse(int qnty)
        {
            // Act and Assert
            Assert.False(P1B.Order.CheckCupcakeQuantity(qnty));
        }
    }
}
