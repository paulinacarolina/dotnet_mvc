using Xunit;

namespace XUnit.Coverlet.MSBuild;

public class UnitTest1
{
    double numero = 3.0;
    string[] nombres = { "Paulina", "Arantxa", "David" };

    [Fact]
    public void TestNumber()
    {
        Assert.IsType<int>(ParseDoubleToInt(numero));
    }

    [Fact]
    public void TestIncludingInCollection()
    {
        Assert.NotNull(nombres.Select(x=> x.ToString() == "Paulina"));
    }


    #region funciones 
    public int ParseDoubleToInt(double numero)
    {
        return (int)numero;
    }
    #endregion
}