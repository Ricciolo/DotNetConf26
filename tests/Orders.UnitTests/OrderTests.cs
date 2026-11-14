using Orders.Domain;

namespace Orders.UnitTests;

public sealed class OrderTests
{
    private static readonly DateTimeOffset FixedTime = new(2026, 11, 18, 10, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Create_WithValidLines_CalculatesTotal()
    {
        Order order = Order.Create(" Ada ", [
            OrderLine.Create("book", 2, 12.50m),
            OrderLine.Create("pen", 3, 1.25m)
        ], FixedTime);

        Assert.Equal("Ada", order.Customer);
        Assert.Equal(28.75m, order.Total);
        Assert.Equal(OrderStatus.Submitted, order.Status);
        Assert.Equal(FixedTime, order.CreatedAtUtc);
    }

    [Fact]
    public void Create_WithDuplicateSku_ThrowsDomainRuleException()
    {
        Action action = () => Order.Create("Ada", [
            OrderLine.Create("book", 1, 10m),
            OrderLine.Create("BOOK", 1, 20m)
        ], FixedTime);

        Assert.Throws<DomainRuleException>(action);
    }

    [Fact]
    public void Create_WithoutLines_ThrowsDomainRuleException()
    {
        Assert.Throws<DomainRuleException>(() => Order.Create("Ada", [], FixedTime));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void OrderLine_WithInvalidQuantity_Throws(int quantity)
    {
        Assert.Throws<DomainRuleException>(() => OrderLine.Create("book", quantity, 1m));
    }
}
